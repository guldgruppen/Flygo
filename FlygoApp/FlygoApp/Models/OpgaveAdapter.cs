using FlyGoWebService.Models;

namespace FlygoApp.Models
{
    public class OpgaveAdapter
    {
        public OpgaveArkiv OpgaveArkiv { get; set; }
        public Flyopgave Flyopgave { get; set; }

        public OpgaveAdapter(OpgaveArkiv opgaveArkiv, Flyopgave Flyopgave)
        {
            OpgaveArkiv = opgaveArkiv;
            Flyopgave = Flyopgave;
        }
    }
}
