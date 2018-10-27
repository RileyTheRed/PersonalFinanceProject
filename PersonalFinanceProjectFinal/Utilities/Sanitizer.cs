using System;
using System.Text.RegularExpressions;

namespace PersonalFinanceProjectFinal.Utilities
{
    class Sanitizer
    {

        private static Regex amountFormat = new Regex("([0-9]{1})([0-9]*)(.)([0-9][0-9])"); // not the greatest regex


        /// <summary>
        /// returns true if the amount and date inputs are invalid
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool InvalidNewExpense(string amount, DateTime date)
        {
            if (!ValidAmount(amount) || (date > DateTime.Now.Date))
                return true;
            return false;
        }


        /// <summary>
        /// returns true if the input amount is valid
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static bool ValidAmount(string amount)
        {
            if (amountFormat.IsMatch(amount))
            {
                if (amount.Substring(0, amount.IndexOf('.')).Length > 10 ||
                    amount.Substring(amount.IndexOf('.')).Length < 3)
                    return false;
                return true;
            }
            return false;
            
        }

    }
}
