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
    /// Interaction logic for HostDetails_UserControl.xaml
    /// </summary>
    public partial class HostDetails_UserControl : UserControl
    {
        HostingUnit h = new HostingUnit();
        public HostDetails_UserControl()
        {
            InitializeComponent();
            
        }

        private void SaveHostDetails_Click(object sender, RoutedEventArgs e)
        {
            h.Owner = new Host();
            h.Owner.FirstName = HostFirstNameTextBox.Text;
            h.Owner.LastName = HostLastNameTextBox.Text;
            h.Owner.FhoneNumber = int.Parse(HostPhoneTextBox.Text);
            h.Owner.MailAddress = HostMailTextBox.Text;

            MessageBox.Show("Saved!!!");
        }       
    }
}
