using LiveCharts;
using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PersonalFinanceProjectFinal.View_Models
{
    public class UserDashboardVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        public UserDashboard dashboard { get; set; }
        public User currentUser { get; set; }
        public Window child { get; set; }


        public UserDashboardVM(UserDashboard dash, User current)
        {
            dashboard = dash;
            currentUser = current;
            
        }


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
            Database.UpdateDatabase(currentUser);

            dashboard.Owner.Visibility = System.Windows.Visibility.Visible;
            dashboard.Owner.IsEnabled = true;
            dashboard.Close();
        }


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


        private void UpdateChart()
        {
            List<double> monthlyExpenseSoFar = new List<double>();
            List<double> monthlyIncomeSoFar = new List<double>();

            var tempExpense = 0.0;
            var tempIncome = 0.0;

            foreach (ExistingExpense item in currentUser.ExistingUserExpenses)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedExpenseRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempExpense += item.Amount;
                }
            }

            foreach (ExistingIncome item in currentUser.ExistingUserIncome)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedIncomeRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempIncome += item.Amount;
                }
            }

            foreach (NewIncome item in currentUser.NewUserIncome)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedIncomeRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempIncome += item.Amount;
                }
            }

            foreach (NewExpense item in currentUser.NewUserExpenses)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
                    && currentUser.ModifiedExpenseRecords.Count(x => x.Hash.Equals(item.Hash)) == 0)
                {
                    tempExpense += item.Amount;
                }
            }

            foreach (SearchResultRecord item in currentUser.ModifiedExpenseRecords)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year && !item.Status.Equals("--"))
                {
                    tempExpense += item.Amount;
                }
            }

            foreach (SearchResultRecord item in currentUser.ModifiedIncomeRecords)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year && !item.Status.Equals("--"))
                {
                    tempIncome += item.Amount;
                }
            }

            monthlyExpenseSoFar.Add(tempExpense);
            monthlyIncomeSoFar.Add(tempIncome);

            if (monthlyExpenseSoFar[0] == 0.0 && monthlyIncomeSoFar[0] == 0.0)
            {
                dashboard.pieChart.Visibility = Visibility.Hidden;
            }
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
