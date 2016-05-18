using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using FlygoApp.Views;

namespace FlygoApp.ViewModels
{
    public class RedcapViewModel
    {
        #region instance fields
        private SøgFlyOpgaveHandler handler;
        private ICommand _searchCommand;
        private ICommand _logoutCommand;
        private NavigationService _navigationService;
        #endregion
        #region Properties

        public DateTimeOffset DateTimeNow { get; set; }
        public string FlyopgaveNr { get; set; }

        public DateTimeOffset Date { get; set; }

        public ICommand SearchCommand
        {
            get { return _searchCommand ?? (new RelayCommand(SearchAsync)); }
            set { _searchCommand = value; }
        }

        public ICommand LogoutCommand
        {
            get
            {
                return _logoutCommand ?? (_logoutCommand = new RelayCommand(() => { _navigationService.Navigate(typeof(LoginPage)); }));
            }
            set { _logoutCommand = value; }
        }
      
        #endregion
        public RedcapViewModel()
        {
            _navigationService = new NavigationService();
            handler = new SøgFlyOpgaveHandler();
            DateTimeNow = DateTimeOffset.Now;
        }
        #region Metoder
        public async void SearchAsync()
        {
            DateTime tempt = DateTime.Parse(Date.ToString());
            try
            {
                handler.SearchForFlyopgave(FlyopgaveNr, tempt);
            }
            catch (NullOrEmptyException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            catch (InfoWrongException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            catch (DateWrongException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }



        } 
        #endregion
    }
}
