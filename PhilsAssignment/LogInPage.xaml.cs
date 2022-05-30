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
        }



        private void _userNameInput_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void _passwordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
