using MDMSender.Functions;
using MDMSender.Models;
using MDMSender.Services;
using System.Windows;

namespace MDMSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isRunning = false;   // 타이머 실행 여부
        private Task? timerTask = null;     // 현재 실행 중인 타이머 작업

        private NotifyIcon? notifyIcon;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            RightStackPanel.Content = new StartWindow();
            LogService.LogMessage("프로그램 시작.");
        }

        private void InitializeTrayIcon()
        {
            try
            {
                notifyIcon = new NotifyIcon
                {
                    Icon = new Icon(SystemIcons.Information, 40, 40),
                    Visible = true,
                    Text = "MDMSender - Running in Background"
                };

                notifyIcon.ContextMenuStrip = new ContextMenuStrip();
                notifyIcon.ContextMenuStrip.Items.Add("Open", null, OpenApp_Click);
                notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitApp_Click);

                notifyIcon.DoubleClick += OpenApp_Click;
            }
            catch(Exception ex)
            {
                LogService.LogMessage("MDMSender가 시작되었습니다.");
            }
        }


        private void OpenApp_Click(object? sender, EventArgs e)
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Activate(); // 창에 포커스 설정
        }

        private void ExitApp_Click(object? sender, EventArgs e)
        {
            notifyIcon!.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            base.OnStateChanged(e);

            if (WindowState == WindowState.Minimized)
            {
                this.Hide();

                // 트레이 아이콘을 업데이트하여 알림 표시 시 문제 해결
                notifyIcon!.Icon = SystemIcons.Warning; // 임시 아이콘 설정
                notifyIcon!.Icon = SystemIcons.Information; // 원래 아이콘 복구

                notifyIcon!.BalloonTipTitle = "프로그램 알림";
                notifyIcon!.BalloonTipText = "프로그램이 백그라운드에서 동작중입니다.";
                notifyIcon!.ShowBalloonTip(100);
            }
        }
        

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                this.Hide(); // 창 숨기기 (트레이 아이콘만 남김)
                
            }
        }

        // 창 닫기 시 트레이 아이콘 리소스 해제
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            notifyIcon!.Dispose(); // 리소스 정리
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
                    System.Windows.MessageBox.Show("MDM 프로그램 시작.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
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
                    System.Windows.MessageBox.Show("MDM 프로그램 정지.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
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

            await System.Windows.Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                await MDMFunctions.AddDataAsync(new SenderModel { DateTime = "1", Query = "A" });
            });
        }

    }
}