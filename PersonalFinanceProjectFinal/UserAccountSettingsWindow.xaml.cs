using PersonalFinanceProjectFinal.Models;
using System.Windows;
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
