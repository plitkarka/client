using System;
using System.Globalization;
using Plitkarka.Infrastructure.StringResources;

namespace Plitkarka.ValueConverters;

public class ResourcesValueConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var key = parameter as string;

        return string.IsNullOrEmpty(key) ? null : Strings.ResourceManager.GetString(key, culture) ?? null;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return null;
    }
}

