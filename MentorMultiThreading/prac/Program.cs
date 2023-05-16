using System;
using System.Threading;

class Program
{
    private static ManualResetEventSlim manualResetEvent;

    static void Main(string[] args)
    {
        manualResetEvent = new ManualResetEventSlim(false);

        // Queue work items in the ThreadPool
        ThreadPool.QueueUserWorkItem(WorkItem1);
        ThreadPool.QueueUserWorkItem(WorkItem2);
        ThreadPool.QueueUserWorkItem(WorkItem3);

        // Wait for all work items to complete
        manualResetEvent.Wait();

        // All work items have completed
        Console.WriteLine("All work items completed.");
    }

    static void WorkItem1(object state)
    {
        manualResetEvent.Reset();
        // Simulate work
        Thread.Sleep(2000);

        Console.WriteLine("WorkItem1 completed.");
        manualResetEvent.Set(); // Signal completion
    }

    static void WorkItem2(object state)
    {
        manualResetEvent.Reset();
        // Simulate work
        Thread.Sleep(3000);

        Console.WriteLine("WorkItem2 completed.");
        manualResetEvent.Set(); // Signal completion
    }

    static void WorkItem3(object state)
    {
        manualResetEvent.Reset();
        // Simulate work
        Thread.Sleep(1500);

        Console.WriteLine("WorkItem3 completed.");
        manualResetEvent.Set(); // Signal completion
    }
}
