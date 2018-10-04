﻿using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for UserDashboard.xaml
    /// </summary>
    public partial class UserDashboard : Window
    {

        User currentUser;

        public UserDashboard(string uname)
        {
            InitializeComponent();
            currentUser = Database.GetUserData(uname);
            lblGreeting.Content = $"Welcome back, {currentUser.FirstName}";
        }
    }
}