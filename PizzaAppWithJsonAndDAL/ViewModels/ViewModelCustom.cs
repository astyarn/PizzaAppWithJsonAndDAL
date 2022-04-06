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

        private ObservableCollection<ToppingPresenterCheck> _listeMedToppings;
        public ObservableCollection<ToppingPresenterCheck> TextListeMedToppings
        {
            get { return _listeMedToppings; }
            set
            {
                _listeMedToppings = value;
                OnPropertyChanged(nameof(TextListeMedToppings));
            }
        }
        public int ToppingTæller { get; set; }

        private ObservableCollection<IngredientPresenter> _textListeMedBunde;
        public ObservableCollection<IngredientPresenter> TextListeMedBunde
        {
            get { return _textListeMedBunde; }
            set
            {
                _textListeMedBunde = value;
                OnPropertyChanged(nameof(TextListeMedBunde));
            }
        }
        private IngredientPresenter _bundeSelectedItem;
        public IngredientPresenter BundeSelectedItem
        {
            get { return _bundeSelectedItem; }
            set { _bundeSelectedItem = value; }
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
        private IngredientPresenter _sovsSelectedItem;
        public IngredientPresenter SovsSelectedItem
        {
            get { return _sovsSelectedItem; }
            set { _sovsSelectedItem = value; }
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
        private IngredientPresenter _ostSelectedItem;
        public IngredientPresenter OstSelectedItem
        {
            get { return _ostSelectedItem; }
            set { _ostSelectedItem = value; }
        }

        private string _antalToppings;
        public string AntalToppings
        {
            get { return _antalToppings; }
            set
            {
                _antalToppings = value;
                OnPropertyChanged(nameof(AntalToppings));
            }
        }

        void ToppingTilSelectionMenu()
        {
            ObservableCollection<ToppingPresenterCheck> temp = new ObservableCollection<ToppingPresenterCheck>();

            foreach (Topping topping in ToppingListe)
            {
                string s = $"{topping.Navn} ({topping.Pris}).";

                temp.Add(new ToppingPresenterCheck(topping.Id, s));
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

        public Pizza pizzaToCustomize { get; set; }

        //Pizza pizzaToCustomize;
        public ViewModelCustom(int pizzaIdToCustomize)
        {
            dal = new DAL.VarerDAL();
            ToppingTæller = 0;
            ToppingListe = dal.GetAllToppings();
            BundListe = dal.GetAllBunde();
            SovsListe = dal.GetAllSovse();
            OstListe = dal.GetAllOste();

            ToppingTilSelectionMenu();
            BundTilSelectionBox();
            SovsTilSelectionBox();
            OstTilSelectionBox();

            pizzaToCustomize = dal.GetPizzaById(pizzaIdToCustomize);
            InitializeSelectionsOfPizzaParts();

        }

        private void InitializeSelectionsOfPizzaParts()
        {
            foreach (IngredientPresenter bt in TextListeMedBunde)
            {
                if (bt.menuID == pizzaToCustomize.Bund.Id)
                {
                    BundeSelectedItem = bt;
                    break;
                }
            }
            foreach (IngredientPresenter st in TextListeMedSovs)
            {
                if (st.menuID == pizzaToCustomize.Sovs.Id)
                {
                    SovsSelectedItem = st;
                    break;
                }
            }
            foreach (IngredientPresenter ot in TextListeMedOst)
            {
                if (ot.menuID == pizzaToCustomize.Ost.Id)
                {
                    OstSelectedItem = ot;
                    break;
                }
            }
            if (pizzaToCustomize.PizzaTopping != null)
            {
                ToppingTæller = pizzaToCustomize.PizzaTopping.Count;
                foreach (Topping top in pizzaToCustomize.PizzaTopping)
                {
                    foreach(ToppingPresenterCheck tpc in TextListeMedToppings)
                    {
                        if(top.Id == tpc.menuID)
                        {
                            tpc.Checked = true;
                        }
                    }
                }
            }
            else
            {
                ToppingTæller = 0;
            }
            UpdateAntalToppingsLabel();
        }

        public void SkiftBundPåPizza()
        {
            if (pizzaToCustomize.Bund.Id != BundeSelectedItem.menuID)
            {
                foreach (Bund b in BundListe)
                {
                    if (BundeSelectedItem.menuID == b.Id)
                    {
                        pizzaToCustomize.Bund.Id = b.Id;
                        pizzaToCustomize.Bund.Navn = b.Navn;
                        pizzaToCustomize.Bund.Pris = b.Pris;
                        break;
                    }
                }
            }
        }
        public void SkiftSovsPåPizza()
        {
            if (pizzaToCustomize.Sovs.Id != SovsSelectedItem.menuID)
            {
                foreach (Sovs s in SovsListe)
                {
                    if (SovsSelectedItem.menuID == s.Id)
                    {
                        pizzaToCustomize.Sovs.Id = s.Id;
                        pizzaToCustomize.Sovs.Navn = s.Navn;
                        pizzaToCustomize.Sovs.Pris = s.Pris;
                        break;
                    }
                }
            }
        }
        public void SkiftOstPåPizza()
        {
            if (pizzaToCustomize.Ost.Id != OstSelectedItem.menuID)
            {
                foreach (Ost o in OstListe)
                {
                    if (OstSelectedItem.menuID == o.Id)
                    {
                        pizzaToCustomize.Ost.Id = o.Id;
                        pizzaToCustomize.Ost.Navn = o.Navn;
                        pizzaToCustomize.Ost.Pris = o.Pris;
                        break;
                    }
                }
            }
        }
        public void SkiftToppingPåPizza()
        {
            //Add or remove toppings as they are selected or unselected
        }


        public void UpdateAntalToppingsLabel()
        {
            AntalToppings = $"{ToppingTæller}/4 mulige toppings"; 
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
