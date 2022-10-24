using DeweySystem.Core;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DeweySystem.MVVM.View
{
    /// <summary>
    /// Interaction logic for ProgressManager.xaml
    /// </summary>
    public partial class ProgressManager : UserControl
    {
        public ProgressManager()
        {
            InitializeComponent();
            populateProgressBar();
        }


        //The purpose of this method is to read the value for the progress bar from a textfile , this value is then set into the components of the window
        public void populateProgressBar()
        {
            double progressBarValueTotal = worker.ReadTextFileProgessBar() + worker.ReadTextFileProgessBarBarIdentifyArea();
            double progressBarValueReplaceBook = worker.ReadTextFileProgessBar();
            double progressBarIdentifyArea = worker.ReadTextFileProgessBarBarIdentifyArea();

            txtProgressBarReplace.Text = $"{Math.Round((progressBarValueReplaceBook / 3 * 100)) + "%"}";
            txtOverall.Text = $"{Math.Round((progressBarValueTotal / 9 * 100)) + "%"}";
            txtProgressIdentifyArea.Text = $"{Math.Round((progressBarIdentifyArea / 5 * 100)) + "%"}";
            identifyArea.Value = progressBarIdentifyArea;
            pbReplaceProgress.Value = progressBarValueReplaceBook;
            pbOverall.Value = (progressBarValueTotal / 9);
        }

        //The purpose of this method is to reset the values for the progress bar in the textfile , this value is then set into the components of the window
        private void Button_resetOverall(object sender, RoutedEventArgs e)
        {
            worker.WriteTextFileProgessBar(0);
            worker.WriteTextFileProgessBarBarIdentifyArea(0);
            txtProgressBarReplace.Text = "0%";
            txtOverall.Text = "0%";
            txtProgressIdentifyArea.Text = "0%";
            pbReplaceProgress.Value = 0;
            pbOverall.Value = 0;
            identifyArea.Value = 0;
        }
    }
}
