using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hakaton1.Infrastructure
{
    public class DbConn
    {
        private const string stringConnection = @"Server=localhost; Port=5432; Database=Hakaton; UID = postgres; PWD=postgres";
        private static NpgsqlConnection _sqlConnection;

        public static NpgsqlConnection npgSqlConnection
        {
            get
            {
                if (_sqlConnection == null)
                {
                    _sqlConnection = new NpgsqlConnection(stringConnection);
                }
                if (_sqlConnection.State != System.Data.ConnectionState.Open)
                {
                    _sqlConnection.Open();
                }
                return _sqlConnection;
            }
        }
    }
}
