﻿using PizzaAppWithJsonAndDAL.Ting;
using PizzaAppWithJsonAndDAL.ViewModels;
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

namespace PizzaAppWithJsonAndDAL
{
    /// <summary>
    /// Interaction logic for CustomPizzaWindow.xaml
    /// </summary>
    public partial class CustomPizzaWindow : Window
    {
        public CustomPizzaWindow()
        {
            InitializeComponent();
            ViewModelCustom vm = new ViewModelCustom();
            DataContext = vm;
        }

        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBoxZone_UnChecked(object sender, RoutedEventArgs e)
        {

        }
    }
}
