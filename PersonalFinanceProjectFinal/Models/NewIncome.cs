using PersonalFinanceProjectFinal.Utilities;
using System;

namespace PersonalFinanceProjectFinal.Models
{
    public class NewIncome
    {

        #region Properties
        public string OwnerID { get; private set; }
        public string Hash { get; private set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        private Random Random = new System.Random();
        #endregion


        /// <summary>
        /// Constructor for a new income record
        /// </summary>
        /// <param name="ownerid"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="cat"></param>
        /// <param name="descript"></param>
        public NewIncome(string ownerid, double amount, DateTime date, string cat, string descript)
        {
            OwnerID = ownerid;
            Hash = Security.getHashSha256(ownerid + amount.ToString() + date.ToString() + Random.Next());
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }

    }
}
