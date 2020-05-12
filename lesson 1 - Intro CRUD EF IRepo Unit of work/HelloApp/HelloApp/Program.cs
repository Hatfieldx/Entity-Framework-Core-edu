using HelloApp.DataAccess;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace HelloApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var conf = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();            

            var options = new DbContextOptionsBuilder<AppContext>()
                .UseSqlServer(conf.GetConnectionString("Default"), x => x.MigrationsAssembly("HelloApp.Migrations"))
                .Options;     

            using (var db = new UOWImplementation(options))
            {

                await db.UserRepo.CreateAsync(new User { Name = "EntitiFrameWork_First", Age = 88 });

                await db.SaveChangesAsync();

                var user = await db.UserRepo.GetAsync(5);

                Console.WriteLine(user.Name);
            }
            
            Console.ReadKey();
        }
    }
}
