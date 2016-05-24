using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public class DtoStatistikSingleton : DataTransferBase<int>
    {
        private static DtoStatistikSingleton _dtoStatistikSingleton;
        public int Tester { get; set; }
        public static DtoStatistikSingleton GetInstance
            => _dtoStatistikSingleton ?? (_dtoStatistikSingleton = new DtoStatistikSingleton());

        private DtoStatistikSingleton()
        {

        }

        public async Task<string> GetAntalFejlSamlet()
        {
            string temp = await LoadSingle("api/Views/GetAntalFejlSamlet");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetBaggerFejl()
        {
            string temp = await LoadSingle("api/Views/GetBaggerFejl");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetBaggerForsinket()
        {
            string temp = await LoadSingle("api/Views/GetBaggersForsinket");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetBaggersKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetBaggersKlargøringer");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetCatersFejl()
        {
            string temp = await LoadSingle("api/Views/GetCatersFejl");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetCatersForsinket()
        {
            string temp = await LoadSingle("api/Views/GetCatersForsinket");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetCatersKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetCatersKlargøringer");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetCrewFejl()
        {
            string temp = await LoadSingle("api/Views/GetCrewFejl");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<String> GetCrewForsinket()
        {
            string temp = await LoadSingle("api/Views/GetCrewForsinket");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";           
        }
        public async Task<string> GetCrewKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetCrewKlargøringer");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetFuelersFejl()
        {
            string temp = await LoadSingle("api/Views/GetFuelersFejl");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetFuelersForsinket()
        {
            string temp = await LoadSingle("api/Views/GetFuelersForsinket");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetFuelersKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetFuelersKlargøringer");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetKlargøringerIAlt()
        {
            string temp = await LoadSingle("api/Views/GetKlargøringerIAlt");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetMekanikerFejl()
        {
            string temp = await LoadSingle("api/Views/GetMekanikerFejl");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetMekanikerForsinket()
        {
            string temp = await LoadSingle("api/Views/GetMekanikerForsinket");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetMekanikerKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetMekanikerKlargøringer");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetRedcapFejl()
        {
            string temp = await LoadSingle("api/Views/GetRedcapFejl");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetRedcapForsinket()
        {
            string temp = await LoadSingle("api/Views/GetRedcapForsinket");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetRedcapKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetRedcapKlargøringer");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetRengøringFejl()
        {
            string temp = await LoadSingle("api/Views/GetRengøringFejl");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetRengøringForsinket()
        {
            string temp = await LoadSingle("api/Views/GetRengøringForsinket");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetRengøringKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetRengøringKlargøringer");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
        public async Task<string> GetSamletForsinkelser()
        {
            string temp = await LoadSingle("api/Views/GetSamletForsinkelser");
            return (!String.IsNullOrEmpty(temp)) ? temp : "0";
        }
    }
}
