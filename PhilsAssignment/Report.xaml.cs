﻿using System;
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

namespace PhilsAssignment
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Page
    {
        csvSplit data = new csvSplit();
       currentUser user = new currentUser();
        LogInPage logInPage = new LogInPage();
        public Report()
        {
            InitializeComponent();
            stufffff();
        }
        public void stufffff()
        {
            (user.UserName, user.Privellage) = logInPage.setVals(); // fuck knows if this will work but i have hope
            string[,] temp = new string[25, 5];
            string[] userInfo = new string[5];
            temp = data.split();
            for(int i = 0; i < temp.GetLength(0); i++)
            {
                if(user.UserName == temp[i,0])
                {
                  userInfo[0] = temp[i,0];
                    userInfo[1] = temp[i,1];
                    userInfo[2] = temp[i,2];
                    userInfo[3] = temp[i,3];
                    userInfo[4] = temp[i,4];
                    i = temp.GetLength(0);
                }
            }

            string[] currentProjects = new string[10];
            string[] hoursBooked = new string[10];
            string split = userInfo[3];
            currentProjects = split.Split(';');
            split = userInfo[4];
            hoursBooked = split.Split(';');
            
            for(int i = 0; i < currentProjects.Length; i++)
            {
                searchBox.Items.Add(currentProjects[i]);
            }

        }

        private void searchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
