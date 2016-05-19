using System;
using System.Windows.Input;
using Windows.UI.Popups;
using FlygoApp.Commons;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using FlygoApp.Views;

namespace FlygoApp.ViewModels
{
    public class WorkerViewModel
    {
        #region instance fields
        private readonly SøgFlyOpgaveHandler _handler;
        private ICommand _searchCommand;
        private ICommand _logoutCommand;
        private readonly NavigationService _navigationService;
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
        public WorkerViewModel()
        {
            _navigationService = new NavigationService();
            _handler = new SøgFlyOpgaveHandler();
            DateTimeNow = DateTimeOffset.Now;
        }
        #region Metoder
        public async void SearchAsync()
        {
            DateTime tempt = DateTime.Parse(Date.ToString());
            try
            {
                _handler.SearchForFlyopgave(FlyopgaveNr, tempt);
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
