using C_Yassine_Faissal.ViewModels;
using System;
using System.Windows;

namespace C_Yassine_Faissal
{
    public partial class LoginWindow : Window
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        // Constructor voor LoginWindow
        public LoginWindow()
        {
            InitializeComponent();

            // Stel de DataContext in op een nieuwe instantie van de LoginViewModel-klasse.
            DataContext = new LoginViewModel();

            // Stel de CloseAction in op een lambda-functie die het venster sluit wanneer het wordt aangeroepen.
            (DataContext as LoginViewModel).CloseAction = new Action(() => this.Close());
        }

        // Eventhandler voor het klikken op de "Guest Login" knop.
        private void GuestLoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Open een nieuw hoofdvenster met de "isGuest" en "isAdmin" vlaggen ingesteld op "false".
            var mainWindow = new MainWindow(false, false);
            mainWindow.Show();

            // Sluit het huidige venster.
            Close();
        }


        // Eventhandler voor het klikken op de "Login" knop.
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Haal de LoginViewModel op uit de DataContext en stel de eigenschappen Email en Password in op de waarden van de tekstvakken.
            var viewModel = DataContext as LoginViewModel;
            viewModel.Email = EmailTextBox.Text;
            viewModel.Password = PasswordTextBox.Password;

            // Als het LoginCommand kan worden uitgevoerd, voer het dan uit en sluit het venster.
            if (viewModel.LoginCommand.CanExecute(null))
            {
                viewModel.LoginCommand.Execute(null);
                Close();
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
