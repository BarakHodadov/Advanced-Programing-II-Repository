using ImageService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ImageServiceGUI.Views
{
    class LogTypeToColorConverter : IValueConverter
    {
        public LogTypeToColorConverter()
        {

        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (value)
            {
                case "INFO":
                    return "#FF3FCD16";
                case "WARNING":
                    return "Yellow";
                case "ERROR":
                    return "Red";
                default:
                    return "White";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
