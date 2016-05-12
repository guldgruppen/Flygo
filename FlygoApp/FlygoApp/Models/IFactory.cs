using System;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public interface IFactory
    {
        FlyRute CreateFlyrute(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer);
    }
}
