using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels.Popups;
using System;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class UpdateItemPopup : Window
    {
        // View model voor UpdateItemPopup
        private readonly UpdateItemPopupViewModel _viewModel;

        // Het geselecteerde item
        private Item _selectedItem;

        // Constructor voor UpdateItemPopup view
        public UpdateItemPopup(LibraryContext libraryContext)
        {
            InitializeComponent();

            // Instantieer het view model
            _viewModel = new UpdateItemPopupViewModel(libraryContext);

            // Wijs het view model toe aan de DataContext
            DataContext = _viewModel;
        }

        // Zoek button event handler
        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Zoek het item
            _selectedItem = _viewModel.FindItemByTitle(SearchTitleTextBox.Text);

            // Als het item is gevonden, vul de velden in
            if (_selectedItem != null)
            {
                TitleTextBox.Text = _selectedItem.Title;
                AuthorComboBox.SelectedItem = _selectedItem.Author;
                DescriptionTextBox.Text = _selectedItem.Description;
                ItemTypeComboBox.SelectedItem = _selectedItem.ItemType;
                ItemStatusComboBox.SelectedItem = _selectedItem.ItemStatus;
            }

            // Als het item niet is gevonden, toon een foutmelding
            else
            {
                MessageBox.Show("Item not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Opslaan button event handler
        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Als er geen item is geselecteerd, toon een foutmelding en stop de functie
            if (_selectedItem == null)
            {
                MessageBox.Show("Please search for an item before updating.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update de velden van het geselecteerde item
            _selectedItem.Title = TitleTextBox.Text;
            _selectedItem.Author = (Author)AuthorComboBox.SelectedItem;
            _selectedItem.Description = DescriptionTextBox.Text;
            _selectedItem.ItemType = (ItemType)ItemTypeComboBox.SelectedItem;
            _selectedItem.ItemStatus = (ItemStatus)ItemStatusComboBox.SelectedItem;

            // Probeer het item te updaten en toon een melding bij succes of een foutmelding bij een fout
            try
            {
                await _viewModel.UpdateItemAsync(_selectedItem);
                MessageBox.Show("Item updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating item: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
