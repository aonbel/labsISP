using System.Text.Json;

namespace lab2.Services;

public class StreamService<TEntity>
{
    private readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);
    
    public async Task WriteToStreamAsync(Stream stream, IEnumerable<TEntity> data, IProgress<string> progress)
    {
        progress.Report($"Entered writing method from stream with thread {Environment.CurrentManagedThreadId}");
        
        await _semaphore.WaitAsync();
        
        await using var writer = new StreamWriter(stream, leaveOpen: true);
        
        progress.Report($"Writing data to stream with thread {Environment.CurrentManagedThreadId} started");
        
        foreach (var entity in data)
        {
            await Task.Delay(10);
            await writer.WriteLineAsync(JsonSerializer.Serialize(entity));
        }
        
        await writer.FlushAsync();
        progress.Report($"Writing data to stream with thread {Environment.CurrentManagedThreadId} ended");
        
        _semaphore.Release();
    }

    public async Task CopyFromStreamAsync(Stream stream, string filename, IProgress<string> progress)
    {
        progress.Report($"Entered copying method from stream with thread {Environment.CurrentManagedThreadId}");
        
        await _semaphore.WaitAsync();

        if (stream.CanSeek)
        {
            stream.Position = 0;
        }
        
        using var reader = new StreamReader(stream);
        await using var fileStream = new FileStream(filename, FileMode.Create);
        await using var writer = new StreamWriter(fileStream);
        
        progress.Report($"Copying data from stream with thread {Environment.CurrentManagedThreadId} started");

        while (!reader.EndOfStream)
        {
            await writer.WriteLineAsync(await reader.ReadLineAsync());
        }
        
        progress.Report($"Copying data from stream with thread {Environment.CurrentManagedThreadId} ended");
        
        _semaphore.Release();
    }

    public async Task<int> GetStatisticsAsync(string filename, Func<TEntity, bool> filter)
    {
        await using var fileStream = new FileStream(filename, FileMode.Open);
        using var reader = new StreamReader(fileStream);
        var result = 0;

        while (!reader.EndOfStream)
        {
            var entityJson = await reader.ReadLineAsync();

            if (entityJson is null)
            {
                continue;
            }

            var entity = JsonSerializer.Deserialize<TEntity>(entityJson);

            if (entity is null)
            {
                throw new FileLoadException();
            }

            if (filter(entity))
            {
                result++;
            }
        }

        return result;
    }
}