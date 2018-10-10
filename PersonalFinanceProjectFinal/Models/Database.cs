using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace PersonalFinanceProjectFinal.Models
{
    partial class Database
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
                        expenses.Add(new ExistingExpense(reader.GetValue(1).ToString(), (double)reader.GetValue(2),
                            reader.GetDateTime(3), reader.GetValue(4).ToString(), reader.GetValue(5).ToString()));
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
                        income.Add(new ExistingIncome(reader.GetValue(1).ToString(), (double)reader.GetValue(2),
                            reader.GetDateTime(3), reader.GetValue(4).ToString(), reader.GetValue(5).ToString()));
                    }
                }
            }

            return new User(tempID, tempName, expenses, income);

        }


        
        #endregion

    }
}
