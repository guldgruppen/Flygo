﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Models;

namespace FlygoApp.Persistency
{
    public class DTOSingleton
    {
        public List<Destination> DestinationListe = new List<Destination>();
        public List<Fly> FlyListe = new List<Fly>();
        public List<Flyrute> FlyruteListe = new List<Flyrute>();
        public List<Hangar> HangarListe = new List<Hangar>();
        private static DTOSingleton Singleton;

        private DTOSingleton()
        {
            
        }

        public static DTOSingleton GetInstance()
        {        
            return Singleton ?? (Singleton = new DTOSingleton());
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

        public void LoadFly()
        {
            
        }

    }
}
