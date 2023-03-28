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
            // Initialiseer de benodigde variabelen en voeg een Frame toe aan de MainContent stackpanel.
            InitializeComponent();

            IsAdmin = isAdmin;
            IsEmployee = isEmployee;



            var optionsBuilder = new DbContextOptionsBuilder<LibraryContext>();
            optionsBuilder.UseSqlite(GetConnectionString());
            _libraryContext = new LibraryContext(optionsBuilder.Options);
            _contentFrame = new Frame();
            _contentFrame.NavigationUIVisibility = System.Windows.Navigation.NavigationUIVisibility.Hidden;
            MainContent.Children.Add(_contentFrame);

            // Voeg de ItemsViewModel toe aan de DataContext van de MainWindow.
            var mainViewModel = new MainViewModel(_libraryContext);
            DataContext = mainViewModel;
            _itemsView = new ItemsView(mainViewModel.ItemsViewModel);

            // Update the visibility of the SideBarPanel based on user role.
            UsersExpander.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            AuthorsExpander.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            ItemsExpander.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;

            // Zet de zichtbaarheid van de knoppen afhankelijk van of de gebruiker een admin of employee is.
            CreateUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            DeleteUserButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            CreateItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            DeleteItemButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            CreateAuthorButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            UpdateAuthorButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;
            DeleteAuthorButton.Visibility = (IsAdmin || IsEmployee) ? Visibility.Visible : Visibility.Collapsed;

            // Laad ItemsView als standaardcontent.
            _contentFrame.Content = _itemsView;
        }



        // Methode om LoginWindow te tonen wanneer MainWindow wordt gesloten.
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            this.Owner?.Show(); // Show the LoginWindow when the MainWindow is closed
        }
        
        // Methode om een nieuwe gebruiker aan te maken.
        private async Task CreateUserAsync(User newUser)
        {
            if (newUser != null)
            {
                _libraryContext.Users.Add(newUser);
                await _libraryContext.SaveChangesAsync();
            }
        }

        // Methode om de connection string op te halen uit appsettings.json.
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
            // Maak een nieuwe CreateItemPopup en geef de LibraryContext mee als parameter.
            var createItemPopup = new CreateItemPopup(_libraryContext);

            // Stel de Owner van de popup in als dit MainWindow.
            createItemPopup.Owner = this;

            // Toon de popup.
            createItemPopup.ShowDialog();
        }

        private void UpdateItemButton_Click_1(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe UpdateItemPopup en geef de LibraryContext mee als parameter.
            var updateItemPopup = new UpdateItemPopup(_libraryContext);

            // Stel de Owner van de popup in als dit MainWindow.
            updateItemPopup.Owner = this;

            // Toon de popup.
            updateItemPopup.ShowDialog();
        }


        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe DeleteItemPopup met een UpdateItemPopupViewModel als DataContext, en geef de LibraryContext mee als parameter.
            var deleteItemPopup = new DeleteItemPopup(new UpdateItemPopupViewModel(_libraryContext));
            deleteItemPopup.Owner = this;
            deleteItemPopup.ShowDialog();
        }



        private void CreateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe CreateAuthorPopup en geef de LibraryContext mee als parameter.
            var createAuthorPopup = new CreateAuthorPopup(_libraryContext);
            createAuthorPopup.Owner = this;
            createAuthorPopup.ShowDialog();
        }

        private void UpdateAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe UpdateAuthorPopup met een UpdateAuthorPopupViewModel als DataContext, en geef de LibraryContext mee als parameter.
            var updateAuthorPopup = new UpdateAuthorPopup(new UpdateAuthorPopupViewModel(_libraryContext));
            updateAuthorPopup.Owner = this;
            updateAuthorPopup.ShowDialog();
        }

        private void DeleteAuthorButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe DeleteAuthorPopup met een DeleteAuthorPopupViewModel als DataContext, en geef de LibraryContext mee als parameter.
            var deleteAuthorPopup = new DeleteAuthorPopup(new DeleteAuthorPopupViewModel(_libraryContext));
            deleteAuthorPopup.Owner = this;
            deleteAuthorPopup.ShowDialog();
        }




        private void CreateUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe CreateUserPopup en geef de LibraryContext mee als parameter.
            var createUserPopup = new CreateUserPopup(_libraryContext);
            createUserPopup.ShowDialog();
        }


        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe UpdateUserPopup.
            var updateUserPopup = new UpdateUserPopup();

            // Stel de DataContext van de popup in als een nieuwe UpdateUserPopupViewModel met de LibraryContext als parameter.
            updateUserPopup.DataContext = new UpdateUserPopupViewModel(_libraryContext);

            // Stel de Owner van de popup in als dit MainWindow.
            updateUserPopup.Owner = this;

            // Toon de popup.
            updateUserPopup.ShowDialog();
        }

        private void DeleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            // Maak een nieuwe DeleteUserPopup.
            var deleteUserPopup = new DeleteUserPopup();
            
            // Stel de DataContext van de popup in als een nieuwe DeleteUserPopupViewModel met de LibraryContext als parameter.
            deleteUserPopup.DataContext = new DeleteUserPopupViewModel(_libraryContext);

            // Stel de Owner van de popup in als dit MainWindow.
            deleteUserPopup.Owner = this;
            
            // Toon de popup.
            deleteUserPopup.ShowDialog();
        }

     
        // Laadt het opgegeven ContentControl in het hoofdframe van het MainWindow.
        private void LoadContent(ContentControl content)
        {
            _contentFrame.Content = content;
        }

    }
}