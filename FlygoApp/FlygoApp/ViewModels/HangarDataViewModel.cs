using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Popups;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Models;
using FlygoApp.Persistency;

namespace FlygoApp.ViewModels
{
    public class HangarDataViewModel : INotifyPropertyChanged
    {

        #region instance fields

        private DtoHangarSingleton _dtoHangar;
        private ICommand _deleteHangarCommand;
        private int _selectedIndex = -1;
        private ICommand _insertHangarCommand;
        private string _selectedHangarNavn;
        private string _insertHangarNavn;

        #endregion
        
        #region Properties

        public ObservableCollection<Hangar> Hangars { get; set; }
        public string Placering { get; set; }
        

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                
                if(_selectedIndex >= 0)
                    SelectedHangarNavn = Hangars[_selectedIndex].Placering;
                OnPropertyChanged();
            }
        }

        public string SelectedHangarNavn
        {
            get { return _selectedHangarNavn; }
            set
            {
                _selectedHangarNavn = value;
                OnPropertyChanged();
            }
        }

        public string InsertHangarNavn
        {
            get { return _insertHangarNavn; }
            set
            {
                _insertHangarNavn = value;
                OnPropertyChanged();
            }
        }

        public ICommand DeleteHangarCommand
        {
            get { return _deleteHangarCommand ?? (_deleteHangarCommand = new RelayCommandWithParameter(DeleteHangarAsync)); }
            set { _deleteHangarCommand = value; }
        }

        public ICommand UpdateHangarCommand { get; set; }

        public ICommand InsertHangarCommand
        {
            get { return _insertHangarCommand ?? (_insertHangarCommand = new RelayCommand(InsertHangar)); }
            set { _insertHangarCommand = value; }
        }

        #endregion

        public HangarDataViewModel()
        {
            _dtoHangar = DtoHangarSingleton.GetInstance();
            Hangars = new ObservableCollection<Hangar>();
            LoadHangarData();
        }
        #region Metoder

        public void InsertHangar()
        {
            if (String.IsNullOrEmpty(InsertHangarNavn))
            {
                throw new ArgumentException("Du mangler at indtaste en hangar");
            }
            Hangar hangar = new Hangar(InsertHangarNavn);
            Hangars.Add(hangar);
            _dtoHangar.PostHangar(hangar);          
            _dtoHangar.Loadhangar();
        }

        public async void DeleteHangarAsync(object param)
        {
            try
            {
                int hangarId = (int) param;
                Hangar hangar = Hangars.Single(x => x.Id.Equals(hangarId));

                if (hangar != null)
                {
                    var myMessageDialog =
                        new MessageDialog("Er du sikker på at slette hangar: " + hangar.Placering);
                    myMessageDialog.Commands.Add(new UICommand("YES", command =>
                    {
                        _dtoHangar.DeleteHangar(hangarId);                                             
                        Hangars.Remove(hangar);
                        _dtoHangar.Loadhangar();

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
        
        public void LoadHangarData()
        {
            Hangars.Clear();
            _dtoHangar.Loadhangar();
            Hangars = _dtoHangar.HangarListe.ToObservableCollection();
            
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
