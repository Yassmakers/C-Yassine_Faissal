using C_Yassine_Faissal.ViewModels.Popups;
using System.Windows.Controls;
using System.Windows;


namespace C_Yassine_Faissal.Views.Popups
{
    public partial class DeleteBookPopup : UserControl
    {
        public DeleteBookPopup()
        {
            InitializeComponent();
            DataContext = new DeleteBookPopupViewModel();
        }

        private void DeleteBookButton_Click(object sender, RoutedEventArgs e)
        {
            // Your implementation here
        }
    }
}
