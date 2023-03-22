using System.Windows;
using System.Windows.Controls;
using System.Threading.Tasks;
using C_Yassine_Faissal.Views.Popups;
using System;
using Microsoft.Extensions.Configuration;
using System.IO;
using Microsoft.EntityFrameworkCore;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.Data;

// ...


namespace C_Yassine_Faissal
{
    public partial class MainWindow : Window
    {
        private Frame _contentFrame;

        private LibraryContext _libraryContext;
        public DbSet<User> Users { get; set; }

        public MainWindow()

        {
            InitializeComponent();

            var loginWindow = new LoginWindow();
            if (loginWindow.ShowDialog() == true)
            {
                // TODO: Check user role here and show/hide buttons accordingly
                // For example:
                bool isAdmin = true; // Replace this with the actual check for the user role
                bool isEmployee = true; // Replace this with the actual check for the user role

                CreateUserButton.Visibility = (isAdmin || isEmployee) ? Visibility.Visible : Visibility.Collapsed;
                UpdateUserButton.Visibility = (isAdmin || isEmployee) ? Visibility.Visible : Visibility.Collapsed;
                DeleteUserButton.Visibility = (isAdmin || isEmployee) ? Visibility.Visible : Visibility.Collapsed;
                CreateItemButton.Visibility = (isAdmin || isEmployee) ? Visibility.Visible : Visibility.Collapsed;
                UpdateItemButton.Visibility = (isAdmin || isEmployee) ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                Application.Current.Shutdown();
            }

            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());
            _libraryContext = new LibraryContext(optionsBuilder.Options);
            _contentFrame = new Frame();
            _contentFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            MainContent.Children.Add(_contentFrame);

        }
        private async Task CreateUserAsync(User newUser)
        {
            if (newUser != null)
            {
                _libraryContext.Users.Add(newUser);
                await _libraryContext.SaveChangesAsync();
            }
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            return configuration.GetConnectionString("LibraryDatabase");
        }

        

  

        private void LoadContent(ContentControl content)
        {
            _contentFrame.Content = content;
        }


        

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new C_Yassine_Faissal.Views.Search());
        }

        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            var createUserWindow = new CreateUserWindow();
            createUserWindow.ShowDialog();
        }


        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new C_Yassine_Faissal.Views.Popups.UpdateUserPopup());
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new C_Yassine_Faissal.Views.Popups.DeleteUserPopup());
        }

        private void CreateItemButton_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new Views.Popups.CreateItemPopup());
        }


        private void UpdateItemButton_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new C_Yassine_Faissal.Views.Popups.UpdateItemPopup());
        }



    }
}