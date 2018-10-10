using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace PersonalFinanceProjectFinal.Pages
{
    /// <summary>
    /// Interaction logic for NewExpensePage.xaml
    /// </summary>
    public partial class NewExpensePage : Page
    {

        User currentUser;

        private static string DefaultCatMessage = "Please select a category...";

        public NewExpensePage(ref User currUser)
        {
            InitializeComponent();
            currentUser = currUser;
            

            foreach(string item in Utilities.Categories.GetCategories())
            {
                cmbCategory.Items.Add(item);
            }
            
        }

        private void btnSubmitExpense_Click(object sender, RoutedEventArgs e)
        {
            if (dteDate.SelectedDate.HasValue)
            {
                if (Sanitizer.ValidNewExpense(txtAmount.Text, dteDate.SelectedDate.Value))
                {
                    if (txtDescription.Text.Length > 100)
                    {
                        currentUser.NewUserExpenses.Add(new NewExpense(currentUser.UserID, double.Parse(txtAmount.Text), dteDate.SelectedDate.Value,
                            cmbCategory.Text, txtDescription.Text.Substring(0, 100)));
                    }
                    else
                    {
                        currentUser.NewUserExpenses.Add(new NewExpense(currentUser.UserID, double.Parse(txtAmount.Text), dteDate.SelectedDate.Value,
                            cmbCategory.Text, txtDescription.Text));
                    }
                }
            }
        }
    }
}
