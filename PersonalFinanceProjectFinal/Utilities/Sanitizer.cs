using System;
using System.Text.RegularExpressions;

namespace PersonalFinanceProjectFinal.Utilities
{
    class Sanitizer
    {

        //private static Regex amountFormat = new Regex("([0-9]{1})([0-9]*)(.)([0-9][0-9])"); // not the greatest regex


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
            try
            {
                if (amount == null)
                    return false;
                var temp = Double.Parse(amount);
                return true;
            }
            catch (FormatException e)
            {
                return false;
            }
        }


        public static string GetSanitizedDescription(string dirty)
        {
            if (dirty == null)
                return "";
            return Regex.Replace(dirty, "[^A-Za-z0-9 ]", "");
        }

    }
}
