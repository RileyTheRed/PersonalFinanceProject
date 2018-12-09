/**
 * UserDashboardVM.cs
 * 
 * This is the View Model for the UserDashboard window.
 * 
 * Author: Riley Wells
 * */


using LiveCharts;
using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PersonalFinanceProjectFinal.View_Models
{
    public class UserDashboardVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        #region Properties
        public UserDashboard dashboard { get; set; }
        public User currentUser { get; set; }
        public Window child { get; set; }
        #endregion


        /// <summary>
        /// Constructor for UserDashboardVM takes in a UserDashboard
        /// </summary>
        /// <param name="dash"></param>
        /// <param name="current"></param>
        public UserDashboardVM(UserDashboard dash, User current)
        {
            dashboard = dash;
            currentUser = current;
            
        }


        #region Commands
        /// <summary>
        /// Defintion of the LogoutCommand ICommand, related _logoutCommand DelegateCommand,
        /// and LogoutClicked function.
        /// 
        /// LogoutClicked function updates all the new or edited information for the user
        /// into the database.
        /// </summary>
        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new DelegateCommand(LogoutClicked);
                }

                return _logoutCommand;
            }
        }
        DelegateCommand _logoutCommand; 
        private void LogoutClicked(object obj)
        {
            //update the database
            Database.UpdateDatabase(currentUser); 

            dashboard.Owner.Visibility = System.Windows.Visibility.Visible;
            dashboard.Owner.IsEnabled = true;
            dashboard.Close();
        }


        /// <summary>
        /// Defintion of the AddRecordsCommand ICommand, related _addRecordsCommand 
        /// DelegateCommand, and AddRecordsClicked function.
        /// 
        /// AddRecordsClicked spawns a new AddRecordsWindow, sets its datacontext to 
        /// its related View Model, and sets the Owner of the window to the UserDashboard
        /// window.
        /// </summary>
        public ICommand AddRecordsCommand
        {
            get
            {
                if (_addRecordsCommand == null)
                {
                    _addRecordsCommand = new DelegateCommand(AddRecordsClicked);
                }
                return _addRecordsCommand;
            }
        }
        DelegateCommand _addRecordsCommand;
        private void AddRecordsClicked(object obj)
        {
            var temp = currentUser;
            child = new AddRecordsWindow(ref temp);
            child.Owner = dashboard;
            child.DataContext = (new AddRecordsWindowVM(currentUser, child as AddRecordsWindow));
            
            child.Show();

            dashboard.Visibility = Visibility.Hidden;
            dashboard.IsEnabled = false;
        }


        /// <summary>
        /// Defintion of the EditRecordsCommand ICommand, related _editRecordsCommand
        /// DelegateCommand, and EditRecordsClicked function.
        /// 
        /// EditRecordsClicked spawns a new SearchRecordsWindow and sets its owner to the
        /// UserDashboard.
        /// </summary>
        public ICommand EditRecordsCommand
        {
            get
            {
                if (_editRecordsCommand == null)
                {
                    _editRecordsCommand = new DelegateCommand(EditRecordsClicked);
                }
                return _editRecordsCommand;
            }
        }
        DelegateCommand _editRecordsCommand;
        private void EditRecordsClicked(object obj)
        {
            var temp = currentUser;
            child = new SearchRecordsWindow(ref temp);
            child.Owner = dashboard;

            child.Show();
            dashboard.Visibility = Visibility.Hidden;
            dashboard.IsEnabled = false;
        }


        /// <summary>
        /// Defintion of the UpdateDashboardDataCommand ICommand, related _updateDashbaordDataCommand
        /// DelegateCommand, and UpdateDashboardData function.
        /// 
        /// The UpdateDashboardData function calls the ucDashData control's UpdateDashboardUserData function.
        /// </summary>
        public ICommand UpdateDashboardDataCommand
        {
            get
            {
                if (_updateDashboardDataCommand == null)
                {
                    _updateDashboardDataCommand = new DelegateCommand(UpdateDashboardData);
                }
                return _updateDashboardDataCommand;
            }
        }
        DelegateCommand _updateDashboardDataCommand;
        private void UpdateDashboardData(object obj)
        {
            dashboard.ucDashData.UpdateDashboardUserData();
            UpdateChart();
        }


        /// <summary>
        /// Defintion of the FinanceReportCommand ICommand, related _financeReportCommand
        /// DelegateCommand, and FinanceReportClicked function.
        /// 
        /// FinanceReportClicked spawns a new Window1 (FinanceReportWindow) and sets its
        /// owner to the UserDashboard window.
        /// </summary>
        public ICommand FinanceReportCommand
        {
            get
            {
                if (_financeReportCommand == null)
                {
                    _financeReportCommand = new DelegateCommand(FinanceReportClicked);
                }
                return _financeReportCommand;
            }
        }
        DelegateCommand _financeReportCommand;
        private void FinanceReportClicked(object obj)
        {
            var temp = currentUser;
            child = new Window1(temp);
            child.Owner = dashboard;

            dashboard.IsEnabled = false;
            dashboard.Visibility = Visibility.Hidden;

            child.Show();
        }


        /// <summary>
        /// Defintion of the SettingButtonCommand ICommand, related _settingButtonCommand
        /// DelegateCommand, and SettingButtonClicked function.
        /// 
        /// SettingButtonClicked spawns a new UserAccountSettingsWindow and sets its owner
        /// to the UserDashboard window.
        /// </summary>
        public ICommand SettingButtonCommand
        {
            get
            {
                if (_settingButtonCommand == null)
                {
                    _settingButtonCommand = new DelegateCommand(SettingButtonClicked);
                }
                return _settingButtonCommand;
            }
        }
        DelegateCommand _settingButtonCommand;
        private void SettingButtonClicked(object obj)
        {
            child = new UserAccountSettingsWindow(currentUser);
            child.Owner = dashboard;

            dashboard.IsEnabled = false;
            dashboard.Visibility = Visibility.Hidden;

            child.Show();
        }
        #endregion


        /// <summary>
        /// UpdateChart() when called just recalculates the users total expenses/income for
        /// the current month and updates the values displayed by the pie chart.
        /// 
        /// This is called whenever the UsserDashboard window focus changes, leaving to a new
        /// window or coming back to the dashboard. Changes made to records or records added
        /// are immediately reflected in chart data.
        /// </summary>
        private void UpdateChart()
        {

            // lists needed to hold total monthly income/expense b/c ChartValues constructor takes IEnumerable
            List<double> monthlyExpenseSoFar = new List<double>();
            List<double> monthlyIncomeSoFar = new List<double>();

            var tempExpense = 0.0;
            var tempIncome = 0.0;


            /// for earch of the existing user expense records, as long as its month and year are the 
            /// current month and year, AND that the record hash does not exist in the users modified 
            /// expense records, add the record amount to tempExpense.
            foreach (ExistingExpense item in currentUser.ExistingUserExpenses)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedExpenseRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempExpense += item.Amount;
                }
            }


            /// for each of the existing user income records, as long as its month and year are the 
            /// current month and year, AND that the record hash does not exist in the users modified
            /// income records, add the record amount to tempIncome.
            foreach (ExistingIncome item in currentUser.ExistingUserIncome)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedIncomeRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempIncome += item.Amount;
                }
            }


            /// for each of the new user income records, as long as its month and year are the
            /// current month and year, AND that the record hash does not exist in the users modifed
            /// income records, add the record amount to tempIncome.
            foreach (NewIncome item in currentUser.NewUserIncome)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedIncomeRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempIncome += item.Amount;
                }
            }


            /// for each of the new user expense records, as long as its month and year are the 
            /// current month and year, AND that the records hash does not exist in the users modified
            /// expense records, add the record amount to tempExpense
            foreach (NewExpense item in currentUser.NewUserExpenses)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedExpenseRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempExpense += item.Amount;
                }
            }


            /// for all modified expense records, as long as the record's month and year are the 
            /// same as the current month and year, AND the record has NOT been marked for deletion,
            /// add the record amount to tempExpense
            foreach (SearchResultRecord item in currentUser.ModifiedExpenseRecords)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year && !item.Status.Equals("--"))
                {
                    tempExpense += item.Amount;
                }
            }


            /// for all modified income records, as long as the record's month and year are the 
            /// same as the current month and year, AND the record has not been marked for deletion,
            /// add the record amount to tempIncome
            foreach (SearchResultRecord item in currentUser.ModifiedIncomeRecords)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year && !item.Status.Equals("--"))
                {
                    tempIncome += item.Amount;
                }
            }


            // add the amounts to the lists
            monthlyExpenseSoFar.Add(tempExpense);
            monthlyIncomeSoFar.Add(tempIncome);


            // if there have been no income or expense records for the month, hide the chart
            if (monthlyExpenseSoFar[0] == 0.0 && monthlyIncomeSoFar[0] == 0.0)
            {
                dashboard.pieChart.Visibility = Visibility.Hidden;
            }
            // otherwise, set visitibility if needed and add chart values
            else
            {
                if (dashboard.pieChart.Visibility == Visibility.Hidden)
                {
                    dashboard.pieChart.Visibility = Visibility.Visible;
                }
                dashboard.pieExpense.Values = new ChartValues<double>(monthlyExpenseSoFar);
                dashboard.pieIncome.Values = new ChartValues<double>(monthlyIncomeSoFar);
            }
        }
    }
}
