using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WpfApp5.ViewModel.Converters
{
    public class WeatherIcon : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string path = "C:/Users/f0zzi/source/repos/WpfApp5/WpfApp5/bin/Debug/1-20/"; 
            int tmp = System.Convert.ToInt32(value);
            if (tmp < 10)
                path += "0";
            return path + tmp.ToString() + "-s.png";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
