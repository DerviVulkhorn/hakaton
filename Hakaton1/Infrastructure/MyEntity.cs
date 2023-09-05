using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hakaton1.Infrastructure
{
    public class MyEntity
    {
        public static List<T> Execute<T>(string sqlCommand) where T : new()
        {
            var listObject = new List<T>();
            var typeObject = typeof(T);
            var properties = typeof(T).GetProperties();
            try
            {
                using (NpgsqlCommand pgSqlCommand = new NpgsqlCommand(sqlCommand, DbConn.npgSqlConnection))
                {
                    var sqlDataReader = pgSqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        var newObject = new T();
                        int numberProperties = 0;
                        foreach (var property in properties)
                        {
                            if (Convert.IsDBNull(sqlDataReader.GetValue(numberProperties)))
                            {
                                typeObject.GetProperty(property.Name).SetValue(newObject, null);
                            }
                            else
                            {
                                typeObject.GetProperty(property.Name).SetValue(newObject, sqlDataReader.GetValue(numberProperties));
                            }
                            numberProperties++;
                        }
                        listObject.Add(newObject);
                    }
                }
                DbConn.npgSqlConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ошибка получения объектов" + ex.ToString());
            }
            return listObject;
        }

        public static T Single<T>(string sqlCommand) where T : new()
        {
            var listObject = Execute<T>(sqlCommand);
            if (listObject != null)
            {
                if (listObject.Count != 0)
                {
                    return listObject[0]; //single object
                }
            }
            return default(T);
        }

        public static void Execute(string sqlCommand)
        {
            try
            {
                NpgsqlCommand pgSqlCommand = new NpgsqlCommand(sqlCommand, DbConn.npgSqlConnection);
                pgSqlCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            DbConn.npgSqlConnection.Close();
        }
    }
}
