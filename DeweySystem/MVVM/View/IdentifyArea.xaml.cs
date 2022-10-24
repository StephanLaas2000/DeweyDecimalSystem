using DeweySystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DeweySystem.MVVM.View
{

    /// <summary>
    /// Interaction logic for IdentifyArea.xaml
    /// </summary>
    public partial class IdentifyArea : UserControl
    {
        public Random rand = new Random();
        int RandomNumber;
        List<string> TotalValueList = new List<string>();
        List<string> lbValues = new List<string>();

        public IdentifyArea()
        {
            InitializeComponent();
            worker.populateDictonary();
            populateListBox1();
            progressBarLoad();
            populateValueListBox2();
        }

        //The "progressBarLoad" method is used to call the values from the textfiles for the gamification feature
        public void progressBarLoad()
        {
            double progressBarValue = worker.ReadTextFileProgessBarBarIdentifyArea();

            pbCorrectAnswerBarIdentifyArea.Value = progressBarValue;
            txtScore.Text = $"{"Score"} {progressBarValue + ":5"}";
            txtScorePer.Text = $"{Math.Round((progressBarValue / 5 * 100)) + "%"}";
        }

        //The populateListBox method is used to populate the first listbox with a randomnly shuffled list that obtains values from the dictionary
        public void populateListBox1()
        {
            RandomNumber = rand.Next(2);

            ListHandler.shuffled = new List<string>();

            ListHandler.shuffled = 
                RandomNumber == 0 ? 
                ListHandler.callnumberDic.Select(x => x.Key).OrderBy(_ => rand.Next()).ToList() : 
                ListHandler.callnumberDic.Select(x => x.Value).OrderBy(_ => rand.Next()).ToList();

            for (int i = 0; i < 4; i++)
            {
                lbColumn1.Items.Add(ListHandler.shuffled[i].ToString());
            }

        }
        
        //This method is using values from the dictionary to help get 4 correct answers plus 3 incorrect answers, the 4 correct values are related to the 
        //4 values in the first listbox
        public void populateValueListBox2()
        {
            lbValues = new List<string>();      
            TotalValueList = new List<string>();

            foreach (string item in lbColumn1.Items)
            {
                lbValues.Add(item);
            }

            foreach (string item in lbValues)
            {
               
                foreach (string item1 in RandomNumber == 0 ? 
                    ListHandler.callnumberDic.Where(x => x.Key.Equals(item)).Select(x => x.Value).ToList() : 
                    ListHandler.callnumberDic.Where(x => x.Value.Equals(item)).Select(x => x.Key).ToList())
                {
                    TotalValueList.Add(item1);
               
                }

                HashSet<string> keysToRemove = new HashSet<string> { item };

                ListHandler.callnumberDic = RandomNumber == 0 ? ListHandler.callnumberDic.Where(kvp => !keysToRemove.Contains(kvp.Key))
                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value) : ListHandler.callnumberDic.Where(kvp => !keysToRemove.Contains(kvp.Value))
                            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

               
            }
            foreach (string item1 in RandomNumber == 0 ? ListHandler.callnumberDic.Values.Take(3) : ListHandler.callnumberDic.Keys.Take(3))
            {
                TotalValueList.Add(item1);
            }

            foreach (string item in TotalValueList.OrderBy(x => rand.Next()).ToList())
            {
             
                lbColumn2.Items.Add(item);
            }
        }

        private void Button_reset(object sender, RoutedEventArgs e)
        {

            resetUI();

        }

        //When the user submits there answer
        private void btnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            List<string> numberList = new List<string>() { "1","2","3","4"};
            List<string> lbColumn2List = new List<string>();
            List<string> key = new List<string>();
            List<string> elementList = new List<string>();
            Dictionary<string, string> lbColumn2Dic = new Dictionary<string, string>();  

            //This loop will get all values from the second listbox
            foreach (string item in lbColumn2.Items)
            {
                lbColumn2List.Add(item);

            }

            //This for/foreach process will help get the indexes at which the user had entered a value in a specific textbox and also gets the element
            for (int i = 0; i < numberList.Count; i++)
            {
                foreach (int item1 in getAnswers().ToList().Select((element, index) => element == numberList[i] ? index : -1).Where(i => i >= 0).ToList())
                {
                    
                    elementList.Add(lbColumn2List.ElementAt(item1));
                }
            }

            //These two foreach loops will help us get the corresponding key to the element that are in the elementList which we obtain from the above code.
            foreach (string item in elementList)
            {
                foreach (string item1 in RandomNumber == 0 ?
                    ListHandler.callnumberDic2.Where(x => x.Value.Equals(item)).Select(x => x.Key).ToList() :
                    ListHandler.callnumberDic2.Where(x => x.Key.Equals(item)).Select(x => x.Key).ToList())
                {
                    key.Add(item1);
                }
            }

            //We add both the values from the element list and key into a local dictionary which we will later use to compare
            for (int i = 0; i < elementList.Count; i++)
            {
                lbColumn2Dic.Add(key[i], elementList[i]);
            }

            //This is where the answer get checks by the correct dictionary
            if (listbox1Handler().Values.SequenceEqual(lbColumn2Dic.Values))
            {
                double value = (int)(pbCorrectAnswerBarIdentifyArea.Value += 1);             
                worker.WriteTextFileProgessBarBarIdentifyArea(value);
                txtScore.Text = $"{"Score"} {(value) + ":5"}";
                txtScorePer.Text = $"{Math.Round((value / 5 * 100)) + "%"}";
                MessageBox.Show("Correct !");
                resetUI();

            }
            else
            {
                MessageBox.Show("Inccorect ! , try again...");
                
            }

        }

        //This method is used to get the data from the first listbox and to also store the correct values into the dictionary 
        public Dictionary<string,string> listbox1Handler()
        {
            Dictionary<string, string> lbColumn1Dic = new Dictionary<string, string>();
            List<string> valueList = new List<string>();

            foreach (string item in lbValues)
            {
                foreach (string item1 in RandomNumber == 0 ?
                    ListHandler.callnumberDic2.Where(x => x.Key.Equals(item)).Select(x => x.Value).ToList() :
                    ListHandler.callnumberDic2.Where(x => x.Value.Equals(item)).Select(x => x.Key).ToList())
                {
                    valueList.Add(item1);
                }
            }

            for (int i = 0; i < valueList.Count; i++)
            {
                lbColumn1Dic.Add(lbValues[i], valueList[i]);
            }

            return lbColumn1Dic;
        }

        //This is just a simple method which will reset and the UI elements
        public void resetUI()
        {
            RandomNumber = rand.Next(2);
            lbColumn1.Items.Clear();
            lbColumn2.Items.Clear();

            txbIndex1.Text = "";
            txbIndex2.Text = "";
            txbIndex3.Text = "";
            txbIndex4.Text = "";
            txbIndex5.Text = "";
            txbIndex6.Text = "";
            txbIndex7.Text = "";

            worker.populateDictonary();

            populateListBox1();
            populateValueListBox2();
        }

        //This is called when the user submits an answer, here we will get all the values from the textboxes and store it in a list
        public List<string> getAnswers()
        {
            List<string> answerList = new List<string>();
            string index1 = txbIndex1.Text.ToString();
            string index2 = txbIndex2.Text.ToString();
            string index3 = txbIndex3.Text.ToString();
            string index4 = txbIndex4.Text.ToString();
            string index5 = txbIndex5.Text.ToString();
            string index6 = txbIndex6.Text.ToString();
            string index7 = txbIndex7.Text.ToString();

            answerList.Add(index1);
            answerList.Add(index2);
            answerList.Add(index3);
            answerList.Add(index4);
            answerList.Add(index5);
            answerList.Add(index6);
            answerList.Add(index7);

            return answerList;
        }

        //This restricts what the user can and cannot enter.
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^1-4]+");
            e.Handled = regex.IsMatch(e.Text);
        }


    }
}
