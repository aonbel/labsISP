using Application.Interfaces;
using Application.Models;

namespace Application.Services;

public class ProgressBarsService
{
    private readonly List<ProgressBar> _progressBars = [];

    public void AddAllThreadsFrom(IProgress entity)
    {
        entity.ProgressChanged += (_, args) =>
        {
            foreach (var t in _progressBars.Where(t => int.Parse(t.Name) == args.ThreadId))
            {
                t.Update(args.Progress);
                return;
            }

            _progressBars.Add(new ProgressBar { Name = args.ThreadId.ToString() });
            _progressBars.Last().ProgressBarUpdated += (_, _) => { PrintProgressBars(); };
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