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

public class CreateItemPopupViewModel : ViewModelBase
{

    public ObservableCollection<Author> Authors { get; set; }
    public ObservableCollection<ItemType> ItemTypes { get; set; }
    public ObservableCollection<ItemStatus> ItemStatuses { get; set; }

    private string _title;
    private string _description;
    private ItemType _itemType;
    private int _authorId;
    private LibraryContext _libraryContext;
    public ICommand CreateItemCommand { get; }
    public string Title
    {
        get => _title;
        set
        {
            SetProperty(ref _title, value);
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            SetProperty(ref _description, value);
        }
    }

    public ItemType ItemType
    {
        get => _itemType;
        set
        {
            SetProperty(ref _itemType, value);
        }
    }

    public int AuthorId
    {
        get => _authorId;
        set
        {
            SetProperty(ref _authorId, value);
        }
    }
    private void ExecuteCreateItemCommand()
    {

        

    }
    public ObservableCollection<ItemType> ItemTypeCollection { get; set; }
    public ItemStatus SelectedItemStatus { get; set; }
    public CreateItemPopupViewModel(LibraryContext libraryContext)
    {
        
            _libraryContext = libraryContext;
            Authors = new ObservableCollection<Author>(_libraryContext.Authors.ToList());
            ItemTypes = new ObservableCollection<ItemType>(Enum.GetValues(typeof(ItemType)).Cast<ItemType>());
            ItemStatuses = new ObservableCollection<ItemStatus>(Enum.GetValues(typeof(ItemStatus)).Cast<ItemStatus>());
        

        _libraryContext = libraryContext;
        CreateItemCommand = new RelayCommand(CreateItem);

        // Initialize the ItemTypeCollection with the available item types
        ItemTypeCollection = new ObservableCollection<ItemType>
    {
        ItemType.Book,
        ItemType.CD,
        ItemType.DVD,
        ItemType.Game
    };
    }



   


    private void CreateItem(object obj)
    {
        var newItem = new Item
        {
            Title = Title,
            Description = Description,
            ItemType = ItemType,
            AuthorId = AuthorId,
            ItemStatus = ItemStatus.Available
        };
        newItem.ItemStatus = SelectedItemStatus;
        _libraryContext.Items.Add(newItem);

        try
        {
            _libraryContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            // Inspect the inner exception for more details
            MessageBox.Show($"An error occurred while saving the item: {ex.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Show a confirmation message
        MessageBox.Show($"Item '{Title}' has been created successfully.", "Item Created", MessageBoxButton.OK, MessageBoxImage.Information);

        // Clear the input fields
        Title = string.Empty;
        Description = string.Empty;
        AuthorId = 0;
        ItemType = ItemType.None;
    }


}
