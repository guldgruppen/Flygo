using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using FlygoApp.Models;
using Newtonsoft.Json;

namespace FlygoApp.Persistency
{
    public class FlyrutePersistency
    {
        private static string JsonFileFlyrute = "FlyruteFile.dat";


        public static async void SaveFlyruteAsJsonAsync(ObservableCollection<Flyrute> sh)
        {
            string flyruteJsonString = JsonConvert.SerializeObject(sh);
            SerializeFlyruteFileAsync(flyruteJsonString, JsonFileFlyrute);
        }

        public static async Task<List<Flyrute>> LoadFlyruteFromJsonAsync()
        {
            string flyruteJsonString = await DeserializeFlyruteFileAsync(JsonFileFlyrute);
            if (flyruteJsonString != null)
                return (List<Flyrute>)JsonConvert.DeserializeObject(flyruteJsonString, typeof(List<Flyrute>));
            return null;
        }

        private static async void SerializeFlyruteFileAsync(string kunderJsonString, string fileName)
        {
            StorageFile localFile = await ApplicationData.Current.LocalFolder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(localFile, kunderJsonString);
        }

        private static async Task<string> DeserializeFlyruteFileAsync(string fileName)
        {
            try
            {
                StorageFile localFile = await ApplicationData.Current.LocalFolder.GetFileAsync(fileName);
                return await FileIO.ReadTextAsync(localFile);
            }
            catch (FileNotFoundException)
            {
                return null;
            }
        }

    }
}
