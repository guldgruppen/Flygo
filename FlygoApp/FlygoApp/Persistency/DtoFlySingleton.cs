using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public class DtoFlySingleton : DataTransferBase<Fly>
    {
        public List<Fly> FlyListe { get; set; } = new List<Fly>();

        private static DtoFlySingleton _dtoFly;

        public static DtoFlySingleton GetInstance => _dtoFly ?? (_dtoFly = new DtoFlySingleton());
        

        private DtoFlySingleton()
        {
            LoadFly();
        }


        public void LoadFly()
        {
            Load(FlyListe, "api/Flies/GetFly");           
        }

        public void PostFly(Fly fly)
        {
            Post(fly, "api/flies/PostFly");            
        }

        public void DeleteFly(int id)
        {
            Delete(id, "api/Flies/DeleteFly/");           
        }
    }
}
