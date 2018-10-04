using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal.Models
{
    class NewExpense
    {

        public string OwnerID { get; private set; }
        public string Hash { get; private set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public NewExpense(string id, double amount, DateTime date, string cat, string descript)
        {
            OwnerID = id;
            Hash = Security.getHashSha256((id + amount.ToString() + date.ToString()));
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }

    }
}
