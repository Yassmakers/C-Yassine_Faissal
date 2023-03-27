using C_Yassine_Faissal.Data;
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels.Popups;
using System;
using System.Windows;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class UpdateItemPopup : Window
    {
        private readonly UpdateItemPopupViewModel _viewModel;
        private Item _selectedItem;

        public UpdateItemPopup(LibraryContext libraryContext)
        {
            InitializeComponent();
            _viewModel = new UpdateItemPopupViewModel(libraryContext);
            DataContext = _viewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            _selectedItem = _viewModel.FindItemByTitle(SearchTitleTextBox.Text);

            if (_selectedItem != null)
            {
                TitleTextBox.Text = _selectedItem.Title;
                AuthorComboBox.SelectedItem = _selectedItem.Author;
                DescriptionTextBox.Text = _selectedItem.Description;
                ItemTypeComboBox.SelectedItem = _selectedItem.ItemType;
                ItemStatusComboBox.SelectedItem = _selectedItem.ItemStatus;
            }
            else
            {
                MessageBox.Show("Item not found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem == null)
            {
                MessageBox.Show("Please search for an item before updating.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _selectedItem.Title = TitleTextBox.Text;
            _selectedItem.Author = (Author)AuthorComboBox.SelectedItem;
            _selectedItem.Description = DescriptionTextBox.Text;
            _selectedItem.ItemType = (ItemType)ItemTypeComboBox.SelectedItem;
            _selectedItem.ItemStatus = (ItemStatus)ItemStatusComboBox.SelectedItem;

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
