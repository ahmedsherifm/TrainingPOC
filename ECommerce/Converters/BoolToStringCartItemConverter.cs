using System;
using System.Globalization;
using System.Windows.Data;

namespace ECommerce.Converters
{
    public class BoolToStringCartItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
                return "In Stock";

            return "Out Of Stock";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
