using Microsoft.AspNetCore.Identity;
using PlayDiscGolf.Models.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.Account
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
