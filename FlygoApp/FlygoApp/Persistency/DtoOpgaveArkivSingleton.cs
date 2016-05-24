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

        public async Task PostOpgaveArkiv(OpgaveArkiv opg)
        {
            await Post(opg, "api/OpgaveArkivs/PostOpgaveArkiv");           
        }

        public async Task LoadOpgaveArkiv()
        {
            await Load(OpgaveArkivListe, "api/OpgaveArkivs/GetOpgaveArkiv");
        }

        //skal laves til base klasse
        public async Task UpdateOpgaveArkiv(OpgaveArkiv arkiv, int id)
        {
            await Update(arkiv,id, "api/OpgaveArkivs/PutOpgaveArkiv/");                        
        }
    }
}
