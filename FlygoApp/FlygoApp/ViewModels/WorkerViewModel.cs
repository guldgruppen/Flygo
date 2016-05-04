using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Chat;
using Windows.UI.Core;
using FlygoApp.Annotations;
using FlyGoWebService.Models;
using Microsoft.AspNet.SignalR.Client;

namespace FlygoApp.ViewModels
{
    public class WorkerViewModel : INotifyPropertyChanged
    {
        private string _ankomst;
        private string _afgang;
        private string _flyruteNummer;

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


        public HubConnection HubConnection { get; set; }
        public IHubProxy proxy { get; set; }

        public WorkerViewModel()
        {
            HubConnection = new HubConnection("http://flygowebservice1.azurewebsites.net/");
            proxy = HubConnection.CreateHubProxy("OpgaveHub");
            HubConnection.Start();

            proxy.On<FlyRute>("Broadcast", OnMessage);
        }

        //public void GetBroadcast(FlyRute msg)
        //{
        //    proxy.On<FlyRute>("broadcastMessage", OnMessage);           
        //}

        private async void OnMessage(FlyRute msg)
        {
            await Windows.ApplicationModel.Core.CoreApplication.MainView.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                FlyruteNummer = msg.FlyRuteNummer;
                Ankomst = msg.Ankomst.ToString();
                Afgang = msg.Afgang.ToString();
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
