using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System.Windows;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for RecordViewWindow.xaml
    /// </summary>
    public partial class RecordViewWindow : Window
    {

        SearchResultRecord selectedRecord;
        User currentUser;


        /// <summary>
        /// Constructor for the RecordViewWindow
        /// </summary>
        /// <param name="selected"></param>
        public RecordViewWindow(ref SearchResultRecord selected, string typeLastSearched, User current)
        {
            InitializeComponent();
            //colorscheme = colors;
            currentUser = current;
            DataContext = currentUser;
            selectedRecord = selected;
            txtAmount.Text = selectedRecord.Amount.ToString();
            txtDescription.Text = selectedRecord.Description.ToString();
            cmbCategory.Text = selectedRecord.Category;
            dteDate.SelectedDate = selectedRecord.Date;

            if (typeLastSearched.Equals("e"))
            {
                foreach (string item in Categories.GetExpenseCategories())
                {
                    cmbCategory.Items.Add(item);
                }
            }
            else
            {
                foreach (string item in Categories.GetIncomeCategories())
                {
                    cmbCategory.Items.Add(item);
                }
            }

            if (selected.Status.Equals("--"))
            {
                btnDelete.Visibility = Visibility.Hidden;
                btnDelete.IsEnabled = false;

                chkHasBeenDeleted.Visibility = Visibility.Visible;
                chkHasBeenDeleted.IsEnabled = true;
                chkHasBeenDeleted.IsChecked = true;
            }
        }


        /// <summary>
        /// Logic for the Cancel button to bring the user back to the dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
            Owner.IsEnabled = true;
        }


        /// <summary>
        /// Logic for the Delete button, giving the user the ability to mark records for deletion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result =  MessageBox.Show("Are you sure you want to delete this record?", "Confirmation", MessageBoxButton.YesNoCancel);
            if(result == MessageBoxResult.Yes)
            {
                selectedRecord.PreviousStatus = selectedRecord.Status;
                selectedRecord.Status = "--";
                selectedRecord.Modified = 1;
                Owner.IsEnabled = true;
                Owner.Activate();
                Close();
            }
            else if (result == MessageBoxResult.No)
            {
                Owner.IsEnabled = true;
                Close();
            }
            
        }


        /// <summary>
        /// logic for the Submit button, so the user can submit the 
        /// changes, if any, that they have made to the record
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (HasAmountChanged() || HasCategoryChanged() || HasDateChanged() || HasDescriptionChanged())
            {
                if (chkHasBeenDeleted.IsChecked == true)
                {
                    MessageBoxResult result = MessageBox.Show("The record is marked for deletion.\n" +
                        "Did you intent to reinstate the record and save the changes?", "Confimation", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        selectedRecord.Status = "++";
                        selectedRecord.Modified = 1;
                        UpdateSelectedRecord();
                        Owner.IsEnabled = true;
                        Owner.Activate();
                        Close();
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Owner.IsEnabled = true;
                        Owner.Activate();
                        Close();
                    }
                }
                else
                {
                    UpdateSelectedRecord();
                    selectedRecord.Status = "++";
                    selectedRecord.Modified = 1;
                    MessageBox.Show("Record Updated.", "Confirmation");
                    Owner.IsEnabled = true;
                    Owner.Activate();
                    Close();
                }
            }
            else
            {
                if (chkHasBeenDeleted.IsChecked == false && selectedRecord.Status.Equals("--"))
                {
                    MessageBoxResult result = MessageBox.Show("Do you wish to reinstate the record?", "Confirmation", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        if (selectedRecord.PreviousStatus.Equals("++"))
                        {
                            selectedRecord.Status = selectedRecord.PreviousStatus;
                            selectedRecord.Modified = 1;
                            Owner.IsEnabled = true;
                            Owner.Activate();
                            Close();
                        }
                        else
                        {
                            selectedRecord.Status = "";
                            Owner.IsEnabled = true;
                            Owner.Activate();
                            Close();
                        }
                    }
                    else if (result == MessageBoxResult.No)
                    {
                        Owner.IsEnabled = true;
                        Owner.Activate();
                        Close();
                    }
                }
                else
                {

                    MessageBox.Show("No changes made.", "Confirmation");
                    Owner.IsEnabled = true;
                    Owner.Activate();
                    Close();
                }
            }
        }


        private void UpdateSelectedRecord()
        {
            selectedRecord.Amount = double.Parse(txtAmount.Text);
            selectedRecord.Category = cmbCategory.Text;
            selectedRecord.Date = dteDate.SelectedDate.Value;
            if (!txtDescription.Text.Equals(""))
            {
                string sanitizedDescription = Sanitizer.GetSanitizedDescription(txtDescription.Text);
                if (sanitizedDescription.Length < 100)
                    selectedRecord.Description = sanitizedDescription;
                else
                    selectedRecord.Description = sanitizedDescription.Substring(0, 100);
            }
        }


        #region HelperFunctions
        private bool HasAmountChanged()
        {
            return !txtAmount.Text.Equals(selectedRecord.Amount.ToString());
        }


        private bool HasCategoryChanged()
        {
            return cmbCategory.Text != selectedRecord.Category;
        }


        private bool HasDescriptionChanged()
        {
            return !txtDescription.Text.Equals(selectedRecord.Description);
        }


        private bool HasDateChanged()
        {
            return !dteDate.SelectedDate.Equals(selectedRecord.Date);
        }
        #endregion

        
        private void Window_Closed(object sender, System.EventArgs e)
        {
            Close();
            Owner.IsEnabled = true;
        }
    }
}
