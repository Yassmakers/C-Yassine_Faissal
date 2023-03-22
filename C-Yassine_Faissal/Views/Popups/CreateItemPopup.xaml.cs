using System.Windows.Controls;
using System.Windows;
using C_Yassine_Faissal.ViewModels.Popups;


namespace C_Yassine_Faissal.Views.Popups
{
    public partial class CreateItemPopup : UserControl
    {
        public CreateItemPopup()
        {
            InitializeComponent();
            DataContext = new CreateItemPopupViewModel();
        }

        private void CreateItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Your implementation here
        }
    }
}