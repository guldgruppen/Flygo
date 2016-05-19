using FlyGoWebService.Models;

namespace FlygoApp.Models
{

    //Bruges til at sende 2 objekter ned i vores value converters.
    public class OpgaveAdapter
    {
        public OpgaveArkiv OpgaveArkiv { get; set; }
        public Flyopgave Flyopgave { get; set; }

        public OpgaveAdapter(OpgaveArkiv opgaveArkiv, Flyopgave flyopgave)
        {
            OpgaveArkiv = opgaveArkiv;
            Flyopgave = flyopgave;
        }
    }
}
