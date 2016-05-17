namespace FlyGoWebService.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Flyopgave")]
    public partial class Flyopgave
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Flyopgave()
        {
            OpgaveArkiv = new HashSet<OpgaveArkiv>();
        }

        public int Id { get; set; }

        public DateTime Ankomst { get; set; }

        public DateTime Afgang { get; set; }

        public int FlyId { get; set; }

        public int HangarId { get; set; }

        [StringLength(50)]
        public string FlyopgaveNummer { get; set; }

        public virtual Fly Fly { get; set; }

        public virtual Hangar Hangar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpgaveArkiv> OpgaveArkiv { get; set; }
    }
}
