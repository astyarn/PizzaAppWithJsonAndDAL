using PizzaAppWithJsonAndDAL.Ting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PizzaAppWithJsonAndDAL.ViewModels
{

    internal class ViewModelCustom : INotifyPropertyChanged
    {
        DAL.VarerDAL dal;
        ObservableCollection<Topping> ToppingListe;
        ObservableCollection<Bund> BundListe;
        ObservableCollection<Sovs> SovsListe;
        ObservableCollection<Ost> OstListe;

        public ObservableCollection<VarePresenter> CustomSizeOptions { get; set; }
        public VarePresenter CustomSizeSelection { get; set; }

        private ObservableCollection<ToppingPresenterCheck> _textListeMedToppings;
        public ObservableCollection<ToppingPresenterCheck> TextListeMedToppings
        {
            get { return _textListeMedToppings; }
            set
            {
                _textListeMedToppings = value;
                OnPropertyChanged(nameof(TextListeMedToppings));
            }
        }

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
            set { _bundeSelectedItem = value;
                OnPropertyChanged(nameof(BundeSelectedItem)); }
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
            set { _sovsSelectedItem = value;
                OnPropertyChanged(nameof(SovsSelectedItem));
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
        private IngredientPresenter _ostSelectedItem;
        public IngredientPresenter OstSelectedItem
        {
            get { return _ostSelectedItem; }
            set { _ostSelectedItem = value;
                OnPropertyChanged(nameof(OstSelectedItem));
            }
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
        public int ToppingTæller { get; set; }

        public void ToppingTilSelectionMenu()
        {
            ObservableCollection<ToppingPresenterCheck> temp = new ObservableCollection<ToppingPresenterCheck>();

            foreach (Topping topping in ToppingListe)
            {
                string s = $"{topping.Navn} ({topping.Pris * CustomSizeSelection.menuID}).";

                temp.Add(new ToppingPresenterCheck(topping.Id, s));
            }
            TextListeMedToppings = temp;
        }
        public void ToppingTilSelectionMenuUpdate()
        {
            foreach (ToppingPresenterCheck tpc in TextListeMedToppings)
            {
                foreach(Topping topping in ToppingListe)
                {
                    if (tpc.menuID == topping.Id)
                    {
                        tpc.menuText = $"{topping.Navn} ({topping.Pris * CustomSizeSelection.menuID}).";
                        break;
                    }
                }
                
            }
        }
        //
        public void BundTilSelectionBox()
        {
            ObservableCollection<IngredientPresenter> temp = new ObservableCollection<IngredientPresenter>();

            foreach (Bund b in BundListe)
            {
                string s = $"{b.Navn} ({b.Pris * CustomSizeSelection.menuID}).";

                temp.Add(new IngredientPresenter(b.Id, s));
            }
            TextListeMedBunde = temp;
        }
        public void SovsTilSelectionBox()
        {
            ObservableCollection<IngredientPresenter> temp = new ObservableCollection<IngredientPresenter>();

            foreach (Sovs svs in SovsListe)
            {
                string s = $"{svs.Navn} ({svs.Pris * CustomSizeSelection.menuID}).";

                temp.Add(new IngredientPresenter(svs.Id, s));
            }
            TextListeMedSovs = temp;

        }
        public void OstTilSelectionBox()
        {
            ObservableCollection<IngredientPresenter> temp = new ObservableCollection<IngredientPresenter>();

            foreach (Ost o in OstListe)
            {
                string s = $"{o.Navn} ({o.Pris * CustomSizeSelection.menuID}).";

                temp.Add(new IngredientPresenter(o.Id, s));
            }
            TextListeMedOst = temp;
        }

        public Pizza pizzaToCustomize { get; set; }
        
        public ViewModelCustom(int pizzaIdToCustomize, VarePresenter iSize)
        {
            dal = new DAL.VarerDAL();
            ToppingTæller = 0;
            ToppingListe = dal.GetAllToppings();
            BundListe = dal.GetAllBunde();
            SovsListe = dal.GetAllSovse();
            OstListe = dal.GetAllOste();

            //Opretter størrelseslisten til varerne baseret på size enum i Varer klassen
            CustomSizeOptions = new ObservableCollection<VarePresenter>();
            CustomSizeOptions.Add(new VarePresenter(((int)Varer.size.Normal), Varer.size.Normal.ToString()));
            CustomSizeOptions.Add(new VarePresenter(((int)Varer.size.Stor), Varer.size.Stor.ToString()));
            if(iSize.menuID == CustomSizeOptions[0].menuID)
            {
                CustomSizeSelection = CustomSizeOptions[0];
            }
            else
            {
                CustomSizeSelection = CustomSizeOptions[1];
            }
            pizzaToCustomize = (Pizza)dal.GetVareById(pizzaIdToCustomize);
            //Laver data til at vise i comboks og listboks med checkboks
            ToppingTilSelectionMenu();
            BundTilSelectionBox();
            SovsTilSelectionBox();
            OstTilSelectionBox();

            
            InitializeSelectionsOfPizzaParts();

        }

        public void InitializeSelectionsOfPizzaParts()
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

        public void SkiftBundPaaPizza()
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
        public void SkiftSovsPaaPizza()
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
        public void SkiftOstPaaPizza()
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
        public void SkiftToppingPaaPizza()
        {
            //Go through the list of toppings and add/remove checked/unchecked toppings
            foreach(ToppingPresenterCheck tpc in TextListeMedToppings)
            {
                //controls wether a uncheck topping is on the pizza and removes it
                if (!tpc.Checked)
                {
                    foreach (Topping pTop in pizzaToCustomize.PizzaTopping)
                    {
                        if (tpc.menuID == pTop.Id)
                        {
                            pizzaToCustomize.PizzaTopping.Remove(pTop);
                            break;  //needed otherwise code breaks since collection is modified during a foreach
                        }
                    }
                }

                //checks if a checked topping has been added and, if not, adds it
                if(tpc.Checked)
                {
                    int ctrl = 0;   //stays 0 if the topping isnt already on the pizza
                    foreach(Topping pTop in pizzaToCustomize.PizzaTopping)
                    {
                        if(tpc.menuID == pTop.Id)
                        {
                            ctrl++;
                        }
                    }

                    //Adds a topping to the pizza based on the selected ID
                    if (ctrl == 0)
                    {
                        foreach (Topping top in ToppingListe)
                        {
                            if (tpc.menuID == top.Id)
                            {
                                Topping nyTop = new Topping(top.Id, top.Navn, top.Pris);
                                pizzaToCustomize.PizzaTopping.Add(nyTop);
                            }
                        }
                    }
                }
            }
        }


        public void UpdateAntalToppingsLabel()
        {
            AntalToppings = $"{ToppingTæller}/4 mulige toppings"; 
        }

        public void CountNumberOfToppingsSelected()
        {
            int CountingSelectedToppings = 0;
            foreach (ToppingPresenterCheck tpc in TextListeMedToppings)
            {
                if(tpc.Checked == true)
                {
                    CountingSelectedToppings++;
                }
            }
            ToppingTæller = CountingSelectedToppings;
        }

        /// <summary>
        /// Run a check if the user is trying to add a possible 5th topping to the pizza
        /// Prevents the checkbox from being set true if it would mean having 5 selected
        /// </summary>
        /// <param name="iCb">Reference to the CheckBox object belonging to the topping being selected</param>
        public void CheckToppingLimit(CheckBox iCb)
        {
            CountNumberOfToppingsSelected();
            if(ToppingTæller > 4)
            {
                iCb.IsChecked = false;
            }
            else
            { 
                UpdateAntalToppingsLabel();
            }
        }

        public void SætSizePåPizza()
        {
            if (CustomSizeSelection.menuID == 2)
            {
                pizzaToCustomize.Size = Varer.size.Stor;
            }
            else
            {
                pizzaToCustomize.Size = Varer.size.Normal;
            }
        }


        /// <summary>
        /// Method saves the current selection of bund, ost, sovs comboboxes
        /// Updates the text in the combobox selections
        /// Sets the combobox selection back to the selections made 
        /// </summary>
        public void OpdaterTekstPåComboboxVedSizeSkift()
        {
            //Saves selections
            int tempBundId = BundeSelectedItem.menuID;
            int tempSovsId = SovsSelectedItem.menuID;
            int tempOstId = OstSelectedItem.menuID;
            
            //Update the text in the combobox lists. Resets the selections on the combobox.
            ToppingTilSelectionMenuUpdate();
            BundTilSelectionBox();
            SovsTilSelectionBox();
            OstTilSelectionBox();

            //Sets selection back to what was selected before text update
            foreach (IngredientPresenter ip in TextListeMedBunde)
            {
                if (ip.menuID == tempBundId)
                {
                    BundeSelectedItem = ip;
                    break;
                }
            }
            foreach (IngredientPresenter ip in TextListeMedSovs)
            {
                if (ip.menuID == tempSovsId)
                {
                    SovsSelectedItem = ip;
                    break;
                }
            }
            foreach (IngredientPresenter ip in TextListeMedOst)
            {
                if (ip.menuID == tempOstId)
                {
                    OstSelectedItem = ip;
                    break;
                }
            }
        }

        /// <summary>
        /// Takes all the changes made and saves them onto the pizza object.
        /// Calculates new price based on changes.
        /// Sets a new name to identify that this is a custom pizza.
        /// </summary>
        /// <returns>Returns Pizza with all the changes made by the user in the customization window</returns>
        public Pizza SaveCustomizationAndPushPizzaToCart()
        {
            SkiftBundPaaPizza();
            SkiftSovsPaaPizza();
            SkiftOstPaaPizza();
            SkiftToppingPaaPizza();
            SætSizePåPizza();            //Sets the size on the customized pizza in order to calculate price
            pizzaToCustomize.BeregnPris();
            pizzaToCustomize.Navn = $"*{pizzaToCustomize.Navn}*";

            return pizzaToCustomize;
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
