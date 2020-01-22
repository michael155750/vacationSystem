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
    /// Interaction logic for Owner_window.xaml
    /// </summary>
    public partial class Owner_window : Window
    {
        Owner_s_password os;
        public Owner_window()
        {
            InitializeComponent();
            os = new Owner_s_password(this);
            userControlStackPanel.Children.Add(os);
        }

        public void Change_user_control()
        {
            userControlStackPanel.Children.Remove(os);
            OwnerStatisticsUserControl ost = new OwnerStatisticsUserControl();
            userControlStackPanel.Children.Add(ost);
            
        }
    }

    


}
