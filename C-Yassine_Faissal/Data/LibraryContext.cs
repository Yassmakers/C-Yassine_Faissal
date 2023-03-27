using C_Yassine_Faissal.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using C_Yassine_Faissal.Data;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace C_Yassine_Faissal.Data
{
    public class LibraryContext : DbContext
    {
        // Deze klasse is de hoofd-DbContext voor de applicatie en bevat DbSet properties
        // voor de verschillende entiteiten: Items, Users en Authors.
        public LibraryContext(DbContextOptions<LibraryContext> options)
            : base(options)
        {
        }

        // DbSet properties voor Items, Users en Authors
        public DbSet<Item> Items { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Author> Authors { get; set; }

        // andere db set later bijvoegen

        // Zorgt ervoor dat de database is aangemaakt en voert migraties uit
        public void EnsureDatabaseCreated()
        {
            Database.Migrate();

            if (!Users.Any())
            {
                SeedData();
            }
        }




        // Vult de database met seed gegevens
        // Vult de database met seed gegevens
        private void SeedData()
        {
            // Seed authors
            Authors.Add(new Author { Id = 1, Name = "George Orwell" });
            Authors.Add(new Author { Id = 2, Name = "J.K. Rowling" });
            Authors.Add(new Author { Id = 3, Name = "J.R.R. Tolkien" });
            Authors.Add(new Author { Id = 4, Name = "Christopher Nolan" });
            Authors.Add(new Author { Id = 5, Name = "The Beatles" });
            Authors.Add(new Author { Id = 6, Name = "Toei Animations" });


            // Seed items
            Items.Add(new Item
            {
                Id = 1,
                Title = "1984",
                Description = "A dystopian novel by George Orwell.",
                Bookshelf = "A1",
                ItemType = ItemType.Book,
                AuthorId = 1,
                ItemStatus = ItemStatus.Available
            });

            Items.Add(new Item
            {
                Id = 2,
                Title = "Harry Potter and the Philosopher's Stone",
                Description = "The first book in the Harry Potter series by J.K. Rowling.",
                Bookshelf = "A2",
                ItemType = ItemType.Book,
                AuthorId = 2,
                ItemStatus = ItemStatus.Available
            });

            Items.Add(new Item
            {
                Id = 3,
                Title = "The Hobbit",
                Description = "A fantasy novel by J.R.R. Tolkien.",
                Bookshelf = "A3",
                ItemType = ItemType.Game,
                AuthorId = 3,
                ItemStatus = ItemStatus.Available
            });

            Items.Add(new Item
            {
                Id = 4,
                Title = "Inception",
                Description = "A science fiction film written and directed by Christopher Nolan.",
                Bookshelf = "B1",
                ItemType = ItemType.DVD,
                AuthorId = 4,
                ItemStatus = ItemStatus.Available
            });

            Items.Add(new Item
            {
                Id = 5,
                Title = "Abbey Road",
                Description = "An album by The Beatles.",
                Bookshelf = "C1",
                ItemType = ItemType.CD,
                AuthorId = 5,
                ItemStatus = ItemStatus.Available
            });

            // Seed users
            Users.Add(new User
            {
                Id = 1,
                UserName = "JanZuur",
                FirstName = "Jan",
                LastName = "Zuur",
                Email = "janzuur@gmail.nl",
                Password = "jan123", // You should use a hashed password in a real application
                Role = UserRole.Admin
            });

            Users.Add(new User
            {
                Id = 2,
                UserName = "EmployeeAccount",
                FirstName = "Emma",
                LastName = "Robbergen",
                Email = "Robbergen@outlook.com",
                Password = "emma123", // You should use a hashed password in a real application
                Role = UserRole.Employee
            });

            Users.Add(new User
            {
                Id = 3,
                UserName = "GuestAccount",
                FirstName = "Guest",
                LastName = "User",
                Email = "guest@example.com",
                Password = "guest123", // You should use a hashed password in a real application
                Role = UserRole.Guest
            });

            SaveChanges();
        }




        // Update een bestaand Item in de database
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
        // Verwijder een Item uit de database op basis van het itemId
        public void DeleteItem(int itemId)
        {
            var itemToDelete = Items.FirstOrDefault(i => i.Id == itemId);

            if (itemToDelete != null)
            {
                Items.Remove(itemToDelete);
                SaveChanges();
            }
        }


        // Configureert de relaties tussen de entiteiten in de database
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
