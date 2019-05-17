using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonalFinanceProjectFinal.Utilities
{
    class ResultsUtility
    {

        public static ObservableCollection<IncomeRecord> GetIncomeResults(List<IncomeRecord> incomeRecords)
        {
            ObservableCollection<IncomeRecord> tempRecords = new ObservableCollection<IncomeRecord>();
            foreach(IncomeRecord item in incomeRecords)
            {
                tempRecords.Add(item);
            }
            tempRecords.OrderBy(e => e.Date);
            return tempRecords;
        }


        public static ObservableCollection<ExpenseRecord> GetExpenseResults(List<ExpenseRecord> expenseRecords)
        {
            ObservableCollection<ExpenseRecord> tempRecords = new ObservableCollection<ExpenseRecord>();
            foreach(ExpenseRecord item in expenseRecords)
            {
                tempRecords.Add(item);
            }
            tempRecords.OrderBy(e => e.Date);
            return tempRecords;
        }


        /// <summary>
        /// Determines if the user has entered values for either of the range-related Controls.
        /// 
        /// Return 0 - User did not enter values in either control
        /// Return 1 - User only entered a value in the lower-range Control
        /// Return 2 - User only entered a value in the higher-range Control
        /// Return 3 - User entered values in both Controls
        /// 
        /// </summary>
        /// <param name="lowerRange"></param>
        /// <param name="higherRange"></param>
        /// <returns></returns>
        private static int WhatRangeDidUserSelect(Control lowerRange, Control higherRange)
        {
            int low = 0;    // 1 = has value, 0 = no value
            int high = 0;   // 1 = has value, 0 = no value

            if (lowerRange is TextBox && higherRange is TextBox) // controls are TextBoxes
            {
                var tempLow = lowerRange as TextBox;
                var tempHigh = higherRange as TextBox;

                low = tempLow.Text.Equals("MIN") ? 0 : 1;
                high = tempHigh.Text.Equals("MAX") ? 0 : 1;
            }
            else if (lowerRange is DatePicker && higherRange is DatePicker) //  controls are DatePickers
            {
                var tempLow = lowerRange as DatePicker;
                var tempHigh = higherRange as DatePicker;

                low = tempLow.SelectedDate.HasValue ? 1 : 0;
                high = tempHigh.SelectedDate.HasValue ? 1 : 0;
            }

            return (low == 1 && high == 1) ? 3 : (low == 0 && high == 0 ? 0 : (low == 1 ? 1 : 2));

        }


        /// <summary>
        /// Takes in specified input parameters that the user selected, list of user expense records.
        /// 
        /// Taking into account the options that the user selected, for each of the options a list of
        /// records is created that adhere to the input parameters. At the end all the list are intersected
        /// and the result is returned.
        /// </summary>
        /// <param name="expenseRecords"></param>
        /// <param name="combo"></param>
        /// <param name="lowAmountRange"></param>
        /// <param name="highAmountRange"></param>
        /// <param name="lowDateRange"></param>
        /// <param name="highDateRange"></param>
        /// <returns></returns>
        public static List<ExpenseRecord> GetExpenseSearchResults(List<ExpenseRecord> expenseRecords,
            ComboBox combo, TextBox lowAmountRange, TextBox highAmountRange, DatePicker lowDateRange,
            DatePicker highDateRange)
        {

            List<ExpenseRecord> tempResultsWithOrWithoutCombo;
            List<ExpenseRecord> tempResultsWithAmountRange;
            List<ExpenseRecord> tempResultsWithDateRange;
            List<ExpenseRecord> tempIntermediateResults;


            if (combo.Text.Equals("Please select a category..."))
                tempResultsWithOrWithoutCombo = expenseRecords;
            else
                tempResultsWithOrWithoutCombo = expenseRecords.Where(e => e.Category.Equals(combo.Text)).ToList();


            switch (WhatRangeDidUserSelect(lowAmountRange, highAmountRange))
            {
                case 3:
                    tempResultsWithAmountRange = expenseRecords.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 2:
                    tempResultsWithAmountRange = expenseRecords.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 1:
                    tempResultsWithAmountRange = expenseRecords.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    break;

                default:
                    tempResultsWithAmountRange = tempResultsWithOrWithoutCombo;
                    break;

            }


            switch (WhatRangeDidUserSelect(lowDateRange, highDateRange))
            {
                case 3:
                    tempResultsWithDateRange = expenseRecords.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 2:
                    tempResultsWithDateRange = expenseRecords.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 1:
                    tempResultsWithDateRange = expenseRecords.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    break;

                default:
                    tempResultsWithDateRange = tempResultsWithOrWithoutCombo;
                    break;
            }


            tempIntermediateResults = tempResultsWithOrWithoutCombo.Intersect(tempResultsWithAmountRange.Intersect(tempResultsWithDateRange)).ToList();


            return tempIntermediateResults;
        }


        /// <summary>
        /// Takes in specified input parameters that the user selected, list of user income records.
        /// 
        /// Taking into account the options that the user selected, for each of the options a list of
        /// records is created that adhere to the input parameters. At the end all the list are intersected
        /// and the result is returned.
        /// </summary>
        /// <param name="incomeRecords"></param>
        /// <param name="combo"></param>
        /// <param name="lowAmountRange"></param>
        /// <param name="highAmountRange"></param>
        /// <param name="lowDateRange"></param>
        /// <param name="highDateRange"></param>
        /// <returns></returns>
        public static List<ExpenseRecord> GetIncomeSearchResults(List<ExpenseRecord> incomeRecords,
            ComboBox combo, TextBox lowAmountRange, TextBox highAmountRange, DatePicker lowDateRange,
            DatePicker highDateRange)
        {

            List<ExpenseRecord> tempResultsWithOrWithoutCombo;
            List<ExpenseRecord> tempResultsWithAmountRange;
            List<ExpenseRecord> tempResultsWithDateRange;
            List<ExpenseRecord> tempIntermediateResults;


            if (combo.Text.Equals("Please select a category..."))
                tempResultsWithOrWithoutCombo = incomeRecords;
            else
                tempResultsWithOrWithoutCombo = incomeRecords.Where(e => e.Category.Equals(combo.Text)).ToList();


            switch (WhatRangeDidUserSelect(lowAmountRange, highAmountRange))
            {
                case 3:
                    tempResultsWithAmountRange = incomeRecords.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 2:
                    tempResultsWithAmountRange = incomeRecords.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 1:
                    tempResultsWithAmountRange = incomeRecords.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    break;

                default:
                    tempResultsWithAmountRange = tempResultsWithOrWithoutCombo;
                    break;

            }


            switch (WhatRangeDidUserSelect(lowDateRange, highDateRange))
            {
                case 3:
                    tempResultsWithDateRange = incomeRecords.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 2:
                    tempResultsWithDateRange = incomeRecords.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 1:
                    tempResultsWithDateRange = incomeRecords.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    break;

                default:
                    tempResultsWithDateRange = tempResultsWithOrWithoutCombo;
                    break;
            }


            tempIntermediateResults = tempResultsWithOrWithoutCombo.Intersect(tempResultsWithAmountRange.Intersect(tempResultsWithDateRange)).ToList();


            return tempIntermediateResults;
        }
    }
}
