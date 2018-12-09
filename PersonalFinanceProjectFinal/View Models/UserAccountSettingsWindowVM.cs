/**
 * UserAccountSettingsWindowVM.css
 * 
 * Author: Riley Wells
 * */

using PersonalFinanceProjectFinal.Models;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PersonalFinanceProjectFinal.View_Models
{
    public class UserAccountSettingsWindowVM : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged = delegate { };


        #region Properties
        public User cur { get; set; }
        private UserAccountSettingsWindow parent;

        private ComboBoxItem _selectedCurrency;
        public ComboBoxItem SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedCurrency"));
            }
        }

        private ComboBoxItem _selectedColorScheme;
        public ComboBoxItem SelectedColorScheme
        {
            get { return _selectedColorScheme; }
            set
            {
                _selectedColorScheme = value;
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedColorScheme"));
            }
        }
        #endregion
        

        /// <summary>
        /// UsserAccountSettingsWindowVM Constructor takes the current user and  UserAccountSettingsWindow
        /// and sets cur = current, and parent = rent.
        /// </summary>
        /// <param name="current"></param>
        /// <param name="rent"></param>
        public UserAccountSettingsWindowVM(User current, UserAccountSettingsWindow rent)
        {
            cur = current;
            parent = rent;
        }

        #region Commands
        /// <summary>
        /// Definition of the CancelCommand ICommand, related _cancelCommand DelegateCommand,
        /// and CancelClicked function.
        /// 
        /// The CancelClicked function sets its related UserAccountSettingsWindow owner to be
        /// visible and enabled, and closes the UserAccountSettingsWindow.
        /// </summary>
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                {
                    _cancelCommand = new DelegateCommand(CancelClicked);
                }
                return _cancelCommand;
            }
        }
        DelegateCommand _cancelCommand;
        private void CancelClicked(object obj)
        {
            parent.Owner.IsEnabled = true;
            parent.Owner.Visibility = System.Windows.Visibility.Visible;

            parent.Close();
        }


        /// <summary>
        /// Defintion of the SubmitChangesCommand ICommand, related _submitChangesCommand DelegateCommand,
        /// and SubmitChangesClicked function.
        /// 
        /// SubmitChangesClicked 
        /// </summary>
        public ICommand SubmitChangesCommand
        {
            get
            {
                if (_submitChangesCommand == null)
                    _submitChangesCommand = new DelegateCommand(SubmitChangesClicked);
                return _submitChangesCommand;
            }
        }
        DelegateCommand _submitChangesCommand;
        private void SubmitChangesClicked(object obj)
        {
            // update the selected currency type
            if (SelectedCurrency != null)
            {
                cur.CurrencyType = SelectedCurrency.Content.ToString();
            }
            // update the selected color scheme
            if (SelectedColorScheme != null)
            {
                    switch (SelectedColorScheme.Content)
                    {
                        case "Navy/Gray":
                            cur.Color1 = "#252839";
                            cur.Color2 = "#677077";
                            cur.Color3 = "#b5b5b7";
                            cur.Color4 = "#f2b632";
                            break;

                        case "Ocean":
                            cur.Color1 = "#dddfd4";
                            cur.Color2 = "#fae596";
                            cur.Color3 = "#3fb0ac";
                            cur.Color4 = "#173e43";
                            break;

                        case "Fall":
                            cur.Color1 = "#feffff";
                            cur.Color2 = "#98dafc";
                            cur.Color3 = "#daad86";
                            cur.Color4 = "#312c32";
                            break;

                        default:
                            break;
                    }
            }

            parent.Owner.IsEnabled = true;
            parent.Owner.Visibility = System.Windows.Visibility.Visible;
            parent.Close();
        }
        #endregion
    }
}
