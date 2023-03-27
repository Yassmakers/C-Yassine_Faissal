using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using C_Yassine_Faissal.ViewModels;

namespace C_Yassine_Faissal.Views
{
    // De ItemsView-klasse is verantwoordelijk voor het tonen van de Items in de user interface.
    public partial class ItemsView : UserControl
    {
        private ItemsViewModel _ItemsViewModel;

    // De constructor voor de ItemsView neemt een ItemsViewModel object als parameter.
    // Het initialiseert de ItemsViewModel en wijst de DataContext van de view toe aan het ItemsViewModel.
        public ItemsView(ItemsViewModel itemsViewModel)
        {
            InitializeComponent();
            _ItemsViewModel = itemsViewModel;
            DataContext = itemsViewModel;
        }

        // De FilterButton_Click-methode wordt aangeroepen wanneer de gebruiker op de FilterButton klikt.
        // Deze methode roept de ReloadItems-methode aan.
        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadItems();
        }

        // De RefreshButton_Click-methode wordt aangeroepen wanneer de gebruiker op de RefreshButton klikt.
        // Deze methode roept de ReloadItems-methode aan.
        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadItems();
        }

        // De ReloadItems-methode wordt aangeroepen wanneer de gebruiker op de FilterButton of RefreshButton klikt.
        // Deze methode roept de LoadItems-methode van de ItemsViewModel aan om de items te herladen.
        public void ReloadItems()
        {
            _ItemsViewModel.LoadItems();
        }
    }
}
