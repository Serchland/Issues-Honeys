using IssuesHoneys.Business.Types;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace IssuesHoneys.Modules.Issues.Views.Converters
{
    internal class IconMilestoneVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Visibility result = Visibility.Collapsed;

            if (value != null && value is IList<Milestone> milestoneItems && milestoneItems.Count > 0)
                result = Visibility.Visible;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
