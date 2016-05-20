using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace FlygoApp.Persistency
{
    public abstract class DataTransferBase<T> : IDataTransfer<T>
    {
        const string ServerUrl = "http://flygowebservice1.azurewebsites.net/";
        
        public void ClientHeaderInfo(HttpClient client)
        {
            client.BaseAddress = new Uri(ServerUrl);
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public virtual async void Load(List<T> listToAdd, string url)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        IEnumerable<T> loadeddata = response.Content.ReadAsAsync<IEnumerable<T>>().Result;
                        listToAdd.Clear();
                        listToAdd.AddRange(loadeddata);
                    }
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
        public virtual async Task<string> LoadSingle(string url)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    var response = client.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return response.Content.ReadAsStringAsync().Result;
                        
                    }
                    return String.Empty;
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
                
            }
            return String.Empty;
        }
        public virtual async void Post(T type,string url)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    await client.PostAsJsonAsync(url, type);

                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }

        }
        public virtual async void Delete(int id,string url)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    await client.DeleteAsync(url + id);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
        public virtual async void Update(T type, int id, string url)
        {
            HttpClientHandler handler = new HttpClientHandler { UseDefaultCredentials = true };
            using (var client = new HttpClient(handler))
            {
                ClientHeaderInfo(client);
                try
                {
                    await client.PutAsJsonAsync(url + id, type);
                }
                catch (Exception ex)
                {
                    await new MessageDialog(ex.Message).ShowAsync();
                }
            }
        }
    }
}
