using System.Text.RegularExpressions;
namespace PAM.Helpers
{
    public class Checkers
    {
        public static bool checkingInputs(string inputvalue)
        {
            return ((string.IsNullOrEmpty(inputvalue) || string.IsNullOrWhiteSpace(inputvalue)) ? true : false);
        }

        public static bool IsPhoneNumber(string number)
        {
            return Regex.Match(number, @"^(\+[0-9]{9})$").Success;
        }
    }
}