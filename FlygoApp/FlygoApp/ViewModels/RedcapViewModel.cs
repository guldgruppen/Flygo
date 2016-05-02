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
using FlyGoWebService.Models;

namespace FlygoApp.ViewModels
{
    public class RedcapViewModel
    {
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
            
            

        }


    }
}