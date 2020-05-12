using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace StoreProcedures
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //await UploadPictures();

            await DownloadPictures();


            // SqlCommand cmd = new SqlCommand {

            //     CommandText = "spAddUser",
            //     CommandType = CommandType.StoredProcedure
            // };

            // cmd.Parameters.AddWithValue("name", "Test_insert_name");
            // cmd.Parameters.AddWithValue("age", 44);

            // var db = new DataAccess();

            // await db.Connect();

            // //await db.ExecuteNonQueryAsync(cmd);

            // //await cmd.DisposeAsync();

            // cmd = new SqlCommand
            // {
            //     CommandText = "spGetUsers",
            //     CommandType = CommandType.StoredProcedure
            // };

            // cmd.CommandText = "spGetUsers";
            // cmd.Parameters.AddWithValue("age", 33);

            //// await db.ExecuteReaderAsync(cmd);

            // cmd = new SqlCommand
            // {
            //     CommandText = "spGetUsersCount",
            //     CommandType = CommandType.StoredProcedure
            // };

            // var count = cmd.Parameters.Add("count", SqlDbType.Int);

            // count.Direction = ParameterDirection.Output;

            // await db.ExecuteNonQueryAsync(cmd);

            //Console.WriteLine(count.Value);

            Console.ReadKey();
        }

        private static async Task DownloadPictures()
        {
            string directoryPath = @"D:\ADO NET\StoreProcedures\src\out";

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from images";

            var db = new DataAccess();

            await db.Connect();

            var reader = await db.ExecuteReaderAsync(cmd);

            if (!reader.HasRows)
            {
                return;
            }

            List<byte[]> images = new List<byte[]>();            

            while (await reader.ReadAsync())
            {
                if (reader.IsDBNull(1))
                {
                    continue;
                }

                var length = reader.GetBytes(1, 0, null, 0, 0);
                
                byte[] data = new byte[length];

                reader.GetBytes(1, 1, data, 0, (int)length);

                images.Add(data);
            }

            await reader.CloseAsync();

            for (int i = 0; i < images.Count; i++)
            {
                using (FileStream fs = new FileStream(Path.Combine(directoryPath, $"file{i}.jpg"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    await fs.WriteAsync(images[i], 0, images[i].Length);
                }
            }
        }

        static async Task UploadPictures()
        {
            string directoryPath = @"D:\ADO NET\StoreProcedures\src";

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "Insert into Images Values ";

            string currentParamName = "";

            int counter = 0;

            var files = Directory.GetFiles(directoryPath);

            foreach (var pathfile in files)
            {
                using (FileStream fs = new FileStream(pathfile, FileMode.Open, FileAccess.Read))
                {
                    byte[] data = new byte[fs.Length];

                    await fs.ReadAsync(data, 0, data.Length);

                    currentParamName = $"@image{counter}";

                    if (files.Length == (counter + 1))
                    {
                        cmd.CommandText += $"({currentParamName})";
                    }
                    else
                    {
                        cmd.CommandText += $"({currentParamName}), ";
                    }

                    var param = cmd.Parameters.Add(currentParamName, SqlDbType.VarBinary);

                    param.Value = data;

                    counter++;
                }
            }

            var db = new DataAccess();

            await db.Connect();

            await db.ExecuteNonQueryAsync(cmd);
        }
    }
}
