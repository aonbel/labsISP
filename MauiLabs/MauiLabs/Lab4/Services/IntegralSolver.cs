using IProgress = MauiLabs.Lab4.Interfaces.IProgress;

namespace MauiLabs.Lab4.Services;

public class IntegralSolver : IProgress
{
    public Func<double, double> Function { get; set; } = Math.Sin;

    public (double start, double end) Segment { get; set; } = (start: 0, end: 1);

    public int StepsCount { get; set; } = 1000000;

    public int OptimizationCyclesCount { get; set; } = 1000;

    public double IntegralResult { get; private set; }

    public void CountIntegral(CancellationToken cancellationToken)
    {
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

            if (cancellationToken.IsCancellationRequested)
            {
                ProgressChanged?.Invoke(this, 0);
                return;
            }

            ProgressChanged?.Invoke(this, (currentPosition - Segment.start) / segmentLength);
        }
        
        ProgressChanged?.Invoke(this, 1);
    }

    public event EventHandler<double>? ProgressChanged;
}