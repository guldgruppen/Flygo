using System;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class FlyopgaveFactory : IFactory
    {
        

        //Opretter en flyopgave objekt. Der bliver lagt en time til i datetime for at få dansk tid.
        public Flyopgave CreateFlyopgave(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer)
        {
            DateTime fra = DateTime.Parse(ankomst.ToString());
            DateTime til = DateTime.Parse(afgang.ToString());
            DateTime fraDanskTid = fra.AddHours(1);
            DateTime tilDanskTid = til.AddHours(1);

            return new Flyopgave(flyid, hangarid, nummer, fraDanskTid, tilDanskTid);

        }

    }
}
