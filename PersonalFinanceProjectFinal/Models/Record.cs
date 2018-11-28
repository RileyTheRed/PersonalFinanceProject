using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceProjectFinal.Models
{
    public abstract class Record
    {

        #region Properties
        public string Hash { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        #endregion

        public abstract override string ToString();

    }
}
