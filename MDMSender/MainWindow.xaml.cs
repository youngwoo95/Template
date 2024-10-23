using MDMSender.Functions;
using MDMSender.Models;
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
        private bool isRunning = false;   // 타이머 실행 여부
        private Task timerTask = null;     // 현재 실행 중인 타이머 작업

        public MainWindow()
        {
            InitializeComponent();

            // DispatcherTimer 초기화
           
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RightStackPanel.Content = new StartWindow();
        }

        private async void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                await StopTimerAsync(); // 비동기적으로 타이머 중지
            }

            RightStackPanel.Content = new SettingWindow();
        }

        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            RightStackPanel.Content = new StartWindow();

            if (!isRunning && (timerTask == null || timerTask.IsCompleted))
            {
                await StartTimerAsync();
            }
        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                await StopTimerAsync(); // 비동기적으로 타이머 중지
            }
        }

        /// <summary>
        /// 비동기 타이머 시작 메서드
        /// </summary>
        private async Task StartTimerAsync()
        {
            if (!isRunning)
            {
                isRunning = true; // 타이머 실행 중 플래그 설정
                timerTask = Task.Run(async () =>
                {
                    MessageBox.Show("MDM 프로그램 시작.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
                    while (isRunning)
                    {
                        await DoWorkAsync(); // 1초마다 작업 수행
                        await Task.Delay(TimeSpan.FromSeconds(1)); // 1초 대기 (비동기)
                    }
                });

                await timerTask; // 타이머 작업이 완료될 때까지 대기
            }
        }

        /// <summary>
        /// 비동기 타이머 종료 메서드
        /// </summary>
        private async Task StopTimerAsync()
        {
            if (isRunning)
            {
                isRunning = false; // 타이머 중지 플래그 설정

                // 비동기 대기 (타이머 작업이 완료될 때까지)
                if (timerTask != null)
                {
                    await timerTask; // Task.Wait() 대신 await 사용
                    MessageBox.Show("MDM 프로그램 정지.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// 실제 수행할 비동기 작업
        /// </summary>
        private async Task DoWorkAsync()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("1초 경과됨: " + DateTime.Now);
            });

            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                await MDMFunctions.AddDataAsync(new SenderModel { DateTime = "1", Query = "A" });
            });
        }
    }
}