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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostSingIn.xaml
    /// </summary>
    public partial class HostSingIn : Window
    {
        public HostSingIn()
        {
            InitializeComponent();

            HostDetails_UserControl hostDetails_UserControl = new HostDetails_UserControl();
            MainGrid.Children.Add(hostDetails_UserControl);
        }
        private void SaveHostDetails_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("");
        }
    }
}
