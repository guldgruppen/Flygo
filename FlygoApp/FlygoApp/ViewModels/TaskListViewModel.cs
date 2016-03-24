using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Models;
using FlygoApp.Persistency;

namespace FlygoApp.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        #region Instance Fields
        private ICommand _tilføjRuteCommand;
        private ICommand _testCommand;
        private int _selectedIndex = -1;
        private string _selectedFlyruteNr;
        private string _selectedDestinationFra;
        private string _selectedDestinationTil;
        private ICommand _deleteOpgaveCommand;
        private string _selectedDateTimeFra;
        private string _selectedDateTimeTil;
        private DateTime _now;
        private ICommand _changeFlyruteCommand;

        #endregion
        #region Properties
        DispatcherTimer Timer = new DispatcherTimer();
        public string FlyruteNr { get; set; }
        public string Flytype { get; set; }
        public string DestinationFra { get; set; }
        public string DestinationTil { get; set; }
        public TimeSpan TidFra { get; set; }
        public TimeSpan TidTil { get; set; }
        public DateTimeOffset DatoFra { get; set; }
        public DateTimeOffset DatoTil { get; set; }
        public ICommand TilføjRuteCommand
        {
            get { return _tilføjRuteCommand ?? (_tilføjRuteCommand = new RelayCommand(AddOpgave)); }
            set { _tilføjRuteCommand = value; }
        }
        public ICommand TestCommand
        {
            get
            {
                return _testCommand ?? (_testCommand = new RelayCommand(() =>
                {
                    new MessageDialog(FlyruteRegisterProp.Flyruter.Count.ToString()).ShowAsync();
                }));
            }
            set { _testCommand = value; }
        }

        public ICommand ChangeFlyruteCommand
        {
            get { return _changeFlyruteCommand ?? (_changeFlyruteCommand = new RelayCommand(UpdateFlyrute)); }
            set { _changeFlyruteCommand = value; }
        }
        public ICommand DeleteOpgaveCommand
        {
            get { return _deleteOpgaveCommand ?? (_deleteOpgaveCommand = new RelayCommandWithParameter(DeleteOpgave)); }
            set { _deleteOpgaveCommand = value; }
        }
        public ObservableCollection<Flyrute> FlyruterOC { get; set; }     
        public FlyruteRegister FlyruteRegisterProp { get; set; }
        public string SelectedFlyruteNr
        {
            get { return _selectedFlyruteNr; }
            set
            {
                _selectedFlyruteNr = value;
                OnPropertyChanged();
            }
        }
        public string SelectedDestinationFra
        {
            get { return _selectedDestinationFra; }
            set
            {
                _selectedDestinationFra = value;
                OnPropertyChanged();
            }
        }
        public string SelectedDestinationTil
        {
            get { return _selectedDestinationTil; }
            set
            {
                _selectedDestinationTil = value;
                OnPropertyChanged();
            }
        }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                if (value != -1)
                {              
                        SelectedDestinationFra = FlyruteRegisterProp.Flyruter[_selectedIndex].DestinationFra;
                        SelectedDestinationTil = FlyruteRegisterProp.Flyruter[_selectedIndex].DestinationTil;
                        SelectedFlyruteNr = FlyruteRegisterProp.Flyruter[_selectedIndex].FlyruteNr;
                        SelectedDateTimeFra = FlyruteRegisterProp.Flyruter[_selectedIndex].Ankomst.ToString("MM/dd/yyyy HH:mm");
                        SelectedDateTimeTil = FlyruteRegisterProp.Flyruter[_selectedIndex].Afgang.ToString("MM/dd/yyyy HH:mm");
                 }
                OnPropertyChanged();
                
            }
        }
        public string SelectedDateTimeFra
        {
            get { return _selectedDateTimeFra; }
            set
            {
                _selectedDateTimeFra = value;
                OnPropertyChanged();
            }
        }
        public string SelectedDateTimeTil
        {
            get { return _selectedDateTimeTil; }
            set
            {
                _selectedDateTimeTil = value;
                OnPropertyChanged();
            }
        }

        public DateTimeOffset Now { get; set; }
        #endregion
        public TaskListViewModel()
        {
            FlyruteRegisterProp = new FlyruteRegister(this);       
            LoadMovie();
            Now = DateTime.Now;
            CountdownToDeadline();           
        }
        public async void UpdateFlyrute()
        {
            if (SelectedIndex != -1)
            {
                Flyrute updateFlyrute = FlyruteRegisterProp.Flyruter[SelectedIndex];
                var MyMessageDialog = new MessageDialog("Er du sikker på at opdatere flyruten: " + updateFlyrute.FlyruteNr, "Opdatere flyrute");
                MyMessageDialog.Commands.Add(new UICommand("YES", command =>
                {
                    updateFlyrute.FlyruteNr = SelectedFlyruteNr;
                    updateFlyrute.DestinationFra = SelectedDestinationFra;
                    updateFlyrute.DestinationTil = SelectedDestinationTil;
                    updateFlyrute.Afgang = DateTime.Parse(SelectedDateTimeTil);
                    updateFlyrute.Ankomst = DateTime.Parse(SelectedDateTimeFra);
                    FlyrutePersistency.SaveFlyruteAsJsonAsync(FlyruteRegisterProp.Flyruter);
                }));
                MyMessageDialog.Commands.Add(new UICommand("NO", command => { }));
                await MyMessageDialog.ShowAsync();

            }
        }
        #region Metoder
        public async void LoadMovie()
        {
            var loadedMovies = await FlyrutePersistency.LoadFlyruteFromJsonAsync();
            if (loadedMovies != null)
            {
                FlyruteRegisterProp.Flyruter.Clear();
                foreach (var film in loadedMovies)
                {
                    FlyruteRegisterProp.Flyruter.Add(film);
                }
            }
        }
        public async void AddOpgave()
        {
            try
            {
                DateTime fra = DateAndTimeConverter(DatoFra, TidFra);
                DateTime til = DateAndTimeConverter(DatoTil, TidTil);
                Flyrute tempFlyrute = new Flyrute(FlyruteNr, Flytype, fra, til, DestinationFra, DestinationTil);
                FlyruteRegisterProp.AddFlyrute(tempFlyrute);           
                FlyrutePersistency.SaveFlyruteAsJsonAsync(FlyruteRegisterProp.Flyruter);
                await new MessageDialog("Flyrute tilføjet").ShowAsync();
            }
            catch (ArgumentException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }
        public DateTime DateAndTimeConverter(DateTimeOffset dato, TimeSpan tid)
        {
            return new DateTime(dato.Year, dato.Month, dato.Day, tid.Hours, tid.Minutes, 0);
        }
        public async void DeleteOpgave(object param)
        {
            string flyrute = (string)param;
            Flyrute tempFlyrute = FlyruteRegisterProp.Flyruter.First(x => x.FlyruteNr.Equals(flyrute));
            if (tempFlyrute != null)
            {
                var MyMessageDialog = new MessageDialog("Er du sikker på at slette flyruten: "+tempFlyrute.FlyruteNr,"Sletning af flyrute");
                MyMessageDialog.Commands.Add(new UICommand("YES", command =>
                {
                    FlyruteRegisterProp.Flyruter.Remove(tempFlyrute);
                }));
                MyMessageDialog.Commands.Add(new UICommand("NO", command => { }));
                await MyMessageDialog.ShowAsync();
               
            }
            FlyrutePersistency.SaveFlyruteAsJsonAsync(FlyruteRegisterProp.Flyruter);
        }
        public void CountdownToDeadline()
        {
            Timer.Interval = new TimeSpan(0,0,0,1,0);
            Timer.Tick += MyTimer_Tick;
            Timer.Start();
        }

        public void MyTimer_Tick(object o, object sender)
        {
            foreach (var flyrute in FlyruteRegisterProp.Flyruter)
            {
                var now = DateTimeOffset.Parse(DateTimeOffset.Now.ToString("T"));
                var deadline = DateTime.Parse(flyrute.Afgang.ToString("T"));
                var span = deadline - now;
                flyrute.DatePart = flyrute.Afgang.ToString("yyyy MMMM dd");
                flyrute.TimePart = span.ToString();
            }
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
