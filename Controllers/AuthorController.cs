using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class AuthorController : Controller
    {
        private readonly LibraryDbContext _context;

        public AuthorController(LibraryDbContext context)
        {
            _context = context;
        }

        // 📌 1️⃣ 显示所有作者 (使用 ViewModel)
        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors
                .Include(a => a.Books)  // 预加载书籍
                .Select(a => new AuthorViewModel
                {
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                })
                .ToListAsync();

            return View(authors);
        }

        // 📌 2️⃣ 显示一个作者详情
        public async Task<IActionResult> Details(int id)
        {
            var author = await _context.Authors
                .Where(a => a.AuthorId == id) // 只查询指定 ID 的作者
                .Include(a => a.Books)  // 预加载书籍（如果 `ViewModel` 里用得上）
                .Select(a => new AuthorViewModel
                {
                    AuthorId = a.AuthorId,
                    Name = a.Name
                })
                .FirstOrDefaultAsync(); // 只获取单个对象，而不是列表

            if (author == null)
            {
                return NotFound(); // 如果找不到，返回 404 页面
            }

            return View(author); // 传递单个作者对象给视图
        }


        // 📌 3️⃣ 添加新作者 (使用 ViewModel)
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AuthorViewModel authorViewModel)
        {
            if (ModelState.IsValid)
            {
                var author = new Author
                {
                    Name = authorViewModel.Name
                };

                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorViewModel);
        }

        // 📌 4️⃣ 编辑作者 (使用 ViewModel)
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.AuthorId == id)
                .Select(a => new AuthorViewModel
                {
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                })
                .FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AuthorViewModel authorViewModel)
        {
            if (id != authorViewModel.AuthorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var author = await _context.Authors.FindAsync(id);
                if (author == null)
                {
                    return NotFound();
                }

                author.Name = authorViewModel.Name;

                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorViewModel);
        }

        // 📌 5️⃣ 删除作者 (使用 ViewModel)
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .Where(a => a.AuthorId == id)
                .Select(a => new AuthorViewModel
                {
                    AuthorId = a.AuthorId,
                    Name = a.Name,
                })
                .FirstOrDefaultAsync();

            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.AuthorId == id);

            if (author != null)
            {
                if (author.Books.Any())
                {
                    ModelState.AddModelError("", "Cannot delete author with books.");
                    return View(new AuthorViewModel
                    {
                        AuthorId = author.AuthorId,
                        Name = author.Name,
                    });
                }

                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

