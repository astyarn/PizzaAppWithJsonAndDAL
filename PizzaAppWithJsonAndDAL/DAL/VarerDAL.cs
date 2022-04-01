using Newtonsoft.Json;
using PizzaAppWithJsonAndDAL.Ting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.DAL
{
    internal class VarerDAL
    {
        //ObservableCollection<Pizza> PizzaMenu = new ObservableCollection<Pizza>();
        public ObservableCollection<Pizza> PizzaMenu { get; set; }
        ObservableCollection<Topping> ToppingListe = new ObservableCollection<Topping>();
        ObservableCollection<Bund> BundListe = new ObservableCollection<Bund>();
        ObservableCollection<Sovs> SovsListe = new ObservableCollection<Sovs>();
        ObservableCollection<Ost> OstListe = new ObservableCollection<Ost>();


        internal VarerDAL()
        {
            LoadDataFromJson();
        }

        private void LoadDataFromJson()
        {
            //Insert Code that loads Varer Data from a json file
            var json1 = File.ReadAllText("Json/bunde.json");
            var json2 = File.ReadAllText("Json/oste.json");
            
            var json4 = File.ReadAllText("Json/sovse.json");
            var json5 = File.ReadAllText("Json/toppings.json");

            var json3 = File.ReadAllText("Json/pizza.json");
            BundListe = JsonConvert.DeserializeObject<ObservableCollection<Bund>>(json1);
            OstListe = JsonConvert.DeserializeObject<ObservableCollection<Ost>>(json2);
            
            SovsListe = JsonConvert.DeserializeObject<ObservableCollection<Sovs>>(json4);
            ToppingListe = JsonConvert.DeserializeObject<ObservableCollection<Topping>>(json5);

            PizzaMenu = JsonConvert.DeserializeObject<ObservableCollection<Pizza>>(json3);
            //Debug.WriteLine($"Der var {PizzaMenu.Count} pizzaer");
        }

        private void SaveDataToJson()
        {
            //Save data to Json file
            var data10 = JsonConvert.SerializeObject(this);
            File.WriteAllText("PizzariaDAL.json", data10);
        }

        //More functionality to come ...

        public ObservableCollection<ViewModels.VarePresenter> FåPizzaBeskrivelseOgId()
        {
            ObservableCollection<ViewModels.VarePresenter> temp =  new ObservableCollection<ViewModels.VarePresenter>();
            foreach (Pizza item in PizzaMenu)
            {
                string s = $"{item.Navn} ({item.Pris}) indeholder: " +
                           $"{item.Bund.Navn}, {item.Sovs.Navn}, {item.Ost.Navn}, ";
                foreach (Ting.Topping top in item.PizzaTopping)
                {
                    s += top.Navn + ", ";
                }
                temp.Add(new ViewModels.VarePresenter(item.Id, s));
            }
            return temp;
        }

        public ObservableCollection<Pizza> GetAllPizza()
        {
            return PizzaMenu;
        }
        public ObservableCollection<Topping> GetAllToppings()
        {
            return ToppingListe;
        }
        public ObservableCollection<Bund> GetAllBunde()
        {
            return BundListe;
        }
        public ObservableCollection<Sovs> GetAllSovse()
        {
            return SovsListe;
        }
        public ObservableCollection<Ost> GetAllOste()
        {
            return OstListe;
        }

        public Pizza GetPizzaById(int id)
        {
            Pizza pizzaToCopy = null;
            foreach (Pizza item in PizzaMenu)
            {
                if (item.Id == id)
                {
                    pizzaToCopy = item;
                }
            }
            if(pizzaToCopy == null)
            {
                return null;
            }

            Pizza nyPizza = new Pizza();

            nyPizza.Id = pizzaToCopy.Id;
            nyPizza.Navn = pizzaToCopy.Navn;
            nyPizza.Size = pizzaToCopy.Size;
            nyPizza.Bund = new Bund(pizzaToCopy.Bund.Id, pizzaToCopy.Bund.Navn, pizzaToCopy.Bund.Pris);
            nyPizza.Sovs = new Sovs(pizzaToCopy.Sovs.Id, pizzaToCopy.Sovs.Navn, pizzaToCopy.Sovs.Pris);
            nyPizza.Ost = new Ost(pizzaToCopy.Ost.Id, pizzaToCopy.Ost.Navn, pizzaToCopy.Ost.Pris);
            nyPizza.PizzaTopping = new ObservableCollection<Topping>();
            foreach (Topping top in pizzaToCopy.PizzaTopping)
            {
                nyPizza.PizzaTopping.Add(new Topping(top.Id, top.Navn, top.Pris));
            }

            nyPizza.Pris = pizzaToCopy.Pris;    //Evt erstattes af beregnPris metode fra Pizza

            return nyPizza;
        }
    }
}
