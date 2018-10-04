using System.Windows;
using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.Utilities;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Window childWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            Window regWindow = new RegistrationWindow();
            regWindow.Visibility = Visibility.Visible;
            Visibility = Visibility.Hidden;
            IsEnabled = false;
            regWindow.Owner = this;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (Database.ValidateUser(txtUserName.Text, Security.getHashSha256(txtPassword.SecurePassword.ToString())))
            {
                MessageBox.Show("Valid User!");
                childWindow = new UserDashboard(txtUserName.Text);
                Visibility = Visibility.Hidden;
                IsEnabled = false;

                childWindow.Visibility = Visibility.Visible;
                childWindow.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Invalid User!");
            }
        }
    }
}
