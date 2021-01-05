using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.User
{
    public class UserChangePasswordDto
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
