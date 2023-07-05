using System;
using System.Collections.ObjectModel;
using System.Globalization;
using Plitkarka.Models;

namespace Plitkarka.ValueConverters
{
    public class PostsWithImagesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ObservableCollection<Post> posts)
            {
                return new ObservableCollection<Post>(posts.Where(post => !string.IsNullOrEmpty(post.PostImage)));
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

