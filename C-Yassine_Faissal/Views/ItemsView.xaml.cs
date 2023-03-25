using System.Windows;
using System.Windows.Controls;
using C_Yassine_Faissal.ViewModels;

namespace C_Yassine_Faissal.Views
{
    public partial class ItemsView : UserControl
    {
        private ItemsViewModel _ItemsViewModel;

        public ItemsView(ItemsViewModel itemsViewModel)
        {
            InitializeComponent();
            _ItemsViewModel = itemsViewModel;
            DataContext = itemsViewModel;
        }


        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            ReloadItems();
        }
        public void ReloadItems()
        {
            _ItemsViewModel.LoadItems();
        }
    }
}
