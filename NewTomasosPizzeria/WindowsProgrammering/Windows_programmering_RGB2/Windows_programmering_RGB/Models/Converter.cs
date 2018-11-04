using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Windows_programmering_RGB.Models;

namespace Windows_programmering_RGB
{
    public class Converter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var rgbCode = value as ColorModel;

            if (rgbCode != null)
            {
                return $"#{rgbCode.RNumber:x2}{rgbCode.GNumber:x2}{rgbCode.BNumber:x2}";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
