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
            // Controleer of er tekst is ingevoerd in het zoekveld
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
            // Zoek de auteur op basis van de ingevoerde naam
            _selectedAuthor = _libraryContext.Authors.FirstOrDefault(a => a.Name == SearchName);
            if (_selectedAuthor != null)
            {
                // Als de auteur gevonden is, vul dan het Name-veld in
                Name = _selectedAuthor.Name;
            }
            else
            {
                // Als de auteur niet gevonden is, toon dan een bericht
                System.Windows.MessageBox.Show("Author not found");
            }
        }


        private bool CanSaveAuthor()
        {
            // Controleer of er een auteur is geselecteerd
            return _selectedAuthor != null;
        }

        private void SaveAuthor()
        {
            // Update de geselecteerde auteur in de database
            if (_selectedAuthor != null)
            {
                _selectedAuthor.Name = Name;
                _libraryContext.SaveChanges();

                // Toon een bericht dat de auteur succesvol is bijgewerkt
                System.Windows.MessageBox.Show("Author updated successfully.");
            }
            else
            {
                // Toon een bericht dat eerst een auteur gezocht moet worden
                System.Windows.MessageBox.Show("Please search for an author before updating");
            }
        }
    }
}
