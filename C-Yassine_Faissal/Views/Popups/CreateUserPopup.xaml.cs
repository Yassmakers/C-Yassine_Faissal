using C_Yassine_Faissal.Data;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class CreateUserPopup : Window
    {
        public CreateUserPopup(LibraryContext libraryContext)
        {
            InitializeComponent();
            DataContext = new CreateUserPopupViewModel(libraryContext);
            PasswordBox.PasswordChanged += (s, e) => ((CreateUserPopupViewModel)DataContext).Password = PasswordBox.Password;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext != null)
            {
                ((CreateUserPopupViewModel)DataContext).Password = PasswordBox.Password;
            }
        }
    }
}
