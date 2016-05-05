using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Core;
using Windows.UI.Xaml;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Persistency;
using FlyGoWebService;
using FlyGoWebService.Models;
using Microsoft.AspNet.SignalR.Client;

namespace FlygoApp.ViewModels
{
    public class WorkerViewModel : INotifyPropertyChanged
    {
        #region instance fields

        private string _ankomst;
        private string _afgang;
        private string _flyruteNummer;
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime _ankomstDateTime;
        private DateTime _afgangDateTime;
        private TimeSpan _timeSpanCountdown;
        private string _tid;
        private string _countdownTid;
        private ICommand _sendSvarCommand;

        #endregion
        #region Properties
        public string Ankomst
        {
            get { return _ankomst; }
            set
            {
                _ankomst = value;
                OnPropertyChanged();
            }
        }
        public string Afgang
        {
            get { return _afgang; }
            set
            {
                _afgang = value;
                OnPropertyChanged();
            }
        }
        public string FlyruteNummer
        {
            get { return _flyruteNummer; }
            set
            {
                _flyruteNummer = value;
                OnPropertyChanged();
            }
        }
        public string Tid
        {
            get { return _tid; }
            set { _tid = value; OnPropertyChanged(); }
        }
        public string CountdownTid
        {
            get { return _countdownTid; }
            set
            {
                _countdownTid = value;
                OnPropertyChanged();
            }
        }     
        public HubConnection HubConnection { get; set; }
        public IHubProxy Proxy { get; set; }
        public TimeSpan TimeSpanCountdown
        {
            get { return _timeSpanCountdown; }
            set
            {
                _timeSpanCountdown = value;
                OnPropertyChanged();
            }
        }
        public DateTime AnkomstDateTime
        {
            get { return _ankomstDateTime; }
            set
            {
                _ankomstDateTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime AfgangDateTime
        {
            get { return _afgangDateTime; }
            set
            {
                _afgangDateTime = value;
                OnPropertyChanged();
            }
        }

        public BrugerLogIn BrugerLogIn { get; set; }

        #endregion

        public ICommand SendSvarCommand
        {
            get { return _sendSvarCommand ?? (_sendSvarCommand = new RelayCommand(SendSvar)); }
            set { _sendSvarCommand = value; }
        }

        public WorkerViewModel()
        {
            var loginBruger = LoginBrugerSingleton.GetInstance();
            HubConnection = new HubConnection("http://flygowebservice1.azurewebsites.net/");
            Proxy = HubConnection.CreateHubProxy("OpgaveHub");
            HubConnection.Start();

            BrugerLogIn = loginBruger.BrugerLogIn;
            
            Proxy.On<FlyRute>("Broadcast", OnMessage);
        }


        #region Metoder

        public void SendSvar()
        {
            int brugerId = BrugerLogIn.RoleId;
            Proxy.Invoke("BroadcastSvar",brugerId);
        }



        private async void OnMessage(FlyRute msg)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                FlyruteNummer = msg.FlyRuteNummer;
                Ankomst = msg.Ankomst.ToString(CultureInfo.InvariantCulture);
                Afgang = msg.Afgang.ToString(CultureInfo.InvariantCulture);

                AnkomstDateTime = msg.Ankomst;
                AfgangDateTime = msg.Afgang;

                TimeSpan tidspan = msg.Afgang - msg.Ankomst;
                Tid = tidspan.ToString();

                TimeSpanCountdown = msg.Afgang - DateTime.Now;
                CountdownTid = TimeSpanCountdown.ToString(@"dd\.hh\:mm\:ss");

                CountdownToDeadline();

            });
        }
        public void CountdownToDeadline()
        {
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            timer.Tick += MyTimer_Tick;
            timer.Start();
        }

        public void MyTimer_Tick(object o, object sender)
        {
            TimeSpan spantid = AfgangDateTime - DateTime.Now;
            CountdownTid = (DateTime.Now >= AfgangDateTime) ? "færdig" : spantid.ToString(@"dd\.hh\:mm\:ss");

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
