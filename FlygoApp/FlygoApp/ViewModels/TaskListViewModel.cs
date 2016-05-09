using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Models;
using FlyGoWebService.Models;
using Microsoft.AspNet.SignalR.Client;

namespace FlygoApp.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {

        #region Instance Fields

        private ICommand _createFlyruteCommand;
        private int _selectedFlyIndex;
        private int _selectedHangarIndex;
        private ICommand _deleteOpgaveCommand;
        private int _selectedOpgaveIndex = -1;
        private string _selectedFlyruteNummerDetail;
        private string _selectedHangarDetail;
        private string _selectedFlyDetail;
        private string _selectedAnkomstDetail;
        private string _selectedAfgangDetail;
        private string _selectedMekanikerDetails;
        private string _selectedCatersDetails;
        private string _selectedCrewDetails;
        private string _selectedFulersDetails;
        private string _selectedBaggersDetails;
        private FlyRute _selectedFlyrute;
        private OpgaveAdapter _opgaveAdapter;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private string _selectedCountdown;
        private ICommand _sendOpgaveCommand;
        private string _flyruteNr;
        private Uri _imageSource;

        #endregion
        #region Properties         

        public DateTimeOffset Now { get; set; }
        public FlyHandler FlyHandler { get; set; }
        public HangarHandler HangarHandler { get; set; }
        public FlyruteHandler FlyruteHandler { get; set; }

        public string FlyruteNr
        {
            get { return _flyruteNr; }
            set
            {
                _flyruteNr = value;
                bool match = Regex.IsMatch(value, @"^[a-zA-Z]{2}\d{3,4}$");
                if (match)
                {
                    ImageSource = new Uri("ms-appx:///Assets/1461516027_accepted_48.png");
                }
                else
                {
                    ImageSource = new Uri("ms-appx:///Assets/1461516030_cancel_48.png");
                }
                OnPropertyChanged();
            }
        }

        public DateTimeOffset AfgangDato { get; set; }
        public DateTimeOffset AnkomstDato { get; set; }
        public TimeSpan AfgangTid { get; set; }
        public TimeSpan AnkomstTid { get; set; }
        public DateTimeOffset MinYear { get; set; } = DateTime.Now;

        public Uri ImageSource
        {
            get { return _imageSource; }
            set
            {
                _imageSource = value;
                OnPropertyChanged();
            }
        }

        public string SelectedMekanikerDetails
        {
            get { return _selectedMekanikerDetails; }
            set
            {
                _selectedMekanikerDetails = value;
                OnPropertyChanged();
            }
        }
        public OpgaveAdapter OpgaveAdapter
        {
            get { return _opgaveAdapter; }
            set { _opgaveAdapter = value;OnPropertyChanged(); }
        }
        public FlyRute SelectedFlyrute
        {
            get { return _selectedFlyrute; }
            set
            {
                _selectedFlyrute = value;
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
        public int SelectedFlyIndex
        {
            get { return _selectedFlyIndex; }
            set
            {
                _selectedFlyIndex = value;
                OnPropertyChanged();
            }
        }
        public int SelectedHangarIndex
        {
            get { return _selectedHangarIndex; }
            set
            {
                _selectedHangarIndex = value;
                OnPropertyChanged();
            }
        }
        public string SelectedFlyruteNummerDetail
        {
            get { return _selectedFlyruteNummerDetail; }
            set
            {
                _selectedFlyruteNummerDetail = value;
                OnPropertyChanged();
            }
        }
        public string SelectedHangarDetail
        {
            get { return _selectedHangarDetail; }
            set
            {
                _selectedHangarDetail = value;
                OnPropertyChanged();
            }
        }
        public string SelectedFlyDetail
        {
            get { return _selectedFlyDetail; }
            set
            {
                _selectedFlyDetail = value;
                OnPropertyChanged();
            }
        }
        public string SelectedAnkomstDetail
        {
            get { return _selectedAnkomstDetail; }
            set
            {
                _selectedAnkomstDetail = value;
                OnPropertyChanged();
            }
        }
        public string SelectedAfgangDetail
        {
            get { return _selectedAfgangDetail; }
            set
            {
                _selectedAfgangDetail = value;
                OnPropertyChanged();
            }
        }
        public int SelectedOpgaveIndex
        {
            get { return _selectedOpgaveIndex; }
            set
            {
                _selectedOpgaveIndex = value;
                if (_selectedOpgaveIndex >= 0)
                {
                    SelectedFlyruteNummerDetail = FlyruteHandler.Flyruter[_selectedOpgaveIndex].FlyRuteNummer;
                    SelectedAfgangDetail = FlyruteHandler.Flyruter[_selectedOpgaveIndex].AfgangSomText;
                    SelectedAnkomstDetail = FlyruteHandler.Flyruter[_selectedOpgaveIndex].AnkomstSomText;
                    int hangarId = FlyruteHandler.Flyruter[_selectedOpgaveIndex].HangarId;
                    int flyId = FlyruteHandler.Flyruter[_selectedOpgaveIndex].FlyId;
                    SelectedHangarDetail = HangarHandler.Hangar.Single(x => x.Id.Equals(hangarId)).ToString();
                    SelectedFlyDetail = FlyHandler.Fly.Single(x => x.Id.Equals(flyId)).ToString();
             
                    DateTime til = FlyruteHandler.Flyruter[_selectedOpgaveIndex].Afgang;
                    TimeSpan span = til - DateTime.Now;
                    SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");

                    OpgaveArkiv selectedArkiv =
                        FlyruteHandler.OpgaveArkivs.Single(
                            x => x.FlyRuteId.Equals(FlyruteHandler.Flyruter[_selectedOpgaveIndex].Id));
                    SelectedMekanikerDetails = selectedArkiv.Mekanikker.ToString();
                    SelectedBaggersDetails = selectedArkiv.Baggers.ToString();
                    SelectedCatersDetails = selectedArkiv.Caters.ToString();
                    SelectedCrewDetails = selectedArkiv.Crew.ToString();
                    SelectedFulersDetails = selectedArkiv.Fuelers.ToString();
                    SelectedFlyrute = FlyruteHandler.Flyruter[_selectedOpgaveIndex];
                    OpgaveAdapter = new OpgaveAdapter(selectedArkiv,SelectedFlyrute);
                    CountdownToDeadline();

                }
                OnPropertyChanged();
            }
        }
        public ICommand CreateFlyruteCommand
        {
            get { return _createFlyruteCommand ?? (_createFlyruteCommand = new RelayCommand(CreateFlyrute)); }
            set { _createFlyruteCommand = value; }
        }

        public ICommand DeleteOpgaveCommand
        {
            get { return _deleteOpgaveCommand ?? (_deleteOpgaveCommand = new RelayCommandWithParameter(DeleteOpgave)); }
            set { _deleteOpgaveCommand = value; }
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
        #endregion
        public TaskListViewModel()
        {
            Conn = new HubConnection("http://flygowebservice1.azurewebsites.net/");
            Proxy = Conn.CreateHubProxy("OpgaveHub");
            Conn.Start();

            FlyHandler = new FlyHandler();  
            FlyHandler.LoadDtoFly();

            HangarHandler = new HangarHandler();
            HangarHandler.LoadDtoHangar();

            FlyruteHandler = new FlyruteHandler();
            FlyruteHandler.LoadDtoFlyruter();


        }
        public HubConnection Conn { get; set; }
        public IHubProxy Proxy { get; set; }
        public ICommand SendOpgaveCommand
        {
            get { return _sendOpgaveCommand ?? (_sendOpgaveCommand = new RelayCommand(Send)); }
            set { _sendOpgaveCommand = value; }
        }

        public void Send()
        {
            FlyRute rute = FlyruteHandler.Flyruter[_selectedOpgaveIndex];
            if (rute != null)
            {
                Proxy.Invoke("BroadcastOpgave", rute);
            }
        }

        #region Metoder

        public void CountdownToDeadline()
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _timer.Tick += MyTimer_Tick;
            _timer.Start();
        }
        
        public void MyTimer_Tick(object o, object sender)
        {
            DateTime til = FlyruteHandler.Flyruter[_selectedOpgaveIndex].Afgang;
            TimeSpan span = til - DateTime.Now;
            SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");
        }

        public async void DeleteOpgave(object param)
        {
            try
            {
                string flyrute = (string) param;
                FlyRute tempFlyrute = FlyruteHandler.Flyruter.First(x => x.FlyRuteNummer.Equals(flyrute));
                if (tempFlyrute != null)
                {
                    var myMessageDialog =
                        new MessageDialog("Er du sikker på at slette flyruten: " + tempFlyrute.FlyRuteNummer,
                            "Sletning af flyrute");
                    myMessageDialog.Commands.Add(new UICommand("YES", command =>
                    {
                        
                        int id = tempFlyrute.Id;
                        FlyruteHandler.DtoFlyrute.DeleteFlyrute(id);
                        FlyruteHandler.Flyruter.Remove(tempFlyrute);
                        FlyruteHandler.DtoFlyrute.Loadflyrute();
                        
                    }));
                    myMessageDialog.Commands.Add(new UICommand("NO", command => { }));
                    await myMessageDialog.ShowAsync();

                }
            }
            catch (Exception ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }

        }

        public void CreateFlyrute()
        {
            
            int flyId = FlyHandler.Fly[SelectedFlyIndex].Id;
            int hangarId = HangarHandler.Hangar[SelectedHangarIndex].Id;
            DateTime fra = DateAndTimeConverter(AnkomstDato, AnkomstTid);
            DateTime til = DateAndTimeConverter(AfgangDato,AfgangTid);
            FlyruteHandler.Add(til,fra,flyId,hangarId,FlyruteNr); 
            FlyruteHandler.DtoFlyrute.Loadflyrute(); 
        }
        public DateTime DateAndTimeConverter(DateTimeOffset dato, TimeSpan tid)
        {
            return new DateTime(dato.Year, dato.Month, dato.Day, tid.Hours, tid.Minutes, 0);
        }
        #endregion
        #region NotifyChange Region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
