namespace Application.Models;

public class ProgressEventArg : EventArgs
{
    public double Progress { get; init; }
    public int ThreadId { get; init; }
}