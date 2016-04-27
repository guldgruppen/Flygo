using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class FlyHandler
    {
        public readonly  ObservableCollection<Fly> Fly = new ObservableCollection<Fly>();

        public FlyHandler()
        {
            
        }
    }
}
