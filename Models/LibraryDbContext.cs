using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public class LibraryDbContext : DbContext //通过Polymorphism方式实现对不同数据库表的查询（overriding method）
    {
        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) { }
        
        //从model中取出四个table生成db的表格
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<LibraryBranch> LibraryBranches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // define FK
            modelBuilder.Entity<Book>()
                .HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId)
                .OnDelete(DeleteBehavior.Cascade); //delete parent -> child

            modelBuilder.Entity<Book>()
                .HasOne(b => b.LibraryBranch)
                .WithMany()
                .HasForeignKey(b => b.LibraryBranchId)
                .OnDelete(DeleteBehavior.Cascade);

            // Prepopulated data
            modelBuilder.Entity<Author>().HasData(
                new Author { AuthorId = 1, Name = "Gene Kim" },
                new Author { AuthorId = 2, Name = "Andy Weir" }
            );

            modelBuilder.Entity<LibraryBranch>().HasData(
                new LibraryBranch { LibraryBranchId = 1, BranchName = "VPL Downtown" },
                new LibraryBranch { LibraryBranchId = 2, BranchName = "VPL Marpole" }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer { CustomerId = 1, Name = "Harry Potter" },
                new Customer { CustomerId = 2, Name = "Hermione Granger" }
            );

            // only FK
            modelBuilder.Entity<Book>().HasData(
                new Book { BookId = 1, Title = "The Unicorn Project", Genre = "Novel", AuthorId = 1, LibraryBranchId = 1 },
                new Book { BookId = 2, Title = "The Martian", Genre = "Science Fiction", AuthorId = 2, LibraryBranchId = 2 }
            );
        }
    }
}
