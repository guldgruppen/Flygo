using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class DtoFlyopgaveSingleton
    {
        public List<Flyopgave> FlyopgaveListe { get; set; } = new List<Flyopgave>();
        private static DtoFlyopgaveSingleton _dtoFlyopgave;
        private DtoFlyopgaveSingleton()
        {
            LoadFlyopgave();
        }

        public static DtoFlyopgaveSingleton GetInstance()
        {
            return _dtoFlyopgave ?? (_dtoFlyopgave = new DtoFlyopgaveSingleton());
        }

        public async Task PostFlyopgaver(Flyopgave rute)
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
                    await client.PostAsJsonAsync("api/Flyopgaves/PostFlyopgave", rute);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async void DeleteFlyopgave(int id)
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
                    await client.DeleteAsync("api/Flyopgaves/DeleteFlyopgave" + id);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async void LoadFlyopgave()
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
                    var response = client.GetAsync("api/Flyopgaves/GetFlyopgave").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Flyopgave> flyopgavedata = response.Content.ReadAsAsync<IEnumerable<Flyopgave>>().Result;
                        FlyopgaveListe.Clear();
                        foreach (var fly in flyopgavedata)
                        {
                            FlyopgaveListe.Add(fly);
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
