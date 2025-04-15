using MauiLabs.Lab4.Services;

namespace MauiLabs.Lab4;

public partial class Lab4 : ContentPage
{
    private readonly IntegralSolver _integralSolver = new();
    private CancellationTokenSource? _cancellationTokenSource;
    
    public Lab4()
    {
        InitializeComponent();
        
        _integralSolver.ProgressChanged += (_, progress) =>
        {
            MainThread.InvokeOnMainThreadAsync(() =>
            {
                if (Math.Abs(progress - ProgressBar.Progress) > 0.001)
                {
                    ProgressBar.Progress = progress;
                }

                if (Math.Abs(progress - 1) < 0.001)
                {
                    Label.Text = string.Format($"Result of calculation is {_integralSolver.IntegralResult}");
                }

                return Task.CompletedTask;
            }); 
        };
    }
    
    private void StartButton_OnClicked(object? sender, EventArgs e)
    {
        if (_cancellationTokenSource != null)
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource.Dispose();
        }
        
        _cancellationTokenSource = new CancellationTokenSource();
        
        ProgressBar.Progress = 0;
        
        Task.Run(() => _integralSolver.CountIntegral(_cancellationTokenSource.Token));
    }

    private void StopButton_OnClicked(object? sender, EventArgs e)
    {
        _cancellationTokenSource?.Cancel();
    }
}