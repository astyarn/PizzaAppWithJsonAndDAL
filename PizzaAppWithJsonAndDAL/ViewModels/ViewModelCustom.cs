using PizzaAppWithJsonAndDAL.Ting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaAppWithJsonAndDAL.ViewModels
{

    internal class ViewModelCustom : INotifyPropertyChanged
    {
        DAL.VarerDAL dal;
        ObservableCollection<Topping> ToppingListe;
        ObservableCollection<Bund> BundListe;
        ObservableCollection<Sovs> SovsListe;
        ObservableCollection<Ost> OstListe;

        Pizza pizzaToCustomize;
        public ViewModelCustom(int pizzaIdToCustomize)
        {
            dal = new DAL.VarerDAL();
            ToppingListe = dal.GetAllToppings();
            BundListe = dal.GetAllBunde();
            SovsListe = dal.GetAllSovse();
            OstListe = dal.GetAllOste();

            ToppingTilSelectionMenu();
            BundTilSelectionBox();
            SovsTilSelectionBox();
            OstTilSelectionBox();

            pizzaToCustomize = dal.GetPizzaById(pizzaIdToCustomize);


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
        void ToppingTilSelectionMenu()
        {
            ObservableCollection<IngredientPresenter> temp = new ObservableCollection<IngredientPresenter>();
            
            foreach (Topping topping in ToppingListe)
            {
                string s = $"{topping.Navn} ({topping.Pris}).";

                temp.Add(new IngredientPresenter(topping.Id, s));
            }
            TextListeMedToppings = temp;
        }

        void BundTilSelectionBox()
        {
            ObservableCollection<IngredientPresenter> temp = new ObservableCollection<IngredientPresenter>();

            foreach (Bund b in BundListe)
            {
                string s = $"{b.Navn} ({b.Pris}).";

                temp.Add(new IngredientPresenter(b.Id, s));
            }
            TextListeMedBunde = temp;
        }
        void SovsTilSelectionBox()
        {
            ObservableCollection<IngredientPresenter> temp = new ObservableCollection<IngredientPresenter>();

            foreach (Sovs svs in SovsListe)
            {
                string s = $"{svs.Navn} ({svs.Pris}).";

                temp.Add(new IngredientPresenter(svs.Id, s));
            }
            TextListeMedSovs = temp;

        }
        void OstTilSelectionBox()
        {
            ObservableCollection<IngredientPresenter> temp = new ObservableCollection<IngredientPresenter>();

            foreach (Ost o in OstListe)
            {
                string s = $"{o.Navn} ({o.Pris}).";

                temp.Add(new IngredientPresenter(o.Id, s));
            }
            TextListeMedOst = temp;
        }

        private ObservableCollection<IngredientPresenter> _listeMedToppings;
        public ObservableCollection<IngredientPresenter> TextListeMedToppings
        {
            get { return _listeMedToppings; }
            set
            {
                _listeMedToppings = value;
                OnPropertyChanged(nameof(TextListeMedToppings));
            }
        }
        private ObservableCollection<IngredientPresenter> _textListeMedBunde;
        public ObservableCollection<IngredientPresenter> TextListeMedBunde
        {
            get { return _textListeMedBunde; }
            set { 
                _textListeMedBunde = value;
                OnPropertyChanged(nameof(TextListeMedBunde));
            }
        }
        private ObservableCollection<IngredientPresenter> _textListeMedSovs;
        public ObservableCollection<IngredientPresenter> TextListeMedSovs
        {
            get { return _textListeMedSovs; }
            set
            {
                _textListeMedSovs = value;
                OnPropertyChanged(nameof(TextListeMedSovs));
            }
        }
        private ObservableCollection<IngredientPresenter> _textListeMedOst;
        public ObservableCollection<IngredientPresenter> TextListeMedOst
        {
            get { return _textListeMedOst; }
            set
            {
                _textListeMedOst = value;
                OnPropertyChanged(nameof(TextListeMedOst));
            }
        }

    }
}
