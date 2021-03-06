﻿using System;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal.Models
{
    public class NewExpense : Record
    {

        #region Properties
        public string OwnerID { get; private set; } // unique user id created on registration
        private Random Random = new System.Random();
        #endregion


        /// <summary>
        /// Constructor for a new expense record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="cat"></param>
        /// <param name="descript"></param>
        public NewExpense(string id, double amount, DateTime date, string cat, string descript)
        {
            OwnerID = id;
            Hash = Security.getHashSha256(id + amount.ToString() + date.ToString() + Random.Next());
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }


        public override string ToString()
        {
            return $"{Date.ToShortDateString()},\t{Amount},\t{Category},\t{Description}\t\t{Hash}";
        }

    }
}
