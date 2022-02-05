using Data.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Web.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
            Roles = new List<Role>();
            Claims = new List<Claim>();
        }

        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string[] ClaimsIds { get; set; }

        public List<Role> Roles { get; set; }
        public List<Claim> Claims { get; set; }

    }
}
