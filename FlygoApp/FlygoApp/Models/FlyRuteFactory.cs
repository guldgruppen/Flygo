using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class FlyRuteFactory : IFactory
    {
        
        public Flyrute CreateFlyrute(DateTimeOffset afgang, DateTimeOffset ankomst, Fly fly, Hangar hangar, string nummer)
        {

          Flyrute _flyrute = new Flyrute(afgang,ankomst,fly,nummer,hangar);
            return _flyrute;

        }

    }
}
