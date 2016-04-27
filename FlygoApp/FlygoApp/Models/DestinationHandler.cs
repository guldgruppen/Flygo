using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class DestinationHandler
    {
        public readonly ObservableCollection<Destination> Destinationer = new ObservableCollection<Destination>();

      

        public DestinationHandler()
        {
            
        }

    }
}
