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
        private int _selectedIndex;
        private string _selectedFlyruteNr;
        private string _selectedDestinationFra;
        private string _selectedDestinationTil;
        private ICommand _deleteOpgaveCommand;
        private string _selectedDateTimeFra;
        private string _selectedDateTimeTil;
        private DateTime _now;

        #endregion
        #region Properties
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
                        SelectedDateTimeTil = FlyruteRegisterProp.Flyruter[_selectedIndex].Afgang.ToString("mm/dd/yyyy HH:mm");
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
        public void DeleteOpgave(object param)
        {
            string flyrute = (string)param;
            Flyrute tempFlyrute = FlyruteRegisterProp.Flyruter.First(x => x.FlyruteNr.Equals(flyrute));
            if (tempFlyrute != null)
            {
                FlyruteRegisterProp.Flyruter.Remove(tempFlyrute);
            }
            FlyruteRegisterProp.Flyruter.Clear();
            FlyrutePersistency.SaveFlyruteAsJsonAsync(FlyruteRegisterProp.Flyruter);
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
