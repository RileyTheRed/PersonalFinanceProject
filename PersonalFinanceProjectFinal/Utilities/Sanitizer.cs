using System;
using System.Text.RegularExpressions;

namespace PersonalFinanceProjectFinal.Utilities
{
    class Sanitizer
    {
        private static Regex amountFormat = new Regex("([0-9]{1})([0-9]*)(.)([0-9]{2})");

        public static bool ValidNewExpense(string amount, DateTime date)
        {
            if (!amountFormat.IsMatch(amount) || (date > DateTime.Now.Date))
                return false;
            return true;
        }

        public static bool ValidAmount(string amount)
        {
            return amountFormat.IsMatch(amount);
        }

    }
}
