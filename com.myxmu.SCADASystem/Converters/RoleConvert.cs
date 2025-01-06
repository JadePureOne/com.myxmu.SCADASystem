using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace com.myxmu.SCADASystem.Converters
{
    public class RoleConvert: IValueConverter   
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            
            return System.Convert.ToInt32(value) == 0 ? "管理员" : "普通用户";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
