using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;  

namespace LearningCSharp
{
    internal class Program
    {

        class Student
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public int Grade { get; set; }
        }

        static void Main(string[] args)
        {



            // lista od objekti

            List<Student> students = new List<Student>
        {
            new Student { Name = "Marko", Age = 20, Grade = 10 },
            new Student { Name = "Ana", Age = 17, Grade = 9 },
            new Student { Name = "Ivan", Age = 15, Grade = 8 }
        };



            // filter

            var filtered = students.Where(s => s.Grade > 8 || s.Age < 18);

            Console.WriteLine("FILTERED:");
            foreach(var s in students)
            {
                Console.WriteLine(s.Name);
            }



            // select
            var names = students.Select(s => s.Name);

            Console.WriteLine("\nNames:");
            foreach(var n in names)
            {
                Console.WriteLine($"{n}");
            }



            // any
            bool hasTopStudent = students.Any(s => s.Grade == 10);

            Console.WriteLine(hasTopStudent + " -> Has grade 10?");

            var result = students.Select(s => new
            {
                FullName = $"{s.Name}{s.Age}"
               
            });

        }
    }

    
}

