using System;
using System.Collections.Generic;
using System.Text;

namespace PlayDiscGolf.Dtos
{
    public class RegisterUserDto
    {
        public bool CreateUserSucceded = false;

        public bool ErrorMessegeEmail = false;

        public bool ErrorMessegeUsername = false;
    }
}
