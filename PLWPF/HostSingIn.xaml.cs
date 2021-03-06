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

        public HostSingIn()
        {
            try
            {
                InitializeComponent();

                HostingUnit hostingUnit = new HostingUnit();
                hostingUnit.Owner = new Host();
                hostingUnit.Owner.BankBranchDetails = new Bankdetails();

                HostDetails_UserControl hostDetails_UserControl = new HostDetails_UserControl(hostingUnit);
                UcGrid.Children.Add(hostDetails_UserControl);
                UcGrid.DataContext = hostingUnit.Owner;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void SaveHostDetails_Click(object sender, RoutedEventArgs e)
        {
        }

    }
}
