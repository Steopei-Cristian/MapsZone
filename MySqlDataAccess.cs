using Dapper;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataAccess.NET
{
    public class MySqlDataAccess
    {
        public List<T> Load_Data<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new MySqlConnection(connectionString))
            {
                List<T> res = connection.Query<T>(sql, parameters).ToList();
                return res;
            }
        }

        public void Save_Data<T>(string sql, T parameters, string connectionString)
        {
            using(IDbConnection connection = new MySqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }

    }
}