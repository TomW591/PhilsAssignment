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
    public partial class AdminPage : Page
    {
        private string username = "ben.falkingham";
        private string password = "BFalkPass1";
        private string newPassword = "BFalkNewPass1";
        private string firstname = "ben";
        private string lastname = "falkingham";
        private string[,] data;
        public AdminPage()
        {
            InitializeComponent();
            ChangePassword();
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
                if(data[i, 0] != "")
                {
                    for(int j = 0; j < data.GetLength(1) - 1; j++) { writer.Write(data[i, j] + ","); }
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
                    SetData(data);
                    exist = true;
                }
                if (!exist)
                {
                    Console.Write("Username does not exist");
                }
            }
        }

        public bool ValidateName(string name)
        {
            bool validLength = false; bool validChars = true; bool validName = false;
            if(name.Length > 0 && name.Length <= 64) { validLength = true; }
            foreach(char letter in name) { validChars = validChars && (Char.IsUpper(letter) || Char.IsLower(letter)); }
            if (!validLength) { Console.Write("Error - names must be between 1 and 64 chars"); }
            else if (!validChars) { Console.Write("Error - names must only contain alphabetic chars"); }
            else { validName = true; }
            return validName;
        }
        
        public bool ValidatePassword(string newPassword)
        {
            bool validLength = false; bool validUpper = false; bool validLower = false; bool validDigit = false; bool validPassword = false;
            if(newPassword.Length >= 8 && newPassword.Length <= 64) { validLength = true; }
            foreach (char letter in newPassword)
            {
                validUpper = validUpper || Char.IsUpper(letter);
                validLower = validLower || Char.IsLower(letter);
                validDigit = validDigit || Char.IsDigit(letter);
            }
            if (!validLength) { Console.Write("Error - Password must be between 8 and 64 chars"); }
            else if (!validUpper) { Console.Write("Error - Password must contain an upper char"); }
            else if (!validLower) { Console.Write("Error - Password must contain a lower char"); }
            else if (!validDigit) { Console.Write("Error - Password must contain a number"); }
            else { validPassword = true; }
            return validPassword;
        }

        public void ChangePassword()
        {
            data = GetData();
            for (int i = 0; i < data.GetLength(0); i++)
            {
                if (data[i, 0] == username)
                {
                    if (data[i, 1] == password)
                    {
                        if (ValidatePassword(newPassword)) 
                        { 
                            data[i, 1] = newPassword;
                            SetData(data);
                        }
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
                        if(data[i, 0] == temp) 
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
                    if(data[i, 0] == null)
                    {
                        data[i, 0] = temp;
                        data[i, 1] = password;
                        data[i, 2] = "Dev";
                        data[i, 3] = projects;
                        data[i, 4] = hours;
                        break;
                    }
                }

                SetData(data);

            }
        }

    }
}
