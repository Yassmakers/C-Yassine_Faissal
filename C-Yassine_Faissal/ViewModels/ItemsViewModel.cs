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
    public class ItemsViewModel : INotifyPropertyChanged
    {
        private LibraryContext _libraryContext;
        private ObservableCollection<Item> _filteredItems;
        private string _searchText;

        public ObservableCollection<Item> FilteredItems
        {
            get => _filteredItems;
            set
            {
                _filteredItems = value;
                OnPropertyChanged();
            }
        }
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
            private readonly Action<object> _execute;
            private readonly Predicate<object> _canExecute;

            public RelayCommand(Action<object> execute, Predicate<object> canExecute)
            {
                _execute = execute ?? throw new ArgumentNullException(nameof(execute));
                _canExecute = canExecute;
            }

            public event EventHandler CanExecuteChanged
            {
                add { CommandManager.RequerySuggested += value; }
                remove { CommandManager.RequerySuggested -= value; }
            }

            public bool CanExecute(object parameter)
            {
                return _canExecute == null || _canExecute(parameter);
            }

            public void Execute(object parameter)
            {
                _execute(parameter);
            }
        }



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

        public void LoadItems(string search = null, string filterType = null)
        {
            List<Item> items;
            if (string.IsNullOrEmpty(search))
            {
                items = _libraryContext.Items.ToList();
            }
            else
            {
                items = _libraryContext.Items.Where(i => i.Title.Contains(search)).ToList();
            }

            if (!string.IsNullOrEmpty(filterType) && filterType != "All")
            {
                items = items.Where(i => i.ItemType.ToString() == filterType).ToList();
            }

            FilteredItems = new ObservableCollection<Item>(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
