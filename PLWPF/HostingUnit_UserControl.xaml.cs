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

        public HostingUnit_UserControl(HostingUnit h)
        {
            hostingUnit = h;
            InitializeComponent();
            MainGrid.DataContext = hostingUnit;
        }
    }
}
