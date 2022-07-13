//using IssuesHoneys.Business.Types;
//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Data;

//namespace IssuesHoneys.Modules.Issues.Views.Converters
//{
//    public class SelectedItemDetailConverter : IValueConverter
//    {
//        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            var selectedDetailItem = value as Issue;
//            return string.Format("{0} {1}{2}", selectedDetailItem.Title, "#", selectedDetailItem.Number);
//        }

//        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
