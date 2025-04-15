namespace MauiLabs.Lab4.Interfaces;

public interface IProgress
{
    public event EventHandler<double> ProgressChanged;
}