using System.ComponentModel.DataAnnotations;
using Microsoft.Identity.Client;

namespace Test2.Utilities
{
    public class ValidEmailAttribute : ValidationAttribute
    {
        private readonly string allowedDomain;
        public ValidEmailAttribute(string allowedDomain)
        {
            this.allowedDomain = allowedDomain;
        }

        override public bool IsValid(object value)
        {
           string[] strings = value.ToString().Split('@');
           return strings[1] == allowedDomain;
        }
    }
}
