using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Destination
    {
        public int Id { get; set; }
        public string Navn { get; set; }

        public Destination(int id, string navn)
        {
            Id = id;
            Navn = navn;
        }

        public Destination()
        {
            
        }

    }
}
