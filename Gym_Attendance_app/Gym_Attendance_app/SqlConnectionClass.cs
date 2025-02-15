using System.Configuration;

namespace Gym_Attendance_app
{
    internal class SqlConnectionClass
    {
        public static string _connectionString => ConfigurationManager.ConnectionStrings["PowerfullGymDB"].ConnectionString;
    }
}
