using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Views;

namespace FlygoApp.ViewModels
{
    public class AdminViewModel : INotifyPropertyChanged
    {
        #region instance fields
        private bool _openMenu;

        private ICommand _openMenuCommand;
        private ICommand _goToBrugerLoginPageCommand;
        private ICommand _goToFlyDataPageCommand;
        private ICommand _goToHangarDatePageCommand;
        private ICommand _logUdCommand;
       
        private Frame _currentFrame = new Frame();       
        private readonly NavigationService _navigationService;

        #endregion

        #region Properties
        public Frame CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                _currentFrame = value; 
                OnPropertyChanged();
            }
        }
        public bool OpenMenu
        {
            get { return _openMenu; }
            set
            {
                _openMenu = value;
                OnPropertyChanged();
            }
        }
        public ICommand GoToHangarDatePageCommand
        {
            get
            {
                return _goToHangarDatePageCommand ??
                       (_goToHangarDatePageCommand =
                           new RelayCommand(() => CurrentFrame.Navigate(typeof (HangarDataPage))));
            }
            set { _goToHangarDatePageCommand = value; }
        }
        public ICommand GoToFlyDataPageCommand
        {
            get
            {
                return _goToFlyDataPageCommand ??
                       (_goToFlyDataPageCommand = new RelayCommand(() => CurrentFrame.Navigate(typeof (FlyDataPage))));
            }
            set { _goToFlyDataPageCommand = value; }
        }
        public ICommand GoToBrugerLoginPageCommand
        {
            get
            {
                return _goToBrugerLoginPageCommand ??
                       (_goToBrugerLoginPageCommand =
                           new RelayCommand(() => CurrentFrame.Navigate(typeof (BrugerDataPage))));
            }
            set { _goToBrugerLoginPageCommand = value; }
        }
        public ICommand OpenMenuCommand
        {
            get { return _openMenuCommand ?? (_openMenuCommand = new RelayCommand(() => OpenMenu = !OpenMenu)); }
            set { _openMenuCommand = value; }
        }
        public ICommand LogUdCommand
        {
            get
            {
                return _logUdCommand ?? (_logUdCommand = new RelayCommand(() =>
                {
                    _navigationService.Navigate(typeof(LoginPage));
                }));
            }
            set { _logUdCommand = value; }
        }

        #endregion

        public AdminViewModel()
        {
            _navigationService = new NavigationService();
        }
        #region Metoder

        #endregion


        #region OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
        #endregion
    }
}
