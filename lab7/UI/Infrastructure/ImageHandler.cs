namespace UI.Infrastructure;

public class ImageHandler
{
    public async Task SaveImageToStorage(string imageName, FileResult fileResult)
    {
        var imagesFolder = Path.Combine(FileSystem.AppDataDirectory, "Images");
        Directory.CreateDirectory(imagesFolder); 

        var fileName = $"{imageName}{Path.GetExtension(fileResult.FileName)}";
        var destinationPath = Path.Combine(imagesFolder, fileName);

        await using var sourceStream = await fileResult.OpenReadAsync();
        if (File.Exists(destinationPath))
        {
            File.Delete(destinationPath);
        }
        await using var destinationStream = File.Create(destinationPath);
        await sourceStream.CopyToAsync(destinationStream);
    }
}