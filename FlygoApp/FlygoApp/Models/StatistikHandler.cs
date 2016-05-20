using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Persistency;

namespace FlygoApp.Models
{
    public class StatistikHandler
    {       
        private readonly DtoStatistikSingleton _dtoStatistikSingleton;

        public int AntalFejlSamlet { get; set; }
        public int BaggerFejl { get; set; }
        public int BaggerForsinket { get; set; }
        public int BaggersKlargøringer { get; set; }
        public int CatersFejl { get; set; }
        public int CatersForsinket { get; set; }
        public int CatersKlargøringer { get; set; }
        public int CrewFejl { get; set; }
        public int CrewForsinket { get; set; }
        public int CrewKlargøringer { get; set; }
        public int FuelersFejl { get; set; }
        public int FuelersForsinket { get; set; }
        public int FuelersKlargøringer { get; set; }
        public int KlargøringerIAlt { get; set; }
        public int MekanikerFejl { get; set; }
        public int MekanikerForsinket { get; set; }
        public int MekanikerKlargøringer { get; set; }
        public int RedcapFejl { get; set; }
        public int RedcapForsinket { get; set; }
        public int RedcapKlargøringer { get; set; }
        public int SamletForsinkelser { get; set; }
        public int RengøringFejl { get; set; }
        public int RengøringForsinket { get; set; }
        public int RengøringKlargøringer { get; set; }
        public StatistikHandler()
        {
            _dtoStatistikSingleton = DtoStatistikSingleton.GetInstance;
            InitStatistikData();
        }

        public async void InitStatistikData()
        {
            AntalFejlSamlet = await _dtoStatistikSingleton.GetAntalFejlSamlet();
            BaggerFejl = await _dtoStatistikSingleton.GetBaggerFejl();
            BaggerForsinket = await _dtoStatistikSingleton.GetBaggerForsinket();
            BaggersKlargøringer = await _dtoStatistikSingleton.GetBaggersKlargøringer();
            CatersFejl = await _dtoStatistikSingleton.GetCatersFejl();
            CatersForsinket = await _dtoStatistikSingleton.GetCatersForsinket();
            CatersKlargøringer = await _dtoStatistikSingleton.GetCatersKlargøringer();
            CrewFejl = await _dtoStatistikSingleton.GetCrewFejl();
            CrewForsinket = await _dtoStatistikSingleton.GetCrewForsinket();
            CrewKlargøringer = await _dtoStatistikSingleton.GetCrewKlargøringer();
            KlargøringerIAlt = await _dtoStatistikSingleton.GetKlargøringerIAlt();
            FuelersFejl = await _dtoStatistikSingleton.GetFuelersFejl();
            FuelersForsinket = await _dtoStatistikSingleton.GetFuelersForsinket();
            FuelersKlargøringer = await _dtoStatistikSingleton.GetFuelersKlargøringer();
            MekanikerFejl = await _dtoStatistikSingleton.GetMekanikerFejl();
            MekanikerForsinket = await _dtoStatistikSingleton.GetMekanikerForsinket();
            MekanikerKlargøringer = await _dtoStatistikSingleton.GetMekanikerKlargøringer();
            SamletForsinkelser = await _dtoStatistikSingleton.GetSamletForsinkelser();
            RedcapFejl = await _dtoStatistikSingleton.GetRedcapFejl();
            RedcapForsinket = await _dtoStatistikSingleton.GetRedcapForsinket();
            RedcapKlargøringer = await _dtoStatistikSingleton.GetRedcapKlargøringer();
            RengøringFejl = await _dtoStatistikSingleton.GetRengøringFejl();
            RengøringForsinket = await _dtoStatistikSingleton.GetRengøringForsinket();
            RengøringKlargøringer = await _dtoStatistikSingleton.GetRengøringKlargøringer();
        }
    }
}
