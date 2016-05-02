using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Audio;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public interface IFactory
    {
        FlyRute CreateFlyrute(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer);
    }
}
