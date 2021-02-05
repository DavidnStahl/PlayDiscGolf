using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Dtos
{
    public class RegisterUserDto
    {
        public bool CreateUserSucceded { get; set; }
        public bool ErrorMessegeEmail { get; set; }
        public bool ErrorMessegeUsername { get; set; }
    }
}
