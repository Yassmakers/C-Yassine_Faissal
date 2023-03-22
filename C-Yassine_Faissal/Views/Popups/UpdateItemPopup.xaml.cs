using C_Yassine_Faissal.ViewModels.Popups;
using System.Windows;
using System.Windows.Controls;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class UpdateItemPopup : UserControl
    {
        public UpdateItemPopup()
        {
            InitializeComponent();
            DataContext = new CreateUserPopupViewModel();
        }

        private void UpdateItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Your code to create a user here
        }
    }
}