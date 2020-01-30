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
using BE;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnit_UserControl.xaml
    /// </summary>
    public partial class HostingUnit_UserControl : UserControl
    {
        BL.Ibl bl = new FactoryBL().GetBL();
        HostingUnit hostingUnit = new HostingUnit();
        GuestRequest guestRequest = new GuestRequest();

        public HostingUnit_UserControl(HostingUnit h , GuestRequest g)
        {
            guestRequest = g;
            hostingUnit = h;
            InitializeComponent();
            MainGrid.DataContext = hostingUnit;            
        }
        private void Owner_Button_Click(object sender, RoutedEventArgs e)
        {
            bl.AddRequest(guestRequest);
            bl.AddOrder(guestRequest.GuestRequestKey , hostingUnit.HostingUnitKey);
            MessageBox.Show(hostingUnit.HostingUnitName + " Selecte");

            //GuestRequest_UserControl guestRequest_UserControl = new GuestRequest_UserControl();  
            
        }
    }
}
