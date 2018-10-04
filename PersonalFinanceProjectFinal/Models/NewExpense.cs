using System;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal.Models
{
    class NewExpense
    {

        #region Properties
        public string OwnerID { get; private set; } // unique user id created on registration
        public string Hash { get; private set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
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
            Hash = Security.getHashSha256(id + amount.ToString() + date.ToString());
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }

    }
}
