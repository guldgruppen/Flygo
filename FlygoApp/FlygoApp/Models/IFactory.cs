using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;

namespace FlygoApp.Models
{
    public interface IFactory
    {
        Flyrute CreateFlyrute(DateTimeOffset afgang, DateTimeOffset ankomst, Fly fly, Hangar hangar, string nummer);
    }
}
