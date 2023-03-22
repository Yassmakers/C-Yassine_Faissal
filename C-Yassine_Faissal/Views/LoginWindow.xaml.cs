using C_Yassine_Faissal.ViewModels;
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
            DataContext = new LoginViewModel(); // zet de DataContext hier aub
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
                Close();
            }
        }
    }
    }
