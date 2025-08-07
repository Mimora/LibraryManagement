using Microsoft.AspNetCore.Builder;   //buildAPI
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;  //EFCore
using LibraryManagement.Models;       //Models
//Program.cs 代码是 ASP.NET Core MVC 的 入口点
// 负责配置 Web 应用、依赖注入（Dependency Injection） 和 路由（Routing）。
//ASP.NET Core MVC 使用路由（Routing）来将 HTTP 请求映射到 Controller 的 Action 方法。
using LibraryManagement.Data;  // 👈 新增：引用新建的 ApplicationDbContext

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;



var builder = WebApplication.CreateBuilder(args); //Initialize ASP.NET Core 应用程序
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Data Source=Database/library.db";// ✅ 配置 SQLite 数据库（使用原有的 library.db）
//注册DB的context 获取连接
   builder.Services.AddDbContext<LibraryDbContext>(options =>
      options.UseSqlite(connectionString));
 //LibraryDbContext 就会自动使用 appsettings.json 里的 ConnectionString 了

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));


// 注册 SQLite 数据库
//LibraryDbContext，用于 操作数据库
//指定数据库文件路径 Database/library.db
//如果数据库不存在，运行 dotnet ef migrations add 之后，它会自动创建。
//builder.Services.AddDbContext<LibraryDbContext>(options =>
//    options.UseSqlite("Data Source=Database/library.db"));   //如果想换数据库 必须在program.cs里修改

// ✅ 启用 ASP.NET Core Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

//添加 Google FB登录服务
builder.Services.AddAuthentication()
    .AddGoogle(googleOptions =>
    {
        googleOptions.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        googleOptions.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    })
    .AddFacebook(facebookOptions =>
    {
    facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
    facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];

    facebookOptions.Scope.Add("email");
    });


// ✅ Razor Pages 支持（Identity 的页面是 Razor Pages）
builder.Services.AddRazorPages();


// 注册 MVC 组件，允许使用 Controller + View
builder.Services.AddControllersWithViews();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();  //创建 WebApplication 实例(instance)

// 配置 HTTP 请求处理管道
if (app.Environment.IsDevelopment()) //检查是否处于 开发模式（Development）
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LibraryManagement API V1");
        c.RoutePrefix = "swagger";
    });
    app.UseDeveloperExceptionPage();  // 启用开发者错误页面，避免空白页面
}
else
{
    app.UseExceptionHandler("/Error"); // 中间件处理全局异常，转到Razor Pages 的 /Pages/Error.cshtml
    app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}"); // 处理 404
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseRouting(); //order必须在前

app.UseAuthentication(); //add authentication middleware to the request pipeline
app.UseAuthorization();//必须在后！！！！！！！！！！！！！！！！！！！！！！！
//app.MapStaticAssets();


app.MapControllerRoute(
    name: "default",
    //默认controller是HomeController，默认访问Index方法，可选参数optional parameter用于接收url中的id值
    pattern: "{controller=Home}/{action=Index}/{id?}");
   // .WithStaticAssets();

app.MapRazorPages(); // 👈 必须启用 Identity 的 Razor 页面

app.Run();
