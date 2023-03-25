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

    public ItemsViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            LoadItems();
        }

        public void LoadItems(string search = null) 
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
            FilteredItems = new ObservableCollection<Item>(items);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
