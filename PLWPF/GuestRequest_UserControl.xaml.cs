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
    /// Interaction logic for GuestRequest_UserControl.xaml
    /// </summary>
    public partial class GuestRequest_UserControl : UserControl
    {
        BL.Ibl bl = new FactoryBL().GetBL();
        GuestRequest guestRequest = new GuestRequest();

        public GuestRequest_UserControl()
        {
            InitializeComponent();
            this.Area_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
            this.Type_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Types));

            this.ArrivalDate.DisplayDateStart = DateTime.Now;
            this.ArrivalDate.DisplayDateEnd = new DateTime(2020, 12, 31); 
            this.ArrivalDate.SelectedDate = DateTime.Now;
            this.LeavingDate.DisplayDateStart = DateTime.Now.AddDays(1);
            this.LeavingDate.DisplayDateEnd = new DateTime(2020, 12, 31);
            this.LeavingDate.SelectedDate = DateTime.Now.AddDays(1);
            MainGrid.DataContext = guestRequest;

        }

        private void SerchButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateTime.Compare(ArrivalDate.SelectedDate.Value, LeavingDate.SelectedDate.Value) < 0)
            {
                Area_ComboBox.GetBindingExpression(ComboBox.SelectedIndexProperty).UpdateSource();
                Type_ComboBox.GetBindingExpression(ComboBox.SelectedIndexProperty).UpdateSource();

                Adult_Textbox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                Children_Textbox.GetBindingExpression(TextBox.TextProperty).UpdateSource();

                ArrivalDate.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();
                LeavingDate.GetBindingExpression(DatePicker.SelectedDateProperty).UpdateSource();

                guestRequest.Pool = ThreeStateToChoice(SwimmingPool_CheckBox);
                guestRequest.Jacuzzi = ThreeStateToChoice(Jacuzzi_CheckBox);
                guestRequest.Garden = ThreeStateToChoice(Garden_CheckBox);
                guestRequest.ChildrensAttractions = ThreeStateToChoice(ChildrensAttractions_CheckBox);

                List<HostingUnit> HostingUnitList = bl.MachUnitToRequest(guestRequest);

                if (HostingUnitList.Count() == 0)
                {
                    MessageBox.Show("Sorry, their is no hosting unit maching to your request!!");
                    GuestRequest_UserControl guestRequest_UserControl = new GuestRequest_UserControl();
                    (this.Parent as StackPanel).Children.Add(guestRequest_UserControl);
                    (this.Parent as StackPanel).Children.Remove(this);
                }
                else
                {
                    foreach (var hu in HostingUnitList)
                    {
                        HostingUnit_UserControl hostingUnit_UserControl = new HostingUnit_UserControl(hu, guestRequest);
                        (this.Parent as StackPanel).Children.Add(hostingUnit_UserControl);

                    }
                    (this.Parent as StackPanel).Children.Remove(this);
                }
            }
            else
            {
                MessageBox.Show("Leaving day cannot be before arrival day!");
            }
        }



        private Choice ThreeStateToChoice(CheckBox checkBox)
        {
            if (checkBox.IsChecked == null)
                return Choice.Possible;
            if (checkBox.IsChecked.Value == true)
                return Choice.Necessary;
            return Choice.Unnecessary;

        }

        private void Children_Textbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
