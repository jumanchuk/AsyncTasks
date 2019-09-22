using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {

        //ThreadPool.SetMaxThreads(1, 1);

        Task[] tasks = new Task[4];

        for (int i = 0; i < 4; i++)
        {
            Random random = new Random();
            int randomNumber = random.Next(0, 3000);

            var task = Task.Factory.StartNew(() => { Imprimir("Tarea "+(i+1), randomNumber); });
            Thread.Sleep(10);
            tasks[i] = task ;
        }

        Task.Factory.ContinueWhenAll(tasks, ConcurrentTasksCompleted).Wait() ;

        // Done
        // More Code...

        Console.WriteLine();
        Console.WriteLine("Press <enter> to exit.");
        Console.ReadLine();

    }

    static void Imprimir(string s, int t)
    {
        Thread.Sleep(t);
        Console.WriteLine(s + " - ThreadId ({0}) done at {1}.", Thread.CurrentThread.ManagedThreadId, DateTimeOffset.Now.ToString("HH:mm:ss"));
    }

    static void ConcurrentTasksCompleted(Task[] tasks)
    {
        int failures = tasks.Where(t => t.Exception != null).Count();
        Console.WriteLine();
        Console.WriteLine("Concurrent result: {0} successes and {1} failures at {2}.", tasks.Length - failures, failures,DateTime.Now);

    }

}


