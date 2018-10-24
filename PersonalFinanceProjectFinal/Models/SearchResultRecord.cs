using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceProjectFinal.Models
{
    public class SearchResultRecord
    {

        public int Delete { get; set; }
        public int New { get; set; }
        public string Hash { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }


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
        public SearchResultRecord(int del, int isnew, string hash, double dbl, DateTime date, string cat, string desc)
        {
            Delete = del;
            New = isnew;
            Hash = hash;
            Amount = dbl;
            Date = date;
            Category = cat;
            Description = desc;
        }


        /// <summary>
        /// GetExpenseResults takes in the users existing and new expense records
        /// </summary>
        /// <param name="existing"></param>
        /// <param name="newer"></param>
        /// <returns></returns>
        public static ObservableCollection<SearchResultRecord> GetExpenseResults(List<ExistingExpense> existing, List<NewExpense> newer)
        {
            ObservableCollection<SearchResultRecord> tempRecords = new ObservableCollection<SearchResultRecord>();
            foreach (ExistingExpense item in existing)
            {
                tempRecords.Add(new SearchResultRecord(0, 0, item.Hash, item.Amount, item.Date, item.Category, item.Description));
            }
            foreach (NewExpense item in newer)
            {
                tempRecords.Add(new SearchResultRecord(0, 1, item.Hash, item.Amount, item.Date, item.Category, item.Description));
            }
            tempRecords.OrderBy(e => e.Date);
            return tempRecords;
        }


        public override string ToString()
        {
            if (Delete == 1)
            {
                return $"-- {Date},\t{Amount},\t{Category}";
            }
            else if (New == 1)
            {
                return $"++ {Date},\t{Amount},\t{Category}";
            }
            else
            {
                return $"{Date},\t{Amount},\t{Category}";
            }
            
        }

    }
}
