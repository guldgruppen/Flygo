using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlygoApp.Models
{
    public class WorkerTest
    {
        public string Name { get; set; }
        public string Status { get; set; }

        public WorkerTest(string name, string status)
        {
            Name = name;
            Status = status;
        }

        public override string ToString()
        {
            return $"Name: {Name}, Status: {Status}";
        }
    }
}
