using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Persistency
{
    public interface IDataTransfer<in T>
    {
        void Load();
        void Post(T type);
        void Delete(int id);
        
    }
}
