using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace ECommerce.Main.Services
{
    public class ValidationService : IValidationService
    {
        public bool IsRegexValid(string value, string expression)
        {
            if (string.IsNullOrEmpty(expression))
                return true;

            if (string.IsNullOrEmpty(value))
                return false;

            Regex regex = new Regex(expression);
            Match match = regex.Match(value.ToString());

            if (match == null || match == Match.Empty)
                return false;

            return true;
        }
    }
}
