using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels.Popups;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static C_Yassine_Faissal.Commands.RelayCommand;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class DeleteItemPopup : Window
    {
        private readonly UpdateItemPopupViewModel _viewModel; // Viewmodel voor het bijhouden van de data en logica van het DeleteItemPopup venster
        private Item _selectedItem; // Het geselecteerde Item dat zal worden verwijderd

        public DeleteItemPopup(UpdateItemPopupViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string title = SearchTitleTextBox.Text; // Titel van het te zoeken Item
            if (!string.IsNullOrEmpty(title))
            {
                _selectedItem = _viewModel.FindItemByTitle(title); // Zoek het Item in de database via het viewmodel
                if (_selectedItem != null)
                {
                    ItemDetailsTextBlock.Text = $"Title: {_selectedItem.Title}\nAuthor: {_selectedItem.Author.Name}"; // Laat de details van het gevonden Item zien
                }
                else
                {
                    MessageBox.Show("Item not found."); // Toon melding als het Item niet gevonden is
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null) // Controleer of er een Item geselecteerd is om te verwijderen
            {
                await _viewModel.DeleteItemAsync(_selectedItem); // Verwijder het Item asynchroon via het viewmodel
                MessageBox.Show("Item deleted successfully."); // Toon melding dat het Item succesvol verwijderd is
                Close(); // Sluit het DeleteItemPopup venster
            }
            else
            {
                MessageBox.Show("Please search for an item to delete."); // Toon melding als er geen Item geselecteerd is om te verwijderen
            }
        }
    }
}
