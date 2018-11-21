using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Dell.WPF
{
    class EmailAddressRule : ValidationRule
    {
        public EmailAddressRule()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string email = null;
            ValidationResult validationResult = null;
            try
            {
                if (((string)value).Length < 0 && ((string)value).Length > 50)
                {
                    email = (string)value;
                }
            }
            catch (Exception ex)
            {
                validationResult = new ValidationResult(false, "Illegal character or length" + ex.Message);
            }

            if (email != null && email.Length < 50)
            {

                try
                {

                    //    //MailAddress emailAddress = new MailAddress(email);
                    Regex regex = new Regex(@"^[\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

                    if (!regex.IsMatch((string)email))
                        validationResult = new ValidationResult(false, "Incorrect email format");
                    else validationResult = ValidationResult.ValidResult;
                }
                catch (Exception ex)
                {
                    validationResult = new ValidationResult(false, "Illegal character" + ex.Message);
                }
            }
            else
            {
                validationResult = ValidationResult.ValidResult;  
            }
            return validationResult;
        }
    }
}
