// <copyright file="TrueFalseConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager.Converters
{
    using System;
    using System.Globalization;
    using System.Windows.Data;

    /// <summary>
    /// TrueFalse Converter class
    /// </summary>
    public class TrueFalseConverter : IValueConverter
    {
        /// <summary>
        /// Convert from string to bool
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">targettype</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>true if value "1"</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                return value.ToString() == "1";
            }

            return false;
        }

        /// <summary>
        /// Convert from bool to string
        /// </summary>
        /// <param name="value">value</param>
        /// <param name="targetType">target</param>
        /// <param name="parameter">parameter</param>
        /// <param name="culture">culture</param>
        /// <returns>"1" if value true and "0" others</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool bValue = (bool)value;
            if (bValue)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }
}
