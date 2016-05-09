using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;



namespace FlyGoWebService.Models
{

    [Table("OpgaveArkiv")]
    public partial class OpgaveArkiv
    {
        public int Id { get; set; }

        public DateTime? Baggers { get; set; }

        public DateTime? Caters { get; set; }

        public DateTime? Crew { get; set; }

        public int FlyRuteId { get; set; }

        public DateTime? Fuelers { get; set; }

        public DateTime? Mekanikker { get; set; }

        public DateTime? Rengøring { get; set; }

        public DateTime? Redcap { get; set; }

        public virtual FlyRute FlyRute { get; set; }
    }
}
