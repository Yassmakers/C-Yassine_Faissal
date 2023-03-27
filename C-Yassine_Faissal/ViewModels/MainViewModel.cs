using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.ViewModels;

public class MainViewModel : ViewModelBase
{
    // Eigenschap voor de ItemsViewModel.
    public ItemsViewModel ItemsViewModel { get; set; }

    // Constructor
    public MainViewModel(LibraryContext libraryContext)
    {
        // Initialiseer de ItemsViewModel met de libraryContext.
        ItemsViewModel = new ItemsViewModel(libraryContext);
    }
}

