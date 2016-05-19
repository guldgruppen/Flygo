using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Models;
using FlygoApp.Persistency;
using FlygoApp.Views;
using FlyGoWebService.Models;
using Microsoft.AspNet.SignalR.Client;

namespace FlygoApp.ViewModels
{
    public class WorkerTaskListViewModel : INotifyPropertyChanged
    {
        #region instance fields
        private DtoHangarSingleton _dtoHangar;
        private DtoFlySingleton _dtoFly;
        private DtoOpgaveArkivSingleton _dtoOpgaveArkiv;
        private LoginBrugerSingleton _loginBruger;
        private DtoRolesSingleton _dtoRoles;
        private ICommand _backCommand;
        private readonly NavigationService _navigationService;
        private string _selectedMekanikerDetails;
        private string _selectedCatersDetails;
        private string _selectedCrewDetails;
        private string _selectedFulersDetails;
        private string _selectedBaggersDetails;
        private string _selectedCountdown;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private string _selectedRengøringDetails;
        private OpgaveAdapter _opgaveAdapter;
        private string _logInRole;
        private ICommand _sendKorrektSvarCommand;
        private ICommand _sendFejlSvarCommand;
        private int _selectedForsinketTidIndex = -1;
        private string _selectedRedcapDetails;
        private ICommand _logudCommand;

        #endregion
        #region Properties
        public string FlyopgaveNr { get; set; }
        public string Afgang { get; set; }
        public string Ankomst { get; set; }
        public int FlyId { get; set; }
        public Fly FlyType { get; set; }
        public OpgaveArkiv OpgaveArkiv { get; set; }
        public int HangarId { get; set; }
        public Hangar Hangar { get; set; }
        public string SelectedMekanikerDetails
        {
            get { return _selectedMekanikerDetails; }
            set
            {
                _selectedMekanikerDetails = value;
                OnPropertyChanged();
            }
        }
        public string SelectedCatersDetails
        {
            get { return _selectedCatersDetails; }
            set
            {
                _selectedCatersDetails = value;
                OnPropertyChanged();
            }
        }
        public string SelectedCrewDetails
        {
            get { return _selectedCrewDetails; }
            set
            {
                _selectedCrewDetails = value;
                OnPropertyChanged();
            }
        }
        public string SelectedFulersDetails
        {
            get { return _selectedFulersDetails; }
            set
            {
                _selectedFulersDetails = value;
                OnPropertyChanged();
            }
        }
        public string SelectedBaggersDetails
        {
            get { return _selectedBaggersDetails; }
            set
            {
                _selectedBaggersDetails = value;
                OnPropertyChanged();
            }
        }
        public string SelectedRengøringDetails
        {
            get { return _selectedRengøringDetails; }
            set
            {
                _selectedRengøringDetails = value;
                OnPropertyChanged();
            }
        }
        public string SelectedRedcapDetails
        {
            get { return _selectedRedcapDetails; }
            set
            {
                _selectedRedcapDetails = value;
                OnPropertyChanged();
            }
        }

        public string SelectedCountdown
        {
            get { return _selectedCountdown; }
            set
            {
                _selectedCountdown = value;
                OnPropertyChanged();
            }
        }
        public string LogInBrugernavn { get; set; }
        public List<int> ForsinketTid { get; set; } = new List<int>();
        public int SelectedForsinketTidIndex
        {
            get { return _selectedForsinketTidIndex; }
            set
            {
                _selectedForsinketTidIndex = value;
                
                if (_selectedForsinketTidIndex != -1)
                {
                    TimeSpan chosenSpan = TimeSpan.FromMinutes(ForsinketTid[SelectedForsinketTidIndex]);
                    Proxy.Invoke("BroadcastForsinketSvar", _loginBruger.BrugerLogIn.RoleId, chosenSpan);
                    
                }
                
                
                OnPropertyChanged();
            }
        }

        public string LogInRole
        {
            get { return _logInRole; }
            set
            {
                _logInRole = value;
                OnPropertyChanged();
            }
        }

        public ICommand BackCommand
        {
            get { return _backCommand ?? (new RelayCommand((() => _navigationService.Navigate(typeof(WorkerTaskPage))))); }
            set { _backCommand = value; }
        }

        public ICommand LogudCommand
        {
            get
            {
                return _logudCommand ??
                       (_logudCommand = new RelayCommand(() => _navigationService.Navigate(typeof (LoginPage))));
            }
            set { _logudCommand = value; }
        }

        public ICommand SendKorrektSvarCommand
        {
            get { return _sendKorrektSvarCommand ?? (_sendKorrektSvarCommand = new RelayCommand(SendKorrektSvar)); }
            set { _sendKorrektSvarCommand = value; }
        }

        public ICommand SendFejlSvarCommand
        {
            get { return _sendFejlSvarCommand ?? (_sendFejlSvarCommand = new RelayCommand(SendFejlSvar)); }
            set { _sendFejlSvarCommand = value; }
        }

        public Flyopgave Flyopgave { get; set; }
        public HubConnection Conn { get; set; }
        public IHubProxy Proxy { get; set; }
        public OpgaveAdapter OpgaveAdapter
        {
            get { return _opgaveAdapter; }
            set
            {
                _opgaveAdapter = value;
                OnPropertyChanged();
            }
        }

        #endregion

        public WorkerTaskListViewModel()
        {         
            _navigationService = new NavigationService();
            SignalRConnection();
            InitData();
            InsertForsinketTidValgmuligheder();

            Proxy.On<int>("KorrektSvar", OnKorrektMessage);
            Proxy.On<int>("FejlSvar", OnFejlMessage);
            Proxy.On<int, TimeSpan>("ForsinketSvar", OnForsinketMessage);
            
            
           
        }

        public void InitData()
        {
            _dtoHangar = DtoHangarSingleton.GetInstance();
            _dtoFly = DtoFlySingleton.GetInstance();
            _dtoOpgaveArkiv = DtoOpgaveArkivSingleton.GetInstance();
            _loginBruger = LoginBrugerSingleton.GetInstance();
            _dtoRoles = DtoRolesSingleton.GetInstance();
            LogInBrugernavn = _loginBruger.BrugerLogIn.BrugerNavn;

            LogInRole = _dtoRoles.RolesListe.First(x => x.Id.Equals(_loginBruger.BrugerLogIn.RoleId)).ToString();
            var s = SearchListSingleton.GetInstance();

            Flyopgave = s.Flyopgave;
            OpgaveArkiv = _dtoOpgaveArkiv.OpgaveArkivListe.Single(x => x.FlyopgaveId.Equals(Flyopgave.Id));
            FlyopgaveNr = Flyopgave.FlyopgaveNummer;
            Afgang = Flyopgave.AfgangSomText;
            FlyId = Flyopgave.FlyId;
            HangarId = Flyopgave.HangarId;
            GetFlyObject();
            GetHangarObject();
            OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv, Flyopgave);
            OpgaveArkivinit();
            CountdownToDeadline();
        }

        public void InsertForsinketTidValgmuligheder()
        {
            for (int i = 5; i < 120; i+=5)
            {
                ForsinketTid.Add(i);
            }
        }
        public void SignalRConnection()
        {
            Conn = new HubConnection("http://flygowebservice1.azurewebsites.net/");
            Proxy = Conn.CreateHubProxy("OpgaveHub");
            Conn.Start();

            

        }

        #region Metoder

        public void SendKorrektSvar()
        {
            Proxy.Invoke("BroadcastKorrektSvar", _loginBruger.BrugerLogIn.RoleId);
            SelectedForsinketTidIndex = -1; 
        }

        //public void SendForsinketSvar()
        //{
        //    TimeSpan chosenSpan = TimeSpan.FromMinutes(ForsinketTid[SelectedForsinketTidIndex]);       
        //    Proxy.Invoke("BroadcastForsinketSvar", _loginBruger.BrugerLogIn.RoleId, chosenSpan);           
        //}

        public void SendFejlSvar()
        {
            Proxy.Invoke("BroadcastFejlSvar", _loginBruger.BrugerLogIn.RoleId);
            SelectedForsinketTidIndex = -1; 
        }

        private async void OnKorrektMessage(int roleId)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                
                switch (roleId)
                {
                    case 2:
                        SelectedMekanikerDetails = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        OpgaveArkiv.Mekanikker = DateTime.Now;
                        break;
                    case 3:
                        SelectedCrewDetails = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        OpgaveArkiv.Crew = DateTime.Now;
                        break;
                    case 4:
                        SelectedFulersDetails = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        OpgaveArkiv.Fuelers = DateTime.Now;
                        break;
                    case 5:
                        SelectedBaggersDetails = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        OpgaveArkiv.Baggers = DateTime.Now;
                        break;
                    case 6:
                        SelectedCatersDetails = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        OpgaveArkiv.Caters = DateTime.Now;
                        break;
                    case 7:
                        SelectedRedcapDetails = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        OpgaveArkiv.Redcap = DateTime.Now;
                        break;
                    case 9:
                        SelectedRengøringDetails = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                        OpgaveArkiv.Rengøring = DateTime.Now;
                        break;
                }
                OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv,Flyopgave);
                OnPropertyChanged();
                _dtoOpgaveArkiv.UpdateOpgaveArkiv(OpgaveArkiv, OpgaveArkiv.Id);
               
                _dtoOpgaveArkiv.LoadOpgaveArkiv();

            });
        }
        private async void OnForsinketMessage(int roleId, TimeSpan span)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                DateTime forsinketTime = Flyopgave.Afgang + span;
                
                switch (roleId)
                {
                    case 2:
                        SelectedMekanikerDetails = "Forventet klar: "+forsinketTime;
                        OpgaveArkiv.Mekanikker = forsinketTime;
                        break;
                    case 3:
                        SelectedCrewDetails = "Forventet klar: " + forsinketTime;
                        OpgaveArkiv.Crew = forsinketTime;
                        break;
                    case 4:
                        SelectedFulersDetails = "Forventet klar: " + forsinketTime;
                        OpgaveArkiv.Fuelers = forsinketTime;
                        break;
                    case 5:
                        SelectedBaggersDetails = "Forventet klar: " + forsinketTime;
                        OpgaveArkiv.Baggers = forsinketTime;
                        break;
                    case 6:
                        SelectedCatersDetails = "Forventet klar: " + forsinketTime;
                        OpgaveArkiv.Caters = forsinketTime;
                        break;
                    case 7:
                        SelectedRedcapDetails = "Forventet klar: " + forsinketTime;
                        OpgaveArkiv.Redcap = forsinketTime;
                        break;
                    case 9:
                        SelectedRengøringDetails = "Forventet klar: " + forsinketTime;
                        OpgaveArkiv.Rengøring = forsinketTime;
                        break;
                }
                OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv, Flyopgave);
                OnPropertyChanged();
                _dtoOpgaveArkiv.UpdateOpgaveArkiv(OpgaveArkiv, OpgaveArkiv.Id);

                _dtoOpgaveArkiv.LoadOpgaveArkiv();

            });
        }
        private async void OnFejlMessage(int roleId)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {

                switch (roleId)
                {
                    case 2:
                        SelectedMekanikerDetails = "Fejl";
                        OpgaveArkiv.Mekanikker = DateTime.Parse("01-01-1995");
                        break;
                    case 3:
                        SelectedCrewDetails = "Fejl";
                        OpgaveArkiv.Crew = DateTime.Parse("01-01-1995");
                        break;
                    case 4:
                        SelectedFulersDetails = "Fejl";
                        OpgaveArkiv.Fuelers = DateTime.Parse("01-01-1995");
                        break;
                    case 5:
                        SelectedBaggersDetails = "Fejl";
                        OpgaveArkiv.Baggers = DateTime.Parse("01-01-1995");
                        break;
                    case 6:
                        SelectedCatersDetails = "Fejl";
                        OpgaveArkiv.Caters = DateTime.Parse("01-01-1995");
                        break;
                    case 7:
                        SelectedRedcapDetails = "Fejl";
                        OpgaveArkiv.Redcap = DateTime.Parse("01-01-1995");
                        break;
                    case 9:
                        SelectedRengøringDetails = "Fejl";
                        OpgaveArkiv.Rengøring = DateTime.Parse("01-01-1995");
                        break;
                }
                OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv, Flyopgave);
                OnPropertyChanged();
                _dtoOpgaveArkiv.UpdateOpgaveArkiv(OpgaveArkiv, OpgaveArkiv.Id);

                _dtoOpgaveArkiv.LoadOpgaveArkiv();
                    
            });
        }
        public void CountdownToDeadline()
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _timer.Tick += MyTimer_Tick;
            _timer.Start();
        }

        public void MyTimer_Tick(object o, object sender)
        {
            DateTime til = Flyopgave.Afgang;
            TimeSpan span = til - DateTime.Now;
            SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");
        }
        public void OpgaveArkivinit()
        {
            DateTime til = Flyopgave.Afgang;
            TimeSpan span = til - DateTime.Now;
            SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");
            SelectedMekanikerDetails = (OpgaveArkiv.Mekanikker == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Mekanikker.ToString();
            SelectedBaggersDetails = (OpgaveArkiv.Baggers == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Baggers.ToString();
            SelectedCatersDetails = (OpgaveArkiv.Caters == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Caters.ToString();
            SelectedCrewDetails = (OpgaveArkiv.Crew == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Crew.ToString();
            SelectedFulersDetails = (OpgaveArkiv.Fuelers == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Fuelers.ToString();
            SelectedRengøringDetails = (OpgaveArkiv.Rengøring == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Rengøring.ToString();
            SelectedRedcapDetails = (OpgaveArkiv.Redcap == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Redcap.ToString();
        }

        public void GetFlyObject()
        {
            foreach (var fly in _dtoFly.FlyListe)
            {
                if (fly.Id == FlyId)
                {
                    FlyType = fly;
                    break;
                }
            }
        }

        public void GetHangarObject()
        {
            foreach (var hangar in _dtoHangar.HangarListe)
            {
                if (hangar.Id == HangarId)
                {
                    Hangar = hangar;
                    break;
                }
            }
        }

        #endregion

        #region Notify region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
