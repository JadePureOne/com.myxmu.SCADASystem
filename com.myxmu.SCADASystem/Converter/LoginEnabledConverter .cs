using System;
using System.Globalization;
using System.Windows.Data;


namespace com.myxmu.SCADASystem.Converter
{
    
    public class LoginEnabledConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            string userName = values[0] as string;
            string password = values[1] as string;

            return !string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(password);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
