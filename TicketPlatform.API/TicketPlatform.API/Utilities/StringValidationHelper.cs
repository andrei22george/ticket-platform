using System.Text.RegularExpressions;

namespace TicketPlatform.API.Utilities
{
    public static class StringValidationHelper
    {
        public static bool IsEmail(string stringToValidate)
        {
            var regexString = @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/";
            var regex = new Regex(regexString, RegexOptions.Compiled);

            return regex.IsMatch(stringToValidate);
        }

        public static bool IsPhone(string stringToValidate)
        {
            var regexString = @"^(\+4|)?(07[0-8]{1}[0-9]{1}|02[0-9]{2}|03[0-9]{2}){1}?(\s|\.|\-)?([0-9]{3}(\s|\.|\-|)){2}$";
            var regex = new Regex(regexString, RegexOptions.Compiled);

            return regex.IsMatch(stringToValidate);
        }

        public static bool IsURL(string stringToValidate)
        {
            var regexString = @"/^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/";
            var regex = new Regex(regexString, RegexOptions.Compiled);

            return regex.IsMatch(stringToValidate);
        }
    }
}