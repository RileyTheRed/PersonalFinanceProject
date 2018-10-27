using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace PersonalFinanceProjectFinal.Models
{
    public class Database
    {

        #region ConnectionStuff
        // static variables for the connection string and connection
        private static readonly string connectionString = "Data Source=DESKTOP-UGSKI06;" +
            "Initial Catalog=FinanceDatabase;Integrated Security=True;Pooling=False";
        private static SqlConnection con;


        /// <summary>
        /// Opens a connection to the database
        /// </summary>
        public static void GetConnection()
        {
            con = new SqlConnection(connectionString);
            con.Open();
        }


        /// <summary>
        /// Closes the connection to the database
        /// </summary>
        public static void CloseConneciton()
        {
            con.Close();
        }
        #endregion


        #region ValidationFunctions
        /// <summary>
        /// Returns true if the username is already in use
        /// </summary>
        /// <param name="uname"></param>
        /// <returns></returns>
        public static bool UserNameAlreadyExists(string uname)
        {

            // check and see if the uname is empty
            if (uname.Equals("") || uname.Equals(null))
            {
                return false;
            }

            // open a new connection
            GetConnection();
            bool exists = false;

            // query the database to see if any results come up for the uname and email, separately
            //using (SqlCommand cmd = new SqlCommand($"SELECT * FROM .UserTable WHERE userName = {uname}", con))
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM UserTable WHERE userName = '{uname}'", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        exists = true;
                }
            }

            CloseConneciton();
            return exists;
        }


        /// <summary>
        /// Returns true if the email is already in use
        /// </summary>
        /// <param name="uemail"></param>
        /// <returns></returns>
        public static bool UserEmailAlreadyInUse(string uemail)
        {
            // open a new connection
            GetConnection();
            bool exists = false;

            // query the database to see if any results come up for the uname and email, separately
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM UserTable WHERE userEmail = '{uemail}'", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        exists = true;
                }
            }

            CloseConneciton();
            return exists;
        }


        /// <summary>
        /// Returns true if the password provided matches the password associated with the username, if it exists, in the database
        /// </summary>
        /// <param name="username"></param>
        /// <param name="passHash"></param>
        /// <returns></returns>
        public static bool ValidateUser(string username, string passHash)
        {

            GetConnection();
            bool valid = false;

            // query the UserTable to see if anything is returned
            using(SqlCommand cmd = new SqlCommand($"SELECT * FROM UserTable WHERE userName = '{username}' AND userPassHash = '{passHash}'", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                        valid = true;
                }
            }

            CloseConneciton();
            return valid;
        }
        #endregion


        #region "Pull/Push Stuff"
        /// <summary>
        /// Inserts the new user information into the database
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="uname"></param>
        /// <param name="email"></param>
        /// <param name="fname"></param>
        /// <param name="lname"></param>
        /// <param name="passHash"></param>
        public static void InsertNewUser(string userID, string uname, string email, string fname, string lname, string passHash)
        {

            // open a new connection
            GetConnection();

            // query the database to see if any results come up for the uname and email, separately
            using (SqlCommand cmd = new SqlCommand($"INSERT INTO UserTable (" +
                $"userID, userName, userFirstName, userLastName, userEmail, userPassHash) values " +
                $"('{userID}','{uname}','{fname}','{lname}','{email}','{passHash}')", con))
            {
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            CloseConneciton();

        }

        /// <summary>
        /// Returns a User object for the associated username.
        /// 
        /// First, set the first name and user id
        /// Then pull any and all expense and income records associated with the user id
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static User GetUserData(string username)
        {
            List<ExistingExpense> expenses = new List<ExistingExpense>();
            List<ExistingIncome> income = new List<ExistingIncome>();
            string tempID = "";
            string tempName = "";

            GetConnection();

            // get the user id and first name
            using (SqlCommand cmd = new SqlCommand($"SELECT userID, userFirstName FROM UserTable WHERE userName = '{username}'", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tempID = reader.GetValue(0).ToString();
                        tempName = reader.GetValue(1).ToString();
                    }
                }
            }

            // get all existing expense records for this user
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM ExpenseTable WHERE userID = '{tempID}'", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        expenses.Add(new ExistingExpense(Convert.ToString(reader[1]), Convert.ToDouble(reader[2]),
                            Convert.ToDateTime(reader[3]), Convert.ToString(reader[4]).Trim(' '), Convert.ToString(reader[5]).Trim(' ')));
                    }
                }
            }
            
            // get all existing income records for this user
            using (SqlCommand cmd = new SqlCommand($"SELECT * FROM IncomeTable WHERE userID = '{tempID}'", con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        income.Add(new ExistingIncome(Convert.ToString(reader[1]), Convert.ToDouble(reader[2]),
                            Convert.ToDateTime(reader[3]), Convert.ToString(reader[4]).Trim(' '), Convert.ToString(reader[5]).Trim(' ')));
                    }
                }
            }

            return new User(tempID, tempName, expenses, income);

        }


        /// <summary>
        /// Updates all database tables with the new or edited information that the user has entered
        /// </summary>
        /// <param name="user"></param>
        public static void UpdateDatabase(User user)
        {

            GetConnection();

            #region HashIDLists
            List<string> existingExpenseToUpdate;
            List<string> existingExpenseToDelete;

            List<string> existingIncomeToUpdate;
            List<string> existingIncomeToDelete;
            #endregion


            #region PopulateHashListsWithAppropriateValues
            existingExpenseToUpdate = user.ModifiedExpenseRecords.Where(x => x.Status.Equals("++")).Select(x => x.Hash).ToList();
            existingExpenseToDelete = user.ModifiedExpenseRecords.Where(x => x.Status.Equals("--")).Select(x => x.Hash).ToList();

            existingIncomeToUpdate = user.ModifiedIncomeRecords.Where(x => x.Status.Equals("++")).Select(x => x.Hash).ToList();
            existingIncomeToDelete = user.ModifiedIncomeRecords.Where(x => x.Status.Equals("--")).Select(x => x.Hash).ToList();
            #endregion


            #region InsertingAndUpdatingAndDeleting
            /// for each of the existing expense records, determine which hash ids are in the list of
            /// hashes for expense records to update. if they are, then update them with the new values stored
            /// in the corresponding user list of modified expense records
            foreach (ExistingExpense item in user.ExistingUserExpenses)
            {
                if (existingExpenseToUpdate.Contains(item.Hash))
                {
                    var temp = user.ModifiedExpenseRecords.First(x => x.Hash.Equals(item.Hash)); // always exists
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE ExpenseTable SET amount = @amt, date = @dte, category = @cat, description = @desc " +
                            "WHERE expenseID = @id";
                        cmd.Parameters.AddWithValue("@amt", temp.Amount);
                        cmd.Parameters.AddWithValue("@dte", temp.Date);
                        cmd.Parameters.AddWithValue("@cat", temp.Category);
                        cmd.Parameters.AddWithValue("@desc", temp.Description);
                        cmd.Parameters.AddWithValue("@id", temp.Hash);

                        cmd.ExecuteNonQuery();
                    }
                }
            }


            /// for each of the existing income records, determine which hash ids are in the list of hashes
            /// for the income records to update. if they are, then update them with the new values stored
            /// in the corresponding user list of modified income records
            foreach (ExistingIncome item in user.ExistingUserIncome)
            {
                if (existingIncomeToUpdate.Contains(item.Hash))
                {
                    var temp = user.ModifiedIncomeRecords.First(x => x.Hash.Equals(item.Hash));
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "UPDATE IncomeTable SET amount = @amt, date = @dte, category = @cat, description = @desc " +
                            "WHERE incomeID = @id";
                        cmd.Parameters.AddWithValue("@amt", temp.Amount);
                        cmd.Parameters.AddWithValue("@dte", temp.Date);
                        cmd.Parameters.AddWithValue("@cat", temp.Category);
                        cmd.Parameters.AddWithValue("@desc", temp.Description);
                        cmd.Parameters.AddWithValue("@id", temp.Hash);

                        cmd.ExecuteNonQuery();
                    }
                }
            }


            /// check to see if each of the new expense records had been updated at some point
            /// by the user. if the hash id exists in the list of new expense records to update,
            /// then insert the record informtion stored in the users list of modified income records.
            /// otherwise, insert the record from the NewUserExpense list
            foreach (NewExpense expense in user.NewUserExpenses)
            {
                if (existingExpenseToUpdate.Contains(expense.Hash)) // user modified it at some point
                {
                    var temp = user.ModifiedExpenseRecords.First(x => x.Hash.Equals(expense.Hash));
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO ExpenseTable (userID, expenseID, amount, date, category, description) " +
                            "VALUES (@userid, @id, @amt, @dte, @cat, @desc)";
                        cmd.Parameters.AddWithValue("@amt", temp.Amount);
                        cmd.Parameters.AddWithValue("@dte", temp.Date);
                        cmd.Parameters.AddWithValue("@cat", temp.Category.Trim(' '));
                        cmd.Parameters.AddWithValue("@desc", temp.Description.Trim(' '));
                        cmd.Parameters.AddWithValue("@id", temp.Hash);
                        cmd.Parameters.AddWithValue("@userid", user.UserID);

                        cmd.ExecuteNonQuery();
                    }
                }
                else // user didnt modify it after creation
                {
                    using (SqlCommand cmd = new SqlCommand($"INSERT INTO ExpenseTable (" +
                        $"userID,expenseID,amount,date,category,description) values (" +
                        $"'{expense.OwnerID}','{expense.Hash}','{expense.Amount}','{expense.Date}'" +
                        $",'{expense.Category.Trim(' ')}','{expense.Description.Trim(' ')}')", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }


            /// check to see the exact same as the foreach block above, but instead this is for income records
            /// (see the above comment for the description)
            foreach (NewIncome income in user.NewUserIncome)
            {
                if (existingIncomeToUpdate.Contains(income.Hash))
                {
                    var temp = user.ModifiedIncomeRecords.First(x => x.Hash.Equals(income.Hash));
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = con;
                        cmd.CommandText = "INSERT INTO IncomeTable (userID, incomeID, amount, date, category, description) " +
                            "VALUES (@userid, @id, @amt, @dte, @cat, @desc)";
                        cmd.Parameters.AddWithValue("@amt", temp.Amount);
                        cmd.Parameters.AddWithValue("@dte", temp.Date);
                        cmd.Parameters.AddWithValue("@cat", temp.Category.Trim(' '));
                        cmd.Parameters.AddWithValue("@desc", temp.Description.Trim(' '));
                        cmd.Parameters.AddWithValue("@id", temp.Hash);
                        cmd.Parameters.AddWithValue("@userid", user.UserID);

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    using (SqlCommand cmd = new SqlCommand($"INSERT INTO IncomeTable (" +
                        $"userID,incomeID,amount,date,category,description) values (" +
                        $"'{income.OwnerID}','{income.Hash}','{income.Amount}','{income.Date}'" +
                        $",'{income.Category.Trim(' ')}','{income.Description.Trim(' ')}')", con))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
            }


            /// for each of the hash ids for the records the user marked for deletion, do just that
            foreach (string item in existingExpenseToDelete)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM ExpenseTable WHERE expenseID = @id";
                    cmd.Parameters.AddWithValue("@id", item);
                    cmd.ExecuteNonQuery();
                }
            }


            /// for each of the hash ids for the records the user marked for deletion, do just that
            foreach (string item in existingIncomeToDelete)
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandText = "DELETE FROM IncomeTable WHERE incomeID = @id";
                    cmd.Parameters.AddWithValue("@id", item);
                    cmd.ExecuteNonQuery();
                }
            }
            #endregion

            CloseConneciton();

        }
        #endregion

    }
}
