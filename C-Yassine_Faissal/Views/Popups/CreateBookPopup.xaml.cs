using System.Windows.Controls;
using System.Windows;
using C_Yassine_Faissal.ViewModels.Popups;


namespace C_Yassine_Faissal.Views.Popups
{
    public partial class CreateBookPopup : UserControl
    {
        public CreateBookPopup()
        {
            InitializeComponent();
            DataContext = new CreateBookPopupViewModel();
        }

        private void CreateBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Your implementation here
        }
    }
}