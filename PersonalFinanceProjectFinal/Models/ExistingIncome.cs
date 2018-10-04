using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceProjectFinal.Models
{
    class ExistingIncome
    {

        //public string OwnerID { get; private set; }
        public string Hash { get; private set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

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
