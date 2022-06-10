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
            //When a new instance of the window is run, the window opens, the current user is read in, and certain buttons are disabled depending on privileges.
            InitializeComponent();
            GetCurrentUser();
            Disabler();
        }

        //This reads and stores the current user from the "currentUser.txt" file
        public void GetCurrentUser()
        {
            StreamReader read = new StreamReader("currentUser.txt");
            currentUsername = read.ReadLine();
            read.Close();

        }

        //This method reads and stores the data from "users.csv" into a 2 dimensional array.
        public string[,] GetData()
        {
            string[,] info = new string[25, 6];
            csvSplit split = new csvSplit();
            //Data needs to be split on the commas to be read into the array.
            info = split.split();
            return info;
        }

        //This method overwrites the existing "users.csv" file with any changes made when creating/deleting accounts and changing passwords.
        public void SetData(string[,] data)
        {
            StreamWriter writer = new StreamWriter("users.csv");
            //For loop is used to iterate over the array so all data is written back to the file.
            for (int i = 0; i < data.GetLength(0); i++)
            {
                //This condition prevents any blank records being saved to the file.
                if (data[i, 0] != "")
                {
                    for (int j = 0; j < data.GetLength(1) - 1; j++) { writer.Write(data[i, j] + ","); }
                    writer.WriteLine(data[i, 4]);
                }
            }
            writer.Close();
        }

        //This method deletes the user account from the data array depending on the username entered.
        public void DeleteAccount()
        {
            //Get current data stored in the file.
            data = GetData();
            bool exist = false;
            //for loop iterates over the array until the entered username is found.
            for (int i = 0; i < data.GetLength(0); i++)
            {
                //If the username entered actually exists, its record is filled with empty strings.
                if (data[i, 0] == username)
                {
                    data[i, 0] = ""; data[i, 1] = ""; data[i, 2] = ""; data[i, 3] = ""; data[i, 4] = "";
                    ErrorMessage.Text = "Account deleted successfully.";
                    ErrorMessage.Foreground = Brushes.Green;
                    SetData(data);
                    exist = true;
                }
                //Otherwise nothing is deleted, and an error message is displayed.
                if (!exist)
                {
                    ErrorMessage.Text = "Error - Username does not exist.";
                    ErrorMessage.Foreground = Brushes.Red;
                }
            }
        }

        //This method validates the name values entered by the user according to the requirements.
        public bool ValidateName(string name)
        {
            bool validLength = false; bool validChars = true; bool validName = false;
            //If the name is between 1 and 64 characters in length, validLength is set to true.
            if (name.Length > 0 && name.Length <= 64) { validLength = true; }
            //The foreach loop iterates through every character in the name to check if the character is alphabetic (Upper or lowercase letter).
            //If all of the charaters are alphabetic, validChars stays true, otherwise, it is set to false.
            foreach (char letter in name) { validChars = validChars && (Char.IsUpper(letter) || Char.IsLower(letter)); }
            //If validLength is false, the invalid length error message is displayed.
            if (!validLength)
            {
                ErrorMessage.Text = "Error - Names must be between 1 and 64 characters in length. ";
                ErrorMessage.Foreground = Brushes.Red;

            }
            //If validChars is false, the invalid characters error message is displayed.
            else if (!validChars)
            {
                ErrorMessage.Text = "Error - Names must only contain alphabetic characters.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            //Otherwise, the name is valid.
            else { validName = true; }
            return validName;
        }

        //This method validates the passwords entered by the user.
        public bool ValidatePassword(string newPassword)
        {
            bool validLength = false; bool validUpper = false; bool validLower = false; bool validDigit = false; bool validPassword = false;

            //Checks if the password is the correct length
            if (newPassword.Length >= 8 && newPassword.Length <= 64) { validLength = true; }
            //foreach checks the password contains at least one upper and lower character, and a number. 
            foreach (char letter in newPassword)
            {
                validUpper = validUpper || Char.IsUpper(letter);
                validLower = validLower || Char.IsLower(letter);
                validDigit = validDigit || Char.IsDigit(letter);
            }
            //If the length is invalid - Invalid length error message
            if (!validLength)
            {
                ErrorMessage.Text = "Error - Passwords must be between 8 and 64 characters in length.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            //If there are no uppercase characters - Invalid uppercase message 
            else if (!validUpper)
            {
                ErrorMessage.Text = "Error - Passwords must contain a uppercase character.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            //If there are no lowercase characters - Invalid lowercase message
            else if (!validLower)
            {
                ErrorMessage.Text = "Error - Passwords must contain a lowercase character.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            //If there are no numbers - Invalid number message
            else if (!validDigit)
            {
                ErrorMessage.Text = "Error - Passwords must contain a number.";
                ErrorMessage.Foreground = Brushes.Red;
            }
            //Otherewise, the password is valid.
            else { validPassword = true; }
            return validPassword;
        }

        //This method is used for the user to change the password of the account currently logged in.
        public void ChangePassword()
        {
            data = GetData();
            //Checks each username in the array until the correct one is found.
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] == currentUsername)
                {
                    //The old password entry must match what is stored in the array with the username.
                    if (data[i, 1] == password)
                    {
                        //The new password needs to be validated, if it is valid, the password can be changed.
                        if (ValidatePassword(newPassword))
                        {
                            //new password is set in the array and saved to the file, and a confirmation message is displayed.
                            data[i, 1] = newPassword;
                            ErrorMessage.Text = "Password changed successfully.";
                            ErrorMessage.Foreground = Brushes.Green;
                            SetData(data);
                        }
                    }
                    //If the old password is incorrect, an error message is displayed.
                    else
                    {
                        ErrorMessage.Text = "Error - Invalid Old Password.";
                        ErrorMessage.Foreground = Brushes.Red;
                    }
                }

            }
        }

        //This method is used to create new accounts.
        public void CreateAccount()
        {
            data = GetData();
            //The firstname, lastname, and password all need to be valid to create the account.
            if (ValidateName(firstname) && ValidateName(lastname) && ValidatePassword(password))
            {
                int n = 0;
                bool exists = true;
                string temp = "";
                //Username is created in the form [firstname] + "." + [lastname] as per requirements.
                username = firstname.ToLower() + "." + lastname.ToLower();
                while (exists)
                {
                    exists = false;
                    if (n == 0) { temp = username; }
                    //If the username already exists, an increment is added to the end to make it unique.
                    else { temp = username + Convert.ToString(n); }
                    for (int i = 0; i < data.GetLength(0); i++)
                    {
                        if (data[i, 0] == temp)
                        {
                            exists = true;
                        }
                    }
                    n++;
                    //The loop repeats until the username is eventually unique.
                }

                //Stores the current projects in a string.
                string projects = data[1, 3];

                //This calculates the number of projects so a corresponding hours string can be saved.
                //Each value is set to 0 in this string since the account is new and has not booked any hours.
                int projectsNum = data[1, 3].Split(";").Length;
                string hours;
                hours = String.Concat(Enumerable.Repeat("0;", projectsNum - 1)) + "0";

                for (int i = 0; i < data.GetLength(0); i++)
                {
                    //Stored the data to a null record in the array.
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

                //Finally, the account details are written to the file and a confirmation message is displayed.
                ErrorMessage.Text = "Account created successfully.";
                ErrorMessage.Foreground = Brushes.Green;
                SetData(data);

            }
        }

        //This method disables certain buttons depending on the privileges of the user signed in.
        public void Disabler()
        {
            data = GetData();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] == currentUsername)
                {
                    //If the user is a developer, the Create Account, Delete Accoun, and Report Page features are disabled.
                    if (data[i, 2] == "Dev")
                    {
                        _createAccountButton.IsEnabled = false;
                        _deleteAccountButton.IsEnabled = false;
                        _reportPageButton.IsEnabled = false;
                    }
                    //If the user is an admin, all features are available.
                    else
                    {
                        _createAccountButton.IsEnabled = true;
                        _deleteAccountButton.IsEnabled = true;
                        _reportPageButton.IsEnabled = true;
                    }
                }
            }
        }

        //This method displays the Change Password Stack Panel and hides the others when the Change Password Button is pressed.
        private void _changePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordStackPanel.Visibility = Visibility.Visible;
            CreateAccountStackPanel.Visibility = Visibility.Hidden;
            DeleteAccountStackPanel.Visibility = Visibility.Hidden;
            ErrorMessage.Text = "";
        }

        //This method displays the Create Account Stack Panel and hides the others when the Create Account Button is pressed.
        private void _createAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordStackPanel.Visibility = Visibility.Hidden;
            CreateAccountStackPanel.Visibility = Visibility.Visible;
            DeleteAccountStackPanel.Visibility = Visibility.Hidden;
            ErrorMessage.Text = "";
        }

        //This method displays the Delete Account Stack Panel and hides the others when the Delete Account Button is pressed.
        private void _deleteAccountButton_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordStackPanel.Visibility = Visibility.Hidden;
            CreateAccountStackPanel.Visibility = Visibility.Hidden;
            DeleteAccountStackPanel.Visibility = Visibility.Visible;
            ErrorMessage.Text = "";
        }

        //This method stores the old and new password entered by the user and runs the ChangePassword() Method when the Change Password Button is pressed.
        private void _cPButton_Click(object sender, RoutedEventArgs e)
        {
            password = _cPOldPasswordInput.Password;
            newPassword = _cPNewPasswordInput.Password;
            ChangePassword();
        }

        //This method stores the first and lastnames and password entered by the user and runs CreateAccount() when the Create Account Button is pressed.
        private void _cAButton_Click(object sender, RoutedEventArgs e)
        {
            firstname = _cAFirstnameInput.Text;
            lastname = _cALastnameInput.Text;
            password = _cAPasswordInput.Password;
            CreateAccount();
        }

        //This method stores the username entered and runs DeleteAccount() when the Delete Account Button is pressed.
        private void _dAButton_Click(object sender, RoutedEventArgs e)
        {
            username = _dAUserNameInput.Text;
            DeleteAccount();
        }

        //Opens the Admin Page (not binded to any method since the user is already on the Admin Page)
        private void _adminPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //Opens the Projects Page
        private void _projectPageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        //Opens the Report Page
        private void _reportPageButton_Click(object sender, RoutedEventArgs e)
        {
            ReportWin report = new ReportWin();
            report.Show();
            Close();
        }

       
    }
}

