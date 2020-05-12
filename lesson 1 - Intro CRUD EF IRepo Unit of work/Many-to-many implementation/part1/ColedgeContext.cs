using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace part1
{
    public class ColedgeContext : DbContext
    {
        public ColedgeContext(DbContextOptions<ColedgeContext> options)
            : base(options)
        {

        }
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentCourse>(RelationConfiguration);            
            
            //modelBuilder.Entity<StudentCourse>().HasKey(x => new { x.StudentId, x.CourseId });
            ////base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<StudentCourse>()
            //    .HasOne(x => x.Student)
            //    .WithMany(x => x.StudentCourses);

            //modelBuilder.Entity<StudentCourse>()
            //    .HasOne(x => x.Course)
            //    .WithMany(x => x.StudentCourses);

        }

        public void RelationConfiguration(EntityTypeBuilder<StudentCourse> builder)
        {
            builder.HasKey(x => new { x.StudentId, x.CourseId });

            builder
                .HasOne(x => x.Student)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.StudentId);

            builder
                .HasOne(x => x.Course)
                .WithMany(x => x.StudentCourses)
                .HasForeignKey(x => x.CourseId);
        }
    }

    public class ColedgeContextFactory : IDesignTimeDbContextFactory<ColedgeContext>
    {
        public ColedgeContext CreateDbContext(string[] args)
        {
            var conf = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            var options = new DbContextOptionsBuilder<ColedgeContext>()
                .UseSqlServer(conf.GetConnectionString("Default"), opt => opt.CommandTimeout(5))
                .Options;

            return new ColedgeContext(options);
        }
    }
}
