using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    public partial class SearchRecordsWindow : Window, INotifyPropertyChanged
    {

        private static string CategoryDefaultMessage = "Please select a category...";
        private User currentUser;
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


        public event PropertyChangedEventHandler PropertyChanged = delegate { };


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
            DataContext = this;
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

            if ((bool)radioExpense.IsChecked)
            {
                var results = SearchResults.GetExpenseSearchResults(currentUser.ExistingUserExpenses,
                    currentUser.NewUserExpenses, cmbCategory, txtMin, txtMax, dteStart, dteEnd);
                Results = SearchResultRecord.GetExpenseResults(results.Item1, results.Item2);
                //lstSearchResults.ItemsSource = SearchResultRecord.GetExpenseResults(results.Item1,results.Item2);
            }

        }


        private void lstSearchResults_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            selectedRecord = lstSearchResults.SelectedItem as SearchResultRecord;
            RecordViewWindow editWindow = new RecordViewWindow(ref selectedRecord);

            editWindow.Owner = this;
            IsEnabled = false;

            editWindow.IsEnabled = true;
            editWindow.Visibility = Visibility.Visible;
        }
    }
}
