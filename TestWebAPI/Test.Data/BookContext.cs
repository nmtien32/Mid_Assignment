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
            modelBuilder.Entity<User>(e => e.ToTable("User"));

            modelBuilder.Entity<Category>(e => e.ToTable("Category"));

            modelBuilder.Entity<Book>(e => e.ToTable("Book"));
            modelBuilder.Entity<Book>()
                        .HasOne(b => b.Category)
                        .WithMany(c => c.Books)
                        .HasForeignKey(b => b.CategoryId)
                        .IsRequired();

            modelBuilder.Entity<BorrowingRequest>(e => e.ToTable("BookBorrowingRequest"));
           
            modelBuilder.Entity<BorrowingRequest>()
                        .HasOne(b => b.RequestedBy)
                        .WithMany(r => r.BorrowingRequests)
                        .HasForeignKey(b => b.ProcessedByUserId)
                        .IsRequired();

            modelBuilder.Entity<BorrowingRequest>()
                        .HasOne(b => b.ProcessedBy)
                        .WithMany(r => r.ProcessedRequests)
                        .HasForeignKey(b => b.ProcessedByUserId)
                        .IsRequired(false);

            modelBuilder.Entity<BorrowingRequestDetail>(e => e.ToTable("BookBorrowingRequestDetails"));

            modelBuilder.Entity<BorrowingRequestDetail>()
                        .HasKey(d => new { d.BorrowingRequestId, d.BookId });

            modelBuilder.Entity<BorrowingRequestDetail>()
                        .HasOne(b => b.BorrowingRequest)
                        .WithMany(r => r.Details)
                        .HasForeignKey(b => b.BorrowingRequestId)
                        .IsRequired();

            modelBuilder.Entity<BorrowingRequestDetail>()
                        .HasOne(b => b.Book)
                        .WithMany(r => r.Details)
                        .HasForeignKey(b => b.BookId)
                        .IsRequired();
        }

        public DbSet<Category>? Categories { get; set; }
        public DbSet<Book>? Books { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<BorrowingRequest>? BookBorrowingRequests { get; set; }
        public DbSet<BorrowingRequestDetail>? bookBorrowingRequestDetails { get; set; }
    }
}