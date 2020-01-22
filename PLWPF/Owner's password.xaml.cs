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

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for Owner_s_password.xaml
    /// </summary>
    public partial class Owner_s_password : UserControl
    {
        Owner_window father;
        public Owner_s_password(Owner_window fa)
        {
            InitializeComponent();
            father = fa;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (passwordTextBox.Text == "0000")
            {
                father.Change_user_control();
            }
            else
            {
                wrong_password.Visibility = Visibility.Visible;

            }
        }
    }
}
