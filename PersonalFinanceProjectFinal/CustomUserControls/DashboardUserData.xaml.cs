using System;
using System.Windows.Controls;
using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal.CustomUserControls
{

    /// <summary>
    /// Interaction logic for DashboardUserData.xaml
    /// </summary>
    public partial class DashboardUserData : UserControl
    {

        User currentUser;

        public DashboardUserData()
        {
            InitializeComponent();
        }

        public void AttachCurrentUser(ref User current)
        {
            currentUser = current;
        }

        public void UpdateDashboardUserData()
        {

            #region UpdateTop3ExpenseCategoriesFromLastMonth
            var tempTup = ExtractUserDashboardData.GetTopThreeExpenseCategories
                (currentUser.ExistingUserExpenses, currentUser.NewUserExpenses, currentUser.ModifiedExpenseRecords, false);

            lblLastFirstExpense.Content = tempTup.Item1.Item1;
            lblLastSecondExpense.Content = tempTup.Item2.Item1;
            lblLastThirdExpense.Content = tempTup.Item3.Item1;

            lblLastFirstExpensePercent.Content = string.Format("%{0:N2}", tempTup.Item1.Item2 * 100);
            lblLastSecondExpensePercent.Content = string.Format("%{0:N2}", tempTup.Item2.Item2 * 100);
            lblLastThirdExpensePercent.Content = string.Format("%{0:N2}", tempTup.Item3.Item2 * 100);
            #endregion


            #region UpdateTop3ExpenseCategoriesFromCurrentMonth
            tempTup = ExtractUserDashboardData.GetTopThreeExpenseCategories
                (currentUser.ExistingUserExpenses, currentUser.NewUserExpenses, currentUser.ModifiedExpenseRecords, true);

            lblCurrentFirstExpense.Content = tempTup.Item1.Item1;
            lblCurrentSecondExpense.Content = tempTup.Item2.Item1;
            lblCurrentThirdExpense.Content = tempTup.Item3.Item1;

            lblCurrentFirstExpensePercent.Content = string.Format("%{0:N2}", tempTup.Item1.Item2 * 100);
            lblCurrentSecondExpensePercent.Content = string.Format("%{0:N2}", tempTup.Item2.Item2 * 100);
            lblCurrentThirdExpensePercent.Content = string.Format("%{0:N2}", tempTup.Item3.Item2 * 100);
            #endregion


            #region UpdateCurrentToLastIncomeAndExpenseRatio
            var curToLastIncExpRatios = ExtractUserDashboardData.GetCurrentAndLastMonthRatios
                (
                    currentUser.ExistingUserExpenses, currentUser.NewUserExpenses, currentUser.ModifiedExpenseRecords,
                    currentUser.ExistingUserIncome, currentUser.NewUserIncome, currentUser.ModifiedIncomeRecords
                );

            lblExpenseCurrentToLast.Content = string.Format("%{0:N2}", curToLastIncExpRatios.Item1 * 100);
            lblIncomeCurrentToLast.Content = string.Format("%{0:N2}", curToLastIncExpRatios.Item2 * 100);
            #endregion

        }

    }
}
