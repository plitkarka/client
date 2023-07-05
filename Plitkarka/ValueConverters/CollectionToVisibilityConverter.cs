using System;
using System.Collections;
using System.Globalization;

namespace Plitkarka.ValueConverters
{

    public class CollectionToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ICollection collection)
            {
                if (parameter is not null && parameter.Equals("Inverse"))
                    return true;

                return collection.Count == 0;
            }

            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

