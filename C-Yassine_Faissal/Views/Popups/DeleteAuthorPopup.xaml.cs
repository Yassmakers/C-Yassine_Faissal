using C_Yassine_Faissal.ViewModels;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class DeleteAuthorPopup : Window
    {
        public DeleteAuthorPopup(DeleteAuthorPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}