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

using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for HostingUnitOwner.xaml
    /// </summary>
    public partial class HostingUnitOwner : Window
    {
        BL.Ibl bl = FactoryBL.getBL();//צריך להבין איך להעביר את המידע בין החלונות ולכן מופה זה הינו זמני בלבד!!!!

        private List<string> OptionList;
        public HostingUnitOwner()
        {
            InitializeComponent();

            OptionList = new List<string> { "Add Unit", "Update Unit", "Delete Unit" };
            HostComboBox.ItemsSource = OptionList;

        }
        private void HostComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = HostComboBox.SelectedIndex;
            switch (index)
            {
                case 0:
                    MainGrid.Children.RemoveRange(1, 3);
                    AddHostingUnit_UserControl add_uc  = new AddHostingUnit_UserControl();
                    MainGrid.Children.Add(add_uc);
                    Grid.SetRow(add_uc, 1);//?
                    Grid.SetColumn(add_uc, 1);//?
                    break;
                case 1:
                    MainGrid.Children.RemoveRange(1, 3);
                    UpdateHostingUnit_UserControl Update_uc = new UpdateHostingUnit_UserControl();
                    MainGrid.Children.Add(Update_uc);
                    Grid.SetRow(Update_uc, 1);//?
                    Grid.SetColumn(Update_uc, 1);//?
                    break;
                case 2:
                    MainGrid.Children.RemoveRange(1, 3);
                    DeleteHostingUnit_UserControl Delete_uc = new DeleteHostingUnit_UserControl();
                    MainGrid.Children.Add(Delete_uc);
                    Grid.SetRow(Delete_uc, 1);//?
                    Grid.SetColumn(Delete_uc, 1);//?
                    break;
                default:
                    break;
            }
        }

    }
}
