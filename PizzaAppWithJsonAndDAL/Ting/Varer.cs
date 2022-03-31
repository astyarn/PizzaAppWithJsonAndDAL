using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.Ting
{
    internal abstract class Varer
    {
        public int Id { get; set; }

        public int VareKurvId { get; set; }

        public string Navn { get; set; }
        public double Pris { get; set; }
        public enum size
        {
            Normal = 1,
            Stor = 2
        }
        public size Size { get; set; }
    }
}
