using C_Yassine_Faissal.Models;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using C_Yassine_Faissal;
using Microsoft.Extensions.Configuration;
using Bogus;
using System.IO;
using C_Yassine_Faissal.Data;

namespace C_Yassine_Faissal
{
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        private IServiceCollection services = new ServiceCollection();
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Configure services and DbContext here
            var configurationBuilder = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration configuration = configurationBuilder.Build();
            string connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<LibraryContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Register other services, e.g., ViewModels, repositories, etc.
            // services.AddTransient<YourViewModel>();

            services.AddTransient<MainWindow>(); // Add this line to register MainWindow

            serviceProvider = services.BuildServiceProvider();

            // Create and show the main window
            var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
