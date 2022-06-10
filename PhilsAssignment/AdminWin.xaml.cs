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
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminWin : Window
    {
        private string username;
        private string currentUsername;
        private string password;
        private string newPassword;
        private string firstname;
        private string lastname;
        private string[,] data;
        public AdminWin()
        {
            InitializeComponent();
            GetCurrentUser();
            Disabler();
        }

        public void GetCurrentUser()
        {
            StreamReader read = new StreamReader("currentUser.txt");
            currentUsername = read.ReadLine();
            read.Close();

        }

        public string[,] GetData()
        {
            string[,] info = new string[25, 5];
            csvSplit split = new csvSplit();
            info = split.split();
            return info;
        }

        public void SetData(string[,] data)
        {
            StreamWriter writer = new StreamWriter("users.csv");
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] != "")
                {
                    for (int j = 0; j < data.GetLength(1) - 1; j++) { writer.Write(data[i, j] + ","); }
                    writer.WriteLine(data[i, 4]);
                }
            }
            writer.Close();
        }

        public void DeleteAccount()
        {
            data = GetData();
            bool exist = false;
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] == username)
                {
                    data[i, 0] = ""; data[i, 1] = ""; data[i, 2] = ""; data[i, 3] = ""; data[i, 4] = "";
                    ErrorMessage.Text = "Account deleted successfully.";
                    ErrorMessage.Foreground = Brushes.Green;
                    SetData(data);
                    exist = true;
                }
                if (!exist)
                {
                    ErrorMessage.Text = "Error - Username does not exist.";
                    ErrorMessage.Foreground = Brushes.Red;
                }
            }
        }

        public bool ValidateName(string name)
        {
            bool validLength = false; bool validChars = true; bool validName = false;
            if (name.Length > 0 && name.Length <= 64) { validLength = true; }
            foreach (char letter in name) { validChars = validChars && (Char.IsUpper(letter) || Char.IsLower(letter)); }
            if (!validLength)
            {
                ErrorMessage.Text = "Error - Names must be between 1 and 64 characters in length. ";
                ErrorMessage.Foreground = Brushes.Red;

            }
            else if (!validChars)
            {
                ErrorMessage.Text = "Error - Names must only contain alphabetic characters.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            else { validName = true; }
            return validName;
        }

        public bool ValidatePassword(string newPassword)
        {
            bool validLength = false; bool validUpper = false; bool validLower = false; bool validDigit = false; bool validPassword = false;
            if (newPassword.Length >= 8 && newPassword.Length <= 64) { validLength = true; }
            foreach (char letter in newPassword)
            {
                validUpper = validUpper || Char.IsUpper(letter);
                validLower = validLower || Char.IsLower(letter);
                validDigit = validDigit || Char.IsDigit(letter);
            }
            if (!validLength)
            {
                ErrorMessage.Text = "Error - Passwords must be between 8 and 64 characters in length.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            else if (!validUpper)
            {
                ErrorMessage.Text = "Error - Passwords must contain a uppercase character.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            else if (!validLower)
            {
                ErrorMessage.Text = "Error - Passwords must contain a lowercase character.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            else if (!validDigit)
            {
                ErrorMessage.Text = "Error - Passwords must contain a number.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            else { validPassword = true; }
            return validPassword;
        }

        public void ChangePassword()
        {
            data = GetData();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] == currentUsername)
                {
                    if (data[i, 1] == password)
                    {
                        if (ValidatePassword(newPassword))
                        {
                            data[i, 1] = newPassword;
                            ErrorMessage.Text = "Password changed successfully.";
                            ErrorMessage.Foreground = Brushes.Green;
                            SetData(data);
                        }
                    }
                    else
                    {
                        ErrorMessage.Text = "Error - Invalid Old Password.";
                        ErrorMessage.Foreground = Brushes.Red;
                    }
                }

            }
        }

        public void CreateAccount()
        {
            data = GetData();
            if (ValidateName(firstname) && ValidateName(lastname) && ValidatePassword(password))
            {
                int n = 0;
                bool exists = true;
                string temp = "";
                username = firstname.ToLower() + "." + lastname.ToLower();
                while (exists)
                {
                    exists = false;
                    if (n == 0) { temp = username; }
                    else { temp = username + Convert.ToString(n); }
                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        if (data[i, 0] == temp)
                        {
                            exists = true;
                        }
                    }
                    n++;

                }

                string projects = data[1, 3];

                int projectsNum = data[1, 3].Split(";").Length;
                string hours;
                hours = String.Concat(Enumerable.Repeat("0;", projectsNum - 1)) + "0";

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    if (data[i, 0] == null)
                    {
                        data[i, 0] = temp;
                        data[i, 1] = password;
                        data[i, 2] = "Dev";
                        data[i, 3] = projects;
                        data[i, 4] = hours;
                        break;
                    }
                }

                ErrorMessage.Text = "Account created successfully.";
                ErrorMessage.Foreground = Brushes.Green;
                SetData(data);

            }
        }

        public void Disabler()
        {
            data = GetData();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] == currentUsername)
                {
                    if (data[i, 2] == "Dev")
                    {
                        _createAccountButton.IsEnabled = false;
                        _deleteAccountButton.IsEnabled = false;
                        _reportPageButton.IsEnabled = false;
                    }
                    else
                    {
                        _createAccountButton.IsEnabled = true;
                        _deleteAccountButton.IsEnabled = true;
                        _reportPageButton.IsEnabled = true;
                    }
                }
            }
        }

        private void _changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordStackPanel.Visibility = Visibility.Visible;
            CreateAccountStackPanel.Visibility = Visibility.Hidden;
            DeleteAccountStackPanel.Visibility = Visibility.Hidden;
            ErrorMessage.Text = "";
        }

        private void _createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordStackPanel.Visibility = Visibility.Hidden;
            CreateAccountStackPanel.Visibility = Visibility.Visible;
            DeleteAccountStackPanel.Visibility = Visibility.Hidden;
            ErrorMessage.Text = "";
        }

        private void _deleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordStackPanel.Visibility = Visibility.Hidden;
            CreateAccountStackPanel.Visibility = Visibility.Hidden;
            DeleteAccountStackPanel.Visibility = Visibility.Visible;
            ErrorMessage.Text = "";
        }

        private void _cPButton_Click(object sender, RoutedEventArgs e)
        {
            password = _cPOldPasswordInput.Password;
            newPassword = _cPNewPasswordInput.Password;
            ChangePassword();
        }

        private void _cAButton_Click(object sender, RoutedEventArgs e)
        {
            firstname = _cAFirstnameInput.Text;
            lastname = _cALastnameInput.Text;
            password = _cAPasswordInput.Password;
            CreateAccount();
        }

        private void _dAButton_Click(object sender, RoutedEventArgs e)
        {
            username = _dAUserNameInput.Text;
            DeleteAccount();
        }

        private void _adminPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _projectPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _reportPageButton_Click(object sender, RoutedEventArgs e)
        {
            ReportWin report = new ReportWin();
            report.Show();
            Close();
        }
    }
}

