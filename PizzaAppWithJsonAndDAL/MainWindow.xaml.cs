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
        /*
        ObservableCollection<Bund> ocbund = new ObservableCollection<Bund>();
        ObservableCollection<Sovs> ocsovs = new ObservableCollection<Sovs>();
        ObservableCollection<Ost> ocost = new ObservableCollection<Ost>();
        ObservableCollection<Topping> octop = new ObservableCollection<Topping>();
        ObservableCollection<Pizza> ocPizza = new ObservableCollection<Pizza>();
        */

        

        ViewModelMain vm;
        public MainWindow()
        {
            InitializeComponent();
            
            vm = new ViewModelMain();
            DataContext = vm;

            /*
            Bund lysbund = new Bund(0, "Lys bund", 10);
            Bund grovbund = new Bund(1, "Grov bund", 15);
            ocbund.Add(lysbund);
            ocbund.Add(grovbund);

            Sovs tomatsovs = new Sovs(0, "Tomatsovs", 10);
            Sovs kødsovs = new Sovs(1, "Kødsovs", 15);
            ocsovs.Add(tomatsovs);
            ocsovs.Add(kødsovs);

            Ost mozzarella = new Ost(0, "Mozzarella", 10);
            Ost cheddar = new Ost(1, "Cheddar", 15);
            ocost.Add(mozzarella);
            ocost.Add(cheddar);

            Topping skinke = new Topping(0, "Skinke", 5);
            Topping kebab = new Topping(1, "Kebab", 6);
            Topping bacon = new Topping(2, "Bacon", 7);
            Topping salat = new Topping(3, "Salat", 10);
            Topping pepperoni = new Topping(4, "Pepperoni", 5.5);
            octop.Add(skinke);
            octop.Add(kebab);
            octop.Add(bacon);
            octop.Add(salat);
            octop.Add(pepperoni);

            Pizza margherita = new Pizza(0, "Margherita", Varer.size.Normal, lysbund, tomatsovs, mozzarella);
            Pizza salatpizza = new Pizza(1, "Salatpizza", Varer.size.Normal, grovbund, tomatsovs, cheddar, salat, kebab);
            Pizza kødpizza = new Pizza(2, "Kødpizza", Varer.size.Normal, lysbund, kødsovs, mozzarella, bacon, skinke, pepperoni);
            ocPizza.Add(margherita);
            ocPizza.Add(salatpizza);
            ocPizza.Add(kødpizza);

            var data1 = JsonConvert.SerializeObject(ocbund);
            File.WriteAllText("bunde.json", data1);

            var data2 = JsonConvert.SerializeObject(ocsovs);
            File.WriteAllText("sovse.json", data2);

            var data3 = JsonConvert.SerializeObject(ocost);
            File.WriteAllText("oste.json", data3);

            var data4 = JsonConvert.SerializeObject(octop);
            File.WriteAllText("toppings.json", data4);

            var data5 = JsonConvert.SerializeObject(ocPizza);
            File.WriteAllText("pizza.json", data5);
            */
        }

        private void TilføjVareTilVarekurv(object sender, RoutedEventArgs e)
        {
            vm.LægVareIKurvFraMenu();   
        }

        private void FjernVareFraVarekurv(object sender, RoutedEventArgs e)
        {
            vm.FjernVareFraKurvMedVareKurvId();
        }

        private void CustomizeSelctedPizza(object sender, RoutedEventArgs e)
        {
            //Open CustomPizzawindow and send the current selected Pizza to it!
            //Do this with ID of selected pizza
            //Just push an int to the new window
            CustomPizzaWindow customPizzaWindow = new CustomPizzaWindow(vm.GetSelectedPizzaId(), vm.MainSizeSelection);
            customPizzaWindow.ShowDialog();
            if(customPizzaWindow.PizzaWasCustomized && customPizzaWindow.CustomizedPizza != null)
            {
                //Put customized pizza i Kurv
                Pizza test = new Pizza();
                test = customPizzaWindow.CustomizedPizza;
                vm.LægPizzaIKurvFraCustomizering(test);
            }

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            vm.ChangeVareSize();
        }
    }
}
