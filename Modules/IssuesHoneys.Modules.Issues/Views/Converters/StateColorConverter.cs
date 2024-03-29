﻿using IssuesHoneys.Business.Types;
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IssuesHoneys.Modules.Issues.Views.Converters
{
    internal class StateColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Brush result;
            if ((State)value == State.IsOpen)
                result = Brushes.Green;
            else
                result = Brushes.Red;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
