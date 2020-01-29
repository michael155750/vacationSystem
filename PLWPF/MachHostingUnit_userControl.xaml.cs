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

using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MachHostingUnit_UserControl.xaml
    /// </summary>
    public partial class MachHostingUnit_UserControl : UserControl
    {
        BL.Ibl bl = new FactoryBL().GetBL();
        GuestRequest guestRequest = new GuestRequest();
        public MachHostingUnit_UserControl()
        {
            InitializeComponent();
        }

        public MachHostingUnit_UserControl(GuestRequest g)
        {
            guestRequest = g;
            List<HostingUnit> HostingUnitList = bl.MachUnitToRequest(guestRequest);
            InitializeComponent();
            foreach (var h in HostingUnitList)
            {
                HostingUnit_UserControl hostingUnit_UserControl = new HostingUnit_UserControl(h);
                Uc_StackPanel.Children.Add(hostingUnit_UserControl);
            }
            if (HostingUnitList.Count() == 0)
                MessageBox.Show("Sorry, their is no hosting unit maching to your request!!");
            
        }
    }
}
