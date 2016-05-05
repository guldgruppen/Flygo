using System;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class FlyRuteFactory : IFactory
    {
        
        public FlyRute CreateFlyrute(DateTimeOffset afgang, DateTimeOffset ankomst, int flyid, int hangarid, string nummer)
        {
            DateTime fra = DateTime.Parse(ankomst.ToString());
            DateTime til = DateTime.Parse(afgang.ToString());
            DateTime fraDanskTid = fra.AddHours(1);
            DateTime tilDanskTid = til.AddHours(1);
            
            return new FlyRute() {Afgang = tilDanskTid, Ankomst = fraDanskTid, FlyId = flyid,HangarId = hangarid, FlyRuteNummer = nummer};

        }

    }
}
