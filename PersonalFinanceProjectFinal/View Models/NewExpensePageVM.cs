/**
 * NewExpensePageVM.cs
 * 
 * Author: Riley Wells
 * 
 * Updates:
 *      12/8/18 - Small formatting modifications (spacing) and a few more lines of 
 *                documentation.
 **/

using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Pages;
using PersonalFinanceProjectFinal.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;


namespace PersonalFinanceProjectFinal.View_Models
{

    public class NewExpensePageVM : INotifyPropertyChanged
    {


        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        #region Properties
        public User currentUser { get; set; }
        public NewExpensePage page { get; set; }


        private string _amount;
        public string Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Amount"));

            }
        }


        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                _category = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Category"));
            }
        }


        private DateTime? _date;
        public DateTime? Date
        {
            get { return _date; }
            set
            {
                _date = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Date"));
            }
        }


        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Description"));
            }
        }


        private List<string> _catList;
        public List<string> CatList
        {
            get { return _catList; }
            set
            {
                _catList = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CatList"));
            }
        }


        private string DefaultCatMessage = "Please select a category...";
        #endregion


        /// <summary>
        /// Constructor for the NewExpensePageVM
        /// </summary>
        /// <param name="current"></param>
        /// <param name="pg"></param>
        public NewExpensePageVM(ref User current, NewExpensePage pg)
        {
            currentUser = current;
            Category = "Please select a category...";
            Date = null;
            page = pg;
            CatList = Categories.GetExpenseCategories();
            pg.dteDate.DisplayDateEnd = DateTime.Now;
        }


        #region Commands
        public ICommand SubmitCommand
        {
            get
            {
                if (_submitCommand == null)
                {
                    _submitCommand = new DelegateCommand(SubmitClicked);
                }
                return _submitCommand;
            }
        }
        DelegateCommand _submitCommand;
        private void SubmitClicked(object obj)
        {

            // have they entered a valid date?
            if (Date != null)
            {

                //are all other inputs valid?
                if (!Sanitizer.InvalidNewExpense(Amount, Date.Value) && !Category.Equals(DefaultCatMessage))
                {

                    // sanitize desciption of all non alpha-numeric characters
                    string sanitizedDescription = Sanitizer.GetSanitizedDescription(Description);
                    if (sanitizedDescription.Length > 100)
                    {
                        currentUser.NewUserExpenses.Add(new NewExpense(currentUser.UserID, Double.Parse(string.Format("{0:N2}", Double.Parse(Amount))),
                            Date.Value, Category, sanitizedDescription.Substring(0, 100)));
                    }
                    else
                    {
                        currentUser.NewUserExpenses.Add(new NewExpense(currentUser.UserID, Double.Parse(string.Format("{0:N2}", Double.Parse(Amount))),
                            Date.Value, Category, sanitizedDescription));
                    }
                    MessageBox.Show("Record entered successfully!", "Success");
                    ClearInput();
                }
                else
                {
                    MessageBox.Show("Invalid input! Be sure to check all entries.", "Invalid Input");
                }
            }
            else
            {
                MessageBox.Show("Invalid input! Be sure to check all entries.", "Invalid Input");
            }
        }
        #endregion


        private void ClearInput()
        {
            Amount = "";
            Date = null;
            Category = DefaultCatMessage;
            Description = "";
        }


    }
}
