﻿using System;

namespace PersonalFinanceProjectFinal.Models
{
    public class ExistingExpense : Record
    { 

        /// <summary>
        /// Constructor specifically for an EXISTING expense record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="cat"></param>
        /// <param name="descript"></param>
        public ExistingExpense(string id, double amount, DateTime date, string cat, string descript)
        {
            Hash = id;
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }


        /// <summary>
        /// Overridden toString method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Date.ToShortDateString()},\t{Amount},\t{Category},\t{Description}\t\t{Hash}";
        }
    }
}
