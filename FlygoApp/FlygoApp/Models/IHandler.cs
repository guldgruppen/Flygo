using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlygoApp.Annotations;

namespace FlygoApp.Models
{
    public interface IHandler
    {
        Flyrute Get(int Id);
        void Remove(Flyrute flyrute);
        void Update(Flyrute flyrute);


    }
}
