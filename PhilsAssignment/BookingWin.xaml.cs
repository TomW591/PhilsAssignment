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
    /// Interaction logic for BookingWin.xaml
    /// </summary>
    public partial class BookingWin : Window
    {
        private string username;
        public string[] userInfo = new string[6];
        csvSplit data = new csvSplit();
        public string selectedProject;
        string[] currentProjects = new string[10];
        string[] hoursBooked = new string[10];
        string[] taskscompleted = new string[10];   
        public BookingWin()
        {
            InitializeComponent();
            setCurrentUser();
            stufffff();
        }

        private void setCurrentUser()
        {
            StreamReader read = new StreamReader("currentUser.txt");
            username = read.ReadLine();
            read.Close();
        }
        public void stufffff()
        {

            string[,] temp = new string[25, 6];

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

           
            string split = userInfo[3];
            currentProjects = split.Split(';');
            split = userInfo[4];
            hoursBooked = split.Split(';');
            split = userInfo[5];
            taskscompleted = split.Split(';');

            for (int i = 0; i < currentProjects.Length; i++)
            {
                searchBox.Items.Add(currentProjects[i]);
            }

        }

        private void searchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedProject = (string)searchBox.SelectedItem;
           
        }

        private void hoursBook_TextChanged(object sender, TextChangedEventArgs e)
        {
            string hoursadded = hoursBook.Text;
            int count = 0;
            foreach(string element in currentProjects)
            {
                if (selectedProject == element)
                {
                    hoursBooked[count] = hoursBooked[count] + hoursadded;
                }
                count++;
            }
        }

        private void tasksCompleted_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tasksadded = tasksCompleted.Text;
            int count = 0;
            foreach (string element in currentProjects)
            {
                if (selectedProject == element)
                {
                  taskscompleted[count] = taskscompleted[count] + tasksadded;
                }
                count++;
            }
        }

        private void _returnButton_Click(object sender, RoutedEventArgs e)
        {
       
            string returnHours;
            string returnProjects;
            for(int i = 0; i < hoursBooked.Length; i++)
            {
                returnHours = hoursBooked[i].ToString();
                returnHours = returnHours + ';';
                hoursBooked[i] = returnHours;
                    
            }
            returnHours = hoursBooked.ToString();

            for (int i = 0; i < hoursBooked.Length; i++)
            {
                returnProjects = taskscompleted[i].ToString();
                returnProjects = returnProjects + ';';
                taskscompleted[i] = returnProjects;

            }
            returnProjects = taskscompleted.ToString();

            userInfo[4] = returnHours;
            userInfo[5] = returnProjects;

            string[,] temp = new string[25, 6];

            temp = data.split();

            for(int i = 0; i < temp.GetLength(0); i++)
            {
                if(username == temp[i,0])
                {
                    temp[i, 0] = userInfo[0];
                    temp[i, 1] = userInfo[1];
                    temp[i, 2] = userInfo[2];
                    temp[i, 3] = userInfo[3];
                    temp[i, 4] = userInfo[4];
                    temp[i, 5] = userInfo[5];
                }
            }


            string stuff = null;  
            string[] onedimension = new string[25];
            for(int i = 0; i < temp.GetLength(0); i++)
            {
                for(int j =0; j < temp.GetLength(1); j++)
                {
                    stuff = stuff + temp[i, j] +',';
                }
                onedimension[i] = stuff;
                stuff = null;
            }
            StreamWriter writer = new StreamWriter("Users.csv");
            for (int i = 0; i < onedimension.Length; i++)
            {
                if (onedimension[i] == null)
                {
                    i = onedimension.Length;
                }
                writer.Write(onedimension[i]);
            }
            writer.Close();

            ReportWin report = new ReportWin();
            report.Show();
            Close();
        }
    }
}
