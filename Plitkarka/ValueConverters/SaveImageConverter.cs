using System;
using System.Globalization;

namespace Plitkarka.ValueConverters
{
	public class SaveImageConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is true ? "saved.png" : "save.png";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

