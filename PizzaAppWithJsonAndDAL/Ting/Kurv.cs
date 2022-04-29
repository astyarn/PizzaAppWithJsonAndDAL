using PizzaAppWithJsonAndDAL.DAL;
using PizzaAppWithJsonAndDAL.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.Ting
{
    
    internal class Kurv
    {
        public ObservableCollection<Varer> Inventar { get; set; }
        public bool IsThereADiscount { get; set; }

        public int KurvIDofDiscountedPizza { get; set; }

        VarerDAL dal;
        int _tællerVareKurvId;      //Bruges til at give objecterne i varekurven et unikt ID at tage fat i.
        public Kurv()
        {
            Inventar = new ObservableCollection<Varer>();
            _tællerVareKurvId = 0;
            dal = new VarerDAL();
            IsThereADiscount = false;
        }

        public void TilføjVare(Varer input)
        {
            //Inventar.Add(dal.GetPizzaById(id));
            input.VareKurvId = _tællerVareKurvId;
            Inventar.Add(input);
            //OpdaterVareKurvTekst();
            _tællerVareKurvId++;

            //Count Pizza and Drikkevarer, give discount if applicable
            CountVarerAndDetermineDiscount();
        }

        public void FjernVare(int VareKurvId)
        {
            foreach (Varer input in Inventar)
            {
                if (input.VareKurvId == VareKurvId)
                {
                    Inventar.Remove(input);
                    break;
                }
            }
            //also count Pizza and Drikkevarer here, remove discount if necessary
            CountVarerAndDetermineDiscount();
        }

        private void CountVarerAndDetermineDiscount()
        {
            int pizzaCount = Inventar.Where(vare => vare != null && vare is Pizza).Count();
            int drikkeCount = Inventar.Where(vare => vare != null && vare is Drikkevare).Count();
            Debug.WriteLine($"Der var {pizzaCount} pizzaer \n");
            Debug.WriteLine($"Der var {drikkeCount} Drikkevarer \n");

            if(pizzaCount >= 2 && drikkeCount >= 2)
            {
                
                ResetPizzaDiscountPrice(KurvIDofDiscountedPizza);
                IsThereADiscount = true;
                FindTheCheapestPizza();
            }
            else
            {
                if (Inventar.Count() > 0 && pizzaCount > 0)
                {
                    ResetPizzaDiscountPrice(KurvIDofDiscountedPizza);
                }
                IsThereADiscount = false;
            }
        }

        private void FindTheCheapestPizza()
        {
            List<Varer> PizzaList = Inventar.Where(vare => vare is Pizza).ToList();

            //Find the cheapest price
            double cheapestPizzaPrice = PizzaList.Min(vare => vare.Pris);

            //Determine the Pizzas with the cheapest price. Select the first with the cheapest Bund price
            List<Varer> cheapPizzaList = PizzaList.Where(pizza => pizza.Pris == cheapestPizzaPrice).ToList();

            double cheapestBundPrice = cheapPizzaList.Min(pizza => (pizza as Pizza).Bund.Pris);

            Varer cheapestPizza = cheapPizzaList.Where(pizza => (pizza as Pizza).Bund.Pris == cheapestBundPrice).First();
            Debug.WriteLine($"Den valgte billigste pizza med billigste bund er: {cheapestPizza.Navn} på plads nr. {Inventar.IndexOf(cheapestPizza)} \n");
            (cheapestPizza as Pizza).CalculateDiscountPrice();
            KurvIDofDiscountedPizza = cheapestPizza.VareKurvId;
        }
        private void ResetPizzaDiscountPrice(int iKurvID)
        {
            //Try to find the pizza object that had a discount applied
            var p = (Inventar.Where(pizza => pizza.VareKurvId == KurvIDofDiscountedPizza).FirstOrDefault() as Pizza);

            //Tjek if the pizza that had discount stil existed in the collection
            if (p!=null)
            {
                p.ResetPriceFromDiscount(); //Reset discount on the pizza
            }
                
        }

        public ObservableCollection<VarePresenter> OpdaterVareKurvTekst()
        {
            ObservableCollection<VarePresenter> VareBeskrivelser = new ObservableCollection<VarePresenter>();
            foreach (Varer item in Inventar)
            {
                if (item is Pizza)
                {
                    string s = $"{item.Navn} ({item.Size}) {item.Pris} kr. indeholder: " +
                               $"{(item as Pizza).Bund.Navn}, {(item as Pizza).Sovs.Navn}, {(item as Pizza).Ost.Navn}, ";
                    foreach (Ting.Topping top in (item as Pizza).PizzaTopping)
                    {
                        s += top.Navn + ", ";
                    }
                    VareBeskrivelser.Add(new VarePresenter(item.VareKurvId, s));
                }
                if (item is Drikkevare)
                {
                    string t = $"{item.Navn} ({item.Size}) {item.Pris} kr.";
                    VareBeskrivelser.Add(new ViewModels.VarePresenter(item.VareKurvId, t));
                }
            }
            return VareBeskrivelser;
        }

        public double UdregnKurvSamletPris()
        {
            //ADD CHECK for price reduction
            double samletPris = 0;
            foreach (Varer item in Inventar)
            {
                samletPris += item.Pris;
            }
            return samletPris;
        }

    }
}
