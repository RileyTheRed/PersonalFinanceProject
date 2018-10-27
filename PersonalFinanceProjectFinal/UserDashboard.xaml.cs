using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using LiveCharts;
using LiveCharts.Wpf;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {

        User currentUser;
        Window childWindow;

        public UserDashboard(string uname)
        {
            InitializeComponent();
            currentUser = Database.GetUserData(uname);
            lblGreeting.Content = $"Welcome back, {currentUser.FirstName}";
            
            pieChart.DataContext = this;
            UpdateChart();
        }

        private void btnAddRecords_Click(object sender, RoutedEventArgs e)
        {
            childWindow = new AddRecordsWindow(ref currentUser);
            childWindow.Owner = this;

            childWindow.Visibility = Visibility.Visible;
            childWindow.IsEnabled = true;

            Visibility = Visibility.Hidden;
            IsEnabled = false;
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;

            Database.UpdateDatabase(currentUser);
            
            Close();
        }

        private void UpdateChart()
        {
            List<double> monthlyExpenseSoFar = new List<double>();
            List<double> monthlyIncomeSoFar = new List<double>();

            var tempExpense = 0.0;
            var tempIncome = 0.0;

            foreach (ExistingExpense item in currentUser.ExistingUserExpenses)
            {
                if(item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year
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
                pieChart.Visibility = Visibility.Hidden;
            }
            else
            {
                pieExpense.Values = new ChartValues<double>(monthlyExpenseSoFar);
                pieIncome.Values = new ChartValues<double>(monthlyIncomeSoFar);
            }

        }

        private void btnEditRecords_Click(object sender, RoutedEventArgs e)
        {
            childWindow = new SearchRecordsWindow(ref currentUser);
            childWindow.Owner = this;

            childWindow.Visibility = Visibility.Visible;
            childWindow.IsEnabled = true;

            Visibility = Visibility.Hidden;
            IsEnabled = false;
        }

        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateChart();
        }
    }
}
