using System;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public interface IFactory
    {        
        Flyopgave CreateFlyopgave(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer);
    }
}
