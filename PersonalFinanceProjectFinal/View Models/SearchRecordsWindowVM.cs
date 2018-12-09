using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


/*
 * WORK IN PROGRESS, NOT ACTUALLY USED. TOO MUCH IN MY MAIN ALGO's TO REFACTOR, NOT ENOUGH TIME
 * :(
 * */

namespace PersonalFinanceProjectFinal.View_Models
{
    public class SearchRecordsWindowVM : INotifyPropertyChanged
    {

        #region PropertiesAndOtherThings
        private static string CategoryDefaultMessage = "Please select a category...";
        public User currentUser { get; set; }
        private SearchRecordsWindow parent { get; set; }
        //private string[] colors = new string[4];
        private string typeOfRecordSearched = "";
        private bool validAmountRanges = true;
        private bool validDateRanges = true;

        public SearchResultRecord selectedRecord;

        private bool? _radioExpenseChecked;
        public bool? RadioExpenseChecked
        {
            get { return _radioExpenseChecked; }
            set
            {
                _radioExpenseChecked = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RadioExpenseChecked"));
            }
        }

        private bool? _radioincomeChecked;
        public bool? RadioIncomeChecked
        {
            get { return _radioincomeChecked; }
            set
            {
                _radioincomeChecked = value;
                PropertyChanged(this, new PropertyChangedEventArgs("RadioIncome" +
                    "Checked"));
            }
        }

        private string _comboCategory;
        public string ComboCategory
        {
            get { return _comboCategory; }
            set
            {
                _comboCategory = value;
                PropertyChanged(this, new PropertyChangedEventArgs("ComboCategory"));

            }
        }

        private DateTime _startDateTime;
        public DateTime StartDateTime
        {
            get { return _startDateTime; }
            set
            {
                _startDateTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StartDateTime"));
            }
        }

        private DateTime _endDateTime;
        public DateTime EndDateTime
        {
            get { return _endDateTime; }
            set
            {
                _endDateTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EndDateTime"));
            }
        }

        private string _startAmount;
        public string StartAmount
        {
            get { return _startAmount; }
            set
            {
                _startAmount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("StartAmount"));
            }
        }

        private string _endAmount;
        public string EndAmount
        {
            get { return _endAmount; }
            set
            {
                _endAmount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("EndAmount"));
            }
        }

        private ObservableCollection<SearchResultRecord> _results;
        public ObservableCollection<SearchResultRecord> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Results"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { }; // do I even need this?
        #endregion


        public SearchRecordsWindowVM(SearchRecordsWindow parent, User current)
        {
            this.parent = parent;
            currentUser = current;
        }



        public ICommand ReturnDashboardCommand
        {
            get
            {
                if (_returnDashboardCommand == null)
                    _returnDashboardCommand = new DelegateCommand(ReturnDashboardClicked);
                return _returnDashboardCommand;
            }
        }
        DelegateCommand _returnDashboardCommand;
        private void ReturnDashboardClicked(object obj)
        {
            parent.Owner.IsEnabled = true;
            parent.Owner.Visibility = System.Windows.Visibility.Visible;

            parent.Close();
        }


        #region HelperFunctions
        private bool ValidChoice()
        {
            return (RadioExpenseChecked == true) ? true : (RadioIncomeChecked == true ? true : false);
        }

        private bool HasPickedCategory()
        {
            if (ComboCategory != null)
            {
                return ComboCategory.Equals(CategoryDefaultMessage);
            }
            return parent.cmbCategory.Text.Equals(CategoryDefaultMessage);
        }

        private bool HasPickedTwoDates()
        {
            return parent.dteEnd.SelectedDate.HasValue && parent.dteStart.SelectedDate.HasValue;
        }

        private bool HasPickedStartingDate()
        {
            return parent.dteStart.SelectedDate.HasValue;
        }

        private bool HasPickedEndingDate()
        {
            return parent.dteEnd.SelectedDate.HasValue;
        }

        private bool HasEnteredTwoAmounts()
        {
            return !parent.txtMax.Text.Equals("MAX") && !parent.txtMin.Text.Equals("MIN");
        }

        private bool HasEnteredMinAmount()
        {
            return !parent.txtMin.Text.Equals("MIN");
        }

        private bool HasEnteredMaxAmount()
        {
            return !parent.txtMin.Text.Equals("MAX");
        }
        #endregion
    }
}
