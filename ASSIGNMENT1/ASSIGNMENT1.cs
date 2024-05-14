using System;
using System.Collections.Generic;
using System.Linq;

public class Student
{
    public string Name { get; set; }
    public Dictionary<string, List<int>> Marks { get; set; }

    public Student(string name)
    {
        Name = name;
        Marks = new Dictionary<string, List<int>>();
    }

    public double GetAverageMark(string subject)
    {
        if (Marks.ContainsKey(subject))
        {
            List<int> marks = Marks[subject];
            return marks.Count > 0 ? marks.Average() : 0;
        }
        return 0;
    }
}

class Program
{
    static List<Student> students = new List<Student>();

    static void Main(string[] args)
    {
        InitializeStudents();

        while (true)
        {
            Console.WriteLine("Choose an operation:");
            Console.WriteLine("1. View all students");
            Console.WriteLine("2. Add a student");
            Console.WriteLine("3. Update student marks");
            Console.WriteLine("4. Delete a student");
            Console.WriteLine("5. Exit");

            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    ViewAllStudents();
                    break;
                case "2":
                    AddStudent();
                    break;
                case "3":
                    UpdateStudentMarks();
                    break;
                case "4":
                    DeleteStudent();
                    break;
                case "5":
                    Console.WriteLine("Exiting program...");
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void InitializeStudents()
    {
        students.Add(new Student("Sruthi"));
        students.Add(new Student("Anusha"));
        students.Add(new Student("Pooja"));

        students[0].Marks.Add("Java", new List<int> { 95, 90, 86 });
        students[1].Marks.Add("Java", new List<int> { 77, 94, 91 });
        students[2].Marks.Add("Java", new List<int> { 67, 73, 85 });
    }

    static void ViewAllStudents()
    {
        Console.WriteLine("List of Students:");
        foreach (var student in students)
        {
            Console.WriteLine($"Student: {student.Name}");

            foreach (var subject in student.Marks)
            {
                double averageMark = student.GetAverageMark(subject.Key);
                Console.WriteLine($"- {subject.Key}: Marks - {string.Join(", ", subject.Value)}, Average Mark - {averageMark}");
            }

            Console.WriteLine();
        }
    }

    static void AddStudent()
    {
        Console.Write("Enter the name of the new student: ");
        string name = Console.ReadLine();

        students.Add(new Student(name));
        Console.WriteLine($"Student '{name}' added successfully.");
    }

    static void UpdateStudentMarks()
    {
        Console.Write("Enter the name of the student whose marks you want to update: ");
        string name = Console.ReadLine();

        var student = students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (student == null)
        {
            Console.WriteLine("Student '{name}' not found.");
            return;
        }

        Console.Write("Enter the subject (Java, .NET, Python): ");
        string subject = Console.ReadLine();

        if (!student.Marks.ContainsKey(subject))
        {
            student.Marks.Add(subject, new List<int>());
        }

        Console.Write("Enter new marks for {name} in {subject} (separated by space): ");
        string[] marksInput = Console.ReadLine().Split(' ');

        student.Marks[subject].Clear();
        foreach (var mark in marksInput)
        {
            if (int.TryParse(mark, out int result))
            {
                student.Marks[subject].Add(result);
            }
        }

        Console.WriteLine("Marks updated for {name} in {subject}.");
    }

    static void DeleteStudent()
    {
        Console.Write("Enter the name of the student you want to delete: ");
        string name = Console.ReadLine();

        var student = students.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (student == null)
        {
            Console.WriteLine("Student '{name}' not found.");
            return;
        }

        students.Remove(student);
        Console.WriteLine("Student '{name}' deleted successfully.");
    }
}
