using Application.Models;

namespace Application.Interfaces;

public interface IProgress
{
    public event EventHandler<ProgressEventArg> ProgressChanged;
}