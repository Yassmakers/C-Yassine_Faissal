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

        public CreateAuthorPopupViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            Authors = new ObservableCollection<Author>(_libraryContext.Authors.ToList());
            CreateCommand = new RelayCommand(CreateAuthor, obj => CanCreateAuthor());
        }


        private bool CanCreateAuthor()
        {
            return !string.IsNullOrWhiteSpace(Name);
        }
        // Add the following constructor code

        // Add the CreateAuthor method
        private void CreateAuthor(object obj)
        {
            var newAuthor = new Author
            {
                Name = Name
            };

            _libraryContext.Authors.Add(newAuthor);

            try
            {
                _libraryContext.SaveChanges();
                Authors.Add(newAuthor); // Update the ObservableCollection
            }
            catch (DbUpdateException ex)
            {
                MessageBox.Show($"An error occurred while saving the author: {ex.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show($"Author '{newAuthor.Name}' has been created successfully.", "Author Created", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
