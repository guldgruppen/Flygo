using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public sealed class DtoHangarSingleton : DataTransferBase<Hangar>
    {
        public List<Hangar> HangarListe { get; set; } = new List<Hangar>();
        private static DtoHangarSingleton _dtoHangar;

        public static DtoHangarSingleton GetInstance => _dtoHangar ?? (_dtoHangar = new DtoHangarSingleton());
        private DtoHangarSingleton()
        {
            Loadhangar();
        }


        public void Loadhangar()
        {
            Load(HangarListe, "api/Hangars/GetHangar");           
        }

        public void PostHangar(Hangar hangar)
        {
            Post(hangar, "api/Hangars/PostHangar/");           
        }

        public void DeleteHangar(int id)
        {
            Delete(id, "api/Hangars/DeleteHangar/");                       
        }
    }
}



