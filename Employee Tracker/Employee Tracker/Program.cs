using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Friend
{
    public string Name { get; set; }
    public int Age { get; set; }

    public Friend(string name, int age)
    {
        Name = name;
        Age = age;
    }

    public void Introduce() => Console.WriteLine($"{Name}, {Age} years old");
}

class Employee : Friend
{
    public string JobTitle { get; set; }

    public Employee(string name, int age, string job) : base(name, age)
    {
        JobTitle = job;
    }

    public void Work() => Console.WriteLine($"{Name} is working as {JobTitle}");
}

class Program
{
    static string filePath = "employees.txt";

    static void Main()
    {
        List<Employee> employees = LoadEmployees();

        while (true)
        {
            Console.WriteLine("\n--- Employee Tracker ---");
            Console.WriteLine("1. Show all employees");
            Console.WriteLine("2. Add employee");
            Console.WriteLine("3. Remove employee");
            Console.WriteLine("4. Filter by job title");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    foreach (var e in employees)
                    {
                        e.Introduce();
                        e.Work();
                    }
                    break;
                case "2":
                    Console.Write("Name: "); string name = Console.ReadLine();
                    Console.Write("Age: "); int age = int.Parse(Console.ReadLine());
                    Console.Write("Job Title: "); string job = Console.ReadLine();
                    employees.Add(new Employee(name, age, job));
                    SaveEmployees(employees);
                    break;
                case "3":
                    Console.Write("Name to remove: "); string removeName = Console.ReadLine();
                    employees.RemoveAll(e => e.Name.Equals(removeName, StringComparison.OrdinalIgnoreCase));
                    SaveEmployees(employees);
                    break;
                case "4":
                    Console.Write("Job Title to filter: "); string filterJob = Console.ReadLine();
                    var filtered = employees.Where(e => e.JobTitle.Equals(filterJob, StringComparison.OrdinalIgnoreCase));
                    foreach (var e in filtered)
                    {
                        e.Introduce();
                        e.Work();
                    }
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static List<Employee> LoadEmployees()
    {
        List<Employee> employees = new List<Employee>();
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                employees.Add(new Employee(parts[0], int.Parse(parts[1]), parts[2]));
            }
        }
        return employees;
    }

    static void SaveEmployees(List<Employee> employees)
    {
        var lines = employees.Select(e => $"{e.Name},{e.Age},{e.JobTitle}");
        File.WriteAllLines(filePath, lines);
    }
}