namespace Application.Interfaces.IProgress;

public class ProgressEventArg : EventArgs
{
    public double Progress { get; init; }
}