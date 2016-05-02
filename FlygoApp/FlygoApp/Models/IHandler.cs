using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Annotations;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public interface IHandler
    {
        FlyRute Get(int Id);
        void Remove(FlyRute flyrute);
        void Update(FlyRute flyrute);


    }
}
