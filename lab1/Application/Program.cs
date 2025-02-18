using Application.Entities;
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
        
        progressBarsService.AddProgressBar(new ProgressBar(solver1) {Name = "lowest"});
        progressBarsService.AddProgressBar(new ProgressBar(solver2) {Name = "highest"});

        while (thread1.IsAlive)
        {
            Thread.Sleep(1000);
        }
        
        progressBarsService.ClearProgressBars();
        
        var solver = new IntegralSolver { StepsCount = 50000 };
        progressBarsService.AddProgressBar(new ProgressBar(solver) {Name = "curr"});

        for (int i = 0; i < 5; i++)
        {
            Thread thread = new Thread(solver.CountIntegral);
            thread.Start();
        }
    }
}