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
using C_Yassine_Faissal.ViewModels.Popups;
using C_Yassine_Faissal.Views;

namespace C_Yassine_Faissal
{
    public partial class MainWindow : Window
    {
        private Frame _contentFrame;

        private LibraryContext _libraryContext;
        public DbSet<User> Users { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsEmployee { get; set; }

        private ItemsView _itemsView;

        public MainWindow(bool isAdmin = false, bool isEmployee = false)
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

            // Pass the ItemsViewModel from MainViewModel to the ItemsView
            var mainViewModel = new MainViewModel(_libraryContext);
            DataContext = mainViewModel;
            _itemsView = new ItemsView(mainViewModel.ItemsViewModel);

            CreateUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            DeleteUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            CreateItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            DeleteItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            CreateAuthorButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateAuthorButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            DeleteAuthorButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;

            // Load ItemsView by default
            _contentFrame.Content = _itemsView;
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


        private void CreateItemButton_Click(object sender, RoutedEventArgs e)
        {
            var createItemPopup = new CreateItemPopup(_libraryContext);
            createItemPopup.Owner = this;
            createItemPopup.ShowDialog();
        }

        private void UpdateItemButton_Click_1(object sender, RoutedEventArgs e)
        {
            var updateItemPopup = new UpdateItemPopup(_libraryContext);
            updateItemPopup.Owner = this;
            updateItemPopup.ShowDialog();
        }


        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteItemPopup = new DeleteItemPopup(new UpdateItemPopupViewModel(_libraryContext));
            deleteItemPopup.Owner = this;
            deleteItemPopup.ShowDialog();
        }



        private void CreateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var createAuthorPopup = new CreateAuthorPopup(_libraryContext);
            createAuthorPopup.Owner = this;
            createAuthorPopup.ShowDialog();
        }

        private void UpdateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var updateAuthorPopup = new UpdateAuthorPopup(new UpdateAuthorPopupViewModel(_libraryContext));
            updateAuthorPopup.Owner = this;
            updateAuthorPopup.ShowDialog();
        }

        private void DeleteAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteAuthorPopup = new DeleteAuthorPopup(new DeleteAuthorPopupViewModel(_libraryContext));
            deleteAuthorPopup.Owner = this;
            deleteAuthorPopup.ShowDialog();
        }




        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            var createUserPopup = new CreateUserPopup(_libraryContext);
            createUserPopup.ShowDialog();
        }


        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            var updateUserPopup = new UpdateUserPopup();
            updateUserPopup.DataContext = new UpdateUserPopupViewModel(_libraryContext);
            updateUserPopup.Owner = this;
            updateUserPopup.ShowDialog();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var deleteUserPopup = new DeleteUserPopup();
            deleteUserPopup.DataContext = new DeleteUserPopupViewModel(_libraryContext);
            deleteUserPopup.Owner = this;
            deleteUserPopup.ShowDialog();
        }


        private void LoadContent(ContentControl content)
        {
            _contentFrame.Content = content;
        }


       










    }
}