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
                IsThereADiscount = true;
            }
            else
            {
                IsThereADiscount= false;
            }
        }

        private void FindTheCheapestPizza()
        {
            List<Varer> cheapPizzaList = Inventar.Where(vare => vare is Pizza).ToList();

            //Find the cheapest price
            double cheapestPizzaPrice = Inventar.Min(vare => vare.Pris && vare is Pizza);

            //Determine the Pizza's with the cheapest price
            
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
