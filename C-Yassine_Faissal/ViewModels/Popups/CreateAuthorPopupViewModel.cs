using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System;
using static C_Yassine_Faissal.Commands.RelayCommand;
using System.ComponentModel;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class CreateAuthorPopupViewModel : ViewModelBase
    {
        private readonly LibraryContext _libraryContext;

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public ICommand CreateCommand { get; }
        public ICommand CreateAuthorCommand { get; }
        

        public ObservableCollection<Author> Authors { get; set; }
        // Constructor: Initialiseert de data context en laadt de auteurslijst
        public CreateAuthorPopupViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            Authors = new ObservableCollection<Author>(_libraryContext.Authors.ToList());
            CreateCommand = new RelayCommand(CreateAuthor, obj => CanCreateAuthor());
        }

        // Controleert of er een geldige naam is ingevoerd voor de auteur
        private bool CanCreateAuthor()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }

        // Maakt een nieuw Author object aan en voegt deze toe aan de database
        private void CreateAuthor(object obj)
        {
            // Creëer een nieuw Author-object met de ingevoerde gegevens van de gebruiker

            var newAuthor = new Author
            {
                Name = Name
            };

            _libraryContext.Authors.Add(newAuthor);
            // Probeer de wijzigingen op te slaan; toon een foutbericht als er een uitzondering optreedt
            try
            {
                _libraryContext.SaveChanges();
                Authors.Add(newAuthor); 
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"An error occurred while saving the author: {ex.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Als het opslaan succesvol is, toon een informatiebericht
            MessageBox.Show($"Author '{newAuthor.Name}' has been created successfully.", "Author Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
