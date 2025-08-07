using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Models;

namespace LibraryManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<LibraryManagement.Models.Book> Books { get; set; } = default!;
        public DbSet<LibraryManagement.Models.Author> Authors { get; set; } = default!;
        public DbSet<LibraryManagement.Models.LibraryBranch> LibraryBranchs { get; set; } = default!;
        public DbSet<LibraryManagement.Models.Customer> Customers { get; set; } = default!;

        // 如果后面你还想把原始表也迁移进来，可以在这里加
        // public DbSet<Book> Books { get; set; }
    }
}
