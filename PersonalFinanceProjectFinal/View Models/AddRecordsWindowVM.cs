using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Pages;


namespace PersonalFinanceProjectFinal.View_Models
{

    public class AddRecordsWindowVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        
        public User user { get; set; }
        private Page _displayedPage = new AddRecordsPlaceHolder();
        public Page DisplayedPage
        {
            get { return _displayedPage; }
            set
            {
                if (value is NewExpensePage && _displayedPage is NewExpensePage) { }
                else if (value is NewIncomePage && _displayedPage is NewIncomePage) { }
                else
                {
                    _displayedPage = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("DisplayedPage"));
                }
            }
        }
        public AddRecordsWindow window;


        public AddRecordsWindowVM(User current, AddRecordsWindow win)
        {
            //parentVM = rent;
            user = current;
            window = win;
        }


        public ICommand NewExpenseCommand
        {
            get
            {
                if (_newExpenseCommand == null)
                {
                    _newExpenseCommand = new DelegateCommand(NewExpenseClicked);
                }
                return _newExpenseCommand;
            }
        }
        DelegateCommand _newExpenseCommand;
        private void NewExpenseClicked(object obj)
        {
            var temp = user;
            DisplayedPage = new NewExpensePage(ref temp);
            window.frmDashboard.Content = DisplayedPage;
            //parentVM.dashboard.Visibility = System.Windows.Visibility.Hidden;
            //parentVM.dashboard.IsEnabled = false;
        }


        public ICommand NewIncomeCommand
        {
            get
            {
                if (_newIncomeCommand == null)
                {
                    _newIncomeCommand = new DelegateCommand(NewIncomeClicked);
                }
                return _newIncomeCommand;
            }
        }
        DelegateCommand _newIncomeCommand;
        private void NewIncomeClicked(object obj)
        {
            var temp = user;
            DisplayedPage = new NewIncomePage(ref temp);
            window.frmDashboard.Content = DisplayedPage;
        }


        public ICommand ReturnToDashboardCommand
        {
            get
            {
                if (_returnToDashboardCommand == null)
                {
                    _returnToDashboardCommand = new DelegateCommand(ReturnToDashboardClicked);
                }
                return _returnToDashboardCommand;
            }
        }
        DelegateCommand _returnToDashboardCommand;
        private void ReturnToDashboardClicked(object obj)
        {
            window.Owner.Visibility = System.Windows.Visibility.Visible;
            window.Owner.IsEnabled = true;

            window.Close();
        }


    }
}
