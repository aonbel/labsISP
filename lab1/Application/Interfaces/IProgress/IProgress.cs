namespace Application.Interfaces.IProgress;

public interface IProgress
{
    public event EventHandler<ProgressEventArg> ProgressChanged;
}