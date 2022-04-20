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
    internal class ViewModelMain : INotifyPropertyChanged
    {
        DAL.VarerDAL dal;
        Kurv Varekurv;
        public ViewModelMain()
        {
            dal = new DAL.VarerDAL();
            Varekurv = new Kurv();

            MenuPizzaBeskrivelser = new ObservableCollection<VarePresenter>();
            VarekurvBeskrivelser = new ObservableCollection<VarePresenter>();
            
            MenuPizzaBeskrivelser = dal.FåPizzaBeskrivelseOgId();

            //Opretter størrelseslisten til varerne baseret på size enum i Varer klassen
            MainSizeOptions = new ObservableCollection<VarePresenter>();
            MainSizeOptions.Add(new VarePresenter(((int)Varer.size.Normal), Varer.size.Normal.ToString()));
            MainSizeOptions.Add(new VarePresenter(((int)Varer.size.Stor), Varer.size.Stor.ToString()));
            MainSizeSelection = MainSizeOptions[0]; //sætter normal til at være preselected (udløser stadig et change event!!!)
        }
        //Metoder
        /// <summary>
        /// Returns the Id of the selected object in the Menu
        /// </summary>
        /// <returns>Returns Id of selected items as int</returns>
        public int GetSelectedVareId()
        {
            return SelectionMenu.menuID;
        }
        /// <summary>
        /// Checks if the given ID belongs to a Pizza object
        /// </summary>
        /// <param name="iId">Id of a Vare</param>
        /// <returns>Returns true if the Id belongs to a Vare of the type Pizza</returns>
        public bool IsItAPizza(int iId)
        {
            Varer obj = dal.GetVareById(iId);
            if (obj == null)
            { 
                return false; 
            }
            else if (obj is Pizza)
            { 
                return true; 
            }
            else
            { 
                return false; 
            }
        }


        public void LægVareIKurvFraMenu()
        {
            if (SelectionMenu != null)
            {
                int vareId = SelectionMenu.menuID;
                Varekurv.TilføjVare(dal.GetVareById(vareId));
                VarekurvBeskrivelser = Varekurv.OpdaterVareKurvTekst();
            }
            SamletPrisAfKurvTilTekst();
        }
        public void LægPizzaIKurvFraCustomizering(Varer iCustomPizza)
        {
            Varekurv.TilføjVare(iCustomPizza);
            VarekurvBeskrivelser = Varekurv.OpdaterVareKurvTekst();
            
            SamletPrisAfKurvTilTekst();
        }

        public void FjernVareFraKurvMedVareKurvId()
        {
            if (SelectionVarekurv != null)
            {
                int vareKurvId = SelectionVarekurv.menuID;
                Varekurv.FjernVare(vareKurvId);
                VarekurvBeskrivelser = Varekurv.OpdaterVareKurvTekst();
            }
            SamletPrisAfKurvTilTekst();
        }
        void SamletPrisAfKurvTilTekst()
        {
            string s = $"Samlet ordre pris: {Varekurv.UdregnKurvSamletPris()} Kr.";
            TextSamletPrisAfKurv = s;
        }

        public void ChangeVareSize()
        {
            Varer.size iSize;
            if(MainSizeSelection.menuID == 2)
            {
                iSize = Varer.size.Stor;
            }
            else
            {
                iSize = Varer.size.Normal;
            }
            dal.SkiftStørrelsePåPizza(iSize);

            MenuPizzaBeskrivelser = dal.FåPizzaBeskrivelseOgId();   //Opdater MenuListen
        }



        //Properties til sammenknytning med xaml

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string PropertyNavn)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(PropertyNavn));
                //OnPropertyChanged("Beskrivelse");
            }
        }

        private ObservableCollection<VarePresenter> _menuPizzaBeskrivelser;

        public ObservableCollection<VarePresenter> MenuPizzaBeskrivelser
        {
            get { return _menuPizzaBeskrivelser; }
            set { 
                _menuPizzaBeskrivelser = value;
                OnPropertyChanged(nameof(MenuPizzaBeskrivelser));
            }
        }

        public VarePresenter SelectionMenu { get; set; }
        
        public VarePresenter SelectionVarekurv { get; set; }

        private ObservableCollection<VarePresenter> _varekurvBeskrivelser;
        public ObservableCollection<VarePresenter> VarekurvBeskrivelser
        {
            get { return _varekurvBeskrivelser; }
            set
            {
                _varekurvBeskrivelser = value;
                OnPropertyChanged(nameof(VarekurvBeskrivelser));
            }
        }
        
        private string _textSamletPrisAfKurv;
        public string TextSamletPrisAfKurv
        {
            get { return _textSamletPrisAfKurv; }
            set { 
                _textSamletPrisAfKurv = value; 
                OnPropertyChanged(nameof(TextSamletPrisAfKurv));
            }
        }

        //{Binding MainSizeOptions}" SelectedItem="{Binding MainSizeSelection}
        public ObservableCollection<VarePresenter> MainSizeOptions { get; set; }

        public VarePresenter MainSizeSelection { get; set; }
    }
}
