using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Input;

public class DeleteAuthorPopupViewModel : ViewModelBase
{
    private readonly LibraryContext _libraryContext;

    private string _searchName;
    public string SearchName
    {
        get { return _searchName; }
        set { _searchName = value; OnPropertyChanged(); }
    }

    private Author _selectedAuthor;
    public Author SelectedAuthor
    {
        get { return _selectedAuthor; }
        set { _selectedAuthor = value; OnPropertyChanged(); }
    }

    public ICommand SearchCommand { get; }
    public ICommand DeleteCommand { get; }

    // Constructor: Initialiseert de data context en stelt de SearchCommand en DeleteCommand in
    public DeleteAuthorPopupViewModel(LibraryContext libraryContext)
    {
        _libraryContext = libraryContext;
        // Initialiseren van de SearchCommand met een actie om de SearchAuthor-functie aan te roepen en een voorwaarde om te controleren of er gezocht kan worden
        SearchCommand = new RelayCommand(obj => SearchAuthor(), obj => CanSearchAuthor());
        // Initialiseren van de DeleteCommand met een actie om de DeleteAuthor-functie aan te roepen en een voorwaarde om te controleren of er een auteur verwijderd kan worden
        DeleteCommand = new RelayCommand(obj => DeleteAuthor(), obj => CanDeleteAuthor());
    }

    private void SearchAuthor()
    {
        // Zoek de eerste auteur in de database met de opgegeven naam en wijs deze toe aan _selectedAuthor
        _selectedAuthor = _libraryContext.Authors.FirstOrDefault(a => a.Name == SearchName);
        if (_selectedAuthor != null)
        {
            SelectedAuthorDetails = $"Author Name: {_selectedAuthor.Name}\n" +
                                    $"Author ID: {_selectedAuthor.Id}";
        }
        else
        {
            // Als de auteur gevonden is, toon dan een bericht
            MessageBox.Show("Author not found");
            SelectedAuthorDetails = string.Empty;
        }
    }

    private bool CanSearchAuthor()
    {
        // Controleer of er tekst is ingevoerd in het zoekveld
        return !string.IsNullOrEmpty(SearchName);
    }

    private string _selectedAuthorDetails;
    public string SelectedAuthorDetails
    {
        get { return _selectedAuthorDetails; }
        set { _selectedAuthorDetails = value; OnPropertyChanged(); }
    }


    private void DeleteAuthor()
    {
        if (_selectedAuthor != null)
        {        
            // Verwijder de geselecteerde auteur uit de database
            _libraryContext.Authors.Remove(_selectedAuthor);
            _libraryContext.SaveChanges();

            // Toon een bericht dat de auteur succesvol is verwijderd
            MessageBox.Show("Author deleted successfully");
            SelectedAuthor = null;
            SearchName = null;
            SelectedAuthorDetails = string.Empty;
        }
        else
        {
            MessageBox.Show("Please search for an author before deleting");
        }
    }

    private bool CanDeleteAuthor()
    {
        // Controleer of er een auteur is geselecteerd
        return _selectedAuthor != null;
    }
}
