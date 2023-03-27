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

            // Maak een nieuw viewmodel aan voor de CreateAuthorPopup en stel de DataContext in
            _viewModel = new CreateAuthorPopupViewModel(libraryContext);
            DataContext = _viewModel;
        }
    }
}
