    using PersonalFinanceProjectFinal.Models;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Input;

    namespace PersonalFinanceProjectFinal.View_Models
    {
    public class UserAccountSettingsWindowVM : INotifyPropertyChanged
    {

        public User cur { get; set; }
        private UserAccountSettingsWindow parent;

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        private string _selectedCurrency;
        public string SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = (value == "") ? cur.CurrencyType : value;
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

        public UserAccountSettingsWindowVM(User current, UserAccountSettingsWindow rent)
        {
            cur = current;
            parent = rent;
            //SelectedCurrency = "";
        }


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
            if (SelectedCurrency != null)
            {
                if (!SelectedCurrency.Equals(cur.CurrencyType))
                {
                    cur.CurrencyType = SelectedCurrency;
                }
            }

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
            //SelectedColorScheme.
            parent.Owner.IsEnabled = true;
            parent.Owner.Visibility = System.Windows.Visibility.Visible;
            parent.Close();
        }

    }
    }
