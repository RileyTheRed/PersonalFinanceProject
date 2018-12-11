/**
 * AddRecordsWindowVM.cs
 * 
 * Author: Riley Wells
 * 
 * Updates:
 *      12/9/18 - Added more documentation, removed unnecesary couple with View in the NewExpenseClicked
 *                and NewIncomeClicked functions.
 *                
 * */

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


        #region Proprties
        public User user { get; set; }


        /// <summary>
        /// Property used to set the frame content of the AddRecordsWindow.
        /// 
        /// If the assigned value is the same type as the current frame content,
        /// do nothing. Otherwise, set the DisplayedPage to the new value.
        /// </summary>
        private Page _displayedPage;
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
        #endregion


        /// <summary>
        /// Constructor for the AddRecordsWindowVM
        /// </summary>
        /// <param name="current"></param>
        /// <param name="win"></param>
        public AddRecordsWindowVM(User current, AddRecordsWindow win)
        {
            //parentVM = rent;
            user = current;
            window = win;
            DisplayedPage = new AddRecordsPlaceHolder(current);
        }


        #region Commands
        /// <summary>
        /// Defintion of the NewExpenseCommand ICommand, related _newExpenseCommand
        /// DelegateCommand, and the NewExpenseClicked function.
        /// 
        /// NewExpenseClicked sets the DisplayedPage property to a NewExpensePage and
        /// passes the User information to it.
        /// </summary>
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
        }


        /// <summary>
        /// Defintion of the NewIncomeCommand ICommand, related _newIncomeCommand DelegateCommand,
        /// and the NewIncomeClicked function.
        /// 
        /// NewIncomeClicked sets DisplayedPage property to a NewIncomePage, and passes
        /// the user information to it.
        /// </summary>
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
        }


        /// <summary>
        /// Defintion of the ReturnToDashboardCommand ICommand, related _returnToDashboardCommand
        /// DelegateCommand, and the ReturnToDashboardClicked function.
        /// 
        /// ReturnToDashboardClicked sets the parent's owner window to be visible and enabled,
        /// and closes the parent window.
        /// </summary>
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
        #endregion

    }
}
