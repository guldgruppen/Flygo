using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using FlygoApp.Commons;
using FlygoApp.Persistency;
using Newtonsoft.Json.Bson;

namespace FlygoApp.Models
{
    public class FlyruteHandler : IHandler
    {
        private ICommand _createFlyruteCommand;

        public DTOSingleton  DTO { get; set; } = DTOSingleton.GetInstance();
        public static ObservableCollection<Flyrute> Flyruter { get; set; }
        public IFactory FlyRuteFactory { get; set; }

    

        public void Add(DateTimeOffset afgang, DateTimeOffset ankomst, Hangar hangar, Fly fly, string nummer)
        {
            Flyrute rute = FlyRuteFactory.CreateFlyrute(afgang, ankomst, fly, hangar, nummer);

            DTO.PostFlyRuter(rute);



        }

        public FlyruteHandler()
        {
            FlyRuteFactory = new FlyRuteFactory();
            Flyruter = new ObservableCollection<Flyrute>();
            LoadDTOFlyruter();
        }

        public void CheckEksisterendeFlyrute(Flyrute flyrute)
        {
            foreach (var Flyrute in Flyruter)
            {
                
            }
        }

        public Flyrute Get(int Id)
        {
            return null;
        }

        public void Remove(Flyrute flyrute)
        {
           
        }

        public void Update(Flyrute flyrute)
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
