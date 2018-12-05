using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using PersonalFinanceProjectFinal.View_Models;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Effects;

namespace PersonalFinanceProjectFinal.Pages
{
    /// <summary>
    /// Interaction logic for NewExpensePage.xaml
    /// </summary>
    public partial class NewExpensePage : Page
    {

        User currentUser;

        public NewExpensePage(ref User currUser)
        {
            InitializeComponent();
            currentUser = currUser;

            DataContext = new NewExpensePageVM(ref currentUser, this);
            
        }

        public void RunTutorialSequence()
        {
            ClearAllBlur();
            MessageBox.Show("Here is where you enter new record information.", "Help", MessageBoxButton.OK);
            BlurAllButAmount();
            MessageBox.Show("Begin by entering an record amount, in this case, and expense amount. Please leave out any currency indicators " +
                "such as dollar signs. The amount to be entered must be a positive amount as well.", "Help", MessageBoxButton.OK);
            ClearAllBlur();
            BlurAllButDate();
            MessageBox.Show("Next, select the date of the record in question.The date can not be in the future.", "Help", MessageBoxButton.OK);
            ClearAllBlur(); 
            BlurAllButCategory();
            MessageBox.Show("Then select a category that best describes what the record is for.", "Help", MessageBoxButton.OK);
            ClearAllBlur();
            BlurAllButDescription();
            MessageBox.Show("You also have the OPTION enter a custom description to better help be clear about the record. A maximum of 100 characters" +
                " will be stored, so try and keep the description short or your description will be cut short.", "Help", MessageBoxButton.OK);
            ClearAllBlur();
            BlurAllButSubmitButton();
            MessageBox.Show("When you are finished entering the record information, go ahead and press the Submit Record button!", "Help", MessageBoxButton.OK);
            ClearAllBlur();
        }


        #region BlurFunctions
        private void ClearAllBlur()
        {
            rectBackground.Effect = null;
            rectForeground.Effect = null;
            btnSubmitExpense.Effect = null;
            stkpanAmount.Effect = null;
            stkpanCategory.Effect = null;
            stkpanDate.Effect = null;
            stkpanDescription.Effect = null;
        }

        private void BlurAllButAmount()
        {
            rectBackground.Effect = new BlurEffect();
            rectForeground.Effect = new BlurEffect();
            btnSubmitExpense.Effect = new BlurEffect();
            stkpanCategory.Effect = new BlurEffect();
            stkpanDate.Effect = new BlurEffect();
            stkpanDescription.Effect = new BlurEffect();
        }

        private void BlurAllButCategory()
        {
            rectBackground.Effect = new BlurEffect();
            rectForeground.Effect = new BlurEffect();
            btnSubmitExpense.Effect = new BlurEffect();
            stkpanAmount.Effect = new BlurEffect();
            stkpanDate.Effect = new BlurEffect();
            stkpanDescription.Effect = new BlurEffect();
        }

        private void BlurAllButDate()
        {
            rectBackground.Effect = new BlurEffect();
            rectForeground.Effect = new BlurEffect();
            btnSubmitExpense.Effect = new BlurEffect();
            stkpanAmount.Effect = new BlurEffect();
            stkpanCategory.Effect = new BlurEffect();
            stkpanDescription.Effect = new BlurEffect();
        }

        private void BlurAllButDescription()
        {
            rectBackground.Effect = new BlurEffect();
            rectForeground.Effect = new BlurEffect();
            btnSubmitExpense.Effect = new BlurEffect();
            stkpanAmount.Effect = new BlurEffect();
            stkpanCategory.Effect = new BlurEffect();
            stkpanDate.Effect = new BlurEffect();
        }

        private void BlurAllButSubmitButton()
        {
            rectBackground.Effect = new BlurEffect();
            rectForeground.Effect = new BlurEffect();
            stkpanAmount.Effect = new BlurEffect();
            stkpanCategory.Effect = new BlurEffect();
            stkpanDate.Effect = new BlurEffect();
            stkpanDescription.Effect = new BlurEffect();
        }
        #endregion

        private void txtAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            txtAmount.Text = txtAmount.Text.Replace("-", "");
        }
    }
}
