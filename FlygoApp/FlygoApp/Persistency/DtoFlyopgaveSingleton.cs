using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public sealed class DtoFlyopgaveSingleton : DataTransferBase<Flyopgave>
    {
        public List<Flyopgave> FlyopgaveListe { get; set; } = new List<Flyopgave>();

        public static DtoFlyopgaveSingleton GetInstance => _dtoFlyopgave ?? (_dtoFlyopgave = new DtoFlyopgaveSingleton());

        private static DtoFlyopgaveSingleton _dtoFlyopgave;
        private DtoFlyopgaveSingleton()
        {
            LoadFlyopgave();          
        }

        public async Task PostFlyopgaver(Flyopgave rute)
        {
            await Post(rute, "api/Flyopgaves/PostFlyopgave");
        }

        public async Task DeleteFlyopgave(int id)
        {
            await Delete(id, "api/Flyopgaves/DeleteFlyopgave/");            
        }

        public async Task LoadFlyopgave()
        {
            await Load(FlyopgaveListe, "api/Flyopgaves/GetFlyopgave");
        }


    }
}
