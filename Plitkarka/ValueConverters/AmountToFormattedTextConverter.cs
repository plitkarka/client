using System;
using System.Globalization;

namespace Plitkarka.ValueConverters
{
	public class AmountToFormattedTextConverter : IValueConverter
	{
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int number)
            {
                return FormatNumber(number);
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private static string FormatNumber(int number)
        {
            string formattedNumber = number >= 1000 ? $"{number / 1000.0:F1} тис." : number.ToString();
            return $"Пліток: {formattedNumber}";
        }
    }
}

