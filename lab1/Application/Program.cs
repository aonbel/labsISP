using System.Collections;
using Application.Models;
using Application.Services;

namespace Application;

public static class Program
{
    public static void Main(string[] args)
    {
        var solver1 = new IntegralSolver { StepsCount = 50000 };
        var solver2 = new IntegralSolver { StepsCount = 50000 };
        var thread1 = new Thread(solver1.CountIntegral);
        var thread2 = new Thread(solver2.CountIntegral);
        thread1.Priority = ThreadPriority.Lowest;
        thread2.Priority = ThreadPriority.Highest;
        
        var progressBarsService = new ProgressBarsService();

        thread1.Start();
        thread2.Start();
        
        progressBarsService.AddAllThreadsFrom(solver1);
        progressBarsService.AddAllThreadsFrom(solver2);

        while (thread1.IsAlive)
        {
            Thread.Sleep(1000);
        }
        
        progressBarsService.ClearProgressBars();
        
        var solver = new IntegralSolver { StepsCount = 20000 };
        progressBarsService.AddAllThreadsFrom(solver);
        
        var threads = new List<Thread>();
        for (var i = 0; i < 5; i++)
        {
            threads.Add(new Thread(solver.CountIntegral));
            threads.Last().Start();
        }

        while (threads.Any(thread => thread.IsAlive))
        {
            Thread.Sleep(1000);
        }

        threads.Clear();
        progressBarsService.ClearProgressBars();
        solver.ThreadCount = 3;
        
        for (var i = 0; i < 5; i++)
        {
            threads.Add(new Thread(solver.CountIntegral));
            threads.Last().Start();
        }
    }
}