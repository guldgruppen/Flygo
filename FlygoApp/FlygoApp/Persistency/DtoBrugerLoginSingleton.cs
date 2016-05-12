using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlyGoWebService;

namespace FlygoApp.Persistency
{
    public class DtoBrugerLoginSingleton
    {
        public Dictionary<string, BrugerLogIn> BrugerLogInsDictionary = new Dictionary<string, BrugerLogIn>();

        private static DtoBrugerLoginSingleton _dtoBrugerLogin;
        private DtoBrugerLoginSingleton()
        {
           LoadBrugerLogins(); 
        }

        public static DtoBrugerLoginSingleton GetInstance()
        {
            return _dtoBrugerLogin ?? (_dtoBrugerLogin = new DtoBrugerLoginSingleton());
        }

        public async void LoadBrugerLogins()
        {
            const string serverUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler {UseDefaultCredentials = true};

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
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
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }

        }

        public async void PostBrugerLogin(BrugerLogIn login)
        {
            const string serverUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler {UseDefaultCredentials = true};

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    await client.PostAsJsonAsync("api/BrugerLogins/PostBrugerLogIn",login);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async void DeleteBrugerLogin(int id)
        {
            const string serverUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler {UseDefaultCredentials = true};

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    await client.DeleteAsync("/api/BrugerLogIns/DeleteBrugerLogIn/" + id);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
