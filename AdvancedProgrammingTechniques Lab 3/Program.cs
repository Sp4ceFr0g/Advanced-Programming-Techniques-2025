using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3
{
    internal class Program
    {
        static MyDatabaseContext db = new MyDatabaseContext();

        static void Main(string[] args)
        {
            // Clear database before run
            ClearDatabase();

            // Add students and classes
            db.Students.Add(new Student { FirstName = "John", LastName = "Doe" });
            db.Students.Add(new Student { FirstName = "Jane", LastName = "Smith" });
            db.Classes.Add(new Class { Name = "Math" });
            db.Classes.Add(new Class { Name = "Science" });
            db.SaveChanges();

            // Print
            PrintStudents();
            PrintClasses();

            // 2a. Add Teacher to Class
            var mathClass = db.Classes.FirstOrDefault(c => c.Name == "Math");
            if (mathClass != null)
            {
                var teacher = new Teacher { Name = "Mr. Teacher" };
                db.Teachers.Add(teacher);
                db.SaveChanges();
                mathClass.TeacherId = teacher.Id;
                mathClass.Teacher = teacher;
                db.SaveChanges();
            }

            // Retrieve and confirm
            PrintClasses();

            // 2b. Add another Teacher and Class, remove Teacher
            var newClass = new Class { Name = "History" };
            db.Classes.Add(newClass);
            var newTeacher = new Teacher { Name = "Ms. NewTeacher" };
            db.Teachers.Add(newTeacher);
            db.SaveChanges();
            newClass.TeacherId = newTeacher.Id;
            newClass.Teacher = newTeacher;
            db.SaveChanges();

            db.Teachers.Remove(newTeacher);
            db.SaveChanges();

            // Print Classes (foreign key should be null)
            PrintClasses();

            // Remove remaining class, print Teachers
            db.Classes.Remove(newClass);
            db.SaveChanges();
            PrintTeachers();

            // 3. One-to-many: Retrieve teacher classes
            var firstTeacher = db.Teachers.FirstOrDefault();
            if (firstTeacher != null)
            {
                var teacherClasses = firstTeacher.Classes ?? new List<Class>();
                Console.WriteLine("Teacher's Classes: " + string.Join(", ", teacherClasses));
            }

            // 4. Many-to-many: Add relationships (migration needed, but code assumes applied)
            var student1 = db.Students.First();
            var class1 = db.Classes.First();
            student1.Classes = new List<Class> { class1 };
            class1.Students = new List<Student> { student1 };
            db.SaveChanges();

            // Print to confirm
            PrintStudents();
            PrintClasses();

            Console.ReadKey();
        }

        static void ClearDatabase()
        {
            db.Students.RemoveRange(db.Students);
            db.Classes.RemoveRange(db.Classes);
            db.Teachers.RemoveRange(db.Teachers);
            db.SaveChanges();
        }

        static void PrintStudents() => Console.WriteLine("Students:\n" + string.Join("\n", db.Students.ToList()));
        static void PrintClasses() => Console.WriteLine("Classes:\n" + string.Join("\n", db.Classes.ToList()));
        static void PrintTeachers() => Console.WriteLine("Teachers:\n" + string.Join("\n", db.Teachers.ToList()));
    }
}