using System.Linq;
using FlygoApp.Persistency;
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
        DtoRolesSingleton _dtoRolesSingleton = DtoRolesSingleton.GetInstance();
        private string _rolleNavn;

        public string RolleNavn
        {
            get { return _rolleNavn; }
            set { _rolleNavn = value; }
        }

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

        public BrugerLogIn(string brugerNavn, string password, int roleId)
        {
            BrugerNavn = brugerNavn;
            Password = password;
            RoleId = roleId;
            Roles temp = _dtoRolesSingleton.RolesListe.Single(x => x.Id.Equals(RoleId));
            RolleNavn = temp.RoleName;
        }
        public BrugerLogIn(string password, int roleId)
        {
            Password = password;
            RoleId = roleId; 
        }
    }
}
