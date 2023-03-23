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
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            services.AddDbContext<LibraryContext>(options =>
            {
                string dbPath = @"C:\Users\symon\source\repos\C-Yassine_Faissal\C-Yassine_Faissal\bin\Debug\net6.0-windows\library.db";
                options.UseSqlite($"Data Source={dbPath}");
            });

            // Register other services, e.g., ViewModels, repositories, etc.
            // services.AddTransient<YourViewModel>();

            services.AddTransient<MainWindow>(); // Add this line to register MainWindow

            serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<LibraryContext>();
                context.EnsureDatabaseCreated();
            }

            // Show the login window first
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            // Create and show the main window
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();

            // Close the login window
            loginWindow.Close();
        }
    }
}
