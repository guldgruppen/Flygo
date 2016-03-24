using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.ApplicationModel.VoiceCommands;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using FlygoApp.Annotations;
using FlygoApp.Commons;
using FlygoApp.Views;

namespace FlygoApp.ViewModels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        #region Instance Fields
        private ICommand _openMenuCommand;
        private bool _menuOpen;
        private Frame _currentFrame;
        private ICommand _goToStatistikPageCommand;
        private NavigationService _navService;
        private ICommand _goToTilføjFlyrutePageCommand;
        private ICommand _goToIndstillingerPageCommand;
        private ICommand _logudCommand;
        private ICommand _goToHomePageCommand;

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
        public ICommand GoToStatistikPageCommand
        {
            get
            {
                return _goToStatistikPageCommand ?? (_goToStatistikPageCommand = new RelayCommand(() =>
                {
                    _currentFrame.Navigate(typeof (StatistikPage));
                }));
            }
            set { _goToStatistikPageCommand = value; }
        }
        public ICommand GoToTilføjFlyrutePageCommand
        {
            get
            {
                return _goToTilføjFlyrutePageCommand ?? (_goToTilføjFlyrutePageCommand = new RelayCommand(() =>
                {
                    _currentFrame.Navigate(typeof (TilføjFlyrutePage));
                }));
            }
            set { _goToTilføjFlyrutePageCommand = value; }
        }
        public ICommand GoToIndstillingerPageCommand
        {
            get
            {
                return _goToIndstillingerPageCommand ?? (_goToIndstillingerPageCommand = new RelayCommand(() =>
                {
                    _currentFrame.Navigate(typeof (IndstillingerPage));
                }));
            }
            set { _goToIndstillingerPageCommand = value; }
        }
        public ICommand GoToTaskListPageCommand
        {
            get
            {
                return _goToHomePageCommand ?? (_goToHomePageCommand = new RelayCommand(() =>
                {
                    _currentFrame.Navigate(typeof (TaskListPage));
                }));
            }
            set { _goToHomePageCommand = value; }
        }
        public ICommand LogudCommand
        {
            get
            {
                return _logudCommand ??
                       (_logudCommand = new RelayCommand(() => { _navService.Navigate(typeof (LoginPage)); }));
            }
            set { _logudCommand = value; }
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
        public Frame CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                _currentFrame = value;
                OnPropertyChanged();
            }
        }

        #endregion
        public HomePageViewModel()
        {
            WindowStyling.WindowAndTitleBarStyling();
            CurrentFrame = new Frame();
            CurrentFrame.Navigate(typeof(TaskListPage));
            _navService = new NavigationService();
            
        }
     
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
