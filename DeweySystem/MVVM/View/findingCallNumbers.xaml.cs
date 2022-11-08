using DeweySystem.Core;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DeweySystem.MVVM.View
{
    /// <summary>
    /// Interaction logic for findingCallNumbers.xaml
    /// </summary>
    public partial class findingCallNumbers : UserControl
    {
        public int populateRandomValueTree, RandomValueChildOne, RandomValueChildTwo;
        public Random random = new Random();
        public List<int> randomNumberList = new List<int>();
        public int counter = 0;
        List<string> sortedList = new List<string>();
        public findingCallNumbers()
        {
            InitializeComponent();
            PopulateTree();
            Populatelb_LevelOne();
            progressBarLoad();    
        }

        public void progressBarLoad()
        {
            double progressBarValue = worker.ReadTextFileProgessBarFindingCallNumber();

            pbCorrectAnswerBarIdentifyArea.Value = progressBarValue;
            txtScore.Text = $"{"Score"} {progressBarValue + ":5"}";
            txtScorePer.Text = $"{Math.Round((progressBarValue / 5 * 100)) + "%"}";
        }


        private void btnSubmit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            
                string selectedCallNumber = lbLevelCallNum.SelectedItem.ToString();

                if (GetParentOfQuestion().Equals(ParentOfSelectedItem(selectedCallNumber)))
                {
                    MessageBox.Show("Correct Answer! Procced To Next Level");
                    lbLevelCallNum.Items.Clear();
                    Populatelb_LevelTwo();
                    counter++;


                    if (counter > 1)
                    {
                        MessageBox.Show("All Answers Were Correct!");
                        double value = (int)(pbCorrectAnswerBarIdentifyArea.Value += 1);
                        worker.WriteTextFileProgessBarFindingCallNumbers(value);
                        txtScore.Text = $"{"Score"} {(value) + ":5"}";
                        txtScorePer.Text = $"{Math.Round((value / 5 * 100)) + "%"}";

                        lbLevelCallNum.Items.Clear();
                        counter = 0;
                        PopulateTree();
                        Populatelb_LevelOne();
                    }
                }
                else
                {
                    MessageBox.Show("Wrong Answer Answer!");
                    lbLevelCallNum.Items.Clear();
                    counter = 0;
                    PopulateTree();
                    Populatelb_LevelOne();
                }
                

        }

        public string GetParentOfQuestion()
        {
            string ParentVariable = ListHandler.treeList[randomNumberList[0]].Root.Children[RandomValueChildOne].Children[RandomValueChildTwo].Data.toString();

            return HandleInput(ParentVariable);

        }

        public string ParentOfSelectedItem(string selectedItem)
        {

            return HandleInput(selectedItem);
        }

        public string HandleInput(string input)
        {
            for (int i = 0; i < 4; i++)
            {

                if (input.Equals(ListHandler.treeList[randomNumberList[i]].Root.Children[RandomValueChildOne].Data.toString()))
                {
                    input = ListHandler.treeList[randomNumberList[i]].Root.Children[RandomValueChildOne].Parent.Data.toString();
                }
                if (input.Equals(ListHandler.treeList[randomNumberList[i]].Root.Children[RandomValueChildOne].Children[RandomValueChildTwo].Data.toString()))
                {
                    input = ListHandler.treeList[randomNumberList[i]].Root.Children[RandomValueChildOne].Parent.Data.toString();
                }

            }

            return input;
        }

        private void Button_reset(object sender, RoutedEventArgs e)
        {
            lbLevelCallNum.Items.Clear();
            PopulateTree();
            Populatelb_LevelOne();
        }

        public void Populatelb_LevelOne()
        {
          
            for (int i = 0; i < 4; i++)
            {
                sortedList.Add(ListHandler.treeList[randomNumberList[i]].Root.Data.toString());
            }

            populateAtLevels();
        }

        public void Populatelb_LevelTwo()
        {
            
            for (int i = 0; i < 4; i++)
            {
                sortedList.Add(ListHandler.treeList[randomNumberList[i]].Root.Children[RandomValueChildOne].Data.toString());
            }

            populateAtLevels();
        }

        public void populateAtLevels()
        {
            
            sortedList.Sort();

            foreach (string item in sortedList)
            {
                lbLevelCallNum.Items.Add(item);
            }

            sortedList.Clear();
        }

        public void PopulateTree()
        {
            randomNumberList.Clear();
            RandomValueChildOne = random.Next(2);
            RandomValueChildTwo = random.Next(3);

            foreach (var item in worker.ReadFindingCallNumbersTextFile())
            {
                populateRandomValueTree = random.Next(10);

                if (randomNumberList.Contains(populateRandomValueTree))
                {

                }
                else
                {
                    randomNumberList.Add(populateRandomValueTree);
                }

                List<CallNumberLevels> allCallNumberList = new List<CallNumberLevels>() ;
                allCallNumberList = item;

                TreeRoot<CallNumberLevels> TreeList = new TreeRoot<CallNumberLevels>();
                TreeList.Root = new TreeModel<CallNumberLevels>() { Data = new CallNumberLevels(allCallNumberList[0].Number, allCallNumberList[0].Description) };

                for (int i = 0; i < allCallNumberList.Count; i++)
                {
                    TreeList.Root.Children = new List<TreeModel<CallNumberLevels>>
                    {
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[1].Number, allCallNumberList[1].Description), Parent = TreeList.Root },
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[5].Number, allCallNumberList[5].Description), Parent = TreeList.Root }
                    };

                    TreeList.Root.Children[0].Children = new List<TreeModel<CallNumberLevels>>
                    {
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[2].Number, allCallNumberList[2].Description), Parent = TreeList.Root.Children[0]},
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[3].Number, allCallNumberList[3].Description), Parent = TreeList.Root.Children[0]},
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[4].Number, allCallNumberList[4].Description), Parent = TreeList.Root.Children[0]}
                    };

                    TreeList.Root.Children[1].Children = new List<TreeModel<CallNumberLevels>>
                    {
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[6].Number, allCallNumberList[6].Description), Parent = TreeList.Root.Children[1]},
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[7].Number, allCallNumberList[7].Description), Parent = TreeList.Root.Children[1]},
                    new TreeModel<CallNumberLevels>() {Data = new CallNumberLevels(allCallNumberList[8].Number, allCallNumberList[8].Description), Parent = TreeList.Root.Children[1]}
                    };

                }

                ListHandler.treeList.Add(TreeList);

            }

            txtQuestion.Text = $"{"Question: "}{ListHandler.treeList[randomNumberList[0]].Root.Children[RandomValueChildOne].Children[RandomValueChildTwo].Data.Description.ToString()}";

        }

    }
}
