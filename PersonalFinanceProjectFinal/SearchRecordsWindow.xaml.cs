using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
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

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for SearchRecordsWindow.xaml
    /// </summary>
    public partial class SearchRecordsWindow : Window
    {

        private static string CategoryDefaultMessage = "Please select a category...";
        private User currentUser;

        /// <summary>
        /// Constructor for the SearchRecordsWindow
        /// </summary>
        public SearchRecordsWindow(ref User user)
        {
            InitializeComponent();

            currentUser = user;

            foreach (string item in Categories.GetCategories())
            {
                cmbCategory.Items.Add(item);
            }

            dteStart.DisplayDateEnd = DateTime.Now.Date;
            dteEnd.DisplayDateEnd = DateTime.Now.Date;
        }


        /// <summary>
        /// Logic for the button that returns the user back to the dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;


            Close();
        }


        #region FocusCalls
        private void txtMin_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMin.Text.Equals("MIN"))
                txtMin.Text = "";
        }

        private void txtMin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtMin.Text.Equals(""))
                txtMin.Text = "MIN";
        }

        private void txtMax_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtMax.Text.Equals("MAX"))
                txtMax.Text = "";
        }

        private void txtMax_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtMax.Text.Equals(""))
                txtMax.Text = "MAX";
        }
        #endregion
         

        #region HelperFunctions
        private bool ValidChoice()
        {
            return radioExpense.IsChecked.HasValue ? true : (radioIncome.IsChecked.HasValue ? true : false);
        }

        private bool HasPickedCategory()
        {
            return cmbCategory.Text.Equals(CategoryDefaultMessage);
        }

        private bool HasPickedTwoDates()
        {
            return dteEnd.SelectedDate.HasValue && dteStart.SelectedDate.HasValue;
        }

        private bool HasPickedStartingDate()
        {
            return dteStart.SelectedDate.HasValue;
        }

        private bool HasPickedEndingDate()
        {
            return dteEnd.SelectedDate.HasValue;
        }

        private bool HasEnteredTwoAmounts()
        {
            return !txtMax.Text.Equals("MAX") && !txtMin.Text.Equals("MIN");
        }

        private bool HasEnteredMinAmount()
        {
            return !txtMin.Text.Equals("MIN");
        }

        private bool HasEnteredMaxAmount()
        {
            return !txtMin.Text.Equals("MAX");
        }
        #endregion

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            bool hasPickedCat = HasPickedCategory();
            bool hasPickedTwoDates = HasPickedTwoDates();

            DateTime starting;
            DateTime ending;

            string type;

            if (!ValidChoice())
            {
                MessageBox.Show("Please select a record type...", "Invalid Selection");
                return;
            }

            type = radioExpense.IsChecked.HasValue ? "Expense" : "Income";
            
            if (hasPickedCat) // user has selected a category
            {
                // user has entered two amounts
                if (HasEnteredTwoAmounts())
                {
                    if (!Sanitizer.ValidAmount(txtMin.Text) || !Sanitizer.ValidAmount(txtMax.Text)) // make sure the inputs are valid
                    {
                        MessageBox.Show("Invalid entries for MIN and/or MAX amount.", "Invalid Entry");
                        return;
                    }
                    if (Double.Parse(txtMin.Text) > Double.Parse(txtMax.Text))
                    {
                        MessageBox.Show("Minimum amount value cant be greater than Maximum value");
                        return;
                    }
                    if (hasPickedTwoDates) // user has chosen a start and end date range
                    {

                    }
                    else if (HasPickedStartingDate()) // user has chose only a start date
                    {

                    }
                    else if (HasPickedEndingDate()) // user has chosen only an end date
                    {

                    }
                }
                // user has enetered minimum amount only
                else if (HasEnteredMinAmount())
                {

                }
                // user has entered maximum amount only
                else if (HasEnteredMaxAmount())
                {

                }
                else // user has not entered an amount range
                {

                    if (hasPickedTwoDates) // user has chosen a start and end date range
                    {

                    }
                    else if (HasPickedStartingDate()) // user has chose only a start date
                    {

                    }
                    else if (HasPickedEndingDate()) // user has chosen only an end date
                    {

                    }

                }
            }
            else // user has not selected a category
            {
                // user has entered two amounts
                if (HasEnteredTwoAmounts())
                {

                }
                // user has enetered minimum amount only
                else if (HasEnteredMinAmount())
                {

                }
                // user has entered maximum amount only
                else if (HasEnteredMaxAmount())
                {

                }
                else // user has not entered an amount range
                {

                    if (hasPickedTwoDates) // user has chosen a start and end date range
                    {

                    }
                    else if (HasPickedStartingDate()) // user has chose only a start date
                    {

                    }
                    else if (HasPickedEndingDate()) // user has chosen only an end date
                    {

                    }
                    else
                    {
                        foreach (ExistingExpense item in currentUser.ExistingUserExpenses)
                        {
                            lstSearchResults.Items.Add(item.ToString());
                        }
                    }
                }
            }
            

        }
    }
}
