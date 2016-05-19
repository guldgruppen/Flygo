using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Windows.UI.Popups;
using FlyGoWebService.Models;

namespace FlygoApp.Persistency
{
    public class DtoRolesSingleton : DataTransferBase<Roles>
    {
        private static DtoRolesSingleton _dtoRoles;
        public List<Roles> RolesListe { get; set; } = new List<Roles>();
        public static DtoRolesSingleton GetInstance => _dtoRoles ?? (_dtoRoles = new DtoRolesSingleton());
        private DtoRolesSingleton()
        {
            LoadRoles();
        }
        public void LoadRoles()
        {
            Load(RolesListe, "api/Roles/GetRoles");
        }
    }
}
