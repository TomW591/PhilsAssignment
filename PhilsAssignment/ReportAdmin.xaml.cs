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

namespace PhilsAssignment
{
    /// <summary>
    /// Interaction logic for ReportAdmin.xaml
    /// </summary>
    public partial class ReportAdmin : Window
    {
        csvSplit data = new csvSplit();
        public ReportAdmin()
        {
            InitializeComponent();
            datagrid();
        }


        public void datagrid()
        {
            List<User> users = new List<User>();
            string[,] info = new string[25, 6];
            info = data.split();
            for(int i = 0; i < info.GetLength(0); i++)
            {
                if (info[i,0] == null)
                {
                    i = info.GetLength(0);
                }
                else
                {
                    User user = new User();
                    user.username = info[i,0];
                    user.hoursBooked = info[i, 4];
                    users.Add(user);
                }
            }
            usersGrid.ItemsSource = users;

        }

        private void searchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void _adminPageButton_Click(object sender, RoutedEventArgs e)
        {
            AdminWin admin = new AdminWin();
                admin.Show();
            Close();
        }

        private void _projectPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _reportPageButton_Click(object sender, RoutedEventArgs e)
        {
            ReportAdmin report = new ReportAdmin();
            report.Show();
            Close();
        }
    }
    public class User
    { 
        public string username { get; set; }
        public string hoursBooked { get; set; }
    

    }

}
