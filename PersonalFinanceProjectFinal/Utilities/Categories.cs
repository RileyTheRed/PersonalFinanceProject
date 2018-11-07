using System.Collections.Generic;

namespace PersonalFinanceProjectFinal.Utilities
{
    class Categories
    {

        private static List<string> defaultExpenseCategories = new List<string>()
        {
            "Utilities",
            "Transportation",
            "Groceries",
            "Rent",
            "Restaurants",
            "Vices",
            "Insurance",
            "Education",
            "Misc"
        };

        private static List<string> defaultIncomeCategories = new List<string>()
        {
            "Wages and Salaries",
            "Interest Received",
            "Dividends",
            "Business Income",
            "Capital Gains",
            "Rental Income",
            "Unemployment Compensation",
            "Gambling Income"
        };

        public static List<string> GetExpenseCategories()
        {
            return defaultExpenseCategories;
        }

        public static List<string> GetIncomeCategories()
        {
            return defaultIncomeCategories;
        }

    }
}
