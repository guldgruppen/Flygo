using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Brugernavn { get; set; }
        public string Password { get; set; }

        public Person(string name, string brugernavn, string password)
        {
            Name = name;
            Brugernavn = brugernavn;
            Password = password;
        }
        public Person()
        {
            
        }

        public override string ToString()
        {
            return $"Name: {Name}, Brugernavn: {Brugernavn}, Password: {Password}";
        }
    }
}
