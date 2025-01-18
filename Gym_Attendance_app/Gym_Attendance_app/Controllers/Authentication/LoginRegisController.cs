using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Attendance_app.Controllers.Authentication
{
    internal class LoginRegisController
    {
        public bool Login(string email, string password)
        {
            return email == "admin" && password == "1234";
        }

        public bool Register(string name, string email, string password)
        {
            return true;
        }
    }
}
