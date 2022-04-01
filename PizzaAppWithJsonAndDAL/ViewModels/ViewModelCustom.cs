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
            set { 
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
    }
}
