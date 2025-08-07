using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class BookController : Controller
    {
        private readonly LibraryDbContext _context;

        //在 BookController 控制器中注入 LibraryDbContext，让控制器能够操作数据库
        public BookController(LibraryDbContext context) //Dependency Injection
        {
            _context = context;
        }

        // INDEX显示所有书籍
        public async Task<IActionResult> Index() //这个action方法异步获取所有book再返回Index view
        {
            var books = await _context.Books
                .Include(b => b.Author) //eagerloading
                .Include(b => b.LibraryBranch)
                .Select(b => new BookViewModel //用select将 Book Model转换为 BookViewModel，这样 View 只接收需要的数据，避免直接暴露 Book 模型。
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    AuthorName = b.Author != null ? b.Author.Name : "Unknown",
                    BranchName = b.LibraryBranch != null ? b.LibraryBranch.BranchName : "Unknown"

                })
                .ToListAsync(); //Performs a database query and retrieves all Book records 

            return View(books);
        }

        // DETAILS显示此本书籍详情
        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.LibraryBranch)
                .Where(b => b.BookId == id)  // 只查询指定的书籍
                .Select(b => new BookViewModel
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Genre = b.Genre,
                    AuthorName = b.Author != null ? b.Author.Name : "Unknown",
                    BranchName = b.LibraryBranch != null ? b.LibraryBranch.BranchName : "Unknown"
                })
                .FirstOrDefaultAsync(); //获取第一条匹配的记录，没有则返回 null

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }



        // CREATE添加新书
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookViewModel bookViewModel)
        {
            if (ModelState.IsValid)
            {
                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == bookViewModel.AuthorName);
                if (author == null)
                {
                    ModelState.AddModelError("AuthorName", "Author not found.");
                    return View(bookViewModel);
                }

                var libraryBranch = await _context.LibraryBranches.FirstOrDefaultAsync(lb => lb.BranchName == bookViewModel.BranchName);
                if (libraryBranch == null)
                {
                    ModelState.AddModelError("BranchName", "Library Branch not found.");
                    return View(bookViewModel);
                }

                var book = new Book //创建 Book 实例并添加到数据库。
                {
                    Title = bookViewModel.Title,
                    Genre = bookViewModel.Genre,
                    AuthorId = author.AuthorId,
                    LibraryBranchId = libraryBranch.LibraryBranchId
                };

                _context.Books.Add(book);
                await _context.SaveChangesAsync(); //保存更改到数据库
                return RedirectToAction(nameof(Index));
            }

            return View(bookViewModel);
        }

        // EDIT编辑书籍
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .Include(b => b.LibraryBranch)
                .FirstOrDefaultAsync(b => b.BookId == id);  // 预加载 `Author` 和 `LibraryBranch`

            if (book == null)
            {
                return NotFound();
            }

            var bookViewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Genre = book.Genre,
                AuthorName = book.Author?.Name ?? "Unknown Author",  // 处理 `null`
                BranchName = book.LibraryBranch?.BranchName ?? "Unknown Branch"  // 处理 `null`
            };

            return View(bookViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookViewModel bookViewModel)
        {
            if (id != bookViewModel.BookId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                {
                    return NotFound();
                }

                book.Title = bookViewModel.Title;
                book.Genre = bookViewModel.Genre;

                var author = await _context.Authors.FirstOrDefaultAsync(a => a.Name == bookViewModel.AuthorName);
                if (author == null)
                {
                    ModelState.AddModelError("AuthorName", "Author not found.");
                    return View(bookViewModel);
                }

                var libraryBranch = await _context.LibraryBranches.FirstOrDefaultAsync(lb => lb.BranchName == bookViewModel.BranchName);
                if (libraryBranch == null)
                {
                    ModelState.AddModelError("BranchName", "Library Branch not found.");
                    return View(bookViewModel);
                }

                book.AuthorId = author.AuthorId;
                book.LibraryBranchId = libraryBranch.LibraryBranchId;

                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(bookViewModel);
        }

        // DELETE删除书籍
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books
            .Include(b => b.Author)
            .Include(b => b.LibraryBranch)
            .FirstOrDefaultAsync(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            var bookViewModel = new BookViewModel
            {
                BookId = book.BookId,
                Title = book.Title,
                Genre = book.Genre,
                AuthorName = book.Author?.Name ?? "Unknown Author",  // 处理 `null`
                BranchName = book.LibraryBranch?.BranchName ?? "Unknown Branch"  // 处理 `null`
            };

            return View(bookViewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _context.Books.FindAsync(id); //查找
            if (book != null)
            {
                _context.Books.Remove(book); //删除
                await _context.SaveChangesAsync(); //提交更改
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
