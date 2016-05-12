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
