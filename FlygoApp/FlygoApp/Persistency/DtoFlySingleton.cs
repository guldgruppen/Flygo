using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public class DtoFlySingleton
    {
        public List<Fly> FlyListe { get; set; } = new List<Fly>();
        private static DtoFlySingleton _dtoFly;
        private DtoFlySingleton()
        {
            LoadFly();
        }

        public static DtoFlySingleton GetInstance()
        {
            return _dtoFly ?? (_dtoFly = new DtoFlySingleton());

        }

        public async void LoadFly()
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

                    var response = client.GetAsync("api/Flies/GetFly").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Fly> flydata = response.Content.ReadAsAsync<IEnumerable<Fly>>().Result;
                        foreach (var f in flydata)
                        {
                            FlyListe.Add(f);
                        }

                    }
                }
                catch (Exception ex)
                {
                   await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async void PostFly(Fly fly)
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
                    await client.PostAsJsonAsync("api/flies/PostFly", fly);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async void DeleteFly(int id)
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

                    await client.DeleteAsync("api/Flies/DeleteFly/" + id);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
