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
            if (targetType != typeof(string))
                throw new InvalidOperationException("Must convert to a string");

            Log log = (Log)value;
            switch (log.Type)
            {
                case "Info":
                    return "#FF3FCD16";
                case "Warning":
                    return "Yellow";
                case "Error":
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
