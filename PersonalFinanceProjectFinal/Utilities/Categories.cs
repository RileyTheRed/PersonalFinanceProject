using System.Collections.Generic;

namespace PersonalFinanceProjectFinal.Utilities
{
    class Categories
    {

        private static List<string> defaultCategories = new List<string>()
        {
            "Utilities",
            "Transportation",
            "Groceries",
            "Rent",
            "Restaurant",
            "Vice",
            "Insurance",
            "Education",
            "Misc"
        };

        public static List<string> GetCategories()
        {
            return defaultCategories;
        }

    }
}
