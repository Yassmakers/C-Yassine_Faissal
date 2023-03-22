using C_Yassine_Faissal.Models;
using Microsoft.EntityFrameworkCore;

namespace C_Yassine_Faissal.Data
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }

        // Add other DbSet properties as needed

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<User>().ToTable("User");

            modelBuilder.Entity<Item>()
            .HasOne(i => i.Author)
            .WithMany(a => a.Items)
            .HasForeignKey(i => i.AuthorId)
            .IsRequired();

        }
    }
}
