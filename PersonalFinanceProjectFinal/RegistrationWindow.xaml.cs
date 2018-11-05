using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        #region ValidEntiresBooleans
        private bool FirstNameValid = false;
        private bool LastNameValid = false;
        private bool EmailValid = false;
        private bool UserNameValid = false;
        private bool PasswordsValid = false;
        #endregion


        #region HasClickedBooleans
        bool hasClickedEmailField = false;
        bool hasClickedFirstNameField = false;
        bool hasClickedLastNameField = false;
        bool hasClickedUsernameField = false;
        #endregion


        #region StaticMessagesForFields
        private static string StaticFirstNameMessage = "First Name...";
        private static string StaticLastNameMessage = "Last Name...";
        private static string StaticEmailMessage = "Email...";
        private static string StaticUserNameMessage = "Desired Username...";
        #endregion


        #region GotFocusCalls
        private void txtEmail_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!hasClickedEmailField)
            {
                TextBox temp = (TextBox)sender;
                temp.Text = String.Empty;
                hasClickedEmailField = true;
            }
        }

        private void txtLastName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!hasClickedLastNameField)
            {
                TextBox temp = (TextBox)sender;
                temp.Text = String.Empty;
                hasClickedLastNameField = true;
            }
        }

        private void txtFirstName_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!hasClickedFirstNameField)
            {
                TextBox temp = (TextBox)sender;
                temp.Text = String.Empty;
                hasClickedFirstNameField = true;
            }
        }

        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!hasClickedUsernameField)
            {
                TextBox temp = (TextBox)sender;
                temp.Text = String.Empty;
                hasClickedUsernameField = true;
            }
        }
        #endregion


        #region LostFocusCalls
        private void txtFirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            var tempTextBox = (TextBox)sender;
            if (tempTextBox.Text.Equals(String.Empty))
            {
                tempTextBox.Text = StaticFirstNameMessage;
                tempTextBox.BorderThickness = new Thickness(1);
                tempTextBox.BorderBrush = Brushes.Red;
                hasClickedFirstNameField = false;
                FirstNameValid = false;
            }
            else
            {
                FirstNameValid = true;
                tempTextBox.BorderThickness = new Thickness(0);
            }
        }

        private void txtLastName_LostFocus(object sender, RoutedEventArgs e)
        {
            var tempTextBox = (TextBox)sender;
            if (tempTextBox.Text.Equals(String.Empty))
            {
                tempTextBox.Text = StaticLastNameMessage;
                tempTextBox.BorderThickness = new Thickness(1);
                tempTextBox.BorderBrush = Brushes.Red;
                hasClickedLastNameField = false;
                LastNameValid = false;
            }
            else
            {
                LastNameValid = true;
                tempTextBox.BorderThickness = new Thickness(0);
            }
        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {
            var tempTextBox = (TextBox)sender;
            if (tempTextBox.Text.Equals(String.Empty))
            {
                tempTextBox.Text = StaticEmailMessage;
                hasClickedEmailField = false;
                EmailValid = false;
            }
            else
            {
                if (Database.UserEmailAlreadyInUse(tempTextBox.Text))
                {
                    tempTextBox.BorderThickness = new Thickness(1);
                    tempTextBox.BorderBrush = Brushes.Red;
                    EmailValid = false;
                }
                else
                {
                    EmailValid = true;
                    tempTextBox.BorderThickness = new Thickness(0);
                }
            }
        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            var tempTextBox = (TextBox)sender;
            if (tempTextBox.Text.Equals(String.Empty))
            {
                tempTextBox.Text = StaticUserNameMessage;
                hasClickedUsernameField = false;
                UserNameValid = false;
            }
            else
            {
                if (Database.UserNameAlreadyExists(tempTextBox.Text))
                {
                    tempTextBox.BorderThickness = new Thickness(1);
                    tempTextBox.BorderBrush = Brushes.Red;
                    UserNameValid = false;
                }
                else
                {
                    UserNameValid = true;
                    tempTextBox.BorderThickness = new Thickness(0);
                }
            }
        }

        private void txtPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            var tempPassBox = (PasswordBox)sender;
            if (tempPassBox.SecurePassword.Equals(""))
            {
                tempPassBox.BorderBrush = Brushes.Red;
                tempPassBox.BorderThickness = new Thickness(1);
            }
        }

        private void txtConfirmPassword_LostFocus(object sender, RoutedEventArgs e)
        {
            var tempPassBox = (PasswordBox)sender;
            if (tempPassBox.SecurePassword.Equals(""))
            {
                tempPassBox.BorderBrush = Brushes.Red;
                tempPassBox.BorderThickness = new Thickness(1);
                PasswordsValid = false;
            }
            else
            {
                if (Security.SecureStringEqual(tempPassBox.SecurePassword, txtPassword.SecurePassword))
                {
                    PasswordsValid = true;
                    tempPassBox.BorderThickness = new Thickness(0);
                }
                else
                {
                    PasswordsValid = false;
                    tempPassBox.BorderBrush = Brushes.Red;
                    tempPassBox.BorderThickness = new Thickness(1);
                }
            }
        }
        #endregion


        #region ButtonClicks
        private void btnReturnLogin_Click(object sender, RoutedEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;
            Close();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (AllEntriesValid())
            {
                Database.InsertNewUser((Security.getHashSha256(txtUsername.Text + txtEmail.Text)),
                    txtUsername.Text, txtEmail.Text, txtFirstName.Text, txtLastName.Text,
                    Security.getHashSha256(txtConfirmPassword.SecurePassword.ToString()));
                this.Close();
                Owner.Visibility = Visibility.Visible;
                Owner.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Please make sure all inputs are valid!", "Missing Valid Entries");
            }
        }
        #endregion


        /// <summary>
        /// Default constructor for registration window
        /// </summary>
        public RegistrationWindow()
        {
            InitializeComponent();
            txtFirstName.Text = StaticFirstNameMessage;
            txtLastName.Text = StaticLastNameMessage;
            txtEmail.Text = StaticEmailMessage;
            txtUsername.Text = StaticUserNameMessage;
        }


        /// <summary>
        /// Window closed logic
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;
            Close();
        }


        /// <summary>
        /// Returns true if all input entries are valid
        /// </summary>
        /// <returns></returns>
        public bool AllEntriesValid()
        {
            return LastNameValid && FirstNameValid && EmailValid && UserNameValid && Security.SecureStringEqual(txtPassword.SecurePassword, txtConfirmPassword.SecurePassword);
        }


    }
}
