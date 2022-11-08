using System.Windows;

namespace DeweySystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Click_ButtonExit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void comingSoonButton_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
