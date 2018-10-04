using PersonalFinanceProjectFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceProjectFinal.Models
{
    class NewIncome
    {

        public string OwnerID { get; private set; }
        public string Hash { get; private set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public NewIncome(string ownerid, double amount, DateTime date, string cat, string descript)
        {
            OwnerID = ownerid;
            Hash = Security.getHashSha256(ownerid + amount.ToString() + date.ToString());
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }

    }
}
