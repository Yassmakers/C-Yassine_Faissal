using C_Yassine_Faissal.ViewModels;
using System;
using System.Windows;

namespace C_Yassine_Faissal
{
    public partial class LoginWindow : Window
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
            (DataContext as LoginViewModel).CloseAction = new Action(() => this.Close());
        }

        private void GuestLoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var mainWindow = new MainWindow(false, false, this); // Pass the LoginWindow instance
                mainWindow.Show();
                this.Hide(); // Hide the LoginWindow instead of closing it
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = DataContext as LoginViewModel;
            viewModel.Email = EmailTextBox.Text;
            viewModel.Password = PasswordTextBox.Password;

            if (viewModel.LoginCommand.CanExecute(null))
            {
                viewModel.LoginCommand.Execute(null);
                DialogResult = true;
            }
        }
    }
}



