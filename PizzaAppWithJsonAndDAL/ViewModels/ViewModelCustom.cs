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
        public ViewModelCustom()
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
            ObservableCollection<PizzaPresenter> temp = new ObservableCollection<PizzaPresenter>();
            
            foreach (Topping topping in ToppingListe)
            {
                string s = $"{topping.Navn} ({topping.Pris}).";

                temp.Add(new PizzaPresenter(topping.Id, s));
            }
            TextListeMedToppings = temp;
        }

        void BundTilSelectionBox()
        {
            ObservableCollection<PizzaPresenter> temp = new ObservableCollection<PizzaPresenter>();

            foreach (Bund b in BundListe)
            {
                string s = $"{b.Navn} ({b.Pris}).";

                temp.Add(new PizzaPresenter(b.Id, s));
            }
            TextListeMedBunde = temp;
        }
        void SovsTilSelectionBox()
        {
            ObservableCollection<PizzaPresenter> temp = new ObservableCollection<PizzaPresenter>();

            foreach (Sovs svs in SovsListe)
            {
                string s = $"{svs.Navn} ({svs.Pris}).";

                temp.Add(new PizzaPresenter(svs.Id, s));
            }
            TextListeMedSovs = temp;

        }
        void OstTilSelectionBox()
        {
            ObservableCollection<PizzaPresenter> temp = new ObservableCollection<PizzaPresenter>();

            foreach (Ost o in OstListe)
            {
                string s = $"{o.Navn} ({o.Pris}).";

                temp.Add(new PizzaPresenter(o.Id, s));
            }
            TextListeMedOst = temp;
        }

        private ObservableCollection<PizzaPresenter> _listeMedToppings;
        public ObservableCollection<PizzaPresenter> TextListeMedToppings
        {
            get { return _listeMedToppings; }
            set
            {
                _listeMedToppings = value;
                OnPropertyChanged(nameof(TextListeMedToppings));
            }
        }
        private ObservableCollection<PizzaPresenter> _textListeMedBunde;
        public ObservableCollection<PizzaPresenter> TextListeMedBunde
        {
            get { return _textListeMedBunde; }
            set { 
                _textListeMedBunde = value;
                OnPropertyChanged(nameof(TextListeMedBunde));
            }
        }
        private ObservableCollection<PizzaPresenter> _textListeMedSovs;
        public ObservableCollection<PizzaPresenter> TextListeMedSovs
        {
            get { return _textListeMedSovs; }
            set
            {
                _textListeMedSovs = value;
                OnPropertyChanged(nameof(TextListeMedSovs));
            }
        }
        private ObservableCollection<PizzaPresenter> _textListeMedOst;
        public ObservableCollection<PizzaPresenter> TextListeMedOst
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
