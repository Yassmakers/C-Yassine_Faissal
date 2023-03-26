using C_Yassine_Faissal.ViewModels.Popups;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class UpdateAuthorPopup : Window
    {
        public UpdateAuthorPopup(UpdateAuthorPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
