namespace FlyGoWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FlyRute")]
    public partial class FlyRute
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FlyRute()
        {
            OpgaveArkiv = new HashSet<OpgaveArkiv>();
        }

        public int Id { get; set; }

        public DateTime Ankomst { get; set; }

        public DateTime Afgang { get; set; }

        public int FlyId { get; set; }

        public int HangarId { get; set; }

        public virtual Destination Destination { get; set; }

        public virtual Destination Destination1 { get; set; }

        public virtual Fly Fly { get; set; }

        public virtual Hangar Hangar { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OpgaveArkiv> OpgaveArkiv { get; set; }
    }
}
