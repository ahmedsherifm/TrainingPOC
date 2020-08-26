using System;
using System.Collections.Generic;
using System.Text;

namespace ECommerce.Main.Services
{
    public interface IValidationService
    {
        bool IsRegexValid(string value, string expression);
    }
}
