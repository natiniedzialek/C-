// Threads

namespace lab5;
using System;

public static class Program {
    public static void Main(String [] args)
    {
        Task1 t1 = new Task1(10, 3, 1);
        t1.Start();
        
        Task2 t2 = new Task2(@".\data");
        t2.Start();

        Task3 t3 = new Task3(".cs", @".");
        t3.Start();
    }
}
