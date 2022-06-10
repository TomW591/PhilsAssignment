using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class ReportWin : Window
    {
        csvSplit data = new csvSplit();
        private string username;
        public string selectedProject;
        public string[] userInfo = new string[6];
        public ReportWin()
        {
            InitializeComponent();
            setcurrentUser();
            stufffff();
        }
    

        private void setcurrentUser()
        {
            StreamReader read = new StreamReader("currentUser.txt");
            username = read.ReadLine();
            read.Close();
        }

        public void stufffff()
        {

            string[,] temp = new string[25, 5];

            temp = data.split();
            for (int i = 0; i < temp.GetLength(0); i++)
            {
                if (username == temp[i, 0])
                {
                    userInfo[0] = temp[i, 0];
                    userInfo[1] = temp[i, 1];
                    userInfo[2] = temp[i, 2];
                    userInfo[3] = temp[i, 3];
                    userInfo[4] = temp[i, 4];
                    userInfo[5] = temp[i, 5];   
                    i = temp.GetLength(0);
                }
            }

            string[] currentProjects = new string[10];
            string[] hoursBooked = new string[10];
            string split = userInfo[3];
            currentProjects = split.Split(';');
            split = userInfo[4];
            hoursBooked = split.Split(';');

            for (int i = 0; i < currentProjects.Length; i++)
            {
                searchBox.Items.Add(currentProjects[i]);
            }

        }

        private void searchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProject = (string)searchBox.SelectedItem;
            LoadStats();
        }

        private void LoadStats()
        {
            string[] currentProjects = new string[10];
            string[] hoursBooked = new string[10];
            string[] taskscompleted = new string[10];
            string split = userInfo[3];
            currentProjects = split.Split(';');
            split = userInfo[4];
            hoursBooked = split.Split(';');
            split = userInfo[5];
            taskscompleted = split.Split(';');
            int count = 0;


            foreach (string element in currentProjects)
            {
                if (element == selectedProject)
                {
                    _hoursUsed.Text = hoursBooked[count];
                    _tasksCompleted.Text = taskscompleted[count];
                }
                count++;
            }

        }

        private void _bookingButton_Click(object sender, RoutedEventArgs e)
        {
            BookingWin load = new BookingWin();
           load.Show();
            Close();
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
            ReportAdmin load = new ReportAdmin();
            load.Show();
            Close();
        }
    }
}
