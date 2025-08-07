using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    public class LibraryBranchController : Controller
    {
        private readonly LibraryDbContext _context;

        public LibraryBranchController(LibraryDbContext context)
        {
            _context = context;
        }

        // 📌 1️⃣ 显示所有 Library Branches
        public async Task<IActionResult> Index()
        {
            var branches = await _context.LibraryBranches
                .Select(lb => new LibraryBranchViewModel
                {
                    LibraryBranchId = lb.LibraryBranchId,
                    BranchName = lb.BranchName
                })
                .ToListAsync();

            return View(branches);
        }

        // 📌 2️⃣ 显示单个 Library Branch 详情
        public async Task<IActionResult> Details(int id)
        {
            var branch = await _context.LibraryBranches
                .Where(lb => lb.LibraryBranchId == id)
                .Select(lb => new LibraryBranchViewModel
                {
                    LibraryBranchId = lb.LibraryBranchId,
                    BranchName = lb.BranchName
                })
                .FirstOrDefaultAsync();

            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        // 📌 3️⃣ 添加 Library Branch
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LibraryBranchViewModel branchViewModel)
        {
            if (ModelState.IsValid)
            {
                var branch = new LibraryBranch
                {
                    BranchName = branchViewModel.BranchName
                };

                _context.Add(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branchViewModel);
        }

        // 📌 4️⃣ 编辑 Library Branch
        public async Task<IActionResult> Edit(int id)
        {
            var branch = await _context.LibraryBranches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            var branchViewModel = new LibraryBranchViewModel
            {
                LibraryBranchId = branch.LibraryBranchId,
                BranchName = branch.BranchName
            };

            return View(branchViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LibraryBranchViewModel branchViewModel)
        {
            if (id != branchViewModel.LibraryBranchId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var branch = await _context.LibraryBranches.FindAsync(id);
                if (branch == null)
                {
                    return NotFound();
                }

                branch.BranchName = branchViewModel.BranchName;

                _context.Update(branch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(branchViewModel);
        }

        // 📌 5️⃣ 删除 Library Branch
        public async Task<IActionResult> Delete(int id)
        {
            var branch = await _context.LibraryBranches
                .Where(lb => lb.LibraryBranchId == id)
                .Select(lb => new LibraryBranchViewModel
                {
                    LibraryBranchId = lb.LibraryBranchId,
                    BranchName = lb.BranchName
                })
                .FirstOrDefaultAsync();

            if (branch == null)
            {
                return NotFound();
            }

            return View(branch);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var branch = await _context.LibraryBranches.FindAsync(id);
            if (branch != null)
            {
                _context.LibraryBranches.Remove(branch);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
