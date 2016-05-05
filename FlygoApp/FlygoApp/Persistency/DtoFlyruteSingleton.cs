using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class DtoFlyruteSingleton
    {
        public List<FlyRute> FlyruteListe { get; set; } = new List<FlyRute>();
        private static DtoFlyruteSingleton _dtoFlyrute;
        private DtoFlyruteSingleton()
        {
            Loadflyrute();
        }

        public static DtoFlyruteSingleton GetInstance()
        {
            return _dtoFlyrute ?? (_dtoFlyrute = new DtoFlyruteSingleton());
        }

        public async Task PostFlyRuter(FlyRute rute)
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

            const string serverUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler {UseDefaultCredentials = true};

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    await client.DeleteAsync("api/flyRutes/DeleteFlyRute" + id);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async void Loadflyrute()
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
                    var response = client.GetAsync("api/FlyRutes/GetFlyRute").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<FlyRute> flyrutedata = response.Content.ReadAsAsync<IEnumerable<FlyRute>>().Result;
                        FlyruteListe.Clear();
                        foreach (var fly in flyrutedata)
                        {
                            FlyruteListe.Add(fly);
                        }

                    }
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }


    }
}
