using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PersonalFinanceProjectFinal.Models
{
    public class SearchResultRecord : Record
    {

        #region Properties
        public int Modified { get; set; }
        public string Status { get; set; }
        public string PreviousStatus { get; set; }
        public int IsNewRecord { get; set; }
        #endregion


        /// <summary>
        /// Constructor for a SearchResultRecord
        /// 
        /// This oject is to be used for display in the search results list box
        /// </summary>
        /// <param name="del"></param>
        /// <param name="isnew"></param>
        /// <param name="hash"></param>
        /// <param name="dbl"></param>
        /// <param name="date"></param>
        /// <param name="cat"></param>
        /// <param name="desc"></param>
        public SearchResultRecord(int modified, int isnew, string hash, double dbl, DateTime date, string cat, string desc)
        {
            Status = "";
            PreviousStatus = "";
            IsNewRecord = isnew;
            Hash = hash;
            Amount = dbl;
            Date = date.Date;
            Category = cat;
            Description = desc;
        }


        /// <summary>
        /// GetExpenseResults takes in the users existing and new expense records
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="newer"></param>
        /// <returns></returns>
        public static ObservableCollection<SearchResultRecord> GetExpenseResults(List<ExistingExpense> existing, List<NewExpense> newer,
            List<SearchResultRecord> modified)
        {
            ObservableCollection<SearchResultRecord> tempRecords = new ObservableCollection<SearchResultRecord>();

            if (modified.Count > 0)
            {
                foreach (ExistingExpense item in existing)
                {
                    if (modified.Count(x => x.Hash.Equals(item.Hash)) == 0)
                    {
                        tempRecords.Add(new SearchResultRecord(0, 0, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                    }
                }
                foreach (NewExpense item in newer)
                {
                    if (modified.Count(x => x.Hash.Equals(item.Hash)) == 0)
                    {
                        tempRecords.Add(new SearchResultRecord(0, 1, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                    }
                }
                foreach (SearchResultRecord item in modified)
                {
                    tempRecords.Add(item);
                }
            }
            else
            {
                foreach (ExistingExpense item in existing)
                {
                    tempRecords.Add(new SearchResultRecord(0, 1, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                }
                foreach (NewExpense item in newer)
                {
                    tempRecords.Add(new SearchResultRecord(0, 1, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                }
            }



            tempRecords.OrderBy(e => e.Date);
            return tempRecords;
        }


        /// <summary>
        /// GetIncomeResults takes in the users existing and new expense records
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="newer"></param>
        /// <returns></returns>
        public static ObservableCollection<SearchResultRecord> GetIncomeResults(List<ExistingIncome> existing, List<NewIncome> newer,
            List<SearchResultRecord> modified)
        {
            ObservableCollection<SearchResultRecord> tempRecords = new ObservableCollection<SearchResultRecord>();

            if (modified.Count > 0)
            {
                foreach (ExistingIncome item in existing)
                {
                    if (modified.Count(x => x.Hash.Equals(item.Hash)) == 0)
                    {
                        tempRecords.Add(new SearchResultRecord(0, 0, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                    }
                }
                foreach (NewIncome item in newer)
                {
                    if (modified.Count(x => x.Hash.Equals(item.Hash)) == 0)
                    {
                        tempRecords.Add(new SearchResultRecord(0, 1, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                    }
                }
                foreach (SearchResultRecord item in modified)
                {
                    tempRecords.Add(item);
                }
            }
            else
            {
                foreach (ExistingIncome item in existing)
                {
                    tempRecords.Add(new SearchResultRecord(0, 1, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                }
                foreach (NewIncome item in newer)
                {
                    tempRecords.Add(new SearchResultRecord(0, 1, item.Hash, item.Amount, item.Date, item.Category, item.Description));
                }
            }

            tempRecords.OrderBy(e => e.Date);
            return tempRecords;
        }


        public override bool Equals(object obj)
        {
            var temp = obj as SearchResultRecord;
            if (temp != null)
            {
                if (!Hash.Equals(temp.Hash))
                    return false;
                return Modified == temp.Modified && Amount == temp.Amount && Date.Equals(temp.Date) && Category.Equals(temp.Category) && Description.Equals(temp.Description);
            }
            return false;
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
