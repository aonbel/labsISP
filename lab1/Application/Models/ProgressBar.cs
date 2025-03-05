using Application.Interfaces;

namespace Application.Models;

public class ProgressBar
{
    public event EventHandler? ProgressBarUpdated;
    public string Name { get; set; }
    public int Length { get; set; } = 100;
    public int CurrentPercentage { get; set; }
    public int CurrentPosition { get; set; }

    public void Update(double progress)
    {
        var newPercentage = (int)(progress * 100);
        var newPosition = (int)(progress * Length);
        if (newPosition == CurrentPosition && newPercentage == CurrentPercentage)
        {
            return;
        }
        CurrentPercentage = newPercentage;
        CurrentPosition = newPosition;
        ProgressBarUpdated?.Invoke(this, EventArgs.Empty);
    }

    public override string ToString()
    {
        return Name + ": " + new string('#', CurrentPosition) + new string('.', Length - CurrentPosition) + " " + CurrentPercentage + " %";
    }
}