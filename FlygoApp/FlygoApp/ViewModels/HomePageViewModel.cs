using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using FlygoApp.Annotations;
using FlygoApp.Commons;

namespace FlygoApp.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        #region Instance Fields
        private ICommand _openMenuCommand;
        private bool _menuOpen;

        #endregion
        #region Properties
        public ICommand OpenMenuCommand
        {
            get
            {
                return _openMenuCommand ?? (_openMenuCommand = new RelayCommand(() =>
                { MenuOpen = !MenuOpen; }
                ));
            }
            set { _openMenuCommand = value; }
        }
        public bool MenuOpen
        {
            get { return _menuOpen; }
            set
            {
                _menuOpen = value;
                OnPropertyChanged();
            }
        }

        public HomePageViewModel()
        {
            
        }
        #endregion
        #region Notify Changed Region
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
