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

    public partial class LogInWin : Window
    {
        private string password;
        private string username;
        public currentUser user = new currentUser();
        public LogInWin()
        {
            InitializeComponent();

        }

        //This method writes the username entered to "currentUser.txt" after a successful login.
        public void SetCurrentUser()
        {
            StreamWriter writer = new StreamWriter("currentUser.txt");
            writer.Write(username);
            writer.Close();
        }

        //This method stores the username and password entered and attempts to log the user in after pressing the Enter Button.
        private void _logInButton_Click(object sender, RoutedEventArgs e)
        {
            username = _userNameInput.Text;
            password = _passwordBox.Password;
            //Verifies if the username and password are correct.
            if (verify(username, password))
            {
                //If correct, the current user is written to the file and the admin page is opened.
                SetCurrentUser();
                AdminWin admin = new AdminWin();
                admin.Show();
                Close();
            }
            //Otherwise, Error message is displayed.
            else
            {
                _errorMessage.Text = "Error - Invalid username or password";
                _errorMessage.Foreground = Brushes.Red;
            }

        }

        public (string, string) setVals()
        {
            return (user.UserName, user.Privellage);
        }

        //This method verifies if the username and password entered are correct.
        public bool verify(string username, string password)
        {
            string[,] info = new string[25, 6];
            csvSplit split = new csvSplit();
            info = split.split();
            //If nothing is entered, verification fails.
            if (username == null || password == null)
            {
                return false;
            }
            //For loop needs to iterate over the array until the username entered is found
            for (int i = 0; i < info.GetLength(0); i++)
            {
                if (info[i, 0] == username)
                {
                    if (info[i, 1] == password)
                    {
                        //If the username and password match, verification passes.
                        user.UserName = username;
                        user.Privellage = info[i, 2];
                        return true;
                    }
                    
                }
                
            }
            return false;
        }
    }
}
