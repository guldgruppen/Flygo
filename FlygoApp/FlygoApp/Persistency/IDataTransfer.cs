using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Persistency
{
    public interface IDataTransfer<T>
    {
        Task Load(List<T> listToAdd, string url);
        Task Post(T type,string url);
        Task Delete(int id,string url);
        Task Update(T type, int id, string url);

    }
}
