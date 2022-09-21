using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweySystem.Core
{
    public class worker
    {
        public static string filePathProgress = "deweyDecimalList.txt";
        public static string filePathDeweyList = "userProgressBar.txt";

        //This method is to read all data from the dewey decimal list, we use this by "readAllLines"
        public static void ReadTextFileDeweyList()
        {
            string[] lines = System.IO.File.ReadAllLines(filePathProgress);

            foreach (string line in lines)
            {
                ListHandler.deweyDecimalList.Add(line.ToString());
            }

            Random rand = new Random();
            ListHandler.shuffled = ListHandler.deweyDecimalList.OrderBy(_ => rand.Next()).ToList();

        }

        //This method will read the value from the textfile that obtain the progress bar value
        public static double ReadTextFileProgessBar()
        {
            int countMethod =+ 1;

            double progressValue = 0;

            string[] lines = System.IO.File.ReadAllLines(filePathDeweyList);

            foreach (string line in lines)
            {
                progressValue = Convert.ToDouble(line);
            }

            if (countMethod == 2)
            {
                File.Create(filePathDeweyList).Close();
            }

            return progressValue;           
        }

        //This method will write a value to the textfile whether that been the reset value or new updated value.
        public static void WriteTextFileProgessBar(double value)
        {
            File.WriteAllTextAsync(filePathDeweyList, value.ToString());      
        }

        public static void resetReplaceBookUi()
        {
           
                Random rand = new Random();
                ListHandler.shuffled = ListHandler.deweyDecimalList.OrderBy(_ => rand.Next()).ToList();
              
        }
    }
}
