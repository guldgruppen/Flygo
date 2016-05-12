using FlyGoWebService.Models;

namespace FlyGoWebService
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    

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


        public BrugerLogIn()
        {
            
        }

        public BrugerLogIn(string password, int roleId)
        {
            Password = password;
            RoleId = roleId; 
        }
    }
}
