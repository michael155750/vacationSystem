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
            try
            {
                InitializeComponent();
                this.HostingUnitArea_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
                MainGrid.DataContext = hostingUnit;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public AddHostingUnit_UserControl(HostingUnit h)
        {
            try
            {
                InitializeComponent();
                hostingUnit = h;
                this.HostingUnitArea_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
                MainGrid.DataContext = hostingUnit;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        public AddHostingUnit_UserControl(Host host)
	
       {
            try
            {
                InitializeComponent();

                this.HostingUnitArea_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));

                MainGrid.DataContext = hostingUnit;

                hostingUnit.Owner = host;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HostingUnitName_TextBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                HostingUnitSubArea_Textbox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                HostingUnitSwimmingPool_CheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
                HostingUnitJacuzzi_CheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
                HostingUnitChildrensAttractions_CheckBox.GetBindingExpression(CheckBox.IsCheckedProperty).UpdateSource();
                hostingUnit.Area = (Areas)(HostingUnitArea_ComboBox.SelectedIndex + 1);


                bl.AddUnit(hostingUnit);



                (this.Parent as StackPanel).Children.Remove(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
