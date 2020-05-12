using Microsoft.EntityFrameworkCore;

namespace HelloApp
{
    public class AppContext : DbContext
    {
        //private readonly string _connectionString;

        public DbSet<User> Users { get; set; }

        public AppContext(DbContextOptions<AppContext> options)
            : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_connectionString);
        //}
    }
}
