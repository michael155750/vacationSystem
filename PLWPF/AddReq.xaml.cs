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
using BE;
using BL;

namespace PLWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class AddReq : Window
    {
        GuestRequest temp = new GuestRequest();

        public AddReq()
        {
            InitializeComponent();


            BL.Ibl bl = new FactoryBL().GetBL();
            GuestRequest req = new GuestRequest ();

            for (int i = 0; i < 5; i++)
            {
                ComboBoxItem tmp = new ComboBoxItem();
                tmp.Content = (Areas)i;
                AreaBox.Items.Add(tmp);
            }
            for (int i = 0; i < 3; i++)
            {
                ComboBoxItem tmp = new ComboBoxItem();
                tmp.Content = (Types)i;
                TypeBox.Items.Add(tmp);
            }
        }

        



        private void SaveButton_Click(object sender, TextChangedEventArgs e)
        {
            try
            {
                temp.Adults = int.Parse(Adults.Text);
                temp.Children = int.Parse(Children.Text);
                temp.FirstName = FirstName.Text;
                temp.LastName = LastName.Text;
                temp.MailAddress = mail.Text;
                temp.Area = (BE.Areas)AreaBox.SelectedItem;
                temp.SubArea = SubArea.Text;
                temp.Type = (Types)TypeBox.SelectedItem;

                bool? choice;
                choice = poolCheck.IsChecked.Value;
                if (choice == true) temp.Pool = Choice.Necessary;
                else if (choice == false) temp.Pool = Choice.Unnecessary;
                else temp.Pool = Choice.Possible;

                choice = jacuzzyCheck.IsChecked.Value;
                if (choice == true) temp.Jacuzzi = Choice.Necessary;
                else if (choice == false) temp.Jacuzzi = Choice.Unnecessary;
                else temp.Jacuzzi = Choice.Possible;

                choice = gardenCheck.IsChecked.Value;
                if (choice == true) temp.Garden = Choice.Necessary;
                else if (choice == false) temp.Garden = Choice.Unnecessary;
                else temp.Garden = Choice.Possible;

                choice = atractionsCheck.IsChecked.Value;
                if (choice == true) temp.ChildrensAttractions = Choice.Necessary;
                else if (choice == false) temp.ChildrensAttractions = Choice.Unnecessary;
                else temp.ChildrensAttractions = Choice.Possible;


            }
            catch
            { }
        }

        private void openCalendaryButton_Click(object sender, RoutedEventArgs e)
        {
            List<DateTime> d = new List<DateTime> ();
            CalendarWindow calen = new CalendarWindow(ref d);
            
            calen.ShowDialog();
        }


    }
}
