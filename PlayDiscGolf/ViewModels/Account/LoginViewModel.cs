using PlayDiscGolf.Core.Services.Account;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PlayDiscGolf.Models.ViewModels.Account
{
    public class LoginViewModel
    {
        private readonly IAccountService _accountService;
        public LoginViewModel()
        {

        }
        [Required (ErrorMessage = "Username is required")]
        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required (ErrorMessage = "Password is required")]
        [StringLength(maximumLength: 100, ErrorMessage = "Maximum 100 character")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}
