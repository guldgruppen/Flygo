using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public interface IHandler
    {
        Flyopgave Get(int id);
        void Remove(Flyopgave flyopgave);
        void Update(Flyopgave flyopgave);


    }
}
