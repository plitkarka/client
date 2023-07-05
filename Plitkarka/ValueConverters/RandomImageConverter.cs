using System;
using System.Globalization;

namespace Plitkarka.ValueConverters
{
	public class RandomImageConverter : IValueConverter
	{
        private static readonly Random random = new();
        private static readonly string[] images = { "mascot_in_development1.png", "mascot_in_development2.png" };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int index = random.Next(images.Length);
            return images[index];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

