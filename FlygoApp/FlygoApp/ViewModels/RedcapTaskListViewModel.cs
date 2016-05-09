using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using FlyGoWebService;
using FlyGoWebService.Models;
using Microsoft.AspNet.SignalR.Client;

namespace FlygoApp.ViewModels
{
    public class RedcapTaskListViewModel : INotifyPropertyChanged
    {
        #region instance fields
        private DtoHangarSingleton _dtoHangar;
        private DtoFlySingleton _dtoFly;
        private DtoOpgaveArkivSingleton _dtoOpgaveArkiv;
        private LoginBrugerSingleton _loginBruger;
        private DtoRolesSingleton _dtoRoles;
        private ICommand _backCommand;
        private NavigationService navigationService;
        private string _selectedMekanikerDetails;
        private string _selectedCatersDetails;
        private string _selectedCrewDetails;
        private string _selectedFulersDetails;
        private string _selectedBaggersDetails;
        private string _selectedCountdown;
        private ICommand _sendOpgaveCommand;
        private DispatcherTimer _timer = new DispatcherTimer();
        private string _selectedRengøringDetails;
        private OpgaveAdapter _opgaveAdapter;
        private string _logInRole;
        private ICommand _sendKorrektSvarCommand;
        private ICommand _sendForsinketSvarCommand;
        private ICommand _sendFejlSvarCommand;
        private int _selectedForsinketTidIndex;

        #endregion
        #region Properties
        public string FlyRuteNr { get; set; }
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
            get { return _backCommand ?? (new RelayCommand((() => navigationService.Navigate(typeof(RedcapTaskPage))))); }
            set { _backCommand = value; }
        }

        public ICommand SendKorrektSvarCommand
        {
            get { return _sendKorrektSvarCommand ?? (_sendKorrektSvarCommand = new RelayCommand(SendKorrektSvar)); }
            set { _sendKorrektSvarCommand = value; }
        }

        public ICommand SendForsinketSvarCommand    
        {
            get
            {
                return _sendForsinketSvarCommand ?? (_sendForsinketSvarCommand = new RelayCommand(SendForsinketSvar));
            }
            set { _sendForsinketSvarCommand = value; }
        }

        public ICommand SendFejlSvarCommand
        {
            get { return _sendFejlSvarCommand ?? (_sendFejlSvarCommand = new RelayCommand(SendFejlSvar)); }
            set { _sendFejlSvarCommand = value; }
        }

        public FlyRute FlyRute { get; set; }
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

        public RedcapTaskListViewModel()
        {
            navigationService = new NavigationService();
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

            FlyRute = s.FlyRute;
            OpgaveArkiv = _dtoOpgaveArkiv.OpgaveArkivListe.Single(x => x.FlyRuteId.Equals(FlyRute.Id));
            FlyRuteNr = FlyRute.FlyRuteNummer;
            Afgang = FlyRute.AfgangSomText;
            FlyId = FlyRute.FlyId;
            HangarId = FlyRute.HangarId;
            GetFlyObject();
            GetHangarObject();
            OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv, FlyRute);
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
        }

        public void SendForsinketSvar()
        {
            TimeSpan chosenSpan = TimeSpan.FromTicks(ForsinketTid[SelectedForsinketTidIndex]);            
            Proxy.Invoke("BroadcastForsinketSvar", _loginBruger.BrugerLogIn.RoleId, chosenSpan);           
        }

        public void SendFejlSvar()
        {
            Proxy.Invoke("BroadcastFejlSvar", _loginBruger.BrugerLogIn.RoleId);
        }

        private async void OnKorrektMessage(int roleId)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                
                switch (roleId)
                {
                    case 2:
                        SelectedMekanikerDetails = DateTime.Now.ToString();
                        OpgaveArkiv.Mekanikker = DateTime.Now;
                        break;
                    case 3:
                        SelectedCrewDetails = DateTime.Now.ToString();
                        OpgaveArkiv.Crew = DateTime.Now;
                        break;
                    case 4:
                        SelectedFulersDetails = DateTime.Now.ToString();
                        OpgaveArkiv.Fuelers = DateTime.Now;
                        break;
                    case 5:
                        SelectedBaggersDetails = DateTime.Now.ToString();
                        OpgaveArkiv.Baggers = DateTime.Now;
                        break;
                    case 6:
                        SelectedCatersDetails = DateTime.Now.ToString();
                        OpgaveArkiv.Caters = DateTime.Now;
                        break;
                }
                OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv,FlyRute);
                OnPropertyChanged();
                _dtoOpgaveArkiv.UpdateOpgaveArkiv(OpgaveArkiv, OpgaveArkiv.Id);
               
                _dtoOpgaveArkiv.LoadOpgaveArkiv();

            });
        }
        private async void OnForsinketMessage(int roleId, TimeSpan span)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                DateTime forsinketTime = FlyRute.Afgang + span;
                
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
                }
                OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv, FlyRute);
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
                }
                OpgaveAdapter = new OpgaveAdapter(OpgaveArkiv, FlyRute);
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
            DateTime til = FlyRute.Afgang;
            TimeSpan span = til - DateTime.Now;
            SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");
        }
        public void OpgaveArkivinit()
        {
            DateTime til = FlyRute.Afgang;
            TimeSpan span = til - DateTime.Now;
            SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");
            SelectedMekanikerDetails = (OpgaveArkiv.Mekanikker == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Mekanikker.ToString();
            SelectedBaggersDetails = (OpgaveArkiv.Baggers == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Baggers.ToString();
            SelectedCatersDetails = (OpgaveArkiv.Caters == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Caters.ToString();
            SelectedCrewDetails = (OpgaveArkiv.Crew == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Crew.ToString();
            SelectedFulersDetails = (OpgaveArkiv.Fuelers == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Fuelers.ToString();
            //SelectedRengøringDetails = (OpgaveArkiv.Cleaner == DateTime.Parse("1995-01-01")) ? "Fejl" : OpgaveArkiv.Cleaner.ToString();
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
