using System;
using System.Globalization;

namespace Plitkarka.ValueConverters
{
	public class RandomTextConverter : IValueConverter
	{
        private static readonly Random random = new();
        private static readonly string[] labels = { "Розділ у розробці", "Щось пішло не так, почекайте " };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = random.Next(labels.Length);
            return labels[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

