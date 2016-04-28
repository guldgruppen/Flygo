using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using FlygoApp.Persistency;
using FlygoApp.Views;

namespace FlygoApp.ViewModels
{
    public class LoginViewModel
    {
        public string BrugerNavn { get; set; }
        public string Kodeord { get; set; }

        private LoginHandler handler; 

        private ICommand _goToHomePageCommand;
        public ICommand GoToHomePageCommand
        {
            get { return _goToHomePageCommand ?? (_goToHomePageCommand = new RelayCommand((() => Login()))); }
            set { _goToHomePageCommand = value; }   
        }

        public LoginViewModel()
        {
            WindowStyling.WindowAndTitleBarStyling();
            handler = new LoginHandler();
        }

        public void Login()
        {
            try
            {
                handler.CheckLoginInfo(BrugerNavn, Kodeord);
            }
            catch (LoginIsNullOrEmptyException ex)
            {
                new MessageDialog(ex.Message).ShowAsync();
            }
            catch (LoginInfoWrongException ex)
            {
                new MessageDialog(ex.Message).ShowAsync(); 
            }
        }
        

        

    }
}
