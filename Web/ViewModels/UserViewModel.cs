using Data.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Users = new List<User>();
        }

        public string Id { get; set; }
        public bool ResetPassword { get; set; }
        public bool PendingPassword { get; set; }

        [Display(Name = "First name")]
        [Required(ErrorMessage = "The {0} is required")]
        public string FirstName { get; set; }

        [Display(Name = "Last mame")]
        [Required(ErrorMessage = "The {0} is required")]
        public string LastName { get; set; }

        [Display(Name = "Tag")]
        [Required(ErrorMessage = "The {0} is required")]
        public string Tag { get; set; }

        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "The {0} is required")]
        [EmailAddress(ErrorMessage = "Invalid e-mail address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The {0} is required")]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public List<User> Users { get; set; }

    }
}
