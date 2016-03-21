using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using FlygoApp.Commons;
using FlygoApp.Views;

namespace FlygoApp.ViewModels
{
    public class LoginViewModel
    {
        private NavigationService _navService;
        private ICommand _goToHomePageCommand;
        public ICommand GoToHomePageCommand
        {
            get { return _goToHomePageCommand ?? (_goToHomePageCommand = new RelayCommand(() => _navService.Navigate(typeof(HomePage)))); }
            set { _goToHomePageCommand = value; }
        }

        public LoginViewModel()
        {
            WindowStyling.WindowAndTitleBarStyling();
            _navService = new NavigationService();
        }
    }
}
