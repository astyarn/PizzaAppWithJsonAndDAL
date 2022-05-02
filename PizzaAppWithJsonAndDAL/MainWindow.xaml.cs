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
using PizzaAppWithJsonAndDAL.Ting;
using PizzaAppWithJsonAndDAL.DAL;
using PizzaAppWithJsonAndDAL.ViewModels;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using System.IO;

namespace PizzaAppWithJsonAndDAL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ViewModelMain vm;
        public MainWindow()
        {
            InitializeComponent();
            
            vm = new ViewModelMain();
            DataContext = vm;
        }

        private void TilføjVareTilVarekurv(object sender, RoutedEventArgs e)
        {
            //Add a Vare selected from menu to the cart/kurv and displays it
            vm.LægVareIKurvFraMenu();
        }

        private void FjernVareFraVarekurv(object sender, RoutedEventArgs e)
        {
            //Removes a selected Vare from the cart/kurv list
            vm.FjernVareFraKurvMedVareKurvId();
        }

        private void CustomizeSelctedPizza(object sender, RoutedEventArgs e)
        {
            //Open CustomPizzawindow and send the current selected PizzaID to it!
            if (vm.IsItAPizza(vm.GetSelectedVareId()))
            {
                CustomPizzaWindow customPizzaWindow = new CustomPizzaWindow(vm.GetSelectedVareId(), vm.MainSizeSelection);

                customPizzaWindow.ShowDialog();

                //Check if the pizza was customized and if there actually is an existing pizza
                if (customPizzaWindow.PizzaWasCustomized && customPizzaWindow.CustomizedPizza != null)
                {
                    //Put customized pizza into the Kurv
                    Pizza test = new Pizza();
                    test = customPizzaWindow.CustomizedPizza;
                    vm.LægPizzaIKurvFraCustomizering(test);
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Changes size property of all Varer
            vm.ChangeVareSize();
        }
    }
}
