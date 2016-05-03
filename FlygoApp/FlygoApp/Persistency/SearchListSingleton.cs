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
        public FlyRute FlyRute;

        private static SearchListSingleton _instance;

        private SearchListSingleton()
        {
            FlyRute = new FlyRute();
        }

        public static SearchListSingleton GetInstance()
        {
            return _instance ?? (_instance = new SearchListSingleton());
        }

    }
}
