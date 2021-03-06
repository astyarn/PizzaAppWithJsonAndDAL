using PizzaAppWithJsonAndDAL.DAL;
using PizzaAppWithJsonAndDAL.Ting;
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
        ViewModelCustom vm;
        public bool PizzaWasCustomized { get; set; }

        internal Pizza CustomizedPizza { get; set; }

        internal CustomPizzaWindow(int SelectedPizzaId, VarePresenter iSize)
        {
            InitializeComponent();
            PizzaWasCustomized = false;     
            vm = new ViewModelCustom(SelectedPizzaId, iSize);

            DataContext = vm;
        }

        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            vm.CheckToppingLimit(cb);           //check if user tries to add a 5th topping to the pizza
        }

        private void CheckBoxZone_UnChecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            vm.CheckToppingLimit(cb);           //check if user tries to add a 5th topping to the pizza
        }

        private void LægCustomizedPizzaIKurv(object sender, RoutedEventArgs e)
        {
            CustomizedPizza = vm.SaveCustomizationAndPushPizzaToCart(); //Saves the changes made onto a Pizza and amkes it available for the Kurv
            PizzaWasCustomized = true;
            Close();
        }

        private void Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.OpdaterTekstPåComboboxVedSizeSkift();
        }
    }
}
