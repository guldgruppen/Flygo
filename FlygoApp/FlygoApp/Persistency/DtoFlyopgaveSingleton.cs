using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class DtoFlyopgaveSingleton : DataTransferBase<Flyopgave>
    {
        public List<Flyopgave> FlyopgaveListe { get; set; } = new List<Flyopgave>();

        private static DtoFlyopgaveSingleton _dtoFlyopgave;
        private DtoFlyopgaveSingleton()
        {
            LoadFlyopgave();          
        }

        public static DtoFlyopgaveSingleton GetInstance()
        {
            return _dtoFlyopgave ?? (_dtoFlyopgave = new DtoFlyopgaveSingleton());
        }

        public void PostFlyopgaver(Flyopgave rute)
        {
            Post(rute, "api/Flyopgaves/PostFlyopgave");
        }

        public void DeleteFlyopgave(int id)
        {
            Delete(id, "api/Flyopgaves/DeleteFlyopgave");            
        }

        public  void LoadFlyopgave()
        {
            Load(FlyopgaveListe, "api/Flyopgaves/GetFlyopgave");
        }


    }
}
