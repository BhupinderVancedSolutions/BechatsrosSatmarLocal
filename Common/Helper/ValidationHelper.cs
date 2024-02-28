using System.Text.RegularExpressions;

namespace Common.Helper
{
    public class ValidationHelper
    {
        public static bool ValidateEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ValidPhoneNumber(string phoneNumber)
        {
            if (!string.IsNullOrEmpty(phoneNumber))
            {
                Regex regex = new Regex(@"^[0-9]{10}$");
                Match match = regex.Match(phoneNumber);
                if (match.Success)
                    return true;
                else
                    return false;
            }
            else
            {
                return true;
            }
        }
    }
}
