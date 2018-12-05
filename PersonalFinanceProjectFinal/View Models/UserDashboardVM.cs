using PersonalFinanceProjectFinal.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PersonalFinanceProjectFinal.View_Models
{
    public class UserDashboardVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        public UserDashboard dashboard { get; set; }
        public User currentUser { get; set; }
        public Window child { get; set; }


        public UserDashboardVM(UserDashboard dash, User current)
        {
            dashboard = dash;
            currentUser = current;
            
        }



        public ICommand LogoutCommand
        {
            get
            {
                if (_logoutCommand == null)
                {
                    _logoutCommand = new DelegateCommand(LogoutClicked);
                }

                return _logoutCommand;
            }
        }
        DelegateCommand _logoutCommand; 
        private void LogoutClicked(object obj)
        {
            Database.UpdateDatabase(currentUser);

            dashboard.Owner.Visibility = System.Windows.Visibility.Visible;
            dashboard.Owner.IsEnabled = true;
            dashboard.Close();
        }

        public ICommand AddRecordsCommand
        {
            get
            {
                if (_addRecordsCommand == null)
                {
                    _addRecordsCommand = new DelegateCommand(AddRecordsClicked);
                }
                return _addRecordsCommand;
            }
        }
        DelegateCommand _addRecordsCommand;
        private void AddRecordsClicked(object obj)
        {
            var temp = currentUser;
            child = new AddRecordsWindow(ref temp);
            child.Owner = dashboard;
            child.DataContext = (new AddRecordsWindowVM(currentUser, child as AddRecordsWindow));
            
            child.Show();

            dashboard.Visibility = Visibility.Hidden;
            dashboard.IsEnabled = false;
        }
    }
}
