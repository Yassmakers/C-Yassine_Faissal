using C_Yassine_Faissal.ViewModels.Popups;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    // UpdateAuthorPopup is een dialoogvenster dat wordt gebruikt om een auteur bij te werken.
    public partial class UpdateAuthorPopup : Window
    {
        // Constructor die de UpdateAuthorPopupViewModel ontvangt als parameter en de DataContext van het dialoogvenster instelt op deze viewmodel.
        public UpdateAuthorPopup(UpdateAuthorPopupViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
