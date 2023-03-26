using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.ViewModels.Popups;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class CreateAuthorPopup : Window
    {
        private readonly CreateAuthorPopupViewModel _viewModel;

        public CreateAuthorPopup(LibraryContext libraryContext)
        {
            InitializeComponent();
            _viewModel = new CreateAuthorPopupViewModel(libraryContext);
            DataContext = _viewModel;
        }
    }
}
