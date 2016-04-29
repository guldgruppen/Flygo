using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;
using FlyGoWebService;

namespace FlygoApp.Persistency
{
    public class DTOSingleton
    {

        public List<Fly> FlyListe = new List<Fly>();
        public List<Flyrute> FlyruteListe = new List<Flyrute>();
        public List<Hangar> HangarListe = new List<Hangar>();
        public Dictionary<string, string> BrugerLogInsDictionary = new Dictionary<string, string>();
        private static DTOSingleton Singleton;

        private DTOSingleton()
        {
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

                    var response = client.GetAsync("api/flys").Result;

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

                    var response = client.GetAsync("api/FlyRutes").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Flyrute> flyrutedata = response.Content.ReadAsAsync<IEnumerable<Flyrute>>().Result;
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

        public
            void LoadDestination()
        {

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

                    var response = client.GetAsync("api/BrugerLogIns").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<BrugerLogIn> brugerLogin =
                            response.Content.ReadAsAsync<IEnumerable<BrugerLogIn>>().Result;
                        foreach (var bruger in brugerLogin)
                        {
                            BrugerLogInsDictionary.Add(bruger.BrugerNavn, bruger.Password);
                        }

                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        public async void PostFlyRuter(Flyrute rute)
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
                    throw;
                }
            }
        }


    }
}