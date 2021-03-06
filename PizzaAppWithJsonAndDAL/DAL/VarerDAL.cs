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
        public ObservableCollection<Pizza> PizzaMenu { get; set; }
        public ObservableCollection<Drikkevare> DrikkevarerMenu { get; set; }

        ObservableCollection<Topping> ToppingListe = new ObservableCollection<Topping>();
        ObservableCollection<Bund> BundListe = new ObservableCollection<Bund>();
        ObservableCollection<Sovs> SovsListe = new ObservableCollection<Sovs>();
        ObservableCollection<Ost> OstListe = new ObservableCollection<Ost>();

        public ObservableCollection<Varer> AlleVarer { get; set; }


        internal VarerDAL()
        {
            LoadDataFromJson();
            AlleVarer = new ObservableCollection<Varer>();
            foreach (Pizza item in PizzaMenu)
            {
                //item.BeregnPris(); // <----- Tilføjet pga pris 0 ved første pizza
                AlleVarer.Add(item);
            }
            foreach (Drikkevare item in DrikkevarerMenu)
            {
                AlleVarer.Add(item);
            }
        }

        /// <summary>
        /// Loads data from 6 json files. The files: bunde, oste, sovse, toppings, pizza and drikkevarer(.json)
        /// contain all the information to populate menu list and allow creation of pizza's
        /// </summary>
        private void LoadDataFromJson()
        {
            var json1 = File.ReadAllText("Json/bunde.json");
            var json2 = File.ReadAllText("Json/oste.json");
            
            var json4 = File.ReadAllText("Json/sovse.json");
            var json5 = File.ReadAllText("Json/toppings.json");

            var json3 = File.ReadAllText("Json/pizza.json");
            var json6 = File.ReadAllText("Json/drikkevarer.json");
            BundListe = JsonConvert.DeserializeObject<ObservableCollection<Bund>>(json1);
            OstListe = JsonConvert.DeserializeObject<ObservableCollection<Ost>>(json2);
            
            SovsListe = JsonConvert.DeserializeObject<ObservableCollection<Sovs>>(json4);
            ToppingListe = JsonConvert.DeserializeObject<ObservableCollection<Topping>>(json5);

            PizzaMenu = JsonConvert.DeserializeObject<ObservableCollection<Pizza>>(json3);
            DrikkevarerMenu = JsonConvert.DeserializeObject<ObservableCollection<Drikkevare>>(json6);

        }

        private void SaveDataToJson()
        {
            //Save data to Json file
            var data10 = JsonConvert.SerializeObject(this);
            File.WriteAllText("PizzariaDAL.json", data10);
        }

        /// <summary>
        /// Creates a Collection of VarePresenter for all the Varer to be displayed in the menu list. 
        /// </summary>
        /// <returns>ObservableCollection of VarePresenters containing describtion and ID of each Vare</returns>
        public ObservableCollection<ViewModels.VarePresenter> FåPizzaBeskrivelseOgId()
        {
            ObservableCollection<ViewModels.VarePresenter> temp =  new ObservableCollection<ViewModels.VarePresenter>();

            //Goes through all Varer and creates a Varepresenter for them based on type (Pizza or Drikkevare)
            foreach (Varer item in AlleVarer)
            {
                if(item is Pizza)
                { 
                    string s = $"{item.Navn} ({item.Size}) {item.Pris} kr. indeholder: " +
                               $"{(item as Pizza).Bund.Navn}, {(item as Pizza).Sovs.Navn}, {(item as Pizza).Ost.Navn}, ";
                    foreach (Ting.Topping top in (item as Pizza).PizzaTopping)
                    {
                        s += top.Navn + ", ";
                    }
                    temp.Add(new ViewModels.VarePresenter(item.Id, s));
                }

                if (item is Drikkevare)
                {
                    string t = $"{item.Navn} ({item.Size}) {item.Pris} kr.";
                    temp.Add(new ViewModels.VarePresenter(item.Id, t));
                }
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

        /// <summary>
        /// Method to get a Varer from the DAL by using an ID
        /// </summary>
        /// <param name="id">Id of the Varer to copy out from the DAL</param>
        /// <returns>DeepCopy of the Varer identified with id value</returns>
        public Varer GetVareById(int id)
        {
            //Find out if the Id corresponds to a Varer in AlleVarer.
            Varer tempObj = null;
            foreach(Varer item in AlleVarer)
            {
                if(item.Id == id)
                {
                    tempObj = item;
                    break;
                }
            }
            //Determine if the found Varer object is a Pizza or Drikkevare type
            //and perform a deepcopy based on that.
            if (tempObj is Pizza)
            {
                Pizza pizzaToCopy = tempObj as Pizza;
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

                nyPizza.Pris = pizzaToCopy.Pris;
                nyPizza.NormalPris = pizzaToCopy.NormalPris;
                nyPizza.StorPris = pizzaToCopy.StorPris;

                return nyPizza;
            }
            else if (tempObj is Drikkevare)
            {
                //DeepCopy tempObj as a Drikkevare
                Drikkevare drikkevareToCopy = tempObj as Drikkevare;
                Drikkevare nyDrikkevare = new Drikkevare();
                nyDrikkevare.Id = drikkevareToCopy.Id;
                nyDrikkevare.Navn = drikkevareToCopy.Navn;
                nyDrikkevare.OriginalPris = drikkevareToCopy.OriginalPris;
                nyDrikkevare.Size = drikkevareToCopy.Size;
                nyDrikkevare.Pris = drikkevareToCopy.Pris;

                return nyDrikkevare;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Changes value of the size property of all the Varer. Changes size to be the input argument.
        /// </summary>
        /// <param name="iSize">Value the size property will be changed to</param>
        public void SkiftStørrelsePåPizza(Varer.size iSize)
        {
            foreach (Pizza piz in PizzaMenu)
            {
                piz.Size = iSize;
                piz.BeregnPris();
            }

            foreach (Drikkevare driva in DrikkevarerMenu)
            {
                driva.Size = iSize;
                driva.BeregnPris();
            }
        }
    }
}
