using PersonalFinanceProjectFinal.Models;
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
using PersonalFinanceProjectFinal.View_Models;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for UserAccountSettingsWindow.xaml
    /// </summary>
    public partial class UserAccountSettingsWindow : Window
    {
        public UserAccountSettingsWindow(User current)
        {
            InitializeComponent();
            DataContext = new UserAccountSettingsWindowVM(current, this);
        }
    }
}
