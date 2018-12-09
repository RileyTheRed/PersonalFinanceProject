using PersonalFinanceProjectFinal.Models;
using System.Windows;
using System.Windows.Controls;
using PersonalFinanceProjectFinal.View_Models;

namespace PersonalFinanceProjectFinal.Pages
{
    /// <summary>
    /// Interaction logic for NewIncomePage.xaml
    /// </summary>
    public partial class NewIncomePage : Page
    {

        User currentUser;


        /// <summary>
        /// Default constructor for the NewIncomePage
        /// </summary>
        /// <param name="currUser"></param>
        public NewIncomePage(ref User currUser)
        {
            InitializeComponent();
            currentUser = currUser;

            DataContext = new NewIncomePageVM(ref currentUser, this);
        }



        private void txtAmount_LostFocus(object sender, RoutedEventArgs e)
        {
            txtAmount.Text = txtAmount.Text.Replace("-", "");
        }
    }
}
