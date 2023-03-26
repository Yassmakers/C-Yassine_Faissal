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
    public class UpdateAuthorPopupViewModel : ViewModelBase
    {
        private readonly LibraryContext _libraryContext;
        private Author _selectedAuthor;

        private string _searchName;
        public string SearchName
        {
            get { return _searchName; }
            set { _searchName = value; OnPropertyChanged(); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        public ICommand SearchCommand { get; }
        public ICommand SaveCommand { get; }
        private bool CanSearchAuthor()
        {
            return !string.IsNullOrWhiteSpace(SearchName);
        }
        public UpdateAuthorPopupViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            SearchCommand = new RelayCommand(obj => SearchAuthor(), obj => CanSearchAuthor());
            SaveCommand = new RelayCommand(obj => SaveAuthor(), obj => CanSaveAuthor());
        }

        private void SearchAuthor()
        {
            _selectedAuthor = _libraryContext.Authors.FirstOrDefault(a => a.Name == SearchName);
            if (_selectedAuthor != null)
            {
                Name = _selectedAuthor.Name;
            }
            else
            {
                System.Windows.MessageBox.Show("Author not found");
            }
        }


        private bool CanSaveAuthor()
        {
            return _selectedAuthor != null;
        }

        private void SaveAuthor()
        {
            if (_selectedAuthor != null)
            {
                _selectedAuthor.Name = Name;
                _libraryContext.SaveChanges();
                System.Windows.MessageBox.Show("Author updated successfully.");
            }
            else
            {
                System.Windows.MessageBox.Show("Please search for an author before updating");
            }
        }
    }
}
