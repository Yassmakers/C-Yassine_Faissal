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
    // Constructor: Initialiseert de data context en laadt de auteurs-, itemtypes- en itemstatuslijsten
    public CreateItemPopupViewModel(LibraryContext libraryContext)
    {
        
            _libraryContext = libraryContext;
            Authors = new ObservableCollection<Author>(_libraryContext.Authors.ToList());
            ItemTypes = new ObservableCollection<ItemType>(Enum.GetValues(typeof(ItemType)).Cast<ItemType>());
            ItemStatuses = new ObservableCollection<ItemStatus>(Enum.GetValues(typeof(ItemStatus)).Cast<ItemStatus>());
        

        _libraryContext = libraryContext;
        CreateItemCommand = new RelayCommand(CreateItem);

        // Initialiseer de ItemTypeCollection met de beschikbare itemtypen
        ItemTypeCollection = new ObservableCollection<ItemType>
    {
        ItemType.Book,
        ItemType.CD,
        ItemType.DVD,
        ItemType.Game
    };
    }



    private Author _selectedAuthor;
    public Author SelectedAuthor
    {
        get => _selectedAuthor;
        set
        {
            SetProperty(ref _selectedAuthor, value);
            if (value != null)
                AuthorId = value.Id;
        }
    }


    // Creëert een nieuw Item object en voegt deze toe aan de database
    private void CreateItem(object obj)
    {
        // Controleert of een item met dezelfde titel al bestaat in de database (case-insensitief)
        if (_libraryContext.Items.Any(i => i.Title.ToLower() == Title.ToLower()))
        {
            // Toon een foutmelding als er al een item met dezelfde titel bestaat
            MessageBox.Show($"An item with the title '{Title}' already exists. Please choose a different title.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }
        // Maakt een nieuw Item object met de ingevoerde gegevens
        var newItem = new Item
        {
            Title = Title,
            Description = Description,
            ItemType = ItemType,
            AuthorId = AuthorId,
            ItemStatus = ItemStatus.Available
        };
        newItem.ItemStatus = SelectedItemStatus;
        // Voegt het nieuwe Item object toe aan de Items DbSet
        _libraryContext.Items.Add(newItem);
        // Probeert de wijzigingen op te slaan en het venster te sluiten
        try
        {
            // Slaat de wijzigingen op in de database
            _libraryContext.SaveChanges();
        }
        catch (DbUpdateException ex)
        {
            // Toon een foutmelding bij een uitzondering
            MessageBox.Show($"An error occurred while saving the item: {ex.InnerException.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            return;
        }

        // Confirmation bericht
        MessageBox.Show($"Item '{Title}' has been created successfully.", "Item Created", MessageBoxButton.OK, MessageBoxImage.Information);

        // Maakt de input velden leeg
        Title = string.Empty;
        Description = string.Empty;
        AuthorId = 0;
        ItemType = ItemType.None;
        SelectedAuthor = null;
    }


}
