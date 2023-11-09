using System;
using System.Globalization;
using Xamarin.Forms;

namespace AzieKitchen.Converters
{
    public class TruncateValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string text && text.Length > 20)
            {
                return $"{text.Substring(0, 18)}...";
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
