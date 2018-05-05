// <copyright file="ValueConverter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace HappyTourManager
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// Abstract converter class
    /// </summary>
    /// <typeparam name="T"> T param</typeparam>
    public abstract class ValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        private static T converter = null;

        /// <inheritdoc/>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (converter != null)
            {
                return converter;
            }
            else
            {
                converter = new T();
                return converter;
            }
        }

        /// <inheritdoc/>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <inheritdoc/>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
