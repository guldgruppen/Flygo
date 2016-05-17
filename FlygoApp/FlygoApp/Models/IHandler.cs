using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public interface IHandler
    {
        Flyopgave Get(int Id);
        void Remove(Flyopgave Flyopgave);
        void Update(Flyopgave Flyopgave);


    }
}
