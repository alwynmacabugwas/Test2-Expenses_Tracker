using System.ComponentModel.DataAnnotations;

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
