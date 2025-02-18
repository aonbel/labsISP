using Application.Interfaces.IProgress;

namespace Application.Entities;

public class ProgressBar
{
    public event EventHandler? ProgressBarUpdated;
    public string Name { get; set; }
    public int Length { get; set; } = 100;
    public int CurrentPercentage { get; set; } = 0;
    public int CurrentPosition { get; set; } = 0;

    public ProgressBar(IProgress progress)
    {
        progress.ProgressChanged += (sender, args) =>
        {
            var newPercentage = (int)(args.Progress * 100);
            var newPosition = (int)(args.Progress * Length);
            if (newPosition != CurrentPosition || newPercentage != CurrentPercentage)
            {
                CurrentPercentage = newPercentage;
                CurrentPosition = newPosition;
                ProgressBarUpdated?.Invoke(this, EventArgs.Empty);
            }
        };
    }

    public override string ToString()
    {
        return Name + ": " + new string('#', CurrentPosition) + new string('.', Length - CurrentPosition) + " " + CurrentPercentage + " %";
    }
}