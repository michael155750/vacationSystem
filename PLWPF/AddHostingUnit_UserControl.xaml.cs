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
        BL.Ibl bl = FactoryBL.getBL();//צריך להבין איך להעביר את המידע בין החלונות ולכן מופה זה הינו זמני בלבד!!!!

        HostingUnit h = new HostingUnit();

        public AddHostingUnit_UserControl()
        {
            InitializeComponent();

            for(int i = 1; i < 5; i++)
            {
                ComboBoxItem tmp = new ComboBoxItem();
                tmp.Content = (Areas)i;
                HostingUnitArea_ComboBox.Items.Add(tmp);
            }

        }
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            h.HostingUnitName = HostingUnitName_TextBox.Text;
            h.SubArea = HostingUnitSubArea_Textbox.Text;
            h.Area = (Areas)(HostingUnitArea_ComboBox.SelectedIndex+1);

            h.Pool = HostingUnitSwimmingPool_CheckBox.IsChecked.Value; //or just: .Value
            h.Jacuzzi = HostingUnitJacuzzi_CheckBox.IsChecked.Value; //or just: .Value
            h.Garden = HostingUnitGarden_CheckBox.IsChecked.Value; //or just: .Value
            h.ChildrensAttractions = HostingUnitChildrensAttractions_CheckBox.IsChecked.Value; //or just: .Value

            try
            {
                bl.AddUnit(h);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            InitializeComponent(); // מאפס את הנתונים שביוזר קונטרול הנוכחי
        }
    }
}
