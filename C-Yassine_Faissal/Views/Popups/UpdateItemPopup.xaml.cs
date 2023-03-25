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
using C_Yassine_Faissal.Models;
using C_Yassine_Faissal.ViewModels.Popups;

namespace C_Yassine_Faissal.Views.Popups
{
    public partial class UpdateItemPopup : Window
    {
        private readonly UpdateItemPopupViewModel _viewModel;
        private Item _selectedItem;

        public UpdateItemPopup(UpdateItemPopupViewModel viewModel)
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
                    TitleTextBox.Text = _selectedItem.Title;
                    AuthorTextBox.Text = _selectedItem.Author.Name;
                }
                else
                {
                    MessageBox.Show("Item not found.");
                }
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItem != null)
            {
                _selectedItem.Title = TitleTextBox.Text;
                _selectedItem.Author.Name = AuthorTextBox.Text;

                await _viewModel.UpdateItemAsync(_selectedItem);
                MessageBox.Show("Item updated successfully.");
                Close();
            }
            else
            {
                MessageBox.Show("Please search for an item to update.");
            }
        }
    }
}


