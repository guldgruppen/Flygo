using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
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
        private Flyopgave _selectedFlyopgave;
        private OpgaveAdapter _opgaveAdapter;
        private readonly DispatcherTimer _timer = new DispatcherTimer();
        private Uri _imageSource;
        private OpgaveArkiv _selectedOpgaveArkiv;
        public StatistikHandler StatistikHandler { get; set; }

        private int _selectedFlyIndex;
        private int _selectedHangarIndex;
        private int _selectedOpgaveIndex = -1;
        private string _selectedFlyopgaveNummerDetail;
        private string _selectedHangarDetail;
        private string _selectedFlyDetail;
        private string _selectedAnkomstDetail;
        private string _selectedAfgangDetail;
        private string _selectedMekanikerDetails;
        private string _selectedCatersDetails;
        private string _selectedCrewDetails;
        private string _selectedFulersDetails;
        private string _selectedBaggersDetails;
        private string _selectedCountdown;
        private string _flyopgaveNr;

        private ICommand _deleteOpgaveCommand;
        private ICommand _createFlyopgaveCommand;
        #endregion
        #region Properties         
        public HubConnection Conn { get; set; }
        public IHubProxy Proxy { get; set; }
        public DateTimeOffset Now { get; set; }
        public FlyHandler FlyHandler { get; set; }
        public HangarHandler HangarHandler { get; set; }
        public FlyopgaveHandler FlyopgaveHandler { get; set; }
        public string FlyopgaveNr
        {
            get { return _flyopgaveNr; }
            set
            {
                _flyopgaveNr = value;
                bool match = Regex.IsMatch(value, @"^[a-zA-Z]{2}\d{3,4}$");
                ImageSource = match ? new Uri("ms-appx:///Assets/1461516027_accepted_48.png") : new Uri("ms-appx:///Assets/1461516030_cancel_48.png");
                OnPropertyChanged();
            }
        }
        public DateTimeOffset AfgangDato { get; set; }
        public DateTimeOffset AnkomstDato { get; set; }
        public TimeSpan AfgangTid { get; set; }
        public TimeSpan AnkomstTid { get; set; }
        public DateTimeOffset MinYear { get; set; }
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
            set
            {
                _opgaveAdapter = value;
                OnPropertyChanged();
            }
        }
        public Flyopgave SelectedFlyopgave
        {
            get { return _selectedFlyopgave; }
            set
            {
                _selectedFlyopgave = value;
                OnPropertyChanged();
            }
        }
        public OpgaveArkiv SelectedOpgaveArkiv
        {
            get { return _selectedOpgaveArkiv; }
            set
            {
                _selectedOpgaveArkiv = value;
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
        public string SelectedFlyopgaveNummerDetail
        {
            get { return _selectedFlyopgaveNummerDetail; }
            set
            {
                _selectedFlyopgaveNummerDetail = value;
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

        //Bruges til at opdatere alt data i højre side af GUI, der er baseret på valget af flyopgave i venstre side.
        public int SelectedOpgaveIndex
        {
            get { return _selectedOpgaveIndex; }
            set
            {
                _selectedOpgaveIndex = value;
                try
                {
                    if (_selectedOpgaveIndex >= 0)
                    {
                        SelectedFlyopgaveNummerDetail =
                            FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].FlyopgaveNummer;
                        SelectedAfgangDetail = FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].AfgangSomText;
                        SelectedAnkomstDetail = FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].AnkomstSomText;
                        int hangarId = FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].HangarId;
                        int flyId = FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].FlyId;
                        SelectedHangarDetail = HangarHandler.Hangar.Single(x => x.Id.Equals(hangarId)).ToString();
                        SelectedFlyDetail = FlyHandler.Fly.Single(x => x.Id.Equals(flyId)).ToString();

                        DateTime til = FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].Afgang;
                        TimeSpan span = til - DateTime.Now;
                        SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");
                        SelectedFlyopgave = FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex];
                        SelectedOpgaveArkiv =
                            FlyopgaveHandler.OpgaveArkivs.SingleOrDefault(
                                x => x.FlyopgaveId.Equals(FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].Id));
                        OpgaveAdapter = new OpgaveAdapter(SelectedOpgaveArkiv, SelectedFlyopgave);
                        SelectedMekanikerDetails = SelectedOpgaveArkiv.Mekanikker.ToString();
                        SelectedBaggersDetails = SelectedOpgaveArkiv.Baggers.ToString();
                        SelectedCatersDetails = SelectedOpgaveArkiv.Caters.ToString();
                        SelectedCrewDetails = SelectedOpgaveArkiv.Crew.ToString();
                        SelectedFulersDetails = SelectedOpgaveArkiv.Fuelers.ToString();

                        CountdownToDeadline();

                    }
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
                OnPropertyChanged();
            }
        }
        public ICommand CreateFlyopgaveCommand
        {
            get
            {
                return _createFlyopgaveCommand ?? (_createFlyopgaveCommand = new RelayCommand<Object>((create) =>
          {
              CreateFlyopgave();
          }));
            }
            set { _createFlyopgaveCommand = value; }
        }
        //public ICommand DeleteOpgaveCommand
        //{
        //    get { return _deleteOpgaveCommand ?? (_deleteOpgaveCommand = new RelayArgCommand(DeleteOpgave)); }
        //    set { _deleteOpgaveCommand = value; }
        //}
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

            FlyopgaveHandler = new FlyopgaveHandler();
            FlyopgaveHandler.LoadDtoFlyopgaver();

            StatistikHandler = new StatistikHandler();

            MinYear = DateTimeOffset.Now;
            SelectedHangarIndex = -1;
            SelectedFlyIndex = -1;


        }
        #region Metoder

        //Laver en timer der viser deadline
        public void CountdownToDeadline()
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            _timer.Tick += MyTimer_Tick;
            _timer.Start();
        }

        // Fortæller hvad skal opdateres for hvert tick i CountdownToDeadline metoden
        public void MyTimer_Tick(object o, object sender)
        {
            DateTime til = FlyopgaveHandler.Flyopgaver[_selectedOpgaveIndex].Afgang;
            TimeSpan span = til - DateTime.Now;
            SelectedCountdown = DateTime.Now >= til ? "færdig" : span.ToString(@"dd\.hh\:mm\:ss");
        }
        public async void DeleteOpgave(object param)
        {
            try
            {
                string flyopgave = (string)param;
                Flyopgave tempFlyopgave = FlyopgaveHandler.Flyopgaver.First(x => x.FlyopgaveNummer.Equals(flyopgave));
                if (tempFlyopgave != null)
                {
                    var myMessageDialog =
                        new MessageDialog("Er du sikker på at slette Flyopgaven: " + tempFlyopgave.FlyopgaveNummer,
                            "Sletning af Flyopgave");
                    myMessageDialog.Commands.Add(new UICommand("YES", command =>
                    {

                        int id = tempFlyopgave.Id;
                        FlyopgaveHandler.DtoFlyopgave.DeleteFlyopgave(id);
                        FlyopgaveHandler.Flyopgaver.Remove(tempFlyopgave);
                        FlyopgaveHandler.DtoFlyopgave.LoadFlyopgave();

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
        public async void CreateFlyopgave()
        {

            try
            {
                int flyId = FlyHandler.Fly[SelectedFlyIndex].Id;
                int hangarId = HangarHandler.Hangar[SelectedHangarIndex].Id;
                DateTime fra = DateAndTimeConverter(AnkomstDato, AnkomstTid);
                DateTime til = DateAndTimeConverter(AfgangDato, AfgangTid);

                FlyopgaveHandler.Add(til, fra, flyId, hangarId, FlyopgaveNr);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                await new MessageDialog("Intet fly eller hangar valgt. Vælg venligst dette!").ShowAsync(); 

            }



        }
        //Convertere input fra tilføj flyrute til en datetime.
        public DateTime DateAndTimeConverter(DateTimeOffset dato, TimeSpan tid)
        {
            return new DateTime(dato.Year, dato.Month, dato.Day, tid.Hours, tid.Minutes, 0).AddHours(1);
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
