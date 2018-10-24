using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for RecordViewWindow.xaml
    /// </summary>
    public partial class RecordViewWindow : Window
    {

        SearchResultRecord selectedRecord;
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
        private bool hasBeenEdited = false;


        /// <summary>
        /// Constructor for the RecordViewWindow
        /// </summary>
        /// <param name="selected"></param>
        public RecordViewWindow(ref SearchResultRecord selected)
        {
            InitializeComponent();
            selectedRecord = selected;
            txtAmount.Text = selectedRecord.Amount.ToString();
            txtDescription.Text = selectedRecord.Description.ToString();
            cmbCategory.Text = selectedRecord.Category;
            dteDate.SelectedDate = selectedRecord.Date;
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
                selectedRecord.Delete = 1;
                Owner.IsEnabled = true;
                Owner.Activate();
                
                //PropertyChanged(this, new PropertyChangedEventArgs("Results"));
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

        }
    }
}
