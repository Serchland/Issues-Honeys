using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IssuesHoneys.Modules.Issues.Views.Converters
{
    class IconDetailsVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result = Visibility.Collapsed;

            if (value is null || (int)value > 0)
                result = Visibility.Visible;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
