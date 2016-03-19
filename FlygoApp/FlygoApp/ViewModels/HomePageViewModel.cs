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

        public Frame CurrentFrame
        {
            get { return _currentFrame; }
            set
            {
                _currentFrame = value;
                OnPropertyChanged();
            }
        }

        public HomePageViewModel()
        {
            WindowAndTitleBarStyling();
            CurrentFrame = new Frame();
            CurrentFrame.Navigate(typeof (TaskListPage));
        }

        
        public void WindowAndTitleBarStyling()
        {

            var view = ApplicationView.GetForCurrentView();
            view.TitleBar.BackgroundColor = Color.FromArgb(100, 99, 89, 89);
            view.TitleBar.ButtonBackgroundColor = Color.FromArgb(100, 99, 89, 89);
            view.TitleBar.ForegroundColor = Colors.WhiteSmoke;

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
