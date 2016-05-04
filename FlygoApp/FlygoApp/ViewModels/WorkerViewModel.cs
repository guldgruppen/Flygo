using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;
using Windows.UI.Core;
using Windows.UI.Xaml;
using FlygoApp.Annotations;
using FlygoApp.Models;
using FlyGoWebService.Models;
using Microsoft.AspNet.SignalR.Client;

namespace FlygoApp.ViewModels
{
    public class WorkerViewModel : INotifyPropertyChanged
    {
        private string _ankomst;
        private string _afgang;
        private string _flyruteNummer;
        private DispatcherTimer timer = new DispatcherTimer();
        private DateTime _ankomstDateTime;
        private DateTime _afgangDateTime;
        private TimeSpan _timeSpanCountdown;
        private string _tid;
        private string _countdownTid;

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
            set { _tid = value;OnPropertyChanged(); }
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
        public int test { get; set; } = 5;
        public HubConnection HubConnection { get; set; }
        public IHubProxy proxy { get; set; }

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

        public WorkerViewModel()
        {
            HubConnection = new HubConnection("http://flygowebservice1.azurewebsites.net/");
            proxy = HubConnection.CreateHubProxy("OpgaveHub");
            HubConnection.Start();

            proxy.On<FlyRute>("Broadcast", OnMessage);
        }


        private async void OnMessage(FlyRute msg)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                FlyruteNummer = msg.FlyRuteNummer;
                Ankomst = msg.Ankomst.ToString();
                Afgang = msg.Afgang.ToString();
                
                AnkomstDateTime = msg.Ankomst;
                AfgangDateTime = msg.Afgang;

                TimeSpan tidspan = msg.Afgang - msg.Ankomst;
                Tid = tidspan.ToString();

                TimeSpanCountdown = msg.Afgang - DateTime.Now;
                CountdownTid = TimeSpanCountdown.ToString();

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
            
            if (DateTime.Now >= AfgangDateTime)
            {
                CountdownTid = "færdig";
            }
            else
            {
                CountdownTid = TimeSpanCountdown.ToString(@"dd\.hh\:mm\:ss");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
