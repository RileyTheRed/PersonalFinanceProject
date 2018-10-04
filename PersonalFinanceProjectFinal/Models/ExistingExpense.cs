using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceProjectFinal.Models
{
    class ExistingExpense
    {

        //public string OwnerID { get; private set; }
        public string Hash { get; private set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public ExistingExpense(string id, double amount, DateTime date, string cat, string descript)
        {
            //OwnerID = id;
            Hash = id;
            Amount = amount;
            Date = date;
            Category = cat;
            Description = descript;
        }

    }
}
