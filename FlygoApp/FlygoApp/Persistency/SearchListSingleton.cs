using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class SearchListSingleton
    {
        public List<FlyRute> RedcapFlyRuteList; 

        private static SearchListSingleton _instance;

        private SearchListSingleton()
        {
            RedcapFlyRuteList = new List<FlyRute>();
        }

        public static SearchListSingleton GetInstance()
        {
            return _instance ?? (_instance = new SearchListSingleton()); 
        }

    }
}
