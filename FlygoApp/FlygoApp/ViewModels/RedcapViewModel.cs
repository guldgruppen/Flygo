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
        public DateTimeOffset DateTimeNow { get; set; }
        public string FlyRuteNr { get; set; }

        public DateTimeOffset Date { get; set; }

        public ICommand SearchCommand
        {
            get { return _searchCommand ?? (new RelayCommand(Search)); }
            set { _searchCommand = value; }
        }

        private RedcapHandler handler;
        private ICommand _searchCommand;


        public RedcapViewModel()
        {
            handler = new RedcapHandler();
            DateTimeNow = DateTimeOffset.Now;
        }

        public async void Search()
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
    }
}
