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
        private readonly UpdateItemPopupViewModel _viewModel;
        private Item _selectedItem;

        public DeleteItemPopup(UpdateItemPopupViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string title = SearchTitleTextBox.Text;
            if (!string.IsNullOrEmpty(title))
            {
                _selectedItem = _viewModel.FindItemByTitle(title);
                if (_selectedItem != null)
                {
                    ItemDetailsTextBlock.Text = $"Title: {_selectedItem.Title}\nAuthor: {_selectedItem.Author.Name}";
                }
                else
                {
                    MessageBox.Show("Item not found.");
                }
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
            {
                await _viewModel.DeleteItemAsync(_selectedItem);
                MessageBox.Show("Item deleted successfully.");
                Close();
            }
            else
            {
                MessageBox.Show("Please search for an item to delete.");
            }
        }
    }
}
