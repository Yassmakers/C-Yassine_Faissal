using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using C_Yassine_Faissal.Commands;
using System.Windows.Input;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using System;
using Microsoft.EntityFrameworkCore;

namespace C_Yassine_Faissal.ViewModels
{
    // Dit ViewModel beheert de logica achter het tonen van Items en het filteren van de lijst.
    public class ItemsViewModel : INotifyPropertyChanged
    {
        // De data context van de Library.
        private LibraryContext _libraryContext;
        private ObservableCollection<Item> _filteredItems;
        private string _searchText;

        // ObservableCollection eigenschap voor de gefilterde items.
        public ObservableCollection<Item> FilteredItems
        {
            get => _filteredItems;
            set
            {
                _filteredItems = value;
                OnPropertyChanged();
            }
        }

        // ICommand eigenschap voor het zoekcommando.
        private ICommand _searchCommand;
        public ICommand SearchCommand
        {
            get
            {
                if (_searchCommand == null)
                {
                    _searchCommand = new RelayCommand(param => LoadItems(SearchText), param => true);
                }
                return _searchCommand;
            }
        }
        public string SearchText
        {
            get => _searchText;
            set
            {
                _searchText = value;
                OnPropertyChanged();
                LoadItems(_searchText);
            }
        }

        public class RelayCommand : ICommand
        {
            // Velden die acties en voorwaarden bevatten voor het uitvoeren van het commando.
            private readonly Action<object> _execute;
            private readonly Predicate<object> _canExecute;
           
            // Constructor die de acties en voorwaarden initialiseert.
            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            // Event voor het detecteren van wijzigingen in de uitvoeringsstatus van het commando.
            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            // Methode om te controleren of het commando kan worden uitgevoerd.
            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            // Methode om het commando uit te voeren.
            public void Execute(object parameter)
            {
                _execute(parameter);
            }
        }


        // FilterItems methode om items te filteren op basis van de zoektekst.
        private void FilterItems()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                FilteredItems = new ObservableCollection<Item>(_libraryContext.Items.Include(i => i.Author).ToList());
            }
            else
            {
                FilteredItems = new ObservableCollection<Item>(
                    _libraryContext.Items
                    .Include(i => i.Author)
                    .Where(i => i.Title.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || i.Author.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList());
            }
        }


        // Constructor: Initialiseert de data context en laadt de gefilterde items.
        public ItemsViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            FilterItems();
        }

        private string _filterType;
        public string FilterType
        {
            get => _filterType;
            set
            {
                _filterType = value;
                OnPropertyChanged();
                LoadItems(_searchText, _filterType);
            }
        }

        public List<string> FilterOptions { get; set; } = new List<string> { "All", "Book", "CD", "DVD", "Game" }; // Add your item types here

        // Laad items op basis van de zoektekst en het filtertype.
        public void LoadItems(string search = null, string filterType = null)
        {
            // Maak een nieuwe lijst om items op te slaan.
            List<Item> items;

            // Als de zoektekst leeg is, krijgen we alle items in de lijst.
            if (string.IsNullOrEmpty(search))
            {
                items = _libraryContext.Items.Include(i => i.Author).ToList();
            }

            // Als de zoektekst niet leeg is, filteren we de items op basis van de zoektekst.
            else
            {
                // De keuze om EF.Functions.Like te gebruiken om items te filteren op basis van de zoektekst is interessant
                // omdat het een efficiënte manier is om te zoeken naar strings die overeenkomen met een patroon.
                // De resultaten worden gefilterd op basis van de titel van het item of de naam van de auteur van het item.
                items = _libraryContext.Items
                .Include(i => i.Author)
                  .Where(i => EF.Functions.Like(i.Title, $"%{search}%") ||
                    EF.Functions.Like(i.Author.Name, $"%{search}%"))
                 .ToList();
            }

            // Als er een filtertype is geselecteerd, filteren we de items op basis van het geselecteerde filtertype.
            if (!string.IsNullOrEmpty(filterType) && filterType != "All")
            {
                // Filter de lijst op basis van het itemtype.
                items = items.Where(i => i.ItemType.ToString() == filterType).ToList();
            }

            // Geef de gefilterde items weer in een ObservableCollection.
            FilteredItems = new ObservableCollection<Item>(items);
        }


        // Event om aan te geven dat een eigenschap is gewijzigd.
        public event PropertyChangedEventHandler PropertyChanged;

        // Methode om het PropertyChanged event te activeren.
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
