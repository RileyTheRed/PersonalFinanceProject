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
    /// Interaction logic for AddRecordsWindow.xaml
    /// </summary>
    public partial class AddRecordsWindow : Window
    {
        public AddRecordsWindow()
        {
            InitializeComponent();
        }

        private void btnReturnDashboard_Click(object sender, RoutedEventArgs e)
        {
            Owner.Visibility = Visibility.Visible;
            Owner.IsEnabled = true;

            Close();
        }
    }
}
