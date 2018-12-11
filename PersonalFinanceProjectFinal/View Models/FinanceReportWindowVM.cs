/**
 * FinanceReportWindowVM.cs
 * 
 * Author: Riley Wells
 * 
 * Updates:
 *      12/9/18 - Further documentation
 **/

using LiveCharts;
using LiveCharts.Wpf;
using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace PersonalFinanceProjectFinal.View_Models
{
    public class FinanceReportWindowVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        #region Properties
        // the current months expense categories and amounts
        private SeriesCollection _currentSeries;
        public SeriesCollection CurrentSeries
        {
            get { return _currentSeries; }
            set
            {
                _currentSeries = value;
                bool hasSomething = false;
                foreach (PieSeries item in value)
                {
                    if ((double)item.Values[0] > 0)
                    {
                        hasSomething = true;
                        break;
                    }
                }
                if (!hasSomething)
                    HasCurrent = Visibility.Visible;
                else
                    HasCurrent = Visibility.Hidden;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentSeries"));
            }
        }


        // the previous months expense categories and amounts
        private SeriesCollection _previousOneSeries;
        public SeriesCollection PreviousOneSeries
        {
            get { return _previousOneSeries; }
            set
            {
                _previousOneSeries = value;
                bool hasSomething = false;
                foreach (PieSeries item in value)
                {
                    if ((double)item.Values[0] > 0)
                    {
                        hasSomething = true;
                        break;
                    }
                }
                if (!hasSomething)
                    HasPreviousOne = Visibility.Visible;
                else
                    HasPreviousOne = Visibility.Hidden;
                PropertyChanged(this, new PropertyChangedEventArgs("PreviousOneSeries"));
            }
        }


        // the previous - 1 months expense categories and amounts
        private SeriesCollection _previousTwoSeries;
        public SeriesCollection PreviousTwoSeries
        {
            get { return _previousTwoSeries; }
            set
            {
                _previousTwoSeries = value;
                bool hasSomething = false;
                foreach (PieSeries item in value)
                {
                    if ((double)item.Values[0] > 0)
                    {
                        hasSomething = true;
                        break;
                    }
                }
                if (!hasSomething)
                    HasPreviousTwo = Visibility.Visible;
                else
                    HasPreviousTwo = Visibility.Hidden;
                PropertyChanged(this, new PropertyChangedEventArgs("PreviousTwoSeries"));
            }
        }


        // the previous - 2 months expense categories and amounts
        private SeriesCollection _previousThreeSeries;
        public SeriesCollection PreviousThreeSeries
        {
            get { return _previousThreeSeries; }
            set
            {
                _previousThreeSeries = value;
                bool hasSomething = false;
                foreach (PieSeries item in value)
                {
                    if ((double)item.Values[0] > 0)
                    {
                        hasSomething = true;
                        break;
                    }
                }
                if (!hasSomething)
                    HasPreviousThree = Visibility.Visible;
                else
                    HasPreviousThree = Visibility.Hidden;
                PropertyChanged(this, new PropertyChangedEventArgs("PreviousThreeSeries"));
            }
        }


        // the overall average of expense categories and amounts
        private SeriesCollection _averageSeries;
        public SeriesCollection AverageSeries
        {
            get { return _averageSeries; }
            set
            {
                _averageSeries = value;
                bool hasSomething = false;
                foreach (PieSeries item in value)
                {
                    if ((double)item.Values[0] > 0)
                    {
                        hasSomething = true;
                        break;
                    }
                }
                if (!hasSomething)
                    HasAverage = Visibility.Visible;
                else
                    HasAverage = Visibility.Hidden;
                PropertyChanged(this, new PropertyChangedEventArgs("AverageSeries"));
            }
        }


        // is set to hidden if there are no expense records for the current month 
        private Visibility _hasCurrent;
        public Visibility HasCurrent
        {
            get { return _hasCurrent; }
            set
            {
                _hasCurrent = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HasCurrent"));
            }
        }


        // is set to hidden if there are no expense records for the previous month
        private Visibility _hasPreviousOne;
        public Visibility HasPreviousOne
        {
            get { return _hasPreviousOne; }
            set
            {
                _hasPreviousOne = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HasPreviousOne"));
            }
        }


        // is set to hidden if there are no expense records for the previous - 1 month
        private Visibility _hasPreviousTwo;
        public Visibility HasPreviousTwo
        {
            get { return _hasPreviousTwo; }
            set
            {
                _hasPreviousTwo = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HasPreviousTwo"));
            }
        }


        // is set to hidden if there are no expense records for the previous - 2 month
        private Visibility _hasPreviousThree;
        public Visibility HasPreviousThree
        {
            get { return _hasPreviousThree; }
            set
            {
                _hasPreviousThree = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HasPreviousThree"));
            }
        }


        // is set to hidden if there are no expense records at all for the user
        private Visibility _hasAverage;
        public Visibility HasAverage
        {
            get { return _hasAverage; }
            set
            {
                _hasAverage = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HasAverage"));
            }
        }


        // used to display the users overall expense-to-income ratio
        private double _average;
        public double Average
        {
            get { return _average; }
            set
            {
                if ((value * 100) > 110)
                    _average = 110;
                else
                    _average = value * 100;
                Message = "";
                PropertyChanged(this, new PropertyChangedEventArgs("Average"));
            }
        }

        
        // depending on the value of Average, a certain message is displayed below the finance "speedometer"
        private string _message;
        public string Message
        {
            get { return _message; }
            set
            {
                if (Average > 100)
                {
                    if (Average >= 110)
                    {
                        _message =
                            $"On average, you spend %{Average}, if not more, of what you make each month.";
                    }
                    else
                    {
                        _message =
                            $"On average you spend %{Average} of what you make each month.";
                    }
                }
                else
                {
                    _message =
                        $"On average you spend %{Average} of what you make each month.";
                }
                PropertyChanged(this, new PropertyChangedEventArgs("Average"));
            }
        }


        // parent window and user
        private Window1 parent { get; set; }
        public User cur { get; set; }
        #endregion


        /// <summary>
        /// GetSeries extracts the users expense data and inserts the values to 
        /// their respective SeriesCollection.
        /// </summary>
        /// <param name="current"></param>
        private void GetSeries(User current)
        {

            // extract all expense data for the user
            var temp = ExtractUserDashboardData.GetExpenseReport(current);


            // get the totals for each expense category  - current month
            var tempSeries = new SeriesCollection();
            for (int i = 0; i < temp.Item1.Item2.Count; i++)
            {
                tempSeries.Add
                    (new PieSeries
                    {
                        Title = temp.Item1.Item2[i].Item1,
                        Values = new ChartValues<double> { temp.Item1.Item2[i].Item2 },
                        DataLabels = true
                    });
            }
            CurrentSeries = tempSeries;


            // get the totals for each expense category - previous month
            tempSeries = new SeriesCollection();
            for (int i = 0; i < temp.Item2.Item2.Count; i++)
            {
                tempSeries.Add
                    (new PieSeries
                    {
                        Title = temp.Item2.Item2[i].Item1,
                        Values = new ChartValues<double> { temp.Item2.Item2[i].Item2 },
                        DataLabels = true
                    });
            }
            PreviousOneSeries = tempSeries;


            // get the totals for each expense category - (previous - 1) month
            tempSeries = new SeriesCollection();
            for (int i = 0; i < temp.Item3.Item2.Count; i++)
            {
                tempSeries.Add
                    (new PieSeries
                    {
                        Title = temp.Item3.Item2[i].Item1,
                        Values = new ChartValues<double> { temp.Item3.Item2[i].Item2 },
                        DataLabels = true
                    });
            }
            PreviousTwoSeries = tempSeries;


            // get the totals for each expense category - (previous - 2) month
            tempSeries = new SeriesCollection();
            for (int i = 0; i < temp.Item4.Item2.Count; i++)
            {
                tempSeries.Add
                    (new PieSeries
                    {
                        Title = temp.Item4.Item2[i].Item1,
                        Values = new ChartValues<double> { temp.Item4.Item2[i].Item2 },
                        DataLabels = true
                    });
            }
            PreviousThreeSeries = tempSeries;


            // get the overall average for each expense category
            tempSeries = new SeriesCollection();
            for (int i = 0; i < temp.Item5.Count; i++)
            {
                tempSeries.Add
                    (new PieSeries
                    {
                        Title = temp.Item5[i].Item1,
                        Values = new ChartValues<double> { temp.Item5[i].Item2 },
                        DataLabels = true
                    });
            }
            AverageSeries = tempSeries;


            // set the average expense-to-income ratio
            Average = (temp.Item6 / temp.Item7);

        }

        
        /// <summary>
        /// Constructor for the FinanceReportWindowVM
        /// </summary>
        /// <param name="current"></param>
        /// <param name="window"></param>
        public FinanceReportWindowVM(User current, Window1 window)
        {
            parent = window;
            cur = current;
            GetSeries(current);
        }


        #region Commands
        /// <summary>
        /// Defintion of ReturnDashboardCommand ICommand, related _returnDashboardCommand
        /// DelegateCommand, and ReturnDashboardClicked function.
        /// 
        /// ReturnDashboardClicked sets the parent's owner's visibility and enables the window,
        /// then closes the parent window.
        /// </summary>
        public ICommand ReturnDashboardCommand
        {
            get
            {
                if (_returnDashboardCommand == null)
                {
                    _returnDashboardCommand = new DelegateCommand(ReturnDashboardClicked);
                }
                return _returnDashboardCommand;
            }
        }
        DelegateCommand _returnDashboardCommand;
        private void ReturnDashboardClicked(object obj)
        {
            parent.Owner.Visibility = Visibility.Visible;
            parent.Owner.IsEnabled = true;

            parent.Close();
        }
        #endregion
    }
}
