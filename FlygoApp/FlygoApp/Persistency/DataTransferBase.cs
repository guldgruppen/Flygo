using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;

namespace FlygoApp.Persistency
{
    public abstract class DataTransferBase<T> : IDataTransfer<T>
    {
        const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";
        private readonly HttpClientHandler _handler = new HttpClientHandler { UseDefaultCredentials = true };
        public async void HttpClientHeaderInfo(Action<HttpClient> action)
        {
            using (var client = new HttpClient(_handler))
            {
                client.BaseAddress = new Uri(ServerUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    action(client);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
        public virtual void Load(List<T> listToAdd, string url)
        {
            HttpClientHeaderInfo(client =>
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    IEnumerable<T> loadeddata = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                    listToAdd.Clear();
                    listToAdd.AddRange(loadeddata);
                }
            });
        }      
        public virtual void Post(T type,string url)
        {
            HttpClientHeaderInfo(async client =>
            {               
                await client.PostAsJsonAsync(url, type);
            });
        }
        public virtual void Delete(int id,string url)
        {          
            HttpClientHeaderInfo(async client =>
            {
                await client.DeleteAsync(url + id);
            });
        }
    }
}
