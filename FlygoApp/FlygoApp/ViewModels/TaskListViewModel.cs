using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Models;
using FlygoApp.Persistency;

namespace FlygoApp.ViewModels
{
    public class TaskListViewModel : INotifyPropertyChanged
    {
        private int _selectedIndex;

        #region Instance Fields

        private ICommand _createFlyruteCommand;
        private ICommand _showCommand;

        #endregion

        #region Properties         

        public DateTimeOffset Now { get; set; }
        #endregion

        public FlyHandler FlyHandler { get; set; }

        public HangarHandler HangarHandler { get; set; }

        public FlyruteHandler FlyruteHandler { get; set; }

        public string FlyruteNr { get; set; }
        public Fly Fly { get; set; } = new Fly("Airbus","B111");
        public Hangar Hangar { get; set; } = new Hangar(2,"D3");
        public DateTimeOffset Afgang { get; set; }
        public DateTimeOffset Ankomst { get; set; }

        public ICommand CreateFlyruteCommand
        {
            get { return _createFlyruteCommand ?? (_createFlyruteCommand = new RelayCommand(CreateFlyrute)); }
            set { _createFlyruteCommand = value; }
        }

        public ICommand ShowCommand
        {
            get
            {
                return _showCommand ?? (_showCommand = new RelayCommand(() =>
                { new MessageDialog(FlyruteHandler.Flyruter.Count.ToString()).ShowAsync(); }
                    ));
            }
            set { _showCommand = value; }
        }

        public TaskListViewModel()
        {

            FlyHandler = new FlyHandler();  
            FlyHandler.LoadDtoFly();

            HangarHandler = new HangarHandler();
            HangarHandler.LoadDtoHangar();

            FlyruteHandler = new FlyruteHandler();
            FlyruteHandler.Flyruter.Add(new Flyrute(DateTime.Now,DateTime.Now,Fly,"Virker",Hangar));


        }
        
        

        #region Metoder

        public void CreateFlyrute()
        {
             
                FlyruteHandler.Add(DateTime.Now, DateTime.Now, Hangar, Fly, FlyruteNr);

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
