using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.Ting
{
    internal class Sovs : Ingrediens
    {
        public Sovs(int iId, string iNavn, double iPris)
        {
            Id = iId;
            Navn = iNavn;
            Pris = iPris;
        }
    }
}
