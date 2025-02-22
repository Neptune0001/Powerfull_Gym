using Gym_Attendance_app.Views.Authentication;

namespace Gym_Attendance_app.Entities
{
    internal class Answer
    {
        public Answer() 
        {

        }

        public UserEnt userEnt { get; set; }


        public void TypesOfAnswerDb_AutheticatedUser(int code)
        {
            switch (code)
            {
                case 1:
                    ControlUtils.MessageBox_Show("Usuario registrado exitosamente.", "Registro Exitoso", false, "info");
                    break;
                case -1:
                    ControlUtils.MessageBox_Show("Correo electrónico no registrado.", "Error", false, "error");
                    break;
                case -2:
                    ControlUtils.MessageBox_Show("Contraseña incorrecta.", "Error", false, "error");
                    break;
                case -3:
                    ControlUtils.MessageBox_Show("El usuario está inactivo.", "Atención", false, "warning");
                    break;
                default:
                    ControlUtils.MessageBox_Show("Error desconocido.", "Error", false, "error");
                    break;
            }
        }

    }
}
