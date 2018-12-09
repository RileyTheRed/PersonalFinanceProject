using System.Windows;
using System.Windows.Media.Effects;
using System.Windows.Navigation;
using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Pages;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for AddRecordsWindow.xaml
    /// </summary>
    public partial class AddRecordsWindow : Window
    {

        User currentUser;


        /// <summary>
        /// Default constructor for the window
        /// </summary>
        public AddRecordsWindow(ref User referencedUser)
        {
            InitializeComponent();
            currentUser = referencedUser;
            //frmDashboard.Content = new AddRecordsPlaceHolder(referencedUser);

        }


        private void RunTutorialSequence()
        {
            ClearAllBlur();
            MessageBox.Show("In this window you can enter new expense or income record information.", "Help", MessageBoxButton.OK);
            BlurAllButAddRecordsButtons();
            MessageBox.Show("You can click on either of the two buttons to start inserting new records.", "Help", MessageBoxButton.OK);
            ClearAllBlur();
            BlurAllButFrame();
            frmDashboard.Content = new NewExpensePage(ref currentUser);
            frmDashboard.Navigated += delegate (object sender, NavigationEventArgs e)
            {
                
                ((NewExpensePage)frmDashboard.Content).RunTutorialSequence();
                ClearAllBlur();
                BlurAllButReturnToDashboardButton();
                MessageBox.Show("When you are done entering new record information, you can navigate back to your Dashboard by clicking" +
                    " the Return To Dashboard button.", "Help", MessageBoxButton.OK);
                ClearAllBlur();
                BlurAllButHelpButton();
                MessageBox.Show("And if at any time you need a reminder or refresh of any of this, just click the Help button.", "Help", MessageBoxButton.OK);
                ClearAllBlur();
            };

        }


        #region BlurFunctions
        private void ClearAllBlur()
        {
            btnAddExpense.Effect = null;
            btnAddIncome.Effect = null;
            btnReturnDashboard.Effect = null;
            btnHelp.Effect = null;
            frmDashboard.Effect = null;
            rectBackground.Effect = null;
        }

        private void BlurAllButAddRecordsButtons()
        {
            //btnAddExpense.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            btnReturnDashboard.Effect = new BlurEffect();
            rectBackground.Effect = new BlurEffect();
            frmDashboard.Effect = new BlurEffect();
        }

        private void BlurAllButReturnToDashboardButton()
        {
            btnAddExpense.Effect = new BlurEffect();
            btnAddIncome.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            rectBackground.Effect = new BlurEffect();
            frmDashboard.Effect = new BlurEffect();
        }

        private void BlurAllButHelpButton()
        {
            btnAddExpense.Effect = new BlurEffect();
            btnAddIncome.Effect = new BlurEffect();
            btnReturnDashboard.Effect = new BlurEffect();
            rectBackground.Effect = new BlurEffect();
            frmDashboard.Effect = new BlurEffect();
        }

        private void BlurAllButFrame()
        {
            btnAddExpense.Effect = new BlurEffect();
            btnAddIncome.Effect = new BlurEffect();
            btnReturnDashboard.Effect = new BlurEffect();
            btnHelp.Effect = new BlurEffect();
            rectBackground.Effect = new BlurEffect();
        }
        #endregion


        private void btnHelp_Click(object sender, RoutedEventArgs e)
        {
            RunTutorialSequence();
        }
    }
}
