using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeweySystem.Core
{
    public class SortingAlgorithm
    {
        //Code Atrribution[3]
        //Website: dotnetcoretutorials
        //Topic: Sorting Algorithms
        //Link: https://dotnetcoretutorials.com/2020/05/10/basic-sorting-algorithms-in-c/
        public static List<string> SortList(List<string> deweylist)
        {
            List<double> arrlist = new List<double>();

            //This foreach will take the dewey Decimal list and split all the numbers into their own element and store in "arrlist"
            foreach (string item in deweylist)
            {
                string[] num = item.Split('\t');
                arrlist.Add(Convert.ToDouble(num[0]));

            }

            int n = deweylist.Count;

            //This for loop is here to handle all the elements that are in the "arrlist"
            for (int i = 1; i < n; ++i)
            {

                //Allocating keys to the "list" received in the parameters and the new split list "deweylist", these keys will turn into the value at each index in the for loop
                string keyOne = deweylist[i];
                double keyTwo = arrlist[i];
                int j = i - 1;

                //This while loop is here to handle the moving index "j" , this will constantly recreate the two lists that we created
                while (j >= 0 && arrlist[j] > keyTwo)
                {
                    arrlist[j + 1] = arrlist[j];
                    deweylist[j + 1] = deweylist[j];
                    j = j - 1;

                }
                arrlist[j + 1] = keyTwo;
                deweylist[j + 1] = keyOne;
            }

            return deweylist;

        }
    }
}
