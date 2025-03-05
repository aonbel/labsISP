using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class IntegralSolver : IProgress
{
    public int ThreadCount
    {
        set => _semaphore = new SemaphoreSlim(value, value);
    }

    private SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

    public Func<double, double> Function { get; set; } = Math.Sin;

    public (double start, double end) Segment { get; set; } = (start: 0, end: 1);

    public int StepsCount { get; set; } = 100000000;

    public int OptimizationCyclesCount { get; set; } = 100000;

    public double IntegralResult { get; private set; }

    public void CountIntegral()
    {
        _semaphore.Wait();

        var segmentLength = Segment.end - Segment.start;
        var step = segmentLength / StepsCount;
        IntegralResult = 0;

        for (var currentPosition = Segment.start; currentPosition < Segment.end; currentPosition += step)
        {
            IntegralResult += Function(currentPosition) * step;

            for (var i = 0; i < OptimizationCyclesCount; i++)
            {
                var something = currentPosition * 2;
            }

            ProgressChanged?.Invoke(this,
                new ProgressEventArg
                {
                    Progress = (currentPosition - Segment.start + step) / segmentLength,
                    ThreadId = Environment.CurrentManagedThreadId,
                });
        }

        _semaphore.Release();
    }

    public event EventHandler<ProgressEventArg>? ProgressChanged;
}