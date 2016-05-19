using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public sealed class DtoOpgaveArkivSingleton : DataTransferBase<OpgaveArkiv>
    {
        public List<OpgaveArkiv> OpgaveArkivListe { get; set; } = new List<OpgaveArkiv>();

        private static DtoOpgaveArkivSingleton _dtoOpgave;
        public static DtoOpgaveArkivSingleton GetInstance => _dtoOpgave ?? (_dtoOpgave = new DtoOpgaveArkivSingleton());
        private DtoOpgaveArkivSingleton()
        {
            LoadOpgaveArkiv();
        }

        public void PostOpgaveArkiv(OpgaveArkiv opg)
        {
            Post(opg, "api/OpgaveArkivs/PostOpgaveArkiv");           
        }

        public void LoadOpgaveArkiv()
        {
            Load(OpgaveArkivListe, "api/OpgaveArkivs/GetOpgaveArkiv");
        }

        //skal laves til base klasse
        public void UpdateOpgaveArkiv(OpgaveArkiv arkiv, int id)
        {
            Update(arkiv,id, "api/OpgaveArkivs/PutOpgaveArkiv/");                        
        }
    }
}
