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

        public async Task<int> GetAntalFejlSamlet()
        {
            string temp = await LoadSingle("api/Views/GetAntalFejlSamlet");
            return int.Parse(temp);
        }
        public async Task<int> GetBaggerFejl()
        {
            string temp = await LoadSingle("api/Views/GetBaggerFejl");
            return int.Parse(temp);
        }
        public async Task<int> GetBaggerForsinket()
        {
            string temp = await LoadSingle("api/Views/GetBaggerForsinket");
            return int.Parse(temp);
        }
        public async Task<int> GetBaggersKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetBaggersKlargøringer");
            return int.Parse(temp);
        }
        public async Task<int> GetCatersFejl()
        {
            string temp = await LoadSingle("api/Views/GetCatersFejl");
            return int.Parse(temp);
        }
        public async Task<int> GetCatersForsinket()
        {
            string temp = await LoadSingle("api/Views/GetCatersForsinket");
            return int.Parse(temp);
        }
        public async Task<int> GetCatersKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetCatersKlargøringer");
            return int.Parse(temp);
        }
        public async Task<int> GetCrewFejl()
        {
            string temp = await LoadSingle("api/Views/GetCrewFejl");
            return int.Parse(temp);
        }
        public async Task<int> GetCrewForsinket()
        {
            string temp = await LoadSingle("api/Views/GetCrewForsinket");
            return int.Parse(temp);
        }
        public async Task<int> GetCrewKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetCrewKlargøringer");
            return int.Parse(temp);
        }
        public async Task<int> GetFuelersFejl()
        {
            string temp = await LoadSingle("api/Views/GetFuelersFejl");
            return int.Parse(temp);
        }
        public async Task<int> GetFuelersForsinket()
        {
            string temp = await LoadSingle("api/Views/GetFuelersForsinket");
            return int.Parse(temp);
        }
        public async Task<int> GetFuelersKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetFuelersKlargøringer");
            return int.Parse(temp);
        }
        public async Task<int> GetKlargøringerIAlt()
        {
            string temp = await LoadSingle("api/Views/GetKlargøringerIAlt");
            return int.Parse(temp);
        }
        public async Task<int> GetMekanikerFejl()
        {
            string temp = await LoadSingle("api/Views/GetMekanikerFejl");
            return int.Parse(temp);
        }
        public async Task<int> GetMekanikerForsinket()
        {
            string temp = await LoadSingle("api/Views/GetMekanikerForsinket");
            return int.Parse(temp);
        }
        public async Task<int> GetMekanikerKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetMekanikerKlargøringer");
            return int.Parse(temp);
        }
        public async Task<int> GetRedcapFejl()
        {
            string temp = await LoadSingle("api/Views/GetRedcapFejl");
            return int.Parse(temp);
        }
        public async Task<int> GetRedcapForsinket()
        {
            string temp = await LoadSingle("api/Views/GetRedcapForsinket");
            return int.Parse(temp);
        }
        public async Task<int> GetRedcapKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetRedcapKlargøringer");
            return int.Parse(temp);
        }
        public async Task<int> GetRengøringFejl()
        {
            string temp = await LoadSingle("api/Views/GetRengøringFejl");
            return int.Parse(temp);
        }
        public async Task<int> GetRengøringForsinket()
        {
            string temp = await LoadSingle("api/Views/GetRengøringForsinket");
            return int.Parse(temp);
        }
        public async Task<int> GetRengøringKlargøringer()
        {
            string temp = await LoadSingle("api/Views/GetRengøringKlargøringer");
            return int.Parse(temp);
        }
        public async Task<int> GetSamletForsinkelser()
        {
            string temp = await LoadSingle("api/Views/GetSamletForsinkelser");
            return int.Parse(temp);
        }
    }
}
