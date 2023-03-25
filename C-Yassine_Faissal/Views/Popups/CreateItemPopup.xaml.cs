﻿using C_Yassine_Faissal.Data;
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

namespace C_Yassine_Faissal.Views.Popups
{
    /// <summary>
    /// Interaction logic for CreateItemPopup.xaml
    /// </summary>
    public partial class CreateItemPopup : Window
    {
        public CreateItemPopup(LibraryContext libraryContext)
        {
            InitializeComponent();
            DataContext = new CreateItemPopupViewModel(libraryContext);
        }
    }
}
