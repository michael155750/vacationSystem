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
    /// Interaction logic for HostSingIn.xaml
    /// </summary>
    public partial class HostSingIn : Window
    {
        HostingUnit hostingUnit = new HostingUnit();
        HostDetails_UserControl hostDetails_UserControl = new HostDetails_UserControl();
        AddHostingUnit_UserControl AddHostingUnit_UserControl = new AddHostingUnit_UserControl();

        public HostSingIn()
        {
            InitializeComponent();
            UcGrid.Children.Add(hostDetails_UserControl);
            UcGrid.DataContext = hostingUnit.Owner;
        }
        private void SaveHostDetails_Click(object sender, RoutedEventArgs e)
        { 
        }
       
    }
}
