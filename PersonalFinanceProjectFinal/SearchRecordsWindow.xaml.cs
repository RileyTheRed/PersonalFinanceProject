using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Effects;

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
        private string[] colors = new string[4];
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
            DataContext = currentUser;

            cmbCategory.Items.Add(CategoryDefaultMessage);

            dteStart.DisplayDateEnd = DateTime.Now.Date;
            dteEnd.DisplayDateEnd = DateTime.Now.Date;

            //gridMain.Effect = new BlurEffect();
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
            txtMin.Text = txtMin.Text.Replace("-","");

            if (txtMin.Text.Equals(""))
                txtMin.Text = "MIN";
            else
            {
                if (Sanitizer.ValidAmount(txtMin.Text))
                {
                    if (!txtMax.Text.Equals("MAX"))
                    {
                        if (Double.Parse(txtMin.Text) > Double.Parse(txtMax.Text))
                            validAmountRanges = false;
                        else
                            validAmountRanges = true;
                    }
                }
                else
                {
                    txtMin.Text = "MIN";
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
            txtMax.Text = txtMax.Text.Replace("-", "");
            if (txtMax.Text.Equals(""))
                txtMax.Text = "MAX";
            else
            {
                if (Sanitizer.ValidAmount(txtMax.Text))
                {
                    if (!txtMin.Text.Equals(""))
                    {
                        if (Double.Parse(txtMax.Text) < Double.Parse(txtMax.Text))
                            validAmountRanges = false;
                        else
                            validAmountRanges = true;
                    }
                }
                else
                {
                    txtMax.Text = "MAX";
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
                    if (Results.Count == 0)
                    {
                        MessageBox.Show("No results found!", "No Results", MessageBoxButton.OK);
                    }
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
                    if (Results.Count == 0)
                    {
                        MessageBox.Show("No results found!", "No Results", MessageBoxButton.OK);
                    }
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
                colors[0] = currentUser.Color1;
                colors[1] = currentUser.Color2;
                colors[2] = currentUser.Color3;
                colors[3] = currentUser.Color4;
                RecordViewWindow editWindow = new RecordViewWindow(ref selectedRecord, typeOfRecordSearched, currentUser);

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

        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            RunTutorial();
        }

        private void RunTutorial()
        {
            ClearAllBlur();
            MessageBox.Show("This is the Search Records Page. You can search different types of records, and specify a number of different types" +
                " of parameters.", "Tutorial", MessageBoxButton.OK);
            BlurAllButSearchStuff();
            MessageBox.Show("The search box lets you specify whether you want to search income or expense records. If you have a specific" +
                " category in mind, an amount range or a specific range of dates you wish to search, you can specifiy all those things here." +
                " At a minimum, you must specifiy a record type.", "Tutorial", MessageBoxButton.OK);
            ClearAllBlur();
            BlurAllButSearchResults();
            MessageBox.Show("The results of your record search, if any, are displayed here in this box. By default the results are listed in" +
                " decending order by date. You can double click on a search result and you will be prompted with a new window where you can modify the record," +
                " mark it for deletion, or just view it in more detail. Records that have been modified will have a '++' next to it in the result view," +
                " and records that have been marked for deletion will have a '--' next to them. All modified or deleted records are immediately reflected" +
                " on your Dashboard.", "Tutorial", MessageBoxButton.OK);
            ClearAllBlur();
            BlurAllButReturnToDashButton();
            MessageBox.Show("If at any time you wish to return to your Dashboard, all you have to do is click the Return Dashboard button!", "Tutorial", MessageBoxButton.OK);
            ClearAllBlur();
            BlurAllButHelpButton();
            MessageBox.Show("And if at any time you need a reminder of all that you can do, don't hesitate to click the Help button!", "Tutorial", MessageBoxButton.OK);
            ClearAllBlur();
        }

        #region BlurEffectFunctions
        private void ClearAllBlur()
        {
            rectBackground.Effect = null;
            rectLeftGray.Effect = null;
            rectLeftYellow.Effect = null;
            rectRightYellow.Effect = null;
            lstSearchResults.Effect = null;
            btnHelp.Effect = null;
            btnReturnDashboard.Effect = null;
            btnSearch.Effect = null;
            lblAmountRange.Effect = null;
            lblDescription.Effect = null;
            lblEndDate.Effect = null;
            lblCategory.Effect = null;
            lblRecordType.Effect = null;
            lblStartDate.Effect = null;
            radioExpense.Effect = null;
            radioIncome.Effect = null;
            cmbCategory.Effect = null;
            txtMax.Effect = null;
            txtMin.Effect = null;
            dteEnd.Effect = null;
            dteStart.Effect = null;
        }

        private void BlurAllButSearchStuff()
        {
            btnHelp.Effect = new BlurEffect();
            btnReturnDashboard.Effect = new BlurEffect();
            rectBackground.Effect = new BlurEffect();
            rectRightYellow.Effect = new BlurEffect();
            lstSearchResults.Effect = new BlurEffect();
        }

        private void BlurAllButSearchResults()
        {
            btnHelp.Effect = new BlurEffect();
            btnReturnDashboard.Effect = new BlurEffect();
            btnSearch.Effect = new BlurEffect();
            rectLeftGray.Effect = new BlurEffect();
            rectLeftYellow.Effect = new BlurEffect();
            rectBackground.Effect = new BlurEffect();
            lblAmountRange.Effect = new BlurEffect();
            lblCategory.Effect = new BlurEffect();
            lblDescription.Effect = new BlurEffect();
            lblEndDate.Effect = new BlurEffect();
            lblRecordType.Effect = new BlurEffect();
            lblStartDate.Effect = new BlurEffect();
            radioExpense.Effect = new BlurEffect();
            radioIncome.Effect = new BlurEffect();
            cmbCategory.Effect = new BlurEffect();
            txtMax.Effect = new BlurEffect();
            txtMin.Effect = new BlurEffect();
            dteEnd.Effect = new BlurEffect();
            dteStart.Effect = new BlurEffect();
        }

        private void BlurAllButReturnToDashButton()
        {
            rectBackground.Effect = new BlurEffect();
            rectLeftGray.Effect = new BlurEffect();
            rectLeftYellow.Effect = new BlurEffect();
            rectRightYellow.Effect = new BlurEffect();
            lstSearchResults.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            btnSearch.Effect = new BlurEffect();
            lblAmountRange.Effect = new BlurEffect();
            lblDescription.Effect = new BlurEffect();
            lblEndDate.Effect = new BlurEffect();
            lblCategory.Effect = new BlurEffect();
            lblRecordType.Effect = new BlurEffect();
            lblStartDate.Effect = new BlurEffect();
            radioExpense.Effect = new BlurEffect();
            radioIncome.Effect = new BlurEffect();
            cmbCategory.Effect = new BlurEffect();
            txtMax.Effect = new BlurEffect();
            txtMin.Effect = new BlurEffect();
            dteEnd.Effect = new BlurEffect();
            dteStart.Effect = new BlurEffect();
        }

        private void BlurAllButHelpButton()
        {
            rectBackground.Effect = new BlurEffect();
            rectLeftGray.Effect = new BlurEffect();
            rectLeftYellow.Effect = new BlurEffect();
            rectRightYellow.Effect = new BlurEffect();
            lstSearchResults.Effect = new BlurEffect();
            btnReturnDashboard.Effect = new BlurEffect();
            btnSearch.Effect = new BlurEffect();
            lblAmountRange.Effect = new BlurEffect();
            lblDescription.Effect = new BlurEffect();
            lblEndDate.Effect = new BlurEffect();
            lblCategory.Effect = new BlurEffect();
            lblRecordType.Effect = new BlurEffect();
            lblStartDate.Effect = new BlurEffect();
            radioExpense.Effect = new BlurEffect();
            radioIncome.Effect = new BlurEffect();
            cmbCategory.Effect = new BlurEffect();
            txtMax.Effect = new BlurEffect();
            txtMin.Effect = new BlurEffect();
            dteEnd.Effect = new BlurEffect();
            dteStart.Effect = new BlurEffect();
        }
        #endregion

        private void radioExpense_Click(object sender, RoutedEventArgs e)
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add(CategoryDefaultMessage);
            cmbCategory.Text = CategoryDefaultMessage;
            foreach (string item in Categories.GetExpenseCategories())
                cmbCategory.Items.Add(item);
        }

        private void radioIncome_Click(object sender, RoutedEventArgs e)
        {
            cmbCategory.Items.Clear();
            cmbCategory.Items.Add(CategoryDefaultMessage);
            cmbCategory.Text = CategoryDefaultMessage;
            foreach (string item in Categories.GetIncomeCategories())
                cmbCategory.Items.Add(item);
        }
    }
}
