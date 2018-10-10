using System.Windows;
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
        }


        /// <summary>
        /// Closes the current window and takes user back to their dashboard
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReturnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;

            Close();
        }


        /// <summary>
        /// Does one of two things: sets the frame content of the window to a new expense page, or
        /// if there is already content dispalyed in the frame, it checks the type of that content.
        /// 
        /// If the type of the displayed content is of type NewExpensePage, then it does nothing. Otherwise
        /// it removes the content displayed and sets the frame content to a new NewExpensePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddExpense_Click(object sender, RoutedEventArgs e)
        {
            if (frmDashboard.Content != null)
            {
                if (frmDashboard.Content.GetType() == typeof(NewExpensePage))
                {
                    return;
                }
                else
                {
                    
                    frmDashboard.Content = new NewExpensePage(ref currentUser);
                }
            }
            else
            {
                frmDashboard.Content = new NewExpensePage(ref currentUser);
            }
        }


        /// <summary>
        /// Does one of two things: sets the frame content of the window to a new expense page, or
        /// if there is already content dispalyed in the frame, it checks the type of that content.
        /// 
        /// If the type of the displayed content is of type NewExpensePage, then it does nothing. Otherwise
        /// it removes the content displayed and sets the frame content to a new NewExpensePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddIncome_Click(object sender, RoutedEventArgs e)
        {
            if (frmDashboard.Content != null)
            {
                if (frmDashboard.Content.GetType() == typeof(NewIncomePage))
                {
                    return;
                }
                else
                {
                    
                    frmDashboard.Content = new NewIncomePage();
                }
            }
            else
            {
                frmDashboard.Content = new NewIncomePage();
            }
            //frmDashboard.NavigationService.RemoveBackEntry();
        }
    }
}
