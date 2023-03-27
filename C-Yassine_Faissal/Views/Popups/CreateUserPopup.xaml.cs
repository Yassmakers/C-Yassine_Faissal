using C_Yassine_Faissal.Data;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    // Een popup venster om een nieuwe gebruiker aan te maken.
    public partial class CreateUserPopup : Window
    {
        // Constructor voor de CreateUserPopup klasse.
        // Ontvangt de LibraryContext als parameter.
        public CreateUserPopup(LibraryContext libraryContext)
        {
            InitializeComponent();

            // Creeër een nieuwe instantie van CreateUserPopupViewModel
            // en wijs deze toe aan de DataContext.
            DataContext = new CreateUserPopupViewModel(libraryContext);

            // Voeg een eventhandler toe voor het PasswordChanged event van het PasswordBox element.
            PasswordBox.PasswordChanged += (s, e) => ((CreateUserPopupViewModel)DataContext).Password = PasswordBox.Password;
        }

        // Event handler voor het PasswordChanged event van het PasswordBox element.
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Als de DataContext niet null is, wijs de waarde van PasswordBox.Password toe aan de Password property van de DataContext.
            if (DataContext != null)
            {
                ((CreateUserPopupViewModel)DataContext).Password = PasswordBox.Password;
            }
        }
    }
}
