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
            PizzaWasCustomized = true;
            vm = new ViewModelCustom(SelectedPizzaId, iSize);

            DataContext = vm;
        }

        private void CheckBoxZone_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;

            if(vm.ToppingTæller < 4)
            {
                vm.ToppingTæller++;
                vm.UpdateAntalToppingsLabel();
                
            }
            else
            {
                cb.IsChecked = false;
            }

        }

        private void CheckBoxZone_UnChecked(object sender, RoutedEventArgs e)
        {
            if (vm.ToppingTæller > 0)
            {
                vm.ToppingTæller--;
                vm.UpdateAntalToppingsLabel();
            }
            else
            {
            }
        }

        private void LægCustomizedPizzaIKurv(object sender, RoutedEventArgs e)
        {
            vm.SkiftBundPaaPizza();
            vm.SkiftSovsPaaPizza();
            vm.SkiftOstPaaPizza();
            vm.SkiftToppingPaaPizza();
            vm.SætSizePåPizza();            //sætter kun pris på pizza i kurv, opdaterer ikke i customWindow
            vm.pizzaToCustomize.BeregnPris();
            vm.pizzaToCustomize.Navn = $"*{vm.pizzaToCustomize.Navn}*";
            CustomizedPizza = vm.pizzaToCustomize;
            Close();
        }

        private void Size_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.OpdaterTekstPåComboboxVedSizeSkift();

        }
    }
}
