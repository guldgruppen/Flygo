using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlyGoWebService;

namespace FlygoApp.Persistency
{
    public sealed class DtoBrugerLoginSingleton : DataTransferBase<BrugerLogIn>
    {
        public Dictionary<string, BrugerLogIn> BrugerLogInsDictionary { get; set; } = new Dictionary<string, BrugerLogIn>();

        private static DtoBrugerLoginSingleton _dtoBrugerLogin;
        public static DtoBrugerLoginSingleton GetInstance => _dtoBrugerLogin ?? (_dtoBrugerLogin = new DtoBrugerLoginSingleton());
        private DtoBrugerLoginSingleton()
        {
           LoadBrugerLogins(); 
        }

        public  void LoadBrugerLogins()
        {
            List<BrugerLogIn> temp = new List<BrugerLogIn>();           
            Load(temp,"api/BrugerLogIns/GetBrugerLogIn");
            BrugerLogInsDictionary = ConvertListToDictionary(temp);            
        }
        public Dictionary<string,BrugerLogIn> ConvertListToDictionary(List<BrugerLogIn> brugerlogins)
        {
            return brugerlogins.ToDictionary(bruger => bruger.BrugerNavn, bruger => new BrugerLogIn(bruger.Password, bruger.RoleId));          
        }      
        public void PostBrugerLogin(BrugerLogIn login)
        {
            Post(login,"api/BrugerLogins/PostBrugerLogIn");            
        }
        public void DeleteBrugerLogin(int id)
        {
            Delete(id, "/api/BrugerLogIns/DeleteBrugerLogIn/");           
        }
    }
}
