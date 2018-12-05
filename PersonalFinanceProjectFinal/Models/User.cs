using System.Collections.Generic;

namespace PersonalFinanceProjectFinal.Models
{
    public class User
    {

        #region Properties
        public string UserID { get; private set; }
        public string FirstName;

        public List<ExistingExpense> ExistingUserExpenses { get; set; } // existing records
        public List<ExistingIncome> ExistingUserIncome { get; set; }    // existing records

        public List<NewExpense> NewUserExpenses { get; set; } // reserved for new records
        public List<NewIncome> NewUserIncome { get; set; }    // reserved for new records

        public List<SearchResultRecord> ModifiedExpenseRecords { get; set; }
        public List<SearchResultRecord> ModifiedIncomeRecords { get; set; }

        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Color4 { get; set; }

        public string CurrencyType { get; set; }
        #endregion


        /// <summary>
        /// Constructor for User object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="expenses"></param>
        /// <param name="income"></param>
        public User(string id, string name, List<ExistingExpense> expenses, List<ExistingIncome> income, string[] colors)
        {
            UserID = id;
            FirstName = name;
            ExistingUserExpenses = expenses;
            ExistingUserIncome = income;

            NewUserExpenses = new List<NewExpense>();
            NewUserIncome = new List<NewIncome>();

            ModifiedExpenseRecords = new List<SearchResultRecord>();
            ModifiedIncomeRecords = new List<SearchResultRecord>();

            Color1 = colors[0];
            Color2 = colors[1];
            Color3 = colors[2];
            Color4 = colors[3];
        }

    }
}
