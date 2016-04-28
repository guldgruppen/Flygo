using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Persistency;

namespace FlygoApp.Models
{
    public class HangarHandler
    {
        public  ObservableCollection<Hangar> Hangar { get; set; }

        public DTOSingleton Dto = DTOSingleton.GetInstance();

        public HangarHandler()
        {
           Hangar= new ObservableCollection<Hangar>();
        }

        public void LoadDtoHangar()
        {
            foreach (var H in Dto.HangarListe)
            {
                Hangar.Add(H);
            }
        }

    }
}
