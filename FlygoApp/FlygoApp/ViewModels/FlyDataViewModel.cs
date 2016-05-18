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
    public class FlyDataViewModel : INotifyPropertyChanged
    {
        private ICommand _opretFlyCommand;
        private ICommand _deleteFlyCommand;
        private int _selectedFlyIndex;
        private string _selectedFlyProducent;
        private string _selectedFlyType;
        private DtoFlySingleton _dtoFlySingleton;

        #region instance fields

        #endregion
        #region Properties

        public string FlyType { get; set; }
        public string FlyProducent { get; set; }
        public string SelectedFlyType
        {
            get { return _selectedFlyType; }
            set
            {
                _selectedFlyType = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Fly> Fly { get; set; }
        public string SelectedFlyProducent
        {
            get { return _selectedFlyProducent; }
            set
            {
                _selectedFlyProducent = value;
                OnPropertyChanged();
            }
        }

        public int SelectedFlyIndex
        {
            get { return _selectedFlyIndex; }
            set
            {
                _selectedFlyIndex = value;
                if (_selectedFlyIndex != -1)
                {
                    SelectedFlyType = Fly[_selectedFlyIndex].Type;
                    SelectedFlyProducent = Fly[_selectedFlyIndex].Producent;
                }
                OnPropertyChanged();
            }
        }

        public ICommand OpretFlyCommand
        {
            get { return _opretFlyCommand ?? (_opretFlyCommand = new RelayCommand(OpretFlyAsync)); }
            set { _opretFlyCommand = value; }
        }

        public ICommand DeleteFlyCommand
        {
            get { return _deleteFlyCommand ?? (_deleteFlyCommand = new RelayCommandWithParameter(DeleteFly)); }
            set { _deleteFlyCommand = value; }
        }

        #endregion



        #region Metoder
        public FlyDataViewModel()
        {
            _dtoFlySingleton = DtoFlySingleton.GetInstance();
            Fly = new ObservableCollection<Fly>();
            LoadDtoFly();
            
        }

        public async void OpretFlyAsync()
        {
            try
            {
                CheckException(FlyType, FlyProducent);
                Fly creatingfly = new Fly(FlyProducent,FlyType);
                _dtoFlySingleton.PostFly(creatingfly);
                Fly.Add(creatingfly);
                _dtoFlySingleton.LoadFly();
            }
            catch (ArgumentException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
        }

        public void CheckException(string type,string producent)
        {
            if(String.IsNullOrEmpty(type))
                throw new ArgumentException("Du mangler at indtaste en type");
            if(String.IsNullOrEmpty(producent))
                throw new ArgumentException("Du mangler at indtaste en producent");
        }
        public void DeleteFly(object param)
        {
            int id = (int) param;
            Fly selectedfly = Fly.Single(x => x.Id.Equals(id));
            _dtoFlySingleton.DeleteFly(id);
            Fly.Remove(selectedfly);
            _dtoFlySingleton.LoadFly();
        }

        public void LoadDtoFly()
        {
            foreach (var fly in _dtoFlySingleton.FlyListe)
            {
                Fly.Add(fly);
            }
        }
        #endregion


        #region propertychanged region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
