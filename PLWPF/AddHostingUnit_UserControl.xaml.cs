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
    /// Interaction logic for AddHostingUnit_UserControl.xaml
    /// </summary>
    public partial class AddHostingUnit_UserControl : UserControl
    {
        BL.Ibl bl = new FactoryBL().GetBL();
        HostingUnit hostingUnit = new HostingUnit();

        public AddHostingUnit_UserControl()
        {
            InitializeComponent();
            this.HostingUnitArea_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
            MainGrid.DataContext = hostingUnit;

        }
        public AddHostingUnit_UserControl(HostingUnit h)
        {
            InitializeComponent();
            hostingUnit = h;
            this.HostingUnitArea_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
            MainGrid.DataContext = hostingUnit;

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            HostingUnitName_TextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            HostingUnitSubArea_Textbox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            HostingUnitSwimmingPool_CheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            HostingUnitJacuzzi_CheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            HostingUnitChildrensAttractions_CheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
            hostingUnit.Area = (Areas)(HostingUnitArea_ComboBox.SelectedIndex + 1);

            try
            {
                bl.AddUnit(hostingUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            (this.Parent as StackPanel).Children.Remove(this);
        }
    }
}
