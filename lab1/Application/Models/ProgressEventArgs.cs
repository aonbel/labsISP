namespace Application.Models;

public class ProgressEventArg : EventArgs
{
    public double Progress { get; init; }
}