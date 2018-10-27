using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PersonalFinanceProjectFinal.Utilities
{
    class SearchResults
    {
        
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
        /// Takes in many arguments. Essentially, it takes the input parameters, list of existing user expenses,
        /// new user expenses, and so far modified user expenses. 
        /// 
        /// Lists for each are determined at each step, and at the end, all of the same list types are intersected,
        /// and a tuple containing all applicable results from each passed list is returned
        /// </summary>
        /// <param name="existingExpenses"></param>
        /// <param name="newExpenses"></param>
        /// <param name="modifiedExpenses"></param>
        /// <param name="combo"></param>
        /// <param name="lowAmountRange"></param>
        /// <param name="highAmountRange"></param>
        /// <param name="lowDateRange"></param>
        /// <param name="highDateRange"></param>
        /// <returns></returns>
        public static Tuple<List<ExistingExpense>,List<NewExpense>,List<SearchResultRecord>> GetExpenseSearchResults(List<ExistingExpense> existingExpenses,
            List<NewExpense> newExpenses,List<SearchResultRecord> modifiedExpenses, ComboBox combo, TextBox lowAmountRange, TextBox highAmountRange,
            DatePicker lowDateRange, DatePicker highDateRange)
        {

            #region TempLists
            List<ExistingExpense> tempResultsWithOrWithoutCombo;
            List<ExistingExpense> tempResultsWithAmountRange;
            List<ExistingExpense> tempResultsWithDateRange;
            List<ExistingExpense> tempIntermediateResults;

            List<NewExpense> tempNewResultsWithOrWithoutCombo;
            List<NewExpense> tempNewResultsWithAmountRange;
            List<NewExpense> tempNewResultsWithDateRange;
            List<NewExpense> tempNewIntermediateResults;

            List<SearchResultRecord> tempModifiedResultsWithOrWithoutCombo;
            List<SearchResultRecord> tempModifiedResultsWithAmountRange;
            List<SearchResultRecord> tempModifiedResultsWithDateRange;
            List<SearchResultRecord> tempModifiedIntermediateResults;
            #endregion


            if (combo.Text.Equals("Please select a category..."))
            {
                tempResultsWithOrWithoutCombo = existingExpenses;
                tempNewResultsWithOrWithoutCombo = newExpenses;
                tempModifiedResultsWithOrWithoutCombo = modifiedExpenses;
            }
            else
            {
                tempResultsWithOrWithoutCombo = existingExpenses.Where(e => e.Category.Equals(combo.Text)).ToList();
                tempNewResultsWithOrWithoutCombo = newExpenses.Where(e => e.Category.Equals(combo.Text)).ToList();
                tempModifiedResultsWithOrWithoutCombo = modifiedExpenses.Where(e => e.Category.Equals(combo.Text)).ToList();
            }
            

            switch (WhatRangeDidUserSelect(lowAmountRange, highAmountRange))
            {
                case 3:
                    tempResultsWithAmountRange = existingExpenses.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempNewResultsWithAmountRange = newExpenses.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempModifiedResultsWithAmountRange = modifiedExpenses.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 2:
                    tempResultsWithAmountRange = existingExpenses.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempNewResultsWithAmountRange = newExpenses.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempModifiedResultsWithAmountRange = modifiedExpenses.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 1:
                    tempResultsWithAmountRange = existingExpenses.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    tempNewResultsWithAmountRange = newExpenses.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    tempModifiedResultsWithAmountRange = modifiedExpenses.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    break;

                default:
                    tempResultsWithAmountRange = tempResultsWithOrWithoutCombo;
                    tempNewResultsWithAmountRange = tempNewResultsWithOrWithoutCombo;
                    tempModifiedResultsWithAmountRange = tempModifiedResultsWithOrWithoutCombo;
                    break;
            }


            switch (WhatRangeDidUserSelect(lowDateRange, highDateRange))
            {
                case 3:
                    tempResultsWithDateRange = existingExpenses.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    tempNewResultsWithDateRange = newExpenses.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    tempModifiedResultsWithDateRange = modifiedExpenses.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 2:
                    tempResultsWithDateRange = existingExpenses.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    tempNewResultsWithDateRange = newExpenses.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    tempModifiedResultsWithDateRange = modifiedExpenses.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 1:
                    tempResultsWithDateRange = existingExpenses.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    tempNewResultsWithDateRange = newExpenses.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    tempModifiedResultsWithDateRange = modifiedExpenses.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    break;

                default:
                    tempResultsWithDateRange = tempResultsWithOrWithoutCombo;
                    tempNewResultsWithDateRange = tempNewResultsWithOrWithoutCombo;
                    tempModifiedResultsWithDateRange = tempModifiedResultsWithOrWithoutCombo;
                    break;
            }


            tempIntermediateResults = tempResultsWithOrWithoutCombo.Intersect(tempResultsWithAmountRange.Intersect(tempResultsWithDateRange)).ToList();
            tempNewIntermediateResults = tempNewResultsWithOrWithoutCombo.Intersect(tempNewResultsWithAmountRange.Intersect(tempNewResultsWithDateRange)).ToList();
            tempModifiedIntermediateResults = 
                tempModifiedResultsWithOrWithoutCombo.Intersect(tempModifiedResultsWithAmountRange.Intersect(tempModifiedResultsWithDateRange)).ToList();


            return new Tuple<List<ExistingExpense>, List<NewExpense>, List<SearchResultRecord>>
                (tempIntermediateResults, tempNewIntermediateResults, tempModifiedIntermediateResults);

        }

        /// <summary>
        /// Takes in many arguments. Essentially, it takes the input parameters, list of existing user income,
        /// new user income, and so far modified user income. 
        /// 
        /// Lists for each are determined at each step, and at the end, all of the same list types are intersected,
        /// and a tuple containing all applicable results from each passed list is returned
        /// </summary>
        /// <param name="existingIncome"></param>
        /// <param name="newIncome"></param>
        /// <param name="modifiedIncome"></param>
        /// <param name="combo"></param>
        /// <param name="lowAmountRange"></param>
        /// <param name="highAmountRange"></param>
        /// <param name="lowDateRange"></param>
        /// <param name="highDateRange"></param>
        /// <returns></returns>
        public static Tuple<List<ExistingIncome>, List<NewIncome>, List<SearchResultRecord>> GetIncomeSearchResults(List<ExistingIncome> existingIncome,
            List<NewIncome> newIncome, List<SearchResultRecord> modifiedIncome, ComboBox combo, TextBox lowAmountRange, TextBox highAmountRange,
            DatePicker lowDateRange, DatePicker highDateRange)
        {

            #region TempLists
            List<ExistingIncome> tempResultsWithOrWithoutCombo;
            List<ExistingIncome> tempResultsWithAmountRange;
            List<ExistingIncome> tempResultsWithDateRange;
            List<ExistingIncome> tempIntermediateResults;

            List<NewIncome> tempNewResultsWithOrWithoutCombo;
            List<NewIncome> tempNewResultsWithAmountRange;
            List<NewIncome> tempNewResultsWithDateRange;
            List<NewIncome> tempNewIntermediateResults;

            List<SearchResultRecord> tempModifiedResultsWithOrWithoutCombo;
            List<SearchResultRecord> tempModifiedResultsWithAmountRange;
            List<SearchResultRecord> tempModifiedResultsWithDateRange;
            List<SearchResultRecord> tempModifiedIntermediateResults;
            #endregion


            if (combo.Text.Equals("Please select a category..."))
            {
                tempResultsWithOrWithoutCombo = existingIncome;
                tempNewResultsWithOrWithoutCombo = newIncome;
                tempModifiedResultsWithOrWithoutCombo = modifiedIncome;
            }
            else
            {
                tempResultsWithOrWithoutCombo = existingIncome.Where(e => e.Category.Equals(combo.Text)).ToList();
                tempNewResultsWithOrWithoutCombo = newIncome.Where(e => e.Category.Equals(combo.Text)).ToList();
                tempModifiedResultsWithOrWithoutCombo = modifiedIncome.Where(e => e.Category.Equals(combo.Text)).ToList();
            }


            switch (WhatRangeDidUserSelect(lowAmountRange, highAmountRange))
            {
                case 3:
                    tempResultsWithAmountRange = existingIncome.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempNewResultsWithAmountRange = newIncome.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempModifiedResultsWithAmountRange = modifiedIncome.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)
                        && e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 2:
                    tempResultsWithAmountRange = existingIncome.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempNewResultsWithAmountRange = newIncome.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    tempModifiedResultsWithAmountRange = modifiedIncome.Where(e => e.Amount <= Double.Parse(highAmountRange.Text)).ToList();
                    break;

                case 1:
                    tempResultsWithAmountRange = existingIncome.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    tempNewResultsWithAmountRange = newIncome.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    tempModifiedResultsWithAmountRange = modifiedIncome.Where(e => e.Amount >= Double.Parse(lowAmountRange.Text)).ToList();
                    break;

                default:
                    tempResultsWithAmountRange = tempResultsWithOrWithoutCombo;
                    tempNewResultsWithAmountRange = tempNewResultsWithOrWithoutCombo;
                    tempModifiedResultsWithAmountRange = tempModifiedResultsWithOrWithoutCombo;
                    break;
            }


            switch (WhatRangeDidUserSelect(lowDateRange, highDateRange))
            {
                case 3:
                    tempResultsWithDateRange = existingIncome.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    tempNewResultsWithDateRange = newIncome.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    tempModifiedResultsWithDateRange = modifiedIncome.Where(e => e.Date >= lowDateRange.SelectedDate && e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 2:
                    tempResultsWithDateRange = existingIncome.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    tempNewResultsWithDateRange = newIncome.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    tempModifiedResultsWithDateRange = modifiedIncome.Where(e => e.Date <= highDateRange.SelectedDate).ToList();
                    break;

                case 1:
                    tempResultsWithDateRange = existingIncome.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    tempNewResultsWithDateRange = newIncome.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    tempModifiedResultsWithDateRange = modifiedIncome.Where(e => e.Date >= lowDateRange.SelectedDate).ToList();
                    break;

                default:
                    tempResultsWithDateRange = tempResultsWithOrWithoutCombo;
                    tempNewResultsWithDateRange = tempNewResultsWithOrWithoutCombo;
                    tempModifiedResultsWithDateRange = tempModifiedResultsWithOrWithoutCombo;
                    break;
            }


            tempIntermediateResults = tempResultsWithOrWithoutCombo.Intersect(tempResultsWithAmountRange.Intersect(tempResultsWithDateRange)).ToList();
            tempNewIntermediateResults = tempNewResultsWithOrWithoutCombo.Intersect(tempNewResultsWithAmountRange.Intersect(tempNewResultsWithDateRange)).ToList();
            tempModifiedIntermediateResults =
                tempModifiedResultsWithOrWithoutCombo.Intersect(tempModifiedResultsWithAmountRange.Intersect(tempModifiedResultsWithDateRange)).ToList();


            return new Tuple<List<ExistingIncome>, List<NewIncome>, List<SearchResultRecord>>
                (tempIntermediateResults, tempNewIntermediateResults, tempModifiedIntermediateResults);

        }

    }
}
