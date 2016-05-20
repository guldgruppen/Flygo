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
        private DtoStatistikSingleton _dtoStatistikSingleton;
        public int TestResult { get; set; }
        public StatistikHandler()
        {
            _dtoStatistikSingleton = DtoStatistikSingleton.GetInstance;
            TestResult = _dtoStatistikSingleton.StatistikListe[0].Result;
        }
    }
}
