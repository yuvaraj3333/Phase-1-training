using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

class FileReadExample
{
    static readonly string defaultContent1 = "This is File 1 - created by program.";
    static readonly string defaultContent2 = "This is File 2 - created by program.";

 
    static void ReadFileUsingThreads()
    {
        Thread t1 = new Thread(() => CreateIfNotExistsAndRead("file1.txt", defaultContent1));
        Thread t2 = new Thread(() => CreateIfNotExistsAndRead("file2.txt", defaultContent2));

        t1.Start();
        t2.Start();

        t1.Join();
        t2.Join();

        Console.WriteLine("\n✔ File reading with Threads completed.\n");
    }

    static void CreateIfNotExistsAndRead(string filePath, string defaultContent)
    {
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, defaultContent);
            Console.WriteLine($"[Thread] {filePath} created.");
        }

        string content = File.ReadAllText(filePath);
        Console.WriteLine($"[Thread] {filePath} contents:\n{content}\n");
    }

    static async Task ReadFileUsingTasksAsync()
    {
        Task task1 = CreateIfNotExistsAndReadAsync("file1.txt", defaultContent1);
        Task task2 = CreateIfNotExistsAndReadAsync("file2.txt", defaultContent2);

        await Task.WhenAll(task1, task2);

        Console.WriteLine("\n File reading with async/await completed.\n");
    }

    static async Task CreateIfNotExistsAndReadAsync(string filePath, string defaultContent)
    {
        if (!File.Exists(filePath))
        {
            await File.WriteAllTextAsync(filePath, defaultContent);
            Console.WriteLine($"[Async] {filePath} created.");
        }

        string content = await File.ReadAllTextAsync(filePath);
        Console.WriteLine($"[Async] {filePath} contents:\n{content}\n");
    }

    static async Task Main(string[] args)
    {
        Console.WriteLine("=== Using Threads ===");
        ReadFileUsingThreads();

        Console.WriteLine("=== Using async/await ===");
        await ReadFileUsingTasksAsync();
    }
}
