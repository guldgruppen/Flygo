using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using FlygoApp.Exceptions;
using FlygoApp.Models;
using FlygoApp.Persistency;
using FlyGoWebService.Models;

namespace FlygoApp.ViewModels
{
    public class RedcapViewModel
    {
        public string FlyRuteNr { get; set; }

        public DateTime DateTime { get; set; }

        private RedcapHandler handler;


        public RedcapViewModel()
        {
            handler = new RedcapHandler();
        }

        public async void Search()
        {
            try
            {
                handler.SearchForFlyRute(FlyRuteNr, DateTime);
            }
            catch (NullOrEmptyException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync(); 
            }
            
        }


    }
}