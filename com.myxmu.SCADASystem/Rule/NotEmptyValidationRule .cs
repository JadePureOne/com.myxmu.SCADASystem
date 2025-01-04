using System.Globalization;
using System.Windows.Controls;

namespace com.myxmu.SCADASystem.Rule
{
    public class NotEmptyValidationRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult(false, "必填");
            }

            return ValidationResult.ValidResult;
        }
    }
}