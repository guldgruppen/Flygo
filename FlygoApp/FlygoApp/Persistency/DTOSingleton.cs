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
        public Dictionary<string, BrugerLogIn> BrugerLogInsDictionary = new Dictionary<string, BrugerLogIn>();
        private static DTOSingleton Singleton;

        private DTOSingleton()
        {
            FlyruteListe = new List<FlyRute>();
            LoadFly();
            Loadhangar();
            LoadBrugerLogins();
            Loadflyrute();
        }

        public static DTOSingleton GetInstance()
        {

            return Singleton ?? (Singleton = new DTOSingleton());
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

                    var response = client.GetAsync("api/Flies").Result;

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

                    var response = client.GetAsync("api/FlyRutes/GetFlyrute").Result;

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

                    var response = client.GetAsync("api/Hangars").Result;

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

                    var response = client.GetAsync("api/BrugerLogIns/GetBrugerLogin").Result;

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

        public async void PostFlyRuter(FlyRute rute)
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
                   await client.PostAsJsonAsync("api/FlyRutes",rute);
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
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
                    await client.DeleteAsync("api/flyRutes/" + id);                   
                }
                catch (Exception ex)
                {
                    new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

    }
}