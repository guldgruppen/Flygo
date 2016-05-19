using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Popups;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Persistency;
using FlyGoWebService;
using FlyGoWebService.Models;

namespace FlygoApp.ViewModels
{
    public class BrugerDataViewModel : INotifyPropertyChanged
    {
        #region instance fields

        private DtoBrugerLoginSingleton _dtoBrugerLogin;
        private DtoRolesSingleton _dtoRolesSingleton;

        private int _selectedBrugerIndex;
        private int _selectedRolleIndex;
        private string _selectedBrugernavn;
        private string _selectedKodeord;
        private string _selectedRolle;

        private ICommand _opretBrugerCommand;
        private ICommand _deleteBrugerCommand;

        #endregion
        #region Properties

        public string SelectedBrugernavn
        {
            get { return _selectedBrugernavn; }
            set
            {
                _selectedBrugernavn = value; 
                OnPropertyChanged();
            }
        }

        public string SelectedKodeord
        {
            get { return _selectedKodeord; }
            set
            {
                _selectedKodeord = value;
                OnPropertyChanged();
            }
        }
        public string SelectedRolle
        {
            get { return _selectedRolle; }
            set
            {
                _selectedRolle = value;
                OnPropertyChanged();
            }
        }
        public string Brugernavn { get; set; }
        public string Kodeord { get; set; }
        public int RoleId { get; set; }
        public int SelectedBrugerIndex
        {
            get { return _selectedBrugerIndex; }
            set
            {
                _selectedBrugerIndex = value;
                if (_selectedBrugerIndex != -1)
                {
                    SelectedBrugernavn = BrugerLogIns[_selectedBrugerIndex].BrugerNavn;
                    SelectedKodeord = BrugerLogIns[_selectedBrugerIndex].Password;                   
                    SelectedRolle = BrugerLogIns[_selectedBrugerIndex].RolleNavn;

                }
                OnPropertyChanged();
            }
        }

        public int SelectedRolleIndex
        {
            get { return _selectedRolleIndex; }
            set
            {
                _selectedRolleIndex = value;
                RoleId = RollerList[_selectedRolleIndex].Id;
                OnPropertyChanged();
            }
        }

        public ICommand OpretBrugerCommand
        {
            get { return _opretBrugerCommand ?? (_opretBrugerCommand = new RelayCommand(OpretBrugerAsync)); }
            set { _opretBrugerCommand = value; }
        }
        public ICommand DeleteBrugerCommand
        {
            get { return _deleteBrugerCommand ?? (_deleteBrugerCommand = new RelayCommandWithParameter(DeleteBruger)); }
            set { _deleteBrugerCommand = value; }
        }

        public List<Roles> RollerList { get; set; }

        public ObservableCollection<BrugerLogIn> BrugerLogIns { get; set; }
        #endregion

        public BrugerDataViewModel()
        {
            _dtoBrugerLogin = DtoBrugerLoginSingleton.GetInstance;
            _dtoRolesSingleton = DtoRolesSingleton.GetInstance;
            BrugerLogIns = new ObservableCollection<BrugerLogIn>();
            RollerList = new List<Roles>();
            LoadBrugerData();
        }
        #region Metoder

        public async void OpretBrugerAsync()
        {
            try
            {
                CheckBrugerData(Brugernavn,Kodeord,RoleId);
               BrugerLogIn temp = new BrugerLogIn(Brugernavn,Kodeord,RoleId);
                _dtoBrugerLogin.PostBrugerLogin(temp);
                BrugerLogIns.Add(temp);
                LoadBrugerData();
            }
            catch (ArgumentException ex)
            {

                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        public void CheckBrugerData(string brugernavn, string kodeord, int rolleid)
        {
            if(String.IsNullOrEmpty(brugernavn))
                throw new ArgumentException("Du mangler at indtaste brugernavn");
            if(String.IsNullOrEmpty(kodeord))
                throw new ArgumentException("Du mangler at indtaste kodeord");
            if(rolleid < 0 || rolleid > RollerList.Count)
                throw new ArgumentException("Du mangler at vælge en rolle");
        }

        public void DeleteBruger(object param)
        {
            
        }

        public void LoadBrugerData()
        {
            BrugerLogIns.Clear();
            RollerList.Clear();
            _dtoBrugerLogin.LoadBrugerLogins();
            foreach (var brugerLogIn in _dtoBrugerLogin.BrugerLogInsDictionary)
            {             
                BrugerLogIn temp = new BrugerLogIn(brugerLogIn.Key,brugerLogIn.Value.Password,brugerLogIn.Value.RoleId);
                BrugerLogIns.Add(temp);
            }
            foreach (var roller in _dtoRolesSingleton.RolesListe)
            {
                RollerList.Add(roller);
            }
            
        }
        #endregion

        #region OnPropertyChanged region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
