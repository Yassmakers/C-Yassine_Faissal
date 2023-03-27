using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace C_Yassine_Faissal.Data
{
    // Factoryklasse voor het ontwerpen van de LibraryContext tijdens het ontwikkelen
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LibraryContext>
    {
        // Maakt en retourneert een nieuwe LibraryContext
        public LibraryContext CreateDbContext(string[] args)
        {
            // Laadt de connection string uit de appsettings.json configuratie file
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            // Maakt een nieuwe DbContextOptionsBuilder<LibraryContext>
            var builder = new DbContextOptionsBuilder<LibraryContext>();

            // Haalt de connection string op uit de configuratie
            var connectionString = configuration.GetConnectionString("LibraryDatabase");

            // Configureert de DbContextOptionsBuilder om SQLite te gebruiken met de opgegeven connection string
            builder.UseSqlite(connectionString);

            // Bouwt en retourneert de LibraryContext
            return new LibraryContext(builder.Options);
        }
    }
}
