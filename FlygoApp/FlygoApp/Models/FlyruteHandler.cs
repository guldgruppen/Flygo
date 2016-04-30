using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Popups;
using FlygoApp.Commons;
using FlygoApp.Persistency;
using FlyGoWebService.Models;
using Newtonsoft.Json.Bson;

namespace FlygoApp.Models
{
    public class FlyruteHandler : IHandler
    {
        private ICommand _createFlyruteCommand;

        public DTOSingleton  DTO { get; set; }
        public ObservableCollection<FlyRute> Flyruter { get; set; }
        public IFactory FlyRuteFactory { get; set; }

    

        public async void Add(DateTimeOffset afgang, DateTimeOffset ankomst, int id, int hanid, string nummer)
        {
            try
            {
                FlyRute rute = FlyRuteFactory.CreateFlyrute(afgang, ankomst, id, hanid, nummer);
                DTO.PostFlyRuter(rute);
            }
            catch (ArgumentException ex)
            {
                await new MessageDialog(ex.Message).ShowAsync();
            }
            catch (IndexOutOfRangeException e)
            {              
                await new MessageDialog(e.Message).ShowAsync();
            }
            


        }

        public FlyruteHandler()
        {   
            DTO = DTOSingleton.GetInstance();
            FlyRuteFactory = new FlyRuteFactory();
            Flyruter = new ObservableCollection<FlyRute>();
            
        }

        public void CheckEksisterendeFlyrute(FlyRute flyrute)
        {
            foreach (var Flyrute in Flyruter)
            {
                
            }
        }

        public FlyRute Get(int Id)
        {
            return null;
        }

        public void Remove(FlyRute flyrute)
        {
           
        }

        public void Update(FlyRute flyrute)
        {
           
        }

        

        public void LoadDTOFlyruter()
        {
            foreach (var flyrute in DTO.FlyruteListe)
            {
                Flyruter.Add(flyrute);
            }
        }

        

    }
}
