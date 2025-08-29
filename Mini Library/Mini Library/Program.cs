using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
}

class Program
{
    static string filePath = "books.txt";

    static void Main()
    {
        List<Book> books = LoadBooks();

        while (true)
        {
            Console.WriteLine("\n--- Mini Library ---");
            Console.WriteLine("1. Show all books");
            Console.WriteLine("2. Add book");
            Console.WriteLine("3. Remove book");
            Console.WriteLine("4. Search by author");
            Console.WriteLine("5. Filter books after year");
            Console.WriteLine("6. Exit");
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    foreach (var b in books)
                        Console.WriteLine($"{b.Title} by {b.Author} ({b.Year})");
                    break;
                case "2":
                    Console.Write("Title: "); string title = Console.ReadLine();
                    Console.Write("Author: "); string author = Console.ReadLine();
                    Console.Write("Year: "); int year = int.Parse(Console.ReadLine());
                    books.Add(new Book { Title = title, Author = author, Year = year });
                    SaveBooks(books);
                    break;
                case "3":
                    Console.Write("Title to remove: "); string removeTitle = Console.ReadLine();
                    books.RemoveAll(b => b.Title.Equals(removeTitle, StringComparison.OrdinalIgnoreCase));
                    SaveBooks(books);
                    break;
                case "4":
                    Console.Write("Author to search: "); string searchAuthor = Console.ReadLine();
                    var byAuthor = books.Where(b => b.Author.Equals(searchAuthor, StringComparison.OrdinalIgnoreCase));
                    foreach (var b in byAuthor)
                        Console.WriteLine($"{b.Title} by {b.Author} ({b.Year})");
                    break;
                case "5":
                    Console.Write("Year to filter after: "); int filterYear = int.Parse(Console.ReadLine());
                    var recent = books.Where(b => b.Year > filterYear);
                    foreach (var b in recent)
                        Console.WriteLine($"{b.Title} by {b.Author} ({b.Year})");
                    break;
                case "6":
                    return;
                default:
                    Console.WriteLine("Invalid choice!");
                    break;
            }
        }
    }

    static List<Book> LoadBooks()
    {
        List<Book> books = new List<Book>();
        if (File.Exists(filePath))
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(',');
                books.Add(new Book { Title = parts[0], Author = parts[1], Year = int.Parse(parts[2]) });
            }
        }
        return books;
    }

    static void SaveBooks(List<Book> books)
    {
        var lines = books.Select(b => $"{b.Title},{b.Author},{b.Year}");
        File.WriteAllLines(filePath, lines);
    }
}