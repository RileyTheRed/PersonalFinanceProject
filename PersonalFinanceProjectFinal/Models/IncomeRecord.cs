using PersonalFinanceProjectFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceProjectFinal.Models
{
    class IncomeRecord : Record
    {

        #region Properties
        public string OwnerID { get; private set; }
        private Random Random = new System.Random();
        public int Modified { get; set; }
        public string Status { get; set; }
        public string PreviousStatus { get; set; }
        public int IsNewRecord { get; set; }
        #endregion


        /// <summary>
        /// Constructor for an Income Record, regardless if it is new or existing.
        /// </summary>
        /// <param name="isNewRecord"></param>
        /// <param name="ownerId"></param>
        /// <param name="recordId"></param>
        /// <param name="amount"></param>
        /// <param name="date"></param>
        /// <param name="cat"></param>
        /// <param name="descript"></param>
        public IncomeRecord(
            bool isNewRecord, string ownerId, string recordId, double amount, DateTime date, string cat, 
            string descript
            )
        {

            if (isNewRecord)
            {
                OwnerID = ownerId;
                Hash = Security.getHashSha256(ownerId + amount.ToString() + date.ToString() + Random.Next());
                Amount = amount;
                Date = date;
                Category = cat;
                Description = descript;
            }
            else
            {
                Hash = recordId;
                Amount = amount;
                Date = date;
                Category = cat;
                Description = descript;
            }
            Modified = 0;
            Status = "";
            PreviousStatus = "";
            IsNewRecord = 0;

        }


        public override bool Equals(object obj)
        {
            var temp = obj as IncomeRecord;
            if (temp != null)
            {
                if (!Hash.Equals(temp.Hash))
                    return false;
                return Modified == temp.Modified && Amount == temp.Amount && Date.Equals(temp.Date) &&
                    Category.Equals(temp.Category);
            }
            return false;
        }


        public override string ToString()
        {
            return $"{Date.ToShortDateString()},\t{Amount},\t{Category},\t{Description}\t\t{Hash}";
        }
    }
}
