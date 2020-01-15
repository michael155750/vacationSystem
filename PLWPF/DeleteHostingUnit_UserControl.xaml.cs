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
    /// Interaction logic for DeleteHostingUnit_UserControl.xaml
    /// </summary>
    public partial class DeleteHostingUnit_UserControl : UserControl
    {
        BL.Ibl bl = FactoryBL.getBL();//צריך להבין איך להעביר את המידע בין החלונות ולכן מופה זה הינו זמני בלבד!!!!

        public DeleteHostingUnit_UserControl()
        {
            InitializeComponent();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long key = long.Parse(DeleteTextBox.Text);
                bl.DeleteUnit(key);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
