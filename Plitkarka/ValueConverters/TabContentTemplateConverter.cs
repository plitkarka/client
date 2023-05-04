using System;
using System.Globalization;

namespace Plitkarka.ValueConverters
{
    public class TabContentTemplateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int selectedIndex)
            {
                return selectedIndex switch
                {
                    0 => new DataTemplate(() =>
                    {
                        return new Label
                        {
                            Text = "Плітки Content",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Colors.Black
                        };
                    }),

                    1 => new DataTemplate(() =>
                    {
                        return new Label
                        {
                            Text = "Репліти Content",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Colors.Black
                        };
                    }),

                    2 => new DataTemplate(() =>
                    {
                        return new Label
                        {
                            Text = "Медіа Content",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Colors.Black
                        };
                    }),

                    3 => new DataTemplate(() =>
                    {
                        return new Label
                        {
                            Text = "Вподобайки Content",
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            TextColor = Colors.Black
                        };
                    }),

                    _ => null
                };
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

