using System.Collections.Generic;

namespace ApiServer
{
      public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public List<Student> GetStudents()
        {
            return new List<Student> {new Student {Id = 1, Name = "Ahmed"}, new Student {Id = 1, Name = "mohammed"}};
        }


    }
}