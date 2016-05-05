using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public class DtoHangarSingleton
    {
        public List<Hangar> HangarListe { get; set; } = new List<Hangar>();
        private static DtoHangarSingleton _dtoHangar;


        private DtoHangarSingleton()
        {
            Loadhangar();
        }


        public static DtoHangarSingleton GetInstance()
        {
            return _dtoHangar ?? (_dtoHangar = new DtoHangarSingleton());
        }

        public async void Loadhangar()
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

                    var response = client.GetAsync("api/Hangars/GetHangar").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Hangar> hangardata = response.Content.ReadAsAsync<IEnumerable<Hangar>>().Result;
                        HangarListe.Clear();
                        foreach (var h in hangardata)
                        {
                            HangarListe.Add(h);
                        }

                    }
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }

        }

        public async void PostHangar(Hangar hangar)
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
                   await client.PostAsJsonAsync("api/Hangars/PostHangar/",hangar);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }

        }

        public async void DeleteHangar(int id)
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
                    await client.DeleteAsync("api/Hangars/DeleteHangar/"+id);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }

        }
    }
}



