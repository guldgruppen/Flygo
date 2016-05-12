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
