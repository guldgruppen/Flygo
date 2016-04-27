using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class HangarHandler
    {
        public readonly ObservableCollection<Hangar> Hangar = new ObservableCollection<Hangar>();

        public HangarHandler()
        {
            
        } 
    }
}
