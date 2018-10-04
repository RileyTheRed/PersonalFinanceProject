using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinanceProjectFinal.Models
{
    class User
    {

        public string UserID { get; private set; }
        public string FirstName;
        public List<ExistingExpense> ExistingUserExpenses { get; set; }
        public List<ExistingIncome> ExistingUserIncome { get; set; }
        public List<NewExpense> NewUserExpenses { get; set; }
        public List<NewIncome> NewUserIncome { get; set; }

        public User(string id, string name, List<ExistingExpense> expenses, List<ExistingIncome> income)
        {
            UserID = id;
            FirstName = name;
            ExistingUserExpenses = expenses;
            ExistingUserIncome = income;

            NewUserExpenses = new List<NewExpense>();
            NewUserIncome = new List<NewIncome>();
        }

    }
}
