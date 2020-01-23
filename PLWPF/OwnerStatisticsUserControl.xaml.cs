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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BL;

namespace PLWPF
{
    
    /// <summary>
    /// Interaction logic for OwnerStatisticsUserControl.xaml
    /// </summary>
    public partial class OwnerStatisticsUserControl : UserControl
    {
        BL.Ibl bl = new FactoryBL().GetBL();

        public OwnerStatisticsUserControl()
        {
            InitializeComponent();

            InitLeftStack();
            InitRightStack();

        }

        private void InitLeftStack()
        {
            HostsComboBox.DataContext = bl.GetAllHosts();
            reqComboBox.DataContext = bl.GetAllRequests();
            OrdersComboBox.DataContext = bl.GetAllOrders();
            UnitsComboBox.DataContext = bl.GetAllUnits();
        }

        private void InitRightStack()
        {
            numOfHostsTextBlock.Text = "The number of hosts is:" + bl.GetAllHosts().ToList().Count();
            numOfUnitsTextBlock.Text = "The number of hosting units is:" + bl.GetAllUnits().ToList().Count();
            numOfordersTextBlock.Text = "The number of orders is:" + bl.GetAllOrders().ToList().Count();
            totalcommTextBlock.Text = "The total commition is: " + bl.CalculateCommition().ToString();
            numOfReqTextBlock.Text = "The number of guest requests is:" + bl.GetAllRequests().ToList().Count();
        }
    }
}
