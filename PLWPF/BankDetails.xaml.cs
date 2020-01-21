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
    /// Interaction logic for BankDetails.xaml
    /// </summary>
    public partial class BankDetails : UserControl
    {
        HostingUnit hostingUnit = new HostingUnit();
        public BankDetails()
        {
            InitializeComponent();
            MainGrid.DataContext = hostingUnit.Owner;
        }
        public BankDetails(HostingUnit h)
        {
            hostingUnit = h;
            InitializeComponent();
            MainGrid.DataContext = hostingUnit.Owner;
        }
        private void SaveHostDetails_Click(object sender, RoutedEventArgs e)
        {
            BankNameTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            BankNumTextBlock.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            BranchNumberTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            BranchAddressTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            BankAccountNumberTextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //hostingUnit.Owner.BankBranchDetails.CollectionClearance = (CollectionClearanceCheckBox.IsChecked);

            AddHostingUnit_UserControl addHostingUnit_UserControl = new AddHostingUnit_UserControl(hostingUnit);
            (this.Parent as StackPanel).Children.Add(addHostingUnit_UserControl);
            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
