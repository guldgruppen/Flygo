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
        public int Tester { get; set; }
        public static DtoStatistikSingleton GetInstance
            => _dtoStatistikSingleton ?? (_dtoStatistikSingleton = new DtoStatistikSingleton());

        private DtoStatistikSingleton()
        {
            GetAntanFejlSamlet();
        }

        public async Task GetAntanFejlSamlet()
        {
            string temp = await LoadSingle("api/Views/GetAntalFejlSamlet");
            Tester = int.Parse(temp);
        }
    }
}
