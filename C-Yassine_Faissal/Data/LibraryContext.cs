using C_Yassine_Faissal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using C_Yassine_Faissal.Data;
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


        public void EnsureDatabaseCreated()
        {
            Database.Migrate();

            if (!Users.Any())
            {
                SeedData();
            }
        }



        private void SeedData()
        {
            // Seed authors
            Authors.Add(new Author { Id = 1, Name = "John Doe" });
            Authors.Add(new Author { Id = 2, Name = "Jane Smith" });

            // Seed users
            Users.Add(new User
            {
                Id = 1,
                UserName = "AdminAccount",
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@example.com",
                Password = "admin123", // You should use a hashed password in a real application
                Role = UserRole.Admin
            });

            Users.Add(new User
            {
                Id = 2,
                UserName = "EmployeeAccount",
                FirstName = "Employee",
                LastName = "User",
                Email = "employee@example.com",
                Password = "employee123", // You should use a hashed password in a real application
                Role = UserRole.Employee
            });

            SaveChanges();
        }


        public void UpdateItem(Item updatedItem)
        {
            var existingItem = Items.FirstOrDefault(i => i.Id == updatedItem.Id);

            if (existingItem != null)
            {
                existingItem.Title = updatedItem.Title;
                existingItem.Description = updatedItem.Description;
                existingItem.ItemType = updatedItem.ItemType;
                existingItem.AuthorId = updatedItem.AuthorId;
                existingItem.ItemStatus = updatedItem.ItemStatus;

                SaveChanges();
            }
        }

        public void DeleteItem(int itemId)
        {
            var itemToDelete = Items.FirstOrDefault(i => i.Id == itemId);

            if (itemToDelete != null)
            {
                Items.Remove(itemToDelete);
                SaveChanges();
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().ToTable("Item");
            modelBuilder.Entity<Author>().ToTable("Author");
            modelBuilder.Entity<User>().ToTable("Users");

            modelBuilder.Entity<Item>()
                .HasOne(i => i.Author)
                .WithMany(a => a.Items)
                .HasForeignKey(i => i.AuthorId)
                .IsRequired();
        }

    }
}
