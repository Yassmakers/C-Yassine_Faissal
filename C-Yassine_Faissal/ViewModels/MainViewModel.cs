using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.Views;
using C_Yassine_Faissal.ViewModels;



public class MainViewModel : ViewModelBase
{
    private readonly LibraryContext _context;
    private ObservableCollection<Item> _items;
    private string _filter;
    private ICommand _searchCommand;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _context = (LibraryContext)serviceProvider.GetService(typeof(LibraryContext));
        LoadItems();
        SearchCommand = new RelayCommand(_ => ApplyFilter(Filter));
    }

    public ObservableCollection<Item> Items
    {
        get => _items;
        set => SetProperty(ref _items, value);
    }

    public string Filter
    {
        get => _filter;
        set => SetProperty(ref _filter, value);
    }

    public ICommand SearchCommand
    {
        get => _searchCommand;
        set => SetProperty(ref _searchCommand, value);
    }

    private void LoadItems()
    {
        // Load items from the database
        Items = new ObservableCollection<Item>(_context.Items.Include(i => i.Author).ToList());
    }

    private void ApplyFilter(string filter)
    {
        IQueryable<Item> query = _context.Items.Include(i => i.Author);

        if (!string.IsNullOrWhiteSpace(filter))
        {
            query = query.Where(i => i.Title.Contains(filter) || i.Author.Name.Contains(filter));
        }

        Items = new ObservableCollection<Item>(query.ToList());
    }
}
