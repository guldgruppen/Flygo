using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class OpgaveAdapter
    {
        public OpgaveArkiv OpgaveArkiv { get; set; }
        public FlyRute FlyRute { get; set; }

        public OpgaveAdapter(OpgaveArkiv opgaveArkiv, FlyRute flyRute)
        {
            OpgaveArkiv = opgaveArkiv;
            FlyRute = flyRute;
        }
    }
}
