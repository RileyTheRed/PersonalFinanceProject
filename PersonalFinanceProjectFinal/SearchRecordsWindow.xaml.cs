using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;

namespace PersonalFinanceProjectFinal
{

    /// <summary>
    /// Interaction logic for SearchRecordsWindow.xaml
    /// </summary>
    public partial class SearchRecordsWindow : Window, INotifyPropertyChanged
    {

        #region PropertiesAndOtherThings
        private static string CategoryDefaultMessage = "Please select a category...";
        private User currentUser;
        private string typeOfRecordSearched = "";
        private bool validAmountRanges = true;
        private bool validDateRanges = true;
        private SearchResultRecord selectedRecord;
        private ObservableCollection<SearchResultRecord> _results;
        public ObservableCollection<SearchResultRecord> Results
        {
            get { return _results; }
            set
            {
                _results = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Results"));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged = delegate { }; // do I even need this?
        #endregion


        /// <summary>
        /// Constructor for the SearchRecordsWindow
        /// </summary>
        public SearchRecordsWindow(ref User user)
        {
            InitializeComponent();

            currentUser = user;

            cmbCategory.Items.Add(CategoryDefaultMessage);
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
            if (typeOfRecordSearched.Equals("e"))
                UpdateModifiedExpenseList();
            else if (typeOfRecordSearched.Equals("i"))
                UpdateModifiedIncomeList();

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
            else
            {
                if (!txtMax.Text.Equals(""))
                {
                    if (Double.Parse(txtMin.Text) > Double.Parse(txtMax.Text))
                        validAmountRanges = false;
                    else
                        validAmountRanges = true;
                }
            }
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
            else
            {
                if (!txtMin.Text.Equals(""))
                {
                    if (Double.Parse(txtMax.Text) < Double.Parse(txtMax.Text))
                        validAmountRanges = false;
                    else
                        validAmountRanges = true;
                }
            }
        }

        private void dteStart_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dteStart.SelectedDate != null && dteEnd.SelectedDate != null)
            {
                if (dteStart.SelectedDate > dteEnd.SelectedDate)
                    validDateRanges = false;
                else
                    validDateRanges = true;
            }
        }

        private void dteEnd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (dteStart.SelectedDate != null && dteEnd.SelectedDate != null)
            {
                if (dteStart.SelectedDate > dteEnd.SelectedDate)
                    validDateRanges = false;
                else
                    validDateRanges = true;
            }
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

        
        /// <summary>
        /// Logic for searching for existing records
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {


            if (validDateRanges && validAmountRanges)
            {
                if (typeOfRecordSearched.Equals("e"))
                {
                    UpdateModifiedExpenseList();
                }
                else if (typeOfRecordSearched.Equals("i"))
                {
                    UpdateModifiedIncomeList();
                }

                if ((bool)radioExpense.IsChecked)
                {
                    var results = SearchResults.GetExpenseSearchResults(currentUser.ExistingUserExpenses,
                        currentUser.NewUserExpenses, currentUser.ModifiedExpenseRecords, cmbCategory, txtMin, txtMax, dteStart, dteEnd);
                    Results =
                        new ObservableCollection<SearchResultRecord>(
                            SearchResultRecord.GetExpenseResults(
                                results.Item1, results.Item2, results.Item3).OrderByDescending(x => x.Date).ToList());
                    lstSearchResults.ItemsSource = Results;
                    typeOfRecordSearched = "e";
                }
                else if ((bool)radioIncome.IsChecked)
                {
                    var results = SearchResults.GetIncomeSearchResults(currentUser.ExistingUserIncome,
                        currentUser.NewUserIncome, currentUser.ModifiedIncomeRecords, cmbCategory, txtMin, txtMax, dteStart, dteEnd);
                    Results =
                        new ObservableCollection<SearchResultRecord>(
                            SearchResultRecord.GetIncomeResults(
                                results.Item1, results.Item2, results.Item3).OrderByDescending(x => x.Date).ToList());
                    lstSearchResults.ItemsSource = Results;
                    typeOfRecordSearched = "i";
                }
                else
                {
                    MessageBox.Show("Please select the type of record you wish to search.", "Error");
                }
                return;
            }
            MessageBox.Show("Invalid input ranges! Please check that any range entered\n" +
                "does not have the minimum greater than the specified maximum and vice versa.", "Error");

        }


        /// <summary>
        /// logic for double clicking on a search result to display the window to edit it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstSearchResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (lstSearchResults.SelectedItem != null)
            {
                selectedRecord = lstSearchResults.SelectedItem as SearchResultRecord;
                RecordViewWindow editWindow = new RecordViewWindow(ref selectedRecord);

                editWindow.Owner = this;
                IsEnabled = false;

                editWindow.IsEnabled = true;
                editWindow.Visibility = Visibility.Visible;
            }
        }


        /// <summary>
        /// Used to refresh the list of search results after potential modification of records from the recordviewwindow
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(lstSearchResults.ItemsSource).Refresh();
        }


        /// <summary>
        /// Used to update the list of modified records
        /// 
        /// When a user modifies either an expense or income record, they do have the ability
        /// to modify it yet again. this function ensures that the most recent modification of the record
        /// is stored in the modified list
        /// </summary>
        private void UpdateModifiedExpenseList()
        {
            foreach (SearchResultRecord item in lstSearchResults.Items)
            {
                if (item.Modified == 1)
                {
                    int index = currentUser.ModifiedExpenseRecords.FindIndex(x => x.Hash.Equals(item.Hash));
                    if (index == -1)
                    {
                        currentUser.ModifiedExpenseRecords.Add(item);
                    }
                    else
                    {
                        if (!currentUser.ModifiedExpenseRecords.ElementAt(index).Equals(item))
                        {
                            currentUser.ModifiedExpenseRecords.RemoveAt(index);
                            currentUser.ModifiedExpenseRecords.Add(item);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Used to update the list of modified records
        /// 
        /// When a user modifies either an expense or income record, they do have the ability
        /// to modify it yet again. this function ensures that the most recent modification of the record
        /// is stored in the modified list
        /// </summary>
        private void UpdateModifiedIncomeList()
        {
            foreach (SearchResultRecord item in lstSearchResults.Items)
            {
                if (item.Modified == 1)
                {
                    int index = currentUser.ModifiedIncomeRecords.FindIndex(x => x.Hash.Equals(item.Hash));
                    if (index == -1)
                    {
                        currentUser.ModifiedIncomeRecords.Add(item);
                    }
                    else
                    {
                        if (!currentUser.ModifiedIncomeRecords.ElementAt(index).Equals(item))
                        {
                            currentUser.ModifiedIncomeRecords.RemoveAt(index);
                            currentUser.ModifiedIncomeRecords.Add(item);
                        }
                    }
                }
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (typeOfRecordSearched.Equals("e"))
                UpdateModifiedExpenseList();
            else if (typeOfRecordSearched.Equals("i"))
                UpdateModifiedIncomeList();

            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;

            Close();
        }
    }
}
