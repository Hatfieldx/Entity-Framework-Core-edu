using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;

namespace StoreProcedures
{
    class DataAccess
    {
        private SqlConnection _connection;

        public async Task Connect()
        {
            if (_connection == null)
            {
                _connection = new SqlConnection();
            }

            if (_connection.State == ConnectionState.Open)
            {
                return;
            }

            _connection.ConnectionString = await GetConnectionString();

            await _connection.OpenAsync();
        }

        private async Task<string> GetConnectionString()
        {
            return await Task.Run(() =>
                 {
                     //var connectionStringsSections = ConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;

                     //if (!connectionStringsSections.SectionInformation.IsProtected)
                     //{
                     //    connectionStringsSections.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                     //}

                     return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
                 });
        }

        public async Task<SqlDataReader> ExecuteReaderAsync(SqlCommand cmd)
        {
            cmd.Connection = _connection;

            //cmd.Transaction = await _connection.BeginTransactionAsync() as SqlTransaction;

            //var reader = await cmd.ExecuteReaderAsync();

            //while (await reader.ReadAsync())
            //{
            //    for (int i = 0; i < reader.FieldCount; i++)
            //    {
            //        Console.WriteLine($"{reader.GetName(i)}: {reader[i]}");                    
            //    }
            //    Console.WriteLine(new string('=', 30));
            //}

            ////cmd.Transaction.Commit();

            //await reader.CloseAsync();

            return await cmd.ExecuteReaderAsync();
        }
        public async Task ExecuteNonQueryAsync(SqlCommand cmd)
        {
            cmd.Connection = _connection;

            cmd.Transaction = await _connection.BeginTransactionAsync() as SqlTransaction;

            var res = await cmd.ExecuteNonQueryAsync();

            cmd.Transaction.Commit();

            Console.WriteLine(res.ToString());
        }
        public async Task ExecuteScalarAsync(SqlCommand cmd)
        {
            cmd.Connection = _connection;

            cmd.Transaction = await _connection.BeginTransactionAsync() as SqlTransaction;

            var res = await cmd.ExecuteScalarAsync();

            cmd.Transaction.Commit();

            Console.WriteLine(res.ToString());
        }
    }
}
