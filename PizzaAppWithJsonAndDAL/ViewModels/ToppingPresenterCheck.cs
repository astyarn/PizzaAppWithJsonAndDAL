using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.ViewModels
{
    internal class ToppingPresenterCheck : IngredientPresenter
    {
        public bool Checked { get; set; }
        
        public ToppingPresenterCheck(int iId, string iText) : base(iId, iText)
        {
            menuID = iId;
            menuText = iText;
            Checked = false;
        }
    }
}
