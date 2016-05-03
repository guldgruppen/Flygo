using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;
using FlygoApp.Persistency;
using FlyGoWebService.Models;

namespace FlygoApp.ViewModels
{
    public class RedcapTaskListViewModel
    {
        private SearchListSingleton s; 

        public ObservableCollection<FlyRute> ObservableCollection { get; set; }

        public RedcapTaskListViewModel()
        {
           
            ObservableCollection = new ObservableCollection<FlyRute>();
            s = SearchListSingleton.GetInstance();
            AddToCollection();
            ObservableCollection.Add(new FlyRute(DateTime.Today, DateTime.Today, 4, 5, "aa1234"));
        }

        public void AddToCollection()
        {
            foreach (var rute in s.RedcapFlyRuteList)
            {
                ObservableCollection.Add(rute);
            }
        }


    }
}
