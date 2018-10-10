using System;

namespace PersonalFinanceProjectFinal.Models
{
    public class ExistingIncome
    {

        #region Properties
        public string Hash { get; private set; } // unique income record id
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        #endregion


        /// <summary>
        /// Constructor for an existing income record
        /// </summary>
        /// <param name="id"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="cat"></param>
        /// <param name="descript"></param>
        public ExistingIncome(string id, double amount, DateTime date, string cat, string descript)
        {
            Hash = id;
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }

    }
}
