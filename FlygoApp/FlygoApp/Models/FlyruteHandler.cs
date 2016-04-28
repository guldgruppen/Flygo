using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Persistency;
using Newtonsoft.Json.Bson;

namespace FlygoApp.Models
{
    public class FlyruteHandler : IHandler
    {

        public DTOSingleton  DTO { get; set; } = DTOSingleton.GetInstance();
        public ObservableCollection<Flyrute> Flyruter { get; set; } = new ObservableCollection<Flyrute>();
      

        public void Add(Flyrute flyrute)
        {

        }

        public void CheckEksisterendeFlyrute(Flyrute flyrute)
        {
            
        }

        public Flyrute CreateFlyrute()
        {
            return null;
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
            
        }

        

    }
}
