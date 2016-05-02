using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Fly
    {
        public int Id { get; set; }
        public string Producent { get; set; }
        public string Type { get; set; }
        public string FlyNavn { get; set; }

        public Fly( string producent, string type)     
        {
           
            Producent = producent;
            Type = type;

        }

        public Fly()
        {
            FlyNavn = Producent + " " + Type;
        }

        public override string ToString()
        {
            return $"{Producent} {Type}";
        }
    }
}
