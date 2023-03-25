using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.ViewModels;

public class MainViewModel : ViewModelBase
{
    public ItemsViewModel ItemsViewModel { get; set; }

    public MainViewModel(LibraryContext libraryContext)
    {
        ItemsViewModel = new ItemsViewModel(libraryContext);
    }
}
