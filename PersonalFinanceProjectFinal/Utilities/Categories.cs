/**
 * Categories.cs
 * 
 * Static Category class that contains all different record categories
 * and functions to get record categories.
 * 
 * Author: Riley Wells
 * 
 * Updates:
 *      12/9/18 - Added Documentation
 *      
 * */

using System.Collections.Generic;

namespace PersonalFinanceProjectFinal.Utilities
{

    public static class Categories
    {

        // List of all the Expense categories
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


        // list of all the Income categories
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


        // returns the list of Expense categories
        public static List<string> GetExpenseCategories()
        {
            return defaultExpenseCategories;
        }


        // returnss the list of Income categories
        public static List<string> GetIncomeCategories()
        {
            return defaultIncomeCategories;
        }

    }
}
