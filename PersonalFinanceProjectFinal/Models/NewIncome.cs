using PersonalFinanceProjectFinal.Utilities;
using System;

namespace PersonalFinanceProjectFinal.Models
{
    public class NewIncome : Record
    {

        #region Properties
        public string OwnerID { get; private set; }
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
