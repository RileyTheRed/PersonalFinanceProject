using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System.Windows;
using System.Windows.Controls;

namespace PersonalFinanceProjectFinal.Pages
{
    /// <summary>
    /// Interaction logic for NewIncomePage.xaml
    /// </summary>
    public partial class NewIncomePage : Page
    {

        User currentUser;
        private static string DefaultCatMessage = "Please select a category...";

        public NewIncomePage(ref User currUser)
        {
            InitializeComponent();
            currentUser = currUser;


            foreach (string item in Utilities.Categories.GetCategories())
            {
                cmbCategory.Items.Add(item);
            }
        }


        private void btnSubmitIncome_Click(object sender, RoutedEventArgs e)
        {
            if (dteDate.SelectedDate.HasValue)
            {
                if (Sanitizer.ValidNewExpense(txtAmount.Text, dteDate.SelectedDate.Value))
                {
                    if (txtDescription.Text.Length > 100)
                    {
                        currentUser.NewUserIncome.Add(new NewIncome(currentUser.UserID, double.Parse(txtAmount.Text), dteDate.SelectedDate.Value,
                            cmbCategory.Text, txtDescription.Text.Substring(0, 100)));
                    }
                    else
                    {
                        currentUser.NewUserIncome.Add(new NewIncome(currentUser.UserID, double.Parse(txtAmount.Text), dteDate.SelectedDate.Value,
                            cmbCategory.Text, txtDescription.Text));
                    }
                    MessageBox.Show("Record entered successfully!", "Success");
                    ClearInput();
                
                }
                else
                {
                    MessageBox.Show("Invalid input! Be sure to check all entries.", "Invalid Input");
                }
            }
            else
            {
                MessageBox.Show("Invalid input! Be sure to check all entries.", "Invalid Input");
            }
        }


        private void ClearInput()
        {
            txtAmount.Text = "";
            dteDate.SelectedDate = null;
            cmbCategory.Text = DefaultCatMessage;
            txtDescription.Text = "";
        }
    }
}
