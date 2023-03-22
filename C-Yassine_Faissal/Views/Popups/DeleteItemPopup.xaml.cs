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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace C_Yassine_Faissal.Views.Popups
{
    /// <summary>
    /// Interaction logic for DeleteUserPopup.xaml
    /// </summary>
    public partial class DeleteItemPopup : UserControl
    {
        public DeleteItemPopup()
        {
            InitializeComponent();
            DataContext = new DeleteItemPopupViewModel(); // Update the DataContext assignment
        }

        private void DeleteItemButton_Click(object sender, RoutedEventArgs e)
        {
            // Your implementation here
        }
    }

}