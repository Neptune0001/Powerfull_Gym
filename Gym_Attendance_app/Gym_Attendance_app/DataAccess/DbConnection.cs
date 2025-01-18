using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym_Attendance_app.DataAccess
{
    internal class DbConnection
    {
        private readonly string _connectionString;

        public DbConnection()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["PowerfullGymDB"].ConnectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
