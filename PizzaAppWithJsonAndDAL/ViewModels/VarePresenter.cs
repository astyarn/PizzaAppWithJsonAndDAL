using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.ViewModels
{
    internal class VarePresenter
    {
        public string menuText { get; set; }
        public int menuID { get; set; }

        public VarePresenter(int iId, string iText)
        {
            menuID = iId;
            menuText = iText;
        }
    }
}
