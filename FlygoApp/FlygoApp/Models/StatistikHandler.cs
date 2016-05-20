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
        public int TestResult { get; set; }
        public StatistikHandler()
        {
            var dtoStatistikSingleton = DtoStatistikSingleton.GetInstance;
            TestResult = dtoStatistikSingleton.Tester;
        }
    }
}
