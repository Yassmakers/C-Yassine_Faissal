using C_Yassine_Faissal.Models;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using C_Yassine_Faissal;
using Microsoft.Extensions.Configuration;
using Bogus;
using System.IO;
using C_Yassine_Faissal.Data;
using System;

namespace C_Yassine_Faissal
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        private IServiceCollection services = new ServiceCollection();

        // Wordt aangeroepen bij het starten van de applicatie
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Voeg de database context toe aan de services
            services.AddDbContext<LibraryContext>(options =>
            {
                string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "library.db");
                options.UseSqlite($"Data Source={dbPath}");
            }, ServiceLifetime.Transient, ServiceLifetime.Singleton);


            // Registreer andere services, bijvoorbeeld ViewModels en repositories
            // services.AddTransient<YourViewModel>();

            // Registreer de MainWindow service
            services.AddTransient<MainWindow>();

            // Bouw de ServiceProvider op
            serviceProvider = services.BuildServiceProvider();

            // Maak de database aan als deze nog niet bestaat
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                context.EnsureDatabaseCreated();
            }

            // Toon eerst het login scherm
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            // Maak en toon het hoofdscherm
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            // Sluit het login scherm
            loginWindow.Close();
        }
    }
}
