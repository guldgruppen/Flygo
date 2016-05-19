using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class DtoRolesSingleton
    {
        private static DtoRolesSingleton _dtoRoles;
        public List<Roles> RolesListe { get; set; } = new List<Roles>();
        private DtoRolesSingleton()
        {
            LoadRoles();
        }

        public static DtoRolesSingleton GetInstance()
        {
            return _dtoRoles ?? (_dtoRoles = new DtoRolesSingleton());
        }
        public async void LoadRoles()
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
                    var response = client.GetAsync("api/Roles/GetRoles").Result;

                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<Roles> roller = response.Content.ReadAsAsync<IEnumerable<Roles>>().Result;
                        
                        foreach (var rolle in roller)
                        {
                            RolesListe.Add(rolle);
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
