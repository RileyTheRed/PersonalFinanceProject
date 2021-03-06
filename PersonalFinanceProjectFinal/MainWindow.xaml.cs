﻿using System.Windows;
using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Window childWindow; // for which ever child window the user spawns (dashboard or registration)


        public MainWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Registration button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Window regWindow = new RegistrationWindow();
            regWindow.Visibility = Visibility.Visible;
            Visibility = Visibility.Hidden;
            IsEnabled = false;
            regWindow.Owner = this;
        }


        /// <summary>
        /// Login button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Database.ValidateUser(txtUserName.Text, Security.getPassHashSha256(txtPassword.SecurePassword)))
            {
                MessageBox.Show("Valid User!");
                childWindow = new UserDashboard(txtUserName.Text);
                childWindow.Owner = this;

                Visibility = Visibility.Hidden;
                IsEnabled = false;

                childWindow.Visibility = Visibility.Visible;
                childWindow.IsEnabled = true;

                txtPassword.Clear();
                txtUserName.Clear();
            }
            else
            {
                MessageBox.Show("Invalid User!");
            }
        }

    }
}
