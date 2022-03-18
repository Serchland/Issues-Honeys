using IssuesHoneys.Business;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IssuesHoneys.Modules.Issues.Views.Converters
{
    public class StateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush result;
            if ((State)value == State.IsOpen)
                result = Brushes.Red;
            else
                result = Brushes.Green;

            
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
