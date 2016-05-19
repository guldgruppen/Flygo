using System.Windows.Input;
using Windows.UI.Popups;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using System;

namespace FlygoApp.ViewModels
{
    public class LoginViewModel
    {
        #region instance fields
        private readonly LoginHandler _handler;
        private ICommand _goToHomePageCommand;
        #endregion
        #region Properties
        public string BrugerNavn { get; set; }
        public string Kodeord { get; set; }


        public ICommand GoToHomePageCommand
        {
            get { return _goToHomePageCommand ?? (_goToHomePageCommand = new RelayCommand(Login)); }
            set { _goToHomePageCommand = value; }
        }

        #endregion

        public LoginViewModel()
        {
            WindowStyling.WindowAndTitleBarStyling();
            _handler = new LoginHandler();
        }

        #region Metoder
        public async void Login()
        {
            MessageDialog messageDialog;
            try
            {
                _handler.CheckLoginInfo(BrugerNavn, Kodeord);
            }
            catch (NullOrEmptyException ex)
            {
                messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }
            catch (InfoWrongException ex)
            {
                messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
            }


        }

        #endregion
    }
}
