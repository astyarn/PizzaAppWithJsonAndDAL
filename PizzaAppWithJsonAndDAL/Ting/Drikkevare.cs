using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.Ting
{
    internal class Drikkevare : Varer
    {
        public double OriginalPris { get; set; }
        private double normalPris, storPris;
        public Drikkevare()
        {

        }
        public Drikkevare(int iId, string iNavn, double iPris, size iSize)
        {
            Id = iId;
            Navn = iNavn;
            OriginalPris = iPris;
            Size = iSize;

            BeregnPris();
        }

        public void BeregnPris()
        {
            normalPris = OriginalPris * (int)size.Normal;
            storPris = OriginalPris * (int)size.Stor;
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
