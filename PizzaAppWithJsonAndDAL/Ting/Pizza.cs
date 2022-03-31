using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.Ting
{
    internal class Pizza : Varer
    {
        public Bund Bund { get; set; }
        public Sovs Sovs { get; set; }
        public Ost Ost { get; set; }
        public ObservableCollection<Topping> PizzaTopping { get; set; }

        double normalPris, storPris;

        public Pizza()
        {

        }

        public Pizza(int iId, string iNavn, size iSize, Bund iBund, Sovs iSovs, Ost iOst, params Topping[]? iToppings)
        {
            Id = iId;
            Navn = iNavn;

            Size = iSize;

            Bund = new Bund(iBund.Id, iBund.Navn, iBund.Pris);
            Sovs = new Sovs(iSovs.Id, iSovs.Navn, iSovs.Pris);
            Ost = new Ost(iOst.Id, iOst.Navn, iOst.Pris);
            if (iToppings != null)
            {
                PizzaTopping = new ObservableCollection<Topping>();
                foreach (Topping top in iToppings)
                {
                    PizzaTopping.Add(new Topping(top.Id, top.Navn, top.Pris));
                }
            }
            BeregnPris();
            /*if (iSize == size.Stor)
            {
                Pris = storPris;
            }
            else
            {
                Pris = normalPris;
            }*/
        }

        public void BeregnPris()
        {
            //Kode ... sæt 2 priser på bagrund af en pizzapris beregnet ud fra bund, sovs, ost og toppings
            double endeligPrisPåPizza = 0;
            endeligPrisPåPizza += Bund.Pris + Sovs.Pris + Ost.Pris;
            if (PizzaTopping != null)
            {
                foreach (Topping top in PizzaTopping)
                {
                    endeligPrisPåPizza += top.Pris;
                }
            }

            normalPris = endeligPrisPåPizza * (int)size.Normal;
            storPris = endeligPrisPåPizza * (int)size.Stor;
            if (Size == size.Stor)
            {
                Pris = storPris;
            }
            else
            {
                Pris = normalPris;
            }

        }

    }
}
