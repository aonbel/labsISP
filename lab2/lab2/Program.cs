using lab2.Entities;
using lab2.Services;
using LoremNET;

namespace lab2;

internal abstract class Program
{
    public static async Task Main(string[] args)
    {
        var computerBrands = new List<ComputerBrand>();

        for (var i = 0; i < 1000; i++)
        {
            computerBrands.Add(new ComputerBrand
            {
                Id = i,
                Name = Lorem.Words(2),
                Quantity = Lorem.Number(0, long.MaxValue)
            });
        }

        var streamService = new StreamService<ComputerBrand>();
        var stream = new MemoryStream();
        var progress = new Progress<string>();
        progress.ProgressChanged += (_, e) => Console.WriteLine(e);
        
        var writerTask = streamService.WriteToStreamAsync(stream, computerBrands, progress);

        Thread.Sleep(100);
        
        var readerTask = streamService.CopyFromStreamAsync(stream, "computerBrands.json", progress);
        
        Task.WaitAll(writerTask, readerTask);

        Console.WriteLine(await streamService.GetStatisticsAsync("computerBrands.json", brand => brand.Id > 10));
    }
}