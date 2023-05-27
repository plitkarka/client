using System;
using System.Globalization;

namespace Plitkarka.ValueConverters
{
	public class HeartImageConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is true ? "liked.png" : "like.png";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

