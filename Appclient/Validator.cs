using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Appclient
{
    internal static class Validator
    {
        public static string? MaxLength(string str, string name, int maxLenght)
        {
            return str.Length > maxLenght ? name + " can't be more than " + maxLenght + " characters!" : null;
        }

        public static string? MinLength(string str, string name, int minLenght) {
            return str.Length < minLenght ? name + " should be at least " + minLenght + " characters!" : null;
        }

        public static string? ContaintsSpecialCharacters(string str, string name) {
            return str.Any(ch => !char.IsLetterOrDigit(ch)) ? name + " can only contain letters or digits!": null;
        }

        public static string? IsntNull(string str, string name)
        {
            return str == null ? name + " is required!" : null;
        }

        public static string? Any(string str, string name, int minLength, int maxLength) {
            string? errorMsg = IsntNull(str, name);
            if (errorMsg != null) { return errorMsg; }
            errorMsg = MinLength(str, name, minLength);
            if (errorMsg != null) { return errorMsg; }
            errorMsg = MaxLength(str, name, maxLength);
            if (errorMsg != null) { return errorMsg; }
            errorMsg = ContaintsSpecialCharacters(str, name);
            if (errorMsg != null) { return errorMsg; }
            
            return null;
        }
    }
}
