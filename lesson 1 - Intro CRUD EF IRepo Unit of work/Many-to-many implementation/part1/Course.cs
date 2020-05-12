using System;
using System.Collections.Generic;
using System.Text;

namespace part1
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<StudentCourse> StudentCourses { get; set; }
    }
}
