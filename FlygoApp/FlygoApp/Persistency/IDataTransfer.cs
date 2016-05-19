using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Persistency
{
    public interface IDataTransfer<T>
    {
        void Load(List<T> listToAdd, string url);
        void Post(T type,string url);
        void Delete(int id,string url);
        
    }
}
