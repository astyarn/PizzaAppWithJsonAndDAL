using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.Ting
{
    internal class Drikkevare : Varer
    {
        Drikkevare(int iId, string iNavn, double iPris, size iSize)
        {
            Id = iId;
            Navn = iNavn;
            Pris = iPris;
            Size = iSize;
        }
    }
}
