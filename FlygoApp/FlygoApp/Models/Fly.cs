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

        public string Navn { get; set; }

        public Fly(int id, string navn)
        {
            Id = id;
            Navn = navn;
        }

        public Fly()
        {
            
        }

    }
}
