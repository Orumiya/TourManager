namespace HappyTourManager
{
    using System;
    using System.Globalization;
    using System.Windows.Data;
    using System.Windows.Markup;

    /// <summary>
    /// Abstract converter class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ValueConverter<T> : MarkupExtension, IValueConverter where T: class, new()
    {
        private static T converter = null;

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

        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);
    }
}
