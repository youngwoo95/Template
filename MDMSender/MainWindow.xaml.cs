using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MDMSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            // DispatcherTimer 초기화
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RightStackPanel.Content = new StartWindow();
        }

        private void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            RightStackPanel.Content = new SettingWindow();

        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            RightStackPanel.Content = new StartWindow();
        }

        
    }
}