using PersonalFinanceProjectFinal.Models;
using System.Windows.Controls;

namespace PersonalFinanceProjectFinal.Pages
{
    /// <summary>
    /// Interaction logic for AddRecordsPlaceHolder.xaml
    /// </summary>
    public partial class AddRecordsPlaceHolder : Page
    {
        public AddRecordsPlaceHolder(User current)
        {
            InitializeComponent();
            DataContext = current;
        }
    }
}
