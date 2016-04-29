namespace FlyGoWebService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("OpgaveArkiv")]
    public partial class OpgaveArkiv
    {
        public int Id { get; set; }

        public DateTime Baggers { get; set; }

        public DateTime Caters { get; set; }

        public DateTime Crew { get; set; }

        public int FlyRuteId { get; set; }

        public DateTime Fuelers { get; set; }

        public DateTime Mekanikker { get; set; }

        public virtual FlyRute FlyRute { get; set; }
    }
}
