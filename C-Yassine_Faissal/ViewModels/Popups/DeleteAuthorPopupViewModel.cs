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

    // Add a constructor that accepts a LibraryContext argument
    public DeleteAuthorPopupViewModel(LibraryContext libraryContext)
    {
        _libraryContext = libraryContext;
        SearchCommand = new RelayCommand(obj => SearchAuthor(), obj => CanSearchAuthor());
        DeleteCommand = new RelayCommand(obj => DeleteAuthor(), obj => CanDeleteAuthor());
    }

    private void SearchAuthor()
    {
        _selectedAuthor = _libraryContext.Authors.FirstOrDefault(a => a.Name == SearchName);
        if (_selectedAuthor != null)
        {
            SelectedAuthorDetails = $"Author Name: {_selectedAuthor.Name}\n" +
                                    $"Author ID: {_selectedAuthor.Id}";
        }
        else
        {
            MessageBox.Show("Author not found");
            SelectedAuthorDetails = string.Empty;
        }
    }

    private bool CanSearchAuthor()
    {
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
            _libraryContext.Authors.Remove(_selectedAuthor);
            _libraryContext.SaveChanges();
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
        return _selectedAuthor != null;
    }
}
