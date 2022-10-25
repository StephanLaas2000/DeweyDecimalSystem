using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DeweySystem.Core
{
    public class worker
    {
       //Textfile paths
        public static string filePathDeweyList = "deweyDecimalList.txt";
        public static string filePathProgressBarIdentifyArea = "userProgressBarIdentifyArea.txt";
        public static string fileReplaceBook = "userProgressBar.txt";

        //This method is to read all data from the dewey decimal list, we use this by "readAllLines"
        public static void ReadTextFileDeweyList()
        {
            string[] lines = System.IO.File.ReadAllLines(filePathDeweyList);

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
            int countMethod = +1;

            double progressValue = 0;

            string[] lines = System.IO.File.ReadAllLines(fileReplaceBook);

            foreach (string line in lines)
            {
                progressValue = Convert.ToDouble(line);
            }

            if (countMethod == 2)
            {
                File.Create(fileReplaceBook).Close();
            }

            return progressValue;
        }
        //This method will read the value from the textfile that obtain the progress bar value
        public static double ReadTextFileProgessBarBarIdentifyArea()
        {
            int countMethod = +1;

            double progressValue = 0;

            string[] lines = System.IO.File.ReadAllLines(filePathProgressBarIdentifyArea);

            foreach (string line in lines)
            {
                progressValue = Convert.ToDouble(line);
            }

            if (countMethod == 2)
            {
                File.Create(filePathProgressBarIdentifyArea).Close();
            }

            return progressValue;
        }

        //This method will write a value to the textfile whether that been the reset value or new updated value.
        public static void WriteTextFileProgessBar(double value)
        {
            File.WriteAllTextAsync(fileReplaceBook, value.ToString());
        }

        public static void WriteTextFileProgessBarBarIdentifyArea(double value)
        {
            File.WriteAllTextAsync(filePathProgressBarIdentifyArea, value.ToString());
        }

        public static void resetReplaceBookUi()
        {

            Random rand = new Random();
            ListHandler.shuffled = ListHandler.deweyDecimalList.OrderBy(_ => rand.Next()).ToList();

        }

        //Populating a two dictionary , the first dictionary gets mutated so we can use the second dictionary in place of the first dictionary after mutation
        public static void populateDictonary()
        {
            ListHandler.callnumberDic = new Dictionary<string, string>()
            {
                {"000", "Computer science, information & general works"},
                {"100", "Philosophy & psychology"},
                {"200", "Religion"},
                {"300", "Social sciences"},
                {"400", "Language"},
                {"500", "Science"},
                {"600", "Technology"},
                {"700", "Arts & recreation"},
                {"800", "Literature"},
                {"900", "History & geography"},
            };

            ListHandler.callnumberDic2 = new Dictionary<string, string>()
            {
                {"000", "Computer science, information & general works"},
                {"100", "Philosophy & psychology"},
                {"200", "Religion"},
                {"300", "Social sciences"},
                {"400", "Language"},
                {"500", "Science"},
                {"600", "Technology"},
                {"700", "Arts & recreation"},
                {"800", "Literature"},
                {"900", "History & geography"},
            };
        }
    }
}
