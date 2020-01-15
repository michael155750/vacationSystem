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
        BL.Ibl bl = FactoryBL.getBL();
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OwnerButton_Click(object sender, RoutedEventArgs e)
        {
            string password = OwnerBox.Text;
            if (password == "0000")
            {
                MessageBox.Show("correct!!!");
            }
        }
        private void HostingUnitOwnerButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long key = long.Parse(HostingUnitOwnerBox.Text);

                Host host = bl.FindHostByKey(key);
                HostingUnitOwner hostingUnitOwner = new HostingUnitOwner();
                hostingUnitOwner.ShowDialog();
            }
            catch (Exception ex)
            {
                HostingUnitOwnerBox.Text = null;
                MessageBox.Show(ex.Message);
            }
        }
        private void SingIn_Click(object sender, RoutedEventArgs e)
        {
            HostSingIn hostSingIn = new HostSingIn();
            hostSingIn.ShowDialog();
        }

    }
}
