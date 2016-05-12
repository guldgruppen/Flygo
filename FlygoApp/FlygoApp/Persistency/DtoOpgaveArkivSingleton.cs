using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;
using FlygoApp.Models;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class DtoOpgaveArkivSingleton
    {
        public List<OpgaveArkiv> OpgaveArkivListe { get; set; } = new List<OpgaveArkiv>();

        private static DtoOpgaveArkivSingleton _dtoOpgave;

        private DtoOpgaveArkivSingleton()
        {
            LoadOpgaveArkiv();
        }

        public static DtoOpgaveArkivSingleton GetInstance()
        {
            return _dtoOpgave ?? (_dtoOpgave = new DtoOpgaveArkivSingleton());
        }

        public async Task PostOpgaveArkiv(OpgaveArkiv opg)
        {
            const string serverUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
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

        public async void LoadOpgaveArkiv()
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
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }

        public async void UpdateOpgaveArkiv(OpgaveArkiv arkiv, int id)
        {
            const string serverUrl = "http://flygowebservice1.azurewebsites.net/";

            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };

            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                   await client.PutAsJsonAsync("api/OpgaveArkivs/PutOpgaveArkiv/"+id, arkiv);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
