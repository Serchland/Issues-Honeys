using IssuesHoneys.Business.Types;
using System;
using System.Globalization;
using System.Windows.Data;

namespace IssuesHoneys.Core.Views.Converters
{
    public class AssigneesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var user = (User)value;

            return String.Format("{0}{1}", user.Name, user.SurName);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
