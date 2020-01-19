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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for CalendarWindow.xaml
    /// </summary>
    public partial class CalendarWindow : Window
    {
        private List <DateTime> d;
        
        public CalendarWindow(ref List<DateTime> dates)
        {
            InitializeComponent();
            Ibl bl = new BL_imp();//FactoryBL.getBL();
            d = dates;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
             d = clndr.SelectedDates.ToList() ;
            
        }
    }
}
