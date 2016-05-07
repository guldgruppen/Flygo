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

namespace FlygoApp.ViewModels
{
    public class RedcapViewModel
    {
        #region instance fields
        private RedcapHandler handler;
        private ICommand _searchCommand; 
        #endregion
        #region Properties

        public DateTimeOffset DateTimeNow { get; set; }
        public string FlyRuteNr { get; set; }

        public DateTimeOffset Date { get; set; }

        public ICommand SearchCommand
        {
            get { return _searchCommand ?? (new RelayCommand(SearchAsync)); }
            set { _searchCommand = value; }
        }

        #endregion
        public RedcapViewModel()
        {
            handler = new RedcapHandler();
            DateTimeNow = DateTimeOffset.Now;
        }
        #region Metoder
        public async void SearchAsync()
        {
            DateTime tempt = DateTime.Parse(Date.ToString());
            try
            {
                handler.SearchForFlyRute(FlyRuteNr, tempt);
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
