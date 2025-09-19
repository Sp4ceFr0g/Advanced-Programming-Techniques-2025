using Microsoft.EntityFrameworkCore;
using System;

public class Student
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public ICollection<Class> Classes { get; set; } // For many-to-many

    public override string ToString() => $"Student: {Id} - {FirstName} {LastName}";
}

public class Class
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? TeacherId { get; set; } // Foreign key
    public Teacher Teacher { get; set; }

    public ICollection<Student> Students { get; set; } // For many-to-many

    public override string ToString() => $"Class: {Id} - {Name}, TeacherId: {TeacherId}";
}

public class Teacher
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<Class> Classes { get; set; } // For one-to-many

    public override string ToString() => $"Teacher: {Id} - {Name}";
}

public class MyDatabaseContext : DbContext
{
    public string DbPath { get; }

    public MyDatabaseContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "blogging.db");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");

    public DbSet<Student> Students { get; set; }
    public DbSet<Class> Classes { get; set; }
    public DbSet<Teacher> Teachers { get; set; }
}