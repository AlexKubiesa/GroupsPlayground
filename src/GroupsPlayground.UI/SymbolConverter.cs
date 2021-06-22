﻿using System;
using System.Globalization;
using System.Windows.Data;
using GroupsPlayground.Domain;

namespace GroupsPlayground.UI
{
    [ValueConversion(typeof(Symbol), typeof(string))]
    public class SymbolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            value?.ToString();

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            (value is string str) ? new Symbol(str) : null;
    }
}