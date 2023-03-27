using C_Yassine_Faissal.Commands;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace C_Yassine_Faissal.ViewModels.Popups
{
    public class DeleteUserPopupViewModel : ViewModelBase
    {
        private readonly LibraryContext _libraryContext;

        private string _searchEmail;
        public string SearchEmail
        {
            get { return _searchEmail; }
            set { _searchEmail = value; OnPropertyChanged(); }
        }

        public ICommand SearchAndDeleteCommand { get; }
        // Constructor: Initialiseert de data context en stelt de SearchAndDeleteCommand in
        public DeleteUserPopupViewModel(LibraryContext libraryContext)
        {
            _libraryContext = libraryContext;
            // Initialiseren van de SearchAndDeleteCommand met een actie om de SearchAndDeleteUser-functie aan te roepen
            SearchAndDeleteCommand = new RelayCommand(obj => SearchAndDeleteUser());
        }

        private void SearchAndDeleteUser()
        {
            // Zoek de gebruiker op basis van het ingevoerde e-mailadres
            var userToDelete = _libraryContext.Users.FirstOrDefault(u => u.Email == SearchEmail);

            if (userToDelete != null)
            {
                // Als de gebruiker gevonden is, verwijder deze dan uit de database
                _libraryContext.Users.Remove(userToDelete);
                _libraryContext.SaveChanges();

                // Toon een bericht dat de gebruiker succesvol is verwijderd
                MessageBox.Show("User deleted successfully.");
            }
            else
            {
                // Als de gebruiker niet gevonden is, toon dan een bericht
                MessageBox.Show("User not found");
            }
        }
    }
}
