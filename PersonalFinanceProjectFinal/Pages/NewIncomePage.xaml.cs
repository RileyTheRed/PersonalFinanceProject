using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System;
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


        /// <summary>
        /// Default constructor for the NewIncomePage
        /// </summary>
        /// <param name="currUser"></param>
        public NewIncomePage(ref User currUser)
        {
            InitializeComponent();
            currentUser = currUser;

            dteDate.DisplayDateEnd = DateTime.Today;

            foreach (string item in Utilities.Categories.GetCategories())
            {
                cmbCategory.Items.Add(item);
            }
        }


        /// <summary>
        /// Button logic for submitting new Income records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmitIncome_Click(object sender, RoutedEventArgs e)
        {
            if (dteDate.SelectedDate.HasValue) // make sure the user selected a date
            {
                // check and make sure the inputs are valid
                if (!Sanitizer.InvalidNewExpense(txtAmount.Text, dteDate.SelectedDate.Value) && !cmbCategory.Text.Equals(DefaultCatMessage))
                {
                    if (txtDescription.Text.Length > 100) // description over 100, enter the substring
                    {
                        currentUser.NewUserIncome.Add(new NewIncome(currentUser.UserID, double.Parse(txtAmount.Text.Substring(0, txtAmount.Text.IndexOf('.') + 3)),
                            dteDate.SelectedDate.Value, cmbCategory.Text, txtDescription.Text.Substring(0, 100)));
                    }
                    else
                    {
                        currentUser.NewUserIncome.Add(new NewIncome(currentUser.UserID, double.Parse(txtAmount.Text.Substring(0, txtAmount.Text.IndexOf('.') + 3)),
                            dteDate.SelectedDate.Value, cmbCategory.Text, txtDescription.Text));
                    }
                    MessageBox.Show("Record entered successfully!", "Success");
                    ClearInput();
                }
                else // some invalid input
                {
                    MessageBox.Show("Invalid input! Be sure to check all entries.", "Invalid Input");
                }
            }
            else // some invalid input
            {
                MessageBox.Show("Invalid input! Be sure to check all entries.", "Invalid Input");
            }
        }


        /// <summary>
        /// Helper function that clears all the input from the page
        /// </summary>
        private void ClearInput()
        {
            txtAmount.Text = "";
            dteDate.SelectedDate = null;
            cmbCategory.Text = DefaultCatMessage;
            txtDescription.Text = "";
        }
    }
}
