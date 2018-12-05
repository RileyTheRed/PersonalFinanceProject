using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinanceProjectFinal.Models;

namespace PersonalFinanceProjectFinal.Utilities
{

    class ExtractUserDashboardData
    {

        public static Tuple
            <Tuple<string, double>, 
            Tuple<string, double>, 
            Tuple<string, double>> 
            GetTopThreeExpenseCategories
            (List<ExistingExpense> existing, 
            List<NewExpense> newer, 
            List<SearchResultRecord> modified,
            bool currentMonth)
        {

            string first = "N/A";
            string second = "N/A";
            string third = "N/A";
            Dictionary<string, double> categoriesAndCost = new Dictionary<string, double>();

            double totalCost = 0;

            // if we are looking for the top three for the current month
            if (currentMonth)
            {

                List<string> modifiedHashes = modified.Select(x => x.Hash).ToList(); // get all the hashes of records that have been modified
                foreach (string cat in Categories.GetExpenseCategories())
                {

                    categoriesAndCost.Add(cat, 0.0);

                    // start off with exising expense records
                    foreach (ExistingExpense item in existing)
                    {

                        // only do stuff if the item category is the same as the type being looked at and the month is the current month and current year
                        if (item.Category.Equals(cat) && item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        {

                            // record hasnt been modified
                            if (!modifiedHashes.Contains(item.Hash))
                            {
                                categoriesAndCost[cat] += item.Amount;
                            }

                            // record has been modified
                            else
                            {
                                int indexOfModified = modified.FindIndex(x => x.Hash.Equals(item.Hash));

                                // check and make sure the record isnt marked for deletion and the month and year match the current month and year
                                if (!modified[indexOfModified].Status.Equals("--") &&
                                    modified[indexOfModified].Date.Month == DateTime.Now.Month &&
                                    modified[indexOfModified].Date.Year == DateTime.Now.Year)
                                {
                                    categoriesAndCost[cat] += modified[indexOfModified].Amount;
                                }
                            }
                        }
                    }


                    // then look at new expense records
                    foreach (NewExpense item in newer)
                    {

                        // only do stuff if the item category is the same as the type being looked at and the month is the current month and current year
                        if (item.Category.Equals(cat) && item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        {

                            // record hasnt been modified
                            if (!modifiedHashes.Contains(item.Hash))
                            {
                                categoriesAndCost[cat] += item.Amount;
                            }

                            // record has been modified
                            else
                            {
                                int indexOfModified = modified.FindIndex(x => x.Hash.Equals(item.Hash));

                                // check and make sure the record isnt marked for deletion and the month and year match the current month and year
                                if (!modified[indexOfModified].Status.Equals("--") &&
                                    modified[indexOfModified].Date.Month == DateTime.Now.Month &&
                                    modified[indexOfModified].Date.Year == DateTime.Now.Year)
                                {
                                    categoriesAndCost[cat] += modified[indexOfModified].Amount;
                                }
                            }
                        }
                    }

                }
            }

            // if we are looking for the top three for the last month
            else
            {

                List<string> modifiedHashes = modified.Select(x => x.Hash).ToList(); // get all the hashes of records that have been modified
                foreach (string cat in Categories.GetExpenseCategories())
                {

                    categoriesAndCost.Add(cat, 0.0);

                    // start off with exising expense records
                    foreach (ExistingExpense item in existing)
                    {

                        // only do stuff if the item category is the same as the type being looked at and the month is the current month and current year
                        if (item.Category.Equals(cat) && item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        {

                            // record hasnt been modified
                            if (!modifiedHashes.Contains(item.Hash))
                            {
                                categoriesAndCost[cat] += item.Amount;
                            }

                            // record has been modified
                            else
                            {
                                int indexOfModified = modified.FindIndex(x => x.Hash.Equals(item.Hash));

                                // check and make sure the record isnt marked for deletion and the month and year match the current month and year
                                if (!modified[indexOfModified].Status.Equals("--") &&
                                    modified[indexOfModified].Date.Month == DateTime.Now.Month - 1 &&
                                    modified[indexOfModified].Date.Year == DateTime.Now.Year)
                                {
                                    categoriesAndCost[cat] += modified[indexOfModified].Amount;
                                }
                            }
                        }
                    }


                    // then look at new expense records
                    foreach (NewExpense item in newer)
                    {

                        // only do stuff if the item category is the same as the type being looked at and the month is the current month and current year
                        if (item.Category.Equals(cat) && item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        {

                            // record hasnt been modified
                            if (!modifiedHashes.Contains(item.Hash))
                            {
                                categoriesAndCost[cat] += item.Amount;
                            }

                            // record has been modified
                            else
                            {
                                int indexOfModified = modified.FindIndex(x => x.Hash.Equals(item.Hash));

                                // check and make sure the record isnt marked for deletion and the month and year match the current month and year
                                if (!modified[indexOfModified].Status.Equals("--") &&
                                    modified[indexOfModified].Date.Month == DateTime.Now.Month - 1 &&
                                    modified[indexOfModified].Date.Year == DateTime.Now.Year)
                                {
                                    categoriesAndCost[cat] += modified[indexOfModified].Amount;
                                }
                            }
                        }
                    }

                }
            }


            List<string> keysWithValues = new List<string>();
            foreach (string key in categoriesAndCost.Keys)
            {
                if (categoriesAndCost[key] > 0.0)
                    keysWithValues.Add(key);
                totalCost += categoriesAndCost[key];
            }

            if (keysWithValues.Count > 0)
            {
                first = keysWithValues[0];
                for (int i = 1; i < keysWithValues.Count; i++)
                {
                    if (categoriesAndCost[keysWithValues[i]] > categoriesAndCost[first])
                    {
                        third = second;
                        second = first;
                        first = keysWithValues[i];
                    }
                    else if (!second.Equals("N/A") && categoriesAndCost[keysWithValues[i]] > categoriesAndCost[second])
                    {
                        third = second;
                        second = keysWithValues[i];
                    }
                    else if (second.Equals("N/A"))
                    {
                        second = keysWithValues[i];
                    }
                    else if (!third.Equals("N/A") && categoriesAndCost[keysWithValues[i]] > categoriesAndCost[third])
                    {
                        third = keysWithValues[i];
                    }
                    else if (third.Equals("N/A"))
                    {
                        third = keysWithValues[i];
                    }
                }
            }

            Tuple<string, double> firstValues;
            Tuple<string, double> secondValues;
            Tuple<string, double> thirdValues;


            // compile the number 1 category and its ratio to overall cost for the specified month
            if (!first.Equals("N/A"))
                firstValues = new Tuple<string, double>(first, categoriesAndCost[first] / totalCost);
            else
                firstValues = new Tuple<string, double>(first, 0.0);

            // compile the number 2 category and its ratio to overall cost for the specified month
            if (!second.Equals("N/A"))
                secondValues = new Tuple<string, double>(second, categoriesAndCost[second] / totalCost);
            else
                secondValues = new Tuple<string, double>(second, 0.0);

            // compile the number 3 category and its ratio to overall cost for the specified month
            if (!third.Equals("N/A"))
                thirdValues = new Tuple<string, double>(third, categoriesAndCost[third] / totalCost);
            else
                thirdValues = new Tuple<string, double>(third, 0.0);


            return new Tuple<Tuple<string, double>, Tuple<string, double>, Tuple<string, double>>(firstValues, secondValues, thirdValues);
        }



        public static Tuple<double, double> GetCurrentAndLastMonthRatios
            (
                List<ExistingExpense> existingExpenses, 
                List<NewExpense> newExpenses, 
                List<SearchResultRecord> modifiedExpenses,
                List<ExistingIncome> existingIncomes, 
                List<NewIncome> newIncomes, 
                List<SearchResultRecord> modifiedIncomes
            )
        {

            #region ExpenseCalculations
            List<string> modifiedExpenseHashes = modifiedExpenses.Select(x => x.Hash).ToList();
            double totalCurrentMonthExpenses = 0.0;
            double totalLastMonthExpenses = 0.0;

            foreach (ExistingExpense item in existingExpenses)
            {
                if (!modifiedExpenseHashes.Contains(item.Hash))
                {
                    if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        totalCurrentMonthExpenses += item.Amount;
                    else if (item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        totalLastMonthExpenses += item.Amount;
                }
            }

            foreach (NewExpense item in newExpenses)
            {
                if (!modifiedExpenseHashes.Contains(item.Hash))
                {
                    if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        totalCurrentMonthExpenses += item.Amount;
                    else if (item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        totalLastMonthExpenses += item.Amount;
                }
            }

            foreach (SearchResultRecord item in modifiedExpenses)
            {
                if (!item.Status.Equals("--"))
                {
                    if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        totalCurrentMonthExpenses += item.Amount;
                    else if (item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        totalLastMonthExpenses += item.Amount;
                }
            }
            #endregion


            #region IncomeCalculations
            List<string> modifiedIncomeHashes = modifiedIncomes.Select(x => x.Hash).ToList();
            double totalCurrentMonthIncomes = 0.0;
            double totalLastMonthIncomes = 0.0;

            foreach (ExistingIncome item in existingIncomes)
            {
                if (!modifiedIncomeHashes.Contains(item.Hash))
                {
                    if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        totalCurrentMonthIncomes += item.Amount;
                    else if (item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        totalLastMonthIncomes += item.Amount;
                }
            }

            foreach (NewIncome item in newIncomes)
            {
                if (!modifiedExpenseHashes.Contains(item.Hash))
                {
                    if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        totalCurrentMonthIncomes += item.Amount;
                    else if (item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        totalLastMonthIncomes += item.Amount;
                }
            }

            foreach (SearchResultRecord item in modifiedIncomes)
            {
                if (!item.Status.Equals("--"))
                {
                    if (item.Date.Month == DateTime.Now.Month && item.Date.Year == DateTime.Now.Year)
                        totalCurrentMonthIncomes += item.Amount;
                    else if (item.Date.Month == DateTime.Now.Month - 1 && item.Date.Year == DateTime.Now.Year)
                        totalLastMonthIncomes += item.Amount;
                }
            }
            #endregion


            return new Tuple<double, double>(totalCurrentMonthExpenses / totalLastMonthExpenses, totalCurrentMonthIncomes / totalLastMonthIncomes);
        }



        public static Tuple
            <Tuple<string, List<Tuple<string, double>>>,
            Tuple<string, List<Tuple<string, double>>>,
            Tuple<string, List<Tuple<string, double>>>, 
            Tuple<string, List<Tuple<string, double>>>, 
            List<Tuple<string, double>>>
            GetExpenseReport(User user)
        {


            // reusable variables to be used for calculating the different monthss reports
            List<ExistingExpense> tempExistingExpense;
            List<NewExpense> tempNewExpense;
            List<SearchResultRecord> tempModifiedExpense;
            List<string> tempModifiedHashes;


            // calculate the current months expense report
            tempModifiedExpense =
                user.ModifiedExpenseRecords
                .Where(e => e.Date.Month.Equals(DateTime.Now.Month) && e.Date.Year.Equals(DateTime.Now.Year) && !e.Status.Equals("--"))
                .ToList();

            tempModifiedHashes =
                tempModifiedExpense
                .Select(e => e.Hash)
                .ToList();

            tempExistingExpense =
                user.ExistingUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.Month) && e.Date.Year.Equals(DateTime.Now.Year))
                .ToList();

            tempNewExpense =
                user.NewUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.Month) && e.Date.Year.Equals(DateTime.Now.Year))
                .ToList();

            List<Tuple<string, double>> current = new List<Tuple<string, double>>();
            foreach (string category in Categories.GetExpenseCategories())
            {
                double total = 0;
                total += tempModifiedExpense.Where(e => e.Category.Equals(category)).Select(e => e.Amount).Sum();
                total += tempExistingExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();
                total += tempNewExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();

                current.Add(new Tuple<string, double>(category, total));
            }



            // calculate the previous months expense report
            tempModifiedExpense =
                user.ModifiedExpenseRecords
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-1).Month) && 
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)) && !e.Status.Equals("--"))
                .ToList();

            tempModifiedHashes =
                tempModifiedExpense
                .Select(e => e.Hash)
                .ToList();

            tempExistingExpense =
                user.ExistingUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-1).Month) &&
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)))
                .ToList();

            tempNewExpense =
                user.NewUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-1).Month) &&
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)))
                .ToList();

            List<Tuple<string, double>> previous_1 = new List<Tuple<string, double>>();
            foreach (string category in Categories.GetExpenseCategories())
            {
                double total = 0;
                total += tempModifiedExpense.Where(e => e.Category.Equals(category)).Select(e => e.Amount).Sum();
                total += tempExistingExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();
                total += tempNewExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();

                previous_1.Add(new Tuple<string, double>(category, total));
            }



            // calculate the previous-1 months expense report
            tempModifiedExpense =
                user.ModifiedExpenseRecords
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-2).Month) && 
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)) && !e.Status.Equals("--"))
                .ToList();

            tempModifiedHashes =
                tempModifiedExpense
                .Select(e => e.Hash)
                .ToList();

            tempExistingExpense =
                user.ExistingUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-2).Month) && 
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)))
                .ToList();

            tempNewExpense =
                user.NewUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-2).Month) &&
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)))
                .ToList();

            List<Tuple<string, double>> previous_2 = new List<Tuple<string, double>>();
            foreach (string category in Categories.GetExpenseCategories())
            {
                double total = 0;
                total += tempModifiedExpense.Where(e => e.Category.Equals(category)).Select(e => e.Amount).Sum();
                total += tempExistingExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();
                total += tempNewExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();

                previous_2.Add(new Tuple<string, double>(category, total));
            }



            // calculate the previous-2 months expense report
            tempModifiedExpense =
                user.ModifiedExpenseRecords
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-3).Month) &&
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)) && !e.Status.Equals("--"))
                .ToList();

            tempModifiedHashes =
                tempModifiedExpense
                .Select(e => e.Hash)
                .ToList();

            tempExistingExpense =
                user.ExistingUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-3).Month) &&
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)))
                .ToList();

            tempNewExpense =
                user.NewUserExpenses
                .Where(e => e.Date.Month.Equals(DateTime.Now.AddMonths(-3).Month) &&
                    (e.Date.Year.Equals(DateTime.Now.AddYears(-1).Year) || e.Date.Year.Equals(DateTime.Now.Year)))
                .ToList();

            List<Tuple<string, double>> previous_3 = new List<Tuple<string, double>>();
            foreach (string category in Categories.GetExpenseCategories())
            {
                double total = 0;
                total += tempModifiedExpense.Where(e => e.Category.Equals(category)).Select(e => e.Amount).Sum();
                total += tempExistingExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();
                total += tempNewExpense.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => r.Equals(e.Hash))).Select(e => e.Amount).Sum();

                previous_3.Add(new Tuple<string, double>(category, total));
            }



            // find the first month and year that records began
            DateTime first = DateTime.Now;
            foreach (ExistingExpense item in user.ExistingUserExpenses)
            {
                if (item.Date < first)
                    first = item.Date;
            }
            foreach (NewExpense item in user.NewUserExpenses)
            {
                if (item.Date < first)
                    first = item.Date;
            }
            foreach (SearchResultRecord item in user.ModifiedExpenseRecords)
            {
                if (item.Date < first && !item.Status.Equals("--"))
                    first = item.Date;
            }

            // how many months has there been since record keeping began?
            int months = ((DateTime.Now.Year - first.Year) * 12) + (DateTime.Now.Month - first.Month);


            //compute the average of each expense category since record keeping began
            List<Tuple<string, double>> average = new List<Tuple<string, double>>();

            tempModifiedHashes =
                user.ModifiedExpenseRecords.Where(e => !e.Status.Equals("--")).Select(e => e.Hash).ToList();

            foreach (string category in Categories.GetExpenseCategories())
            {
                double total = 0;
                total += user.ExistingUserExpenses.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => e.Hash.Equals(r))).Select(e => e.Amount).Sum();
                total += user.NewUserExpenses.Where(e => e.Category.Equals(category) && !tempModifiedHashes.Exists(r => e.Hash.Equals(r))).Select(e => e.Amount).Sum();
                total += user.ModifiedExpenseRecords.Where(e => e.Category.Equals(category) && tempModifiedHashes.Exists(r => e.Hash.Equals(r))).Select(e => e.Amount).Sum();
                average.Add(new Tuple<string, double>(category, (total / months)));
            }


            return new Tuple
                <Tuple<string, List<Tuple<string, double>>>,
                Tuple<string, List<Tuple<string, double>>>,
                Tuple<string, List<Tuple<string, double>>>,
                Tuple<string, List<Tuple<string, double>>>,
                List<Tuple<string, double>>>
                (new Tuple<string, List<Tuple<string, double>>>("Current", current),
                new Tuple<string, List<Tuple<string, double>>>(DateTime.Now.AddMonths(-1).Month.ToString(), previous_1),
                new Tuple<string, List<Tuple<string, double>>>(DateTime.Now.AddMonths(-2).Month.ToString(), previous_2),
                new Tuple<string, List<Tuple<string, double>>>(DateTime.Now.AddMonths(-3).Month.ToString(), previous_3),
                average);
        }

    }
}