using System.Globalization;

namespace Plitkarka.ValueConverters;

public class StringToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string collection)
        {
            if (parameter is not null && parameter.Equals("Inverse"))
                return string.IsNullOrWhiteSpace(value?.ToString());
        }

        return !string.IsNullOrWhiteSpace(value?.ToString());
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}