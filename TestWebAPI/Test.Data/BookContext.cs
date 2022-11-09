using Microsoft.EntityFrameworkCore;
using Test.Data.Entities;

namespace Test.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Category
            modelBuilder.Entity<Category>(e => e.ToTable("Category"));

            //Book
            modelBuilder.Entity<Book>(e => e.ToTable("Book"));
            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Category)
                        .WithMany(c => c.Books)
                        .HasForeignKey(b => b.CategoryId)
                        .IsRequired();
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Book>? Books { get; set; }
    }
}