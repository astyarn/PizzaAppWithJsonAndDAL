using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.ViewModels
{
    internal class PizzaPresenter
    {
        public string menuText { get; set; }
        public int menuID { get; set; }

        public PizzaPresenter(int iId, string iText)
        {
            menuID = iId;
            menuText = iText;
        }
    }
}
