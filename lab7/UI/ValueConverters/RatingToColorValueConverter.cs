using System.Globalization;

namespace UI.ValueConverters;

internal class RatingToColorValueConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return (int)value! switch
        {
            <= 2 => Colors.Gold,
            <= 5 => Colors.Silver,
            <= 10 => Colors.Chocolate,
            _ => Colors.Transparent
        };
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}