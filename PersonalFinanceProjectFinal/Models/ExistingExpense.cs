using System;

namespace PersonalFinanceProjectFinal.Models
{
    public class ExistingExpense
    {
        private string v1;
        private double v2;
        private object v3;
        private string v4;
        private string v5;

        #region Properties
        public string Hash { get; private set; } // unique expense id
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        #endregion


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

        public ExistingExpense(string v1, double v2, object v3, string v4, string v5)
        {
            this.v1 = v1;
            this.v2 = v2;
            this.v3 = v3;
            this.v4 = v4;
            this.v5 = v5;
        }

        public override string ToString()
        {
            return $"{Date.ToShortDateString()}, {Amount}, {Category},{Description}";
        }
    }
}
