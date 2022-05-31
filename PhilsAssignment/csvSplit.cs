using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilsAssignment
{
    internal class csvSplit
    {
        public string[,] split()
        {
            string read;
            StreamReader reader = new StreamReader("users.csv");
            string[,] info = new string[25,5];
            int row = 0;
          while(!reader.EndOfStream)
            {
             
                    read = reader.ReadLine();
                    string[] temp = read.Split(',');
                   for(int i = 0; i < temp.Length; i++)
                {
                    info[row,i] = temp[i];
                }
                    row++;
                
                
            }
          return info;


        }
    }
}
