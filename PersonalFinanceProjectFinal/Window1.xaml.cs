using PersonalFinanceProjectFinal.Models;
using PersonalFinanceProjectFinal.View_Models;
using System.Windows;

namespace PersonalFinanceProjectFinal
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {


        public Window1(User current)
        {
            InitializeComponent();
            DataContext = new FinanceReportWindowVM(current, this);
        }


    }
}
