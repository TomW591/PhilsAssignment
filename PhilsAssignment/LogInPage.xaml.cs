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
using System.IO;
namespace PhilsAssignment
{
    /// <summary>
    /// Interaction logic for LogInPage.xaml
    /// </summary>
    public partial class LogInPage : Page
    {
        private string password;
       private string username;
        public LogInPage()
        {
            InitializeComponent();
        
        }

        private void _logInButton_Click(object sender, RoutedEventArgs e)
        {
           username = _userNameInput.Text;
            password = _passwordBox.Password;
            verify(username, password);
        }



        private void _userNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        public bool verify(string username, string password)
        {
            string[,] info = new string[25,5];
            csvSplit split = new csvSplit();
            info = split.split();
            if(username == null || password == null)
            {
                return false;
            }
            for(int i = 0; i < info.GetLength(0); i++)
            {
                if (info[i,0] == username)
                {
                    if (info[i, 1] == password)
                    {
                        return true;
                    }
                    return false;
                }
                
            }
            
            return true;
        }
    }
}
