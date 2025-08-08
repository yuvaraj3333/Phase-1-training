
using System;

public delegate void TestCompletedDelegate();

public class Teacher
{
    public void TestCompleted()
    {
        Console.WriteLine("Teacher: The student has completed the test.");
    }
}

public class Student
{
    private readonly TestCompletedDelegate _testCompletedCallback;

    public Student(TestCompletedDelegate testCompletedCallback)
    {
        _testCompletedCallback = testCompletedCallback;
    }

    public void WriteTest()
    {
        Console.WriteLine("Student: Writing the test...");
    
        System.Threading.Thread.Sleep(1000);

        Console.WriteLine("Student: Finished writing the test.");

        _testCompletedCallback?.Invoke();
    }
}

class Program
{
    static void Main()
    {
        Teacher teacher = new Teacher();

        Student student = new Student(teacher.TestCompleted);

        student.WriteTest();
    }
}

