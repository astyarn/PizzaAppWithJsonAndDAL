using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.Ting
{
    internal abstract class Ingrediens
    {
        public int Id { get; set; }
        public string Navn { get; set; }
        public double Pris { get; set; }

    }
}
