namespace FlyGoWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

    [Table("BrugerLogIn")]
    public partial class BrugerLogIn
    {
        private string _brugerNavn;
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string BrugerNavn
        {
            get { return _brugerNavn; }
            set {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("Brugernavn er null or empty");
                }
                    _brugerNavn = value; }
        }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public int RoleId { get; set; }

        public virtual Roles Roles { get; set; }
    }
}
