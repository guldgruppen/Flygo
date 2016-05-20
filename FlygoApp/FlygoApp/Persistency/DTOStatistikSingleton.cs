using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public class DtoStatistikSingleton : DataTransferBase<Statistik>
    {
        private static DtoStatistikSingleton _dtoStatistikSingleton;
        public List<Statistik> StatistikListe = new List<Statistik>(); 
        public static DtoStatistikSingleton GetInstance
            => _dtoStatistikSingleton ?? (_dtoStatistikSingleton = new DtoStatistikSingleton());

        private DtoStatistikSingleton()
        {
            GetAntanFejlSamlet();
        }

        public void GetAntanFejlSamlet()
        {
            Load(StatistikListe, "api/Views/GetAntalFejlSamlet");
        }
    }
}
