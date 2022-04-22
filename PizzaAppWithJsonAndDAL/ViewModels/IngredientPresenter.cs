using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.ViewModels
{
    internal class IngredientPresenter : INotifyPropertyChanged
    {
        private string _menuText;
        public string menuText 
        { 
            get
            {
                return _menuText;
            }
            set
            {
                _menuText = value;
                OnPropertyChanged(nameof(menuText));
            }
        }
        public int menuID { get; set; }

        public IngredientPresenter(int iId, string iText)
        {
            menuID = iId;
            menuText = iText;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string PropertyNavn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyNavn));
                //OnPropertyChanged("Beskrivelse");
            }
        }
    }
}
