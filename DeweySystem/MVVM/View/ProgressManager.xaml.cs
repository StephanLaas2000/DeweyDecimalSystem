using DeweySystem.Core;
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
            double progressBarValue = worker.ReadTextFileProgessBar();

            txtProgressBarReplace.Text = $"{Math.Round((progressBarValue / 3 * 100)) + "%"}";
            txtOverall.Text = $"{Math.Round((progressBarValue / 9 * 100)) + "%"}";
            pbReplaceProgress.Value = progressBarValue;
            pbOverall.Value = (progressBarValue / 9);
        }


        //The purpose of this method is to reset the values for the progress bar in the textfile , this value is then set into the components of the window
        private void Button_resetReplaceBook(object sender, RoutedEventArgs e)
        {  
            worker.WriteTextFileProgessBar(0);
            txtProgressBarReplace.Text = "0%";
            txtOverall.Text = "0%";
            pbReplaceProgress.Value = 0;
            pbOverall.Value = 0;

        }

        //The purpose of this method is to reset the values for the progress bar in the textfile , this value is then set into the components of the window
        private void Button_resetOverall(object sender, RoutedEventArgs e)
        {      
            worker.WriteTextFileProgessBar(0);
            txtProgressBarReplace.Text = "0%";
            txtOverall.Text = "0%";
            pbReplaceProgress.Value = 0;
            pbOverall.Value = 0;
        }
    }
}
