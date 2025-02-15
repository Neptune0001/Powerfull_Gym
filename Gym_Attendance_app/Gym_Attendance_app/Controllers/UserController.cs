using Dapper;
using Gym_Attendance_app.Entities;
using Gym_Attendance_app.Views.Authentication;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Gym_Attendance_app.DataAccess.Controllers
{
    public static class UserController
    {
        private static Answer answer()
        {
            return new Answer();
        }

        // Método para loguear un usuario
        public static void LoginUser(UserEnt ent)
        {
            using (IDbConnection db = new SqlConnection(SqlConnectionClass._connectionString))
            {
                // Ejecutar el procedimiento almacenado de login
                var res = db.QueryFirstOrDefault<int>("sp_LoginUser", new
                {
                    ent.Email,
                    ent.Password
                }, commandType: CommandType.StoredProcedure);

                
            }
        }

        // Método para registrar un nuevo usuario
        public static int RegisterUser(UserEnt ent)
        {
            using (var db = new SqlConnection(SqlConnectionClass._connectionString))
            {
                // Ejecutar el procedimiento almacenado de registro
                var res = db.QueryFirstOrDefault<int>("sp_RegisterUser", new
                {
                    ent.FirstName,
                    ent.LastName,
                    ent.Phone,
                    ent.Email,
                    ent.Password
                }, commandType: CommandType.StoredProcedure);

                answer().TypesOfAnswerDb_AutheticatedUser(res);

                return res;
            }
        }

    }
}
