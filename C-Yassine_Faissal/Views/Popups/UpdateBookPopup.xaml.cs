using C_Yassine_Faissal.ViewModels.Popups;
using System.Windows;
using System.Windows.Controls;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class UpdateBookPopup : UserControl
    {
        public UpdateBookPopup()
        {
            InitializeComponent();
            DataContext = new CreateUserPopupViewModel();
        }

        private void UpdateBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Your code to create a user here
        }
    }
}