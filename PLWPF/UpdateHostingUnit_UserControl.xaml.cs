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
    /// Interaction logic for UpdateHostingUnit_UserControl.xaml
    /// </summary>
    public partial class UpdateHostingUnit_UserControl : UserControl
    {
        BL.Ibl bl = new FactoryBL().GetBL();
        HostingUnit hostingUnit = new HostingUnit();
        private Calendar MyCalendar;
        long key = 0;
        public UpdateHostingUnit_UserControl()
        {
            InitializeComponent();
            this.HostingUnitArea_ComboBox.ItemsSource = Enum.GetValues(typeof(BE.Areas));
            MyCalendar = CreateCalendar();

            StackPanelCalender.Children.Clear();
            StackPanelCalender.Children.Add(MyCalendar);
            //vbCalendar.Child = null;
            //vbCalendar.Child = MyCalendar;
        }
        private void keyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                key = long.Parse(KeyTextBox.Text);
                hostingUnit = bl.FindUnitByKey(key);

                MainGrid.DataContext = hostingUnit;

                HostingUnitName_TextBox.Text = hostingUnit.HostingUnitName;
                HostingUnitSubArea_Textbox.Text = hostingUnit.SubArea;
                SubareaTextBlock.Text = hostingUnit.Area.ToString();
                HostingUnitSwimmingPool_CheckBox.IsChecked = hostingUnit.Pool;
                HostingUnitGarden_CheckBox.IsChecked = hostingUnit.Garden;
                HostingUnitChildrensAttractions_CheckBox.IsChecked = hostingUnit.ChildrensAttractions;

                SetBlackOutDates(hostingUnit);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                hostingUnit.HostingUnitName = HostingUnitName_TextBox.Text;
                hostingUnit.SubArea = HostingUnitSubArea_Textbox.Text;
                hostingUnit.Area = (Areas)(HostingUnitArea_ComboBox.SelectedIndex + 1);
                hostingUnit.Pool = HostingUnitSwimmingPool_CheckBox.IsChecked.Value;
                hostingUnit.Garden = HostingUnitGarden_CheckBox.IsChecked.Value;
                hostingUnit.ChildrensAttractions = HostingUnitChildrensAttractions_CheckBox.IsChecked.Value;

                List<DateTime> myList = MyCalendar.SelectedDates.ToList();

                MyCalendar = CreateCalendar();
                addCurrentList(myList, hostingUnit);
                SetBlackOutDates(hostingUnit);

                StackPanelCalender.Children.Clear();
                StackPanelCalender.Children.Add(MyCalendar);
                //vbCalendar.Child = null;
                //vbCalendar.Child = MyCalendar;

                bl.UpdateUnit(hostingUnit, key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void SetBlackOutDates(HostingUnit hostingUnit)
        {
            if (hostingUnit.BookedDays.Count > 0)
                foreach (DateTime date in hostingUnit.BookedDays)
                {
                    MyCalendar.BlackoutDates.Add(new CalendarDateRange(date));
                }
        }
        private Calendar CreateCalendar()
        {
            Calendar MonthlyCalendar = new Calendar();
            MonthlyCalendar.Name = "MonthlyCalendar";
            MonthlyCalendar.DisplayMode = CalendarMode.Month;
            MonthlyCalendar.SelectionMode = CalendarSelectionMode.MultipleRange;
            MonthlyCalendar.IsTodayHighlighted = true;
            MonthlyCalendar.DisplayDateStart = new DateTime(2020, 1, 1);
            MonthlyCalendar.DisplayDateEnd = new DateTime(2020, 12, 31);
            return MonthlyCalendar;
        }
        private void addCurrentList(List<DateTime> tList, HostingUnit hostingUnit)
        {
            foreach (DateTime d in tList)
            {
                hostingUnit.BookedDays.Add(d);
            }
        }
    }
}
