
using System;
using System.Globalization;

namespace Plitkarka.ValueConverters
{
	public class TextToSubscriptionConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool val)
                return val is true ? "Читаю" : "Читати";

            return "Читати";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

