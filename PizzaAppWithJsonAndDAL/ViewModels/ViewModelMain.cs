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
        }
        //Metoder

        public int GetSelectedPizzaId()
        {
            return SelectionMenu.menuID;
        }

        public void LægVareIKurvFraMenu()
        {
            if (SelectionMenu != null)
            {
                int vareId = SelectionMenu.menuID;
                Varekurv.TilføjVare(dal.GetPizzaById(vareId));
                VarekurvBeskrivelser = Varekurv.OpdaterVareKurvTekst();
            }
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

    }
}
