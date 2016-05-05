using System.Windows.Input;
using Windows.Security.Cryptography.Core;
using Windows.UI.Popups;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using System;

namespace FlygoApp.ViewModels
{
    public class LoginViewModel
    {
        public string BrugerNavn { get; set; }
        public string Kodeord { get; set; }

        private LoginHandler _handler; 

        private ICommand _goToHomePageCommand;
        public ICommand GoToHomePageCommand
        {
            get { return _goToHomePageCommand ?? (_goToHomePageCommand = new RelayCommand((() => Login()))); }
            set { _goToHomePageCommand = value; }   
        }

        public LoginViewModel()
        {
            WindowStyling.WindowAndTitleBarStyling();
            _handler = new LoginHandler();
        }

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
        

        

    }
}
