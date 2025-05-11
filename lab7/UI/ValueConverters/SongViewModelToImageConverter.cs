using System.Globalization;
using UI.ViewModels;

namespace UI.ValueConverters;

public class SongViewModelToImageConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        var songViewModelId = value as int?;

        var imagesFolder = Path.Combine(FileSystem.AppDataDirectory, "Images");
        Directory.CreateDirectory(imagesFolder);

        var matchingFiles = Directory.EnumerateFiles(imagesFolder, $"{songViewModelId}.*");
        var filePath = matchingFiles.FirstOrDefault();

        return filePath ?? "placeholder.png"; 
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}