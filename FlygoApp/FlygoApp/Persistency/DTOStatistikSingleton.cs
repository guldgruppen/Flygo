using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public class DtoStatistikSingleton : DataTransferBase<int>
    {
        private static DtoStatistikSingleton _dtoStatistikSingleton;
        public List<int> StatistikListe = new List<int>(); 
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
