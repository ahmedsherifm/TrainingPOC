using System;
using System.Globalization;
using System.Windows.Data;

namespace ECommerce.Converters
{
    public class DecimalToCurrencyStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return $"{value:C}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
