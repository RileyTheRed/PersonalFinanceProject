using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using LiveCharts;
using System.Windows.Media.Effects;
using PersonalFinanceProjectFinal.View_Models;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {

        User currentUser;
        //Window childWindow;

        public UserDashboard(string uname)
        {

            InitializeComponent();
            currentUser = Database.GetUserData(uname);
            UserDashboardVM vm = new UserDashboardVM(this, currentUser);
            
            lblGreeting.Content = $"Welcome back, {currentUser.FirstName}";

            DataContext = vm;

            var temp = ExtractUserDashboardData.GetExpenseReport(currentUser);
            
            pieChart.DataContext = this;
            UpdateChart();

            ucDashData.AttachCurrentUser(ref currentUser);
            ucDashData.UpdateDashboardUserData();

            //RunIntroductionSequence();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;
            
            Close();
        }

        public void UpdateChart()
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
                if (pieChart.Visibility == Visibility.Hidden)
                {
                    pieChart.Visibility = Visibility.Visible;
                }
                pieExpense.Values = new ChartValues<double>(monthlyExpenseSoFar);
                pieIncome.Values = new ChartValues<double>(monthlyIncomeSoFar);
            }

        }

        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            UpdateChart();
            ucDashData.UpdateDashboardUserData();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;

            Database.UpdateDatabase(currentUser);

            Close();
        }

        #region IntroductionBlurFunctions
        private void ClearAllBlur()
        {
            rectBlue.Effect = null;
            rectYellow.Effect = null;
            //rectFrontGray.Effect = null;
            btnAddRecords.Effect = null;
            btnEditRecords.Effect = null;
            btnReport.Effect = null;
            btnLogout.Effect = null;
            btnHelp.Effect = null;
            lblGreeting.Effect = null;
            pieChart.Effect = null;
            ucDashData.Effect = null;
        }

        private void BlurAllButAddRecords()
        {
            btnLogout.Effect = new BlurEffect();
            btnReport.Effect = new BlurEffect();
            btnEditRecords.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            lblGreeting.Effect = new BlurEffect();
            pieChart.Effect = new BlurEffect();
            rectYellow.Effect = new BlurEffect();
            rectBlue.Effect = new BlurEffect();
            //rectFrontGray.Effect = new BlurEffect();
            ucDashData.Effect = new BlurEffect();
        }

        private void BlurAllButEditRecords()
        {
            btnLogout.Effect = new BlurEffect();
            btnAddRecords.Effect = new BlurEffect();
            btnReport.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            rectYellow.Effect = new BlurEffect();
            rectBlue.Effect = new BlurEffect();
            //rectFrontGray.Effect = new BlurEffect();
            pieChart.Effect = new BlurEffect();
            lblGreeting.Effect = new BlurEffect();
            ucDashData.Effect = new BlurEffect();
        }

        private void BlurAllButReport()
        {
            btnLogout.Effect = new BlurEffect();
            btnAddRecords.Effect = new BlurEffect();
            btnEditRecords.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            rectYellow.Effect = new BlurEffect();
            rectBlue.Effect = new BlurEffect();
            //rectFrontGray.Effect = new BlurEffect();
            pieChart.Effect = new BlurEffect();
            lblGreeting.Effect = new BlurEffect();
            ucDashData.Effect = new BlurEffect();
        }

        private void BlurAllButChart()
        {
            btnLogout.Effect = new BlurEffect();
            btnAddRecords.Effect = new BlurEffect();
            btnReport.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            rectYellow.Effect = new BlurEffect();
            rectBlue.Effect = new BlurEffect();
            btnEditRecords.Effect = new BlurEffect();
            lblGreeting.Effect = new BlurEffect();
        }
        #endregion

        public void RunIntroductionSequence()
        {
            
            MessageBoxResult introResult = MessageBox.Show("Welcome to your dashboard! Your dashboard is the" +
                "your own personal portal that allows you to access and navigate all that Personal Finance " +
                "has to offer!", "Tutorial", MessageBoxButton.OK);
            if (introResult == MessageBoxResult.OK)
            {
                BlurAllButAddRecords();
                introResult = MessageBox.Show("You can insert expense or income records by clicking on the Add " +
                    "Expense/Income button!", "Tutorial", MessageBoxButton.OK);
                
                if (introResult == MessageBoxResult.OK)
                {
                    ClearAllBlur();
                    BlurAllButEditRecords();
                    introResult = MessageBox.Show("Using the Edit Expense/Income button, you can search all the records" +
                        " you have on file and view or change them.", "Tutorial", MessageBoxButton.OK);

                    if (introResult == MessageBoxResult.OK)
                    {
                        ClearAllBlur();
                        BlurAllButReport();
                        introResult = MessageBox.Show("You can compose and view a detailed report of your finances by clicking on the" +
                            " Finance Report button.", "Tutorial", MessageBoxButton.OK);

                        if (introResult == MessageBoxResult.OK)
                        {
                            ClearAllBlur();
                            BlurAllButChart();
                            introResult = MessageBox.Show("If you have records for the current month on file, they will be displayed in" +
                                " a pie chart here.", "Tutorial", MessageBoxButton.OK);
                        }
                    }
                }
            }

            ClearAllBlur();
        }

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            RunIntroductionSequence();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            UserAccountSettingsWindow userSettings = new UserAccountSettingsWindow();
            userSettings.Owner = this;
            userSettings.Visibility = Visibility.Visible;
            userSettings.IsEnabled = true;

            this.Visibility = Visibility.Hidden;
            IsEnabled = false;
        }
    }
}
