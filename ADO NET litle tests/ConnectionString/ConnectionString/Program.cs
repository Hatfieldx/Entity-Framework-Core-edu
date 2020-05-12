using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ConnectionString
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using (SqlConnection connection = await ConnectAsync())
            {
                //await AddDataAsync(connection);

                await SelectDataAsync(connection);

            }

            Console.ReadKey();
        }

        static async Task AddDataAsync(SqlConnection connection)
        {
            string sqlInsert = "Insert into users values";

            var rnd = new Random();

            for (int i = 0; i < 20; i++)
            {
                sqlInsert += $"('UserName_00{i}', {rnd.Next(10, 90)})" + (i == 19 ? "" : ",");
            }

            var query = connection.CreateCommand();

            query.CommandText = sqlInsert;

            var count = await query.ExecuteNonQueryAsync();

            Console.WriteLine($"Added {count} users");
        }

        static async Task SelectDataAsync(SqlConnection connection)
        {
            string sqlExpression = "select * from users where name = @name";

            var query = connection.CreateCommand();

            query.CommandText = sqlExpression;

            query.Parameters.AddWithValue("name", "Nik");

            var par = new SqlParameter("name", "Nik");

            par.Direction = System.Data.ParameterDirection.Input;

            var dr = await query.ExecuteReaderAsync();

            if (!dr.HasRows)
            {
                return;
            }

            var schema = dr.GetSchemaTable();

            string colName = "";

            while (await dr.ReadAsync())
            {
                Console.Write($" {dr[0]} | {dr[1]} | {dr[2]} |");

                //foreach (var item in schema.Columns as System.Data.DataColumnCollection)
                //{
                //    colName = ((System.Data.DataColumn)item).ColumnName;

                //    Console.Write($" | {colName} : {dr[colName]}  | ");
                //}
            }
        }

        static async Task<SqlConnection> ConnectAsync()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var connection = new SqlConnection(connectionString);
            
            await connection.OpenAsync();

            return connection;
        }
    }
}
