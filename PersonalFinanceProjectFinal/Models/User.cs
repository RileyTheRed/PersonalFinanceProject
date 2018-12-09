using System.Collections.Generic;
using System.ComponentModel;

namespace PersonalFinanceProjectFinal.Models
{
    public class User : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        #region Properties
        public string UserID { get; private set; }
        public string FirstName;

        public List<ExistingExpense> ExistingUserExpenses { get; set; } // existing records
        public List<ExistingIncome> ExistingUserIncome { get; set; }    // existing records

        public List<NewExpense> NewUserExpenses { get; set; } // reserved for new records
        public List<NewIncome> NewUserIncome { get; set; }    // reserved for new records

        public List<SearchResultRecord> ModifiedExpenseRecords { get; set; }
        public List<SearchResultRecord> ModifiedIncomeRecords { get; set; }

        private string _color1;
        public string Color1
        {
            get { return _color1; }
            set
            {
                _color1 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Color1"));
            }
        }

        private string _color2;
        public string Color2
        {
            get { return _color2; }
            set
            {
                _color2 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Color2"));
            }
        }

        private string _color3;
        public string Color3
        {
            get { return _color3; }
            set
            {
                _color3 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Color3"));
            }
        }

        private string _color4;
        public string Color4
        {
            get { return _color4; }
            set
            {
                _color4 = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Color4"));
            }
        }

        private string _currencyType;
        public string CurrencyType
        {
            get { return _currencyType; }
            set
            {
                _currencyType = value;
                switch (value)
                {
                    case "USD":
                        CurrencySign = "$";
                        break;
                    case "JPY":
                        CurrencySign = "¥";
                        break;
                    case "EUR":
                        CurrencySign = "€";
                        break;
                    case "CAD":
                        CurrencySign = "$";
                        break;
                    case "GBP":
                        CurrencySign = "£";
                        break;
                    default:
                        CurrencySign = "$";
                        break;
                }
            }
        }

        public string CurrencySign { get; set; }
        #endregion


        /// <summary>
        /// Constructor for User object
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="expenses"></param>
        /// <param name="income"></param>
        public User(string id, string name, List<ExistingExpense> expenses, List<ExistingIncome> income, string[] colors, string type)
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

            CurrencyType = type;
        }

    }
}
