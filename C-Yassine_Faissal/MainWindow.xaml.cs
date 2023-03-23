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

namespace C_Yassine_Faissal
{
    public partial class MainWindow : Window
    {
        private Frame _contentFrame;

        private LibraryContext _libraryContext;
        public DbSet<User> Users { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsEmployee { get; set; }
        public MainWindow() : this(false, false) { }
        public MainWindow(bool isAdmin, bool isEmployee)
        {
            InitializeComponent();

            IsAdmin = isAdmin;
            IsEmployee = isEmployee;

            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite(GetConnectionString());
            _libraryContext = new LibraryContext(optionsBuilder.Options);
            _contentFrame = new Frame();
            _contentFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            MainContent.Children.Add(_contentFrame);

            CreateUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            DeleteUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            CreateItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
        }

        // 

private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Owner?.Show(); // Show the LoginWindow when the MainWindow is closed
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

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            LoadContent(new C_Yassine_Faissal.Views.Popups.DeleteItemPopup());
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