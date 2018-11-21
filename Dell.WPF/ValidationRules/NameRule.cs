using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dell.WPF
{
    class NameRule : ValidationRule
    {
        public NameRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = null;
            try
            {
                if (((string)value).Length > 0 && ((string)value).Length < 50)
                    name = (string)value;
                return ValidationResult.ValidResult;
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Illegal character or length" + e.Message);
            }  
        }
    }
}
