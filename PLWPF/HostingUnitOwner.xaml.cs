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
        BL.Ibl bl = new FactoryBL().GetBL();
        Host host;
        // private List<string> OptionList;

        public HostingUnitOwner()
        {
            InitializeComponent();

        }


        private void CheckDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                long key = long.Parse(HostLogIn_TextBox.Text);

                host = bl.FindHostByKey(key);

                Uc_StackPanel.Children.Clear(); //clear the satckPanel
                Button AddUnitButton = new Button();//create button that is click will open UC for add unit
                AddUnitButton.Content = "Add Unit";//named the button
                AddUnitButton.Click += AddUnitButton_Click;//add the function to click event
                Button UpdateUnitButton = new Button();
                UpdateUnitButton.Content = "Update Unit";
                UpdateUnitButton.Click += UpdateUnitButton_Click;
                Button DeleteUnitButton = new Button();
                DeleteUnitButton.Content = "Delete Unit";
                DeleteUnitButton.Click += DeleteUnitButton_Click;
                Buttons_Grid.Children.Add(AddUnitButton);//add the button to "Buttons_Grid"
                Buttons_Grid.Children.Add(UpdateUnitButton);
                Buttons_Grid.Children.Add(DeleteUnitButton);
                Grid.SetColumn(AddUnitButton, 0);
                Grid.SetColumn(UpdateUnitButton, 1);
                Grid.SetColumn(DeleteUnitButton, 2);
            }
            catch (Exception ex)
            {
                HostLogIn_TextBox.Text = null;
                MessageBox.Show(ex.Message);
            }
        }

        private void AddUnitButton_Click(object sender, RoutedEventArgs e)
        {

            MainGrid.Children.RemoveRange(1, 3);
            AddHostingUnit_UserControl add_uc = new AddHostingUnit_UserControl(host);
            MainGrid.Children.Add(add_uc);
            Grid.SetRow(add_uc, 1);//?
            Grid.SetColumn(add_uc, 1);//?
        }

        private void UpdateUnitButton_Click(object sender, RoutedEventArgs e)
        {

            MainGrid.Children.RemoveRange(1, 3);
            UpdateHostingUnit_UserControl update_uc = new UpdateHostingUnit_UserControl();
            MainGrid.Children.Add(update_uc);
            Grid.SetRow(update_uc, 1);//?
            Grid.SetColumn(update_uc, 1);//?
        }

        private void DeleteUnitButton_Click(object sender, RoutedEventArgs e)
        {

            MainGrid.Children.RemoveRange(1, 3);
            DeleteHostingUnit_UserControl Delete_uc = new DeleteHostingUnit_UserControl();
            MainGrid.Children.Add(Delete_uc);
            Grid.SetRow(Delete_uc, 1);//?
            Grid.SetColumn(Delete_uc, 1);//?
        }
    }
}
