using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace part1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new ColedgeContextFactory()
                            .CreateDbContext(null))
            {
                //Student s1 = new Student { Name = "Tom" };
                //Student s2 = new Student { Name = "Alice" };
                //Student s3 = new Student { Name = "Bob" };
                //db.Students.AddRange(new List<Student> { s1, s2, s3 });

                Course c1 = new Course { Name = "Алгоритмы" };
                //Course c2 = new Course { Name = "Основы программирования" };
                //db.Courses.AddRange(new List<Course> { c1, c2 });

                //db.SaveChanges();

                //// добавляем к студентам курсы
                //s1.StudentCourses.Add(new StudentCourse { CourseId = c1.Id, StudentId = s1.Id });
                //s2.StudentCourses.Add(new StudentCourse { CourseId = c1.Id, StudentId = s2.Id });
                //s2.StudentCourses.Add(new StudentCourse { CourseId = c2.Id, StudentId = s2.Id });
                //db.SaveChanges();

                //var courses = db.Courses.Include(c => c.StudentCourses).ThenInclude(sc => sc.Student).ToList();
                //// выводим все курсы
                //foreach (var c in courses)
                //{
                //    Console.WriteLine($"\n Course: {c.Name}");
                //    // выводим всех студентов для данного кура
                //    var students = c.StudentCourses.Select(sc => sc.Student).ToList();
                //    foreach (Student s in students)
                //        Console.WriteLine($"{s.Name}");
                //}



                //var cr = db.Courses.Where(x => x.Name == c1.Name)
                //                        .Include(x => x.StudentCourses)
                //                        .ThenInclude(x => x.Student);

                //foreach (var item in cr)
                //{
                //    Console.WriteLine(item.Name);

                //    foreach (var courses in item.StudentCourses)
                //    {
                //        Console.WriteLine(courses.Student.Name);
                //    }
                //}


                //db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;


                //var student1 = db.Find<Student>(5);

                //var student2 = db.Find<Student>(5);

                IEnumerable<Student> st = db.Students;

                
                var q = st.Where(x => x.Id == 5).ToList();


                IQueryable<Student> stQ = db.Students.Where(x => x.Id == 5);

                var stQr = db.Students.Where(x => x.Id == 5).ToList();


                //Console.WriteLine(student.Name);

                //student.Name = "change 1";

                //Console.WriteLine(student.Name);

                //Thread.Sleep(10000);

                //Console.WriteLine("After");

                //db.SaveChanges();

                //Console.WriteLine("Complete");


                //Console.WriteLine(student.Name);


                //var data = db.StudentCourses.Select(x => new
                //{
                //    Course = x.Course.Name,
                //    Student = x.Student.Name
                //});

                //foreach (var item in data)
                //{
                //    Console.WriteLine($"{item.Course} , {item.Student}");

                //foreach (var item1 in item.StudentCourses)
                //{
                //    Console.WriteLine($"student {item.Name}, StCr: {item1.Course.Name}");
                //}



                //var cr2 = db.Courses.Where(x => x.Name == c1.Name);

                //var stcr = db.StudentCourses.Join<Course>((x,y) => )

                //foreach (var item in cr)
                //{
                //    Console.WriteLine();
                //}
            }

            Console.ReadKey();
        }
    }
}
