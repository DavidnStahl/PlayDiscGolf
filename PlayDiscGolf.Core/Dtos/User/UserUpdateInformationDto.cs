using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PlayDiscGolf.Core.Dtos.User
{
    public class UserUpdateInformationDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
