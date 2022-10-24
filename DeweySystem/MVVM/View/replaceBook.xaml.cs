using DeweySystem.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace DeweySystem.MVVM.View
{
    //Code Attribution[1]
    //Website: Youtube
    //Topic: Drag And Drop Operation Between Two Listbox[C#]
    //Link: https://www.youtube.com/watch?v=arkT9zg1jHc
    public partial class replaceBook : UserControl
    {
        //This list will get the current items that are in the second listbox
        List<string> ListBoxRecValues = new List<string>();

        ListBox dragSource = null;

        public replaceBook()
        {
            InitializeComponent();

            //Call the populateListBox method upon loading this window
            PopulateListbox();

            //Call the progressBarLoad method upon loading this window
            progressBarLoad();

            //Setting the textblock to invisiable upon loading this window
            txtLevelComplete.Visibility = Visibility.Collapsed;

            //Call the checkWin method upon loading this window
            checkWin();
        }

        //The purpose of this method is to check the value of the textfile and see if the value is reached its limit, if so we will not allow the user to submit more call numbers
        public void checkWin()
        {
            if (worker.ReadTextFileProgessBar() == 3)
            {
                btnSubmit.IsEnabled = false;
            }
        }

        //The purpose of this method is to read the value for the progress bar from a textfile , this value is then set into the components of the window
        public void progressBarLoad()
        {
            double progressBarValue = worker.ReadTextFileProgessBar();

            pbCorrectAnswer.Value = progressBarValue;
            txtScore.Text = $"{"Score"} {progressBarValue + ":3"}";
            txtScorePer.Text = $"{Math.Round((progressBarValue / 3 * 100)) + "%"}";
        }

        //This method is used to populate the listbox with the dewey decimal values, these values are obtained from the textfile with 200 call numbers
        public void PopulateListbox()
        {
            worker.ReadTextFileDeweyList();

            for (int i = 0; i < 10; i++)
            {
                lbSend.Items.Add(ListHandler.shuffled[i]).ToString();
            }
        }

        //This method handles the first listbox left button click by the user
        private void lbSend_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            dragSource = parent;
            object data = GetDataFromListBox(dragSource, e.GetPosition(parent));

            if (data != null)
            {
                DragDrop.DoDragDrop(parent, data, DragDropEffects.Move);
            }
        }

        //The purpose of this method is to store the data from the selected item from the first listbox, this is done by using the source and point.
        private object? GetDataFromListBox(ListBox source, Point point)
        {
            UIElement? element = source.InputHitTest(point) as UIElement;
            if (element != null)
            {
                object data = DependencyProperty.UnsetValue;
                while (data == DependencyProperty.UnsetValue)
                {
                    data = source.ItemContainerGenerator.ItemFromContainer(element);

                    if (data == DependencyProperty.UnsetValue)
                    {
                        element = VisualTreeHelper.GetParent(element) as UIElement;
                    }

                    if (element == source)
                    {
                        return null;
                    }
                }

                if (data != DependencyProperty.UnsetValue)
                {
                    return data;
                }
            }
            return null;
        }

        //After the user has selected an item to drag across the second listbox will recive this value, this method handles that event.
        private void lbRec_Drop(object sender, DragEventArgs e)
        {
            ListBox parent = (ListBox)sender;
            object data = e.Data.GetData(typeof(string));
            lbSend.Items.Remove(data);
            parent.Items.Add(data);
        }

        //The button_up method allows the user to move items up and down, this is done by storing the current items in a list and subtracting a value of 1 to move the item up the list
        private void Button_Up(object sender, RoutedEventArgs e)
        {
            ListBoxRecValues = new List<string>();

            foreach (string item in lbRec.Items)
            {
                ListBoxRecValues.Add(item);
            }
            var selectedIndex = this.lbRec.SelectedIndex;

            if (selectedIndex > 0)
            {
                var itemToMoveUp = this.ListBoxRecValues[selectedIndex];
                this.ListBoxRecValues.RemoveAt(selectedIndex);
                this.ListBoxRecValues.Insert(selectedIndex - 1, itemToMoveUp);
                this.lbRec.SelectedIndex = selectedIndex - 1;
                lbRec.Items.Clear();
                foreach (var item in ListBoxRecValues)
                {
                    lbRec.Items.Add(item.ToString());
                }

            }

        }

        //The button_down method is the same as the button_up method but we add a value of 1 to the index to move the selected value down
        private void Button_Down(object sender, RoutedEventArgs e)
        {
            ListBoxRecValues = new List<string>();

            foreach (string item in lbRec.Items)
            {
                ListBoxRecValues.Add(item);
            }

            var selectedIndex = this.lbRec.SelectedIndex;

            if (selectedIndex + 1 < this.ListBoxRecValues.Count)
            {
                var itemToMoveDown = this.ListBoxRecValues[selectedIndex];
                this.ListBoxRecValues.RemoveAt(selectedIndex);
                this.ListBoxRecValues.Insert(selectedIndex + 1, itemToMoveDown);
                this.lbRec.SelectedIndex = selectedIndex + 1;
                lbRec.Items.Clear();
                foreach (var item in ListBoxRecValues)
                {
                    lbRec.Items.Add(item.ToString());
                }
            }
        }

        private void Button_reset(object sender, RoutedEventArgs e)
        {
            resetUi();
        }

        //This button handles the submit event when the user has dragged across all 10 items,
        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            List<string> userAnswerList = new List<string>();
            List<string> sortedList = new List<string>();

            if (lbRec.Items.Count == 10)
            {
                foreach (string item in lbRec.Items)
                {
                    userAnswerList.Add(item);
                    sortedList.Add(item);
                }

                SortingAlgorithm.SortList(sortedList);

                if (userAnswerList.SequenceEqual(sortedList))
                {
                    worker.WriteTextFileProgessBar(pbCorrectAnswer.Value += 1);
                    MessageBox.Show("Books are in correct order");
                    if (worker.ReadTextFileProgessBar() == 3)
                    {
                        txtLevelComplete.Visibility = Visibility.Visible;
                        txtLevelComplete.Text = "Congrats You Compeleted This Course !\n" + "You Can Reset Your Score In The Porgress Manager Tab";
                        checkWin();
                    }

                    progressBarLoad();
                    resetUi();

                }
                else
                {
                    MessageBox.Show("The order is incorrect try again !");
                }
            }
            else
            {
                MessageBox.Show("Please enter all 10 values");
            }

        }

        //The resetUi method will refresh the whole user interface
        public void resetUi()
        {
            worker.resetReplaceBookUi();
            lbSend.Items.Clear();
            lbRec.Items.Clear();

            for (int i = 0; i < 10; i++)
            {
                lbSend.Items.Add(ListHandler.shuffled[i]).ToString();
            }

        }

    }
}
