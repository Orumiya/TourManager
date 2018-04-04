using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Markup;

namespace HappyTourManager
{
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
