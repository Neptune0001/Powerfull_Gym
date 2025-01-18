using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Attendance_app.Models.AuthenticationModel
{
    internal class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Validate()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }
    }
}
