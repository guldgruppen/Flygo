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
        
        #region instance fields
        private readonly DtoStatistikSingleton _dtoStatistikSingleton;

        #endregion
        #region Properties
        public string AntalFejlSamlet { get; set; }
        public string BaggerFejl { get; set; }
        public string BaggerForsinket { get; set; }
        public string BaggersKlargøringer { get; set; }
        public string CatersFejl { get; set; }
        public string CatersForsinket { get; set; }
        public string CatersKlargøringer { get; set; }
        public string CrewFejl { get; set; }
        public string CrewForsinket { get; set; }
        public string CrewKlargøringer { get; set; }
        public string FuelersFejl { get; set; }
        public string FuelersForsinket { get; set; }
        public string FuelersKlargøringer { get; set; }
        public string KlargøringerIAlt { get; set; }
        public string MekanikerFejl { get; set; }
        public string MekanikerForsinket { get; set; }
        public string MekanikerKlargøringer { get; set; }
        public string RedcapFejl { get; set; }
        public string RedcapForsinket { get; set; }
        public string RedcapKlargøringer { get; set; }
        public string SamletForsinkelser { get; set; }
        public string RengøringFejl { get; set; }
        public string RengøringForsinket { get; set; }
        public string RengøringKlargøringer { get; set; }

        #endregion
        public StatistikHandler()
        {
            _dtoStatistikSingleton = DtoStatistikSingleton.GetInstance;
            InitStatistikData();
        }

        #region Metoder
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
        #endregion
    }
}
