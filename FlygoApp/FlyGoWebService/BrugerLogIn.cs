namespace FlyGoWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BrugerLogIn")]
    public partial class BrugerLogIn
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string BrugerNavn { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
