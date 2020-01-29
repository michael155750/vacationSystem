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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BL.Ibl bl = new FactoryBL().GetBL();

        public MainWindow()
        {
            InitializeComponent();
            GuestRequest_UserControl guestRequest_UserControl = new GuestRequest_UserControl();
            UserControlGrid.Children.Add(guestRequest_UserControl);
        }


        private void HostingUnitOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            HostingUnitOwner hostingUnitOwner = new HostingUnitOwner();
            hostingUnitOwner.ShowDialog();
        }
        private void SingIn_Click(object sender, RoutedEventArgs e)
        {
            HostSingIn hostSingIn = new HostSingIn();
            hostSingIn.ShowDialog();
        }

        private void Owner_Button_Click(object sender, RoutedEventArgs e)
        {
            Owner_window ow = new Owner_window();
            ow.ShowDialog();

        }
    }
}
