using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlygoApp.Models;
using FlyGoWebService;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class DTOSingleton
    {
        public List<Fly> FlyListe = new List<Fly>();
        public List<FlyRute> FlyruteListe;
        public List<Hangar> HangarListe = new List<Hangar>();
        public List<OpgaveArkiv> OpgaveArkivListe = new List<OpgaveArkiv>(); 
        public Dictionary<string, BrugerLogIn> BrugerLogInsDictionary = new Dictionary<string, BrugerLogIn>();
        private static DTOSingleton Singleton;
        private DTOSingleton()
        {
            FlyruteListe = new List<FlyRute>();
            LoadFly();
            Loadhangar();
            LoadBrugerLogins();
            Loadflyrute();
            LoadOpgaveArkiv();
        }
        public static DTOSingleton GetInstance()
        {

            return Singleton ?? (Singleton = new DTOSingleton());
        }
        public void LoadOpgaveArkiv()
        {
            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {

                    var response = client.GetAsync("api/OpgaveArkivs/GetOpgaveArkiv").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<OpgaveArkiv> opgavearkivdata = response.Content.ReadAsAsync<IEnumerable<OpgaveArkiv>>().Result;
                        foreach (var opg in opgavearkivdata)
                        {
                            OpgaveArkivListe.Add(opg);
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public void LoadFly()
        {

            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {

                    var response = client.GetAsync("api/Flies/GetFly").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Fly> flydata = response.Content.ReadAsAsync<IEnumerable<Fly>>().Result;
                        foreach (var F in flydata)
                        {
                            FlyListe.Add(F);
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }
        public void Loadflyrute()
        {
            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/FlyRutes/GetFlyRute").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<FlyRute> flyrutedata = response.Content.ReadAsAsync<IEnumerable<FlyRute>>().Result;
                        FlyruteListe.Clear();
                        foreach (var Fly in flyrutedata)
                        {
                            FlyruteListe.Add(Fly);
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public int LoadIdFromFlyrute()
        {
            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {

                    var response = client.GetAsync("api/Flyrutes/GetId").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        int Hangardata = response.Content.ReadAsAsync<int>().Result;
                        return Hangardata;
                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return -1;
        }
        public void Loadhangar()
        {
            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {

                    var response = client.GetAsync("api/Hangars/GetHangar").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Hangar> Hangardata = response.Content.ReadAsAsync<IEnumerable<Hangar>>().Result;
                        foreach (var H in Hangardata)
                        {
                            HangarListe.Add(H);
                        }

                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }
        public void LoadBrugerLogins()
        {
            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var response = client.GetAsync("api/BrugerLogIns/GetBrugerLogIn").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<BrugerLogIn> brugerLogin =
                            response.Content.ReadAsAsync<IEnumerable<BrugerLogIn>>().Result;
                        foreach (var bruger in brugerLogin)
                        {
                            BrugerLogInsDictionary.Add(bruger.BrugerNavn, new BrugerLogIn(bruger.Password, bruger.RoleId));
                        }

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }
        public async Task PostFlyRuter(FlyRute rute)
        {

            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {                 
                   await client.PostAsJsonAsync("api/FlyRutes/PostFlyRute", rute);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
        public async void DeleteFlyrute(int id)
        {

            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    await client.DeleteAsync("api/flyRutes/DeleteFlyRute" + id);                   
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
        public async Task PostOpgaveArkiv(OpgaveArkiv opg)
        {
            const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    await client.PostAsJsonAsync("api/OpgaveArkivs/PostOpgaveArkiv", opg);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

    }
}