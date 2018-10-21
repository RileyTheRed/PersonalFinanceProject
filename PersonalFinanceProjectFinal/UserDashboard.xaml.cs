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
                if(item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                {
                    tempExpense += item.Amount;
                }
            }

            foreach (ExistingIncome item in currentUser.ExistingUserIncome)
            {
                if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                {
                    tempIncome += item.Amount;
                }
            }

            monthlyExpenseSoFar.Add(tempExpense);
            monthlyIncomeSoFar.Add(tempIncome);

            pieExpense.Values = new ChartValues<double>(monthlyExpenseSoFar);
            pieIncome.Values = new ChartValues<double>(monthlyIncomeSoFar);

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
    }
}
