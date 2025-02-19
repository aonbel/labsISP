using Application.Models;

namespace Application.Services;

public class ProgressBarsService
{
    private readonly List<ProgressBar> _progressBars = [];

    public void AddProgressBar(ProgressBar progressBar)
    {
        _progressBars.Add(progressBar);

        progressBar.ProgressBarUpdated += (sender, args) =>
        {
            PrintProgressBars();
        };
    }

    public void ClearProgressBars()
    {
        _progressBars.Clear();
    }

    private void PrintProgressBars()
    {
        Console.Clear();
        foreach (var progressBar in _progressBars)
        {
            Console.WriteLine(progressBar);
        }
    }
}