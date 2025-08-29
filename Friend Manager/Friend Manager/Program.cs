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

class Program
{
    static string filePath = "friends.txt";

    static void Main()
    {
        List<Friend> friends = LoadFriends();

        while (true)
        {
            Console.WriteLine("\n--- Friend Manager ---");
            Console.WriteLine("1. Show all friends");
            Console.WriteLine("2. Add friend");
            Console.WriteLine("3. Remove friend");
            Console.WriteLine("4. Filter friends over 21");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    foreach (var f in friends) f.Introduce();
                    break;
                case "2":
                    Console.Write("Name: "); string name = Console.ReadLine();
                    Console.Write("Age: "); int age = int.Parse(Console.ReadLine());
                    friends.Add(new Friend(name, age));
                    SaveFriends(friends);
                    break;
                case "3":
                    Console.Write("Name to remove: "); string removeName = Console.ReadLine();
                    friends.RemoveAll(f => f.Name.Equals(removeName, StringComparison.OrdinalIgnoreCase));
                    SaveFriends(friends);
                    break;
                case "4":
                    var over21 = friends.Where(f => f.Age > 21);
                    foreach (var f in over21) f.Introduce();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static List<Friend> LoadFriends()
    {
        List<Friend> friends = new List<Friend>();
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                friends.Add(new Friend(parts[0], int.Parse(parts[1])));
            }
        }
        return friends;
    }

    static void SaveFriends(List<Friend> friends)
    {
        var lines = friends.Select(f => $"{f.Name},{f.Age}");
        File.WriteAllLines(filePath, lines);
    }
}
