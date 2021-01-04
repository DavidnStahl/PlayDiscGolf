using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.ScoreCard
{
    public class UserChangePasswordDto
    {
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$",
            ErrorMessage = "Password must be at least 8 characters and contain at least 1 of the following: upper case (A-Z), lower case (a-z), number (0-9) and special character (e.g. !@#$%^&*)")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Confirm Password")]
        [StringLength(100, ErrorMessage = "Must be between 5 and 100 characters", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
