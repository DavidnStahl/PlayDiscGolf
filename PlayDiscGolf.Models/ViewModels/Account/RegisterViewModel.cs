using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PlayDiscGolf.Models.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Not a valid emailadress")]
        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required (ErrorMessage = "Username is required")]
        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
