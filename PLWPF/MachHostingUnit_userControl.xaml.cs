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
        List<HostingUnit> HostingUnitList;

        public MachHostingUnit_UserControl()
        {
            InitializeComponent();
        }

        public MachHostingUnit_UserControl(GuestRequest g, List<HostingUnit> h)
        {
            guestRequest = g;
            HostingUnitList = h;

            InitializeComponent();

            foreach (var hu in HostingUnitList)
            {
                HostingUnit_UserControl hostingUnit_UserControl = new HostingUnit_UserControl(hu , guestRequest);
                Uc_StackPanel.Children.Add(hostingUnit_UserControl);
            }            

        }

    }
}
