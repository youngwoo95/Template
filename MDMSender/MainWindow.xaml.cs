using MDMSender.Functions;
using MDMSender.Models;
using MDMSender.Services;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace MDMSender
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isRunning = false; // 타이머 실행 여부
        private Task? timerTask = null; // 현재 실행 중인 타이머 작업

        private NotifyIcon? notifyIcon;

        Stopwatch stopwatch = new Stopwatch();
        
        // FORM_1
        StartWindow? StartForm;
        RESTService? RestService;

        public MainWindow()
        {
            InitializeComponent();
            InitializeTrayIcon();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //RightStackPanel.Content = new StartWindow();
            Console.WriteLine("몇번실행?");
            StartForm = new StartWindow();
            RestService = new RESTService();
            RightStackPanel.Content = StartForm;
        }

        private void InitializeTrayIcon()
        {
            try
            {
                notifyIcon = new NotifyIcon
                {
                    Icon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\run_ico.ico")),
                    Visible = true,
                    Text = "MDMSender_V02"
                };

                notifyIcon.ContextMenuStrip = new ContextMenuStrip();
                notifyIcon.ContextMenuStrip.Items.Add("Open", null, OpenApp_Click);
                notifyIcon.ContextMenuStrip.Items.Add("Exit", null, ExitApp_Click);

                notifyIcon.DoubleClick += OpenApp_Click;
            }
            catch(Exception ex)
            {
                LogService.LogMessage(ex.ToString());
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
                //notifyIcon!.Icon = SystemIcons.Info; // 임시 아이콘 설정
                notifyIcon!.Icon = new Icon(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Images\run_ico.ico")); // 원래 아이콘 복구

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
            System.Windows.Application.Current.Shutdown();
        }

        private async void btnSetting_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isRunning)
                {
                    // 타이머 중지
                    stopwatch.Stop();
                    TimeSpan TimeDate = stopwatch.Elapsed;
                    CommonModel.RunTime = $"{TimeDate.Days}일 {TimeDate.Hours}시간 {TimeDate.Minutes}분 {TimeDate.Seconds}초";
                    await StopTimerAsync(); // 비동기적으로 타이머 중지
                }

                await LogService.LogMessage("설정화면 진입");
                RightStackPanel.Content = new SettingWindow();
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
            }
        }


        private async void btnStart_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //RightStackPanel.Content = new StartWindow();
                RightStackPanel.Content = StartForm;

                if (!isRunning && (timerTask == null || timerTask.IsCompleted))
                {
                    bool isLoad = await StartForm!.LoadSettingFiles();
                    if (!isLoad)
                    {
                        System.Windows.MessageBox.Show("DB 연결파일이 없습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    bool isDBConnection = await StartForm.DBConnection();
                    if(!isDBConnection)
                    {
                        System.Windows.MessageBox.Show("DB에 연결할 수 없습니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    
                    await LogService.LogMessage("MDM SENDER 시작");

                    stopwatch.Start();

                    await StartTimerAsync();
                }
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
            }
        }

        private void btnHome_Click(object sender, RoutedEventArgs e)
        {
            //RightStackPanel.Content = new StartWindow();
            RightStackPanel.Content = StartForm;
            
        }

        private async void btnStop_Click(object sender, RoutedEventArgs e)
        {
            if (isRunning)
            {
                await LogService.LogMessage("MDM SENDER 종료");

                // 타이머 중지
                stopwatch.Stop();
                TimeSpan TimeDate = stopwatch.Elapsed;
                CommonModel.RunTime = $"{TimeDate.Days}일 {TimeDate.Hours}시간 {TimeDate.Minutes}분 {TimeDate.Seconds}초";

                await StopTimerAsync(); // 비동기적으로 타이머 중지
            }
        }
        
        private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        /// <summary>
        /// 비동기 타이머 시작 메서드
        /// </summary>
        private async Task StartTimerAsync()
        {
            try
            {
                if (isRunning) return; // 중복 실행 방지

                isRunning = true; // 타이머 실행 중 플래그 설정
                cancellationTokenSource = new CancellationTokenSource(); // 새로운 토큰 생성

                timerTask = Task.Run(async () =>
                {
                    System.Windows.MessageBox.Show("MDM 프로그램 시작.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);

                    while (!cancellationTokenSource.Token.IsCancellationRequested)
                    {
                        try
                        {
                            await DoWorkAsync(); // 작업 수행
                            await Task.Delay(TimeSpan.FromSeconds(10), cancellationTokenSource.Token); // 취소 가능한 지연
                        }
                        catch (TaskCanceledException)
                        {
                            // 취소된 작업에 대한 예외 무시
                            System.Diagnostics.Debug.WriteLine("타이머 작업이 취소되었습니다.");
                        }
                    }
                }, cancellationTokenSource.Token);

                // 타이머 작업 종료 대기
                await timerTask;
            }
            catch (TaskCanceledException)
            {
                // 취소된 작업에 대한 최상위 예외 처리
                System.Diagnostics.Debug.WriteLine("타이머 작업이 정상적으로 종료되었습니다.");
            }
            catch (Exception ex)
            {
                // 다른 예외 처리
                await LogService.LogMessage($"StartTimerAsync 예외 발생: {ex}");
            }
            finally
            {
                isRunning = false; // 타이머 종료 상태 설정
            }
        }

        /// <summary>
        /// 비동기 타이머 종료 메서드
        /// </summary>
        private async Task StopTimerAsync()
        {
            try
            {
                if (isRunning)
                {
                    cancellationTokenSource.Cancel(); // 타이머 중지 요청

                    if (timerTask != null) // 타이머가 null이 아닐 때만 대기
                    {
                        try
                        {
                            await timerTask; // 타이머 작업 종료 대기
                        }
                        catch (TaskCanceledException)
                        {
                            // 타이머 작업이 정상적으로 취소된 경우 처리
                            System.Diagnostics.Debug.WriteLine("타이머 작업이 취소되었습니다.");
                        }
                        catch (Exception ex)
                        {
                            // 예기치 않은 예외 처리
                            await LogService.LogMessage($"타이머 작업 중 오류: {ex}");
                        }
                        finally
                        {
                            isRunning = false; // 타이머 상태 초기화
                            System.Windows.MessageBox.Show("MDM 프로그램 정지.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        // 타이머가 이미 종료되었거나 초기화되지 않은 경우
                        System.Diagnostics.Debug.WriteLine("타이머 작업이 실행 중이 아닙니다.");
                        isRunning = false;
                    }
                }
            }
            catch (Exception ex)
            {
                // 최상위 예외 처리
                await LogService.LogMessage($"StopTimerAsync 예외 발생: {ex}");
            }
        }

        /// <summary>
        /// 실제 수행할 비동기 작업
        /// </summary>
        private async Task DoWorkAsync()
        {
            try
            {
                // 비동기
                DataTable? ResultDT = await StartForm!.SelectTable();

                if (ResultDT is not null)
                {
                    await MDMFunctions.AddDataAsync(new SenderModel { DateTime = $"[{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}]", Query = $"\t 테스트중입니다.\t {ResultDT.Rows.Count}" });
                    if (ResultDT.Rows.Count > 0)
                    {
                        await LogService.LogMessage($"{DateTime.Now} \t\t {ResultDT.Rows.Count}");
                    }
                    // 테스트용 주석
                    /*
                    bool UpdateResult = await StartForm.UpdateTable(ResultDT);

                    if(UpdateResult)
                    {
                        // 여기서 HTTP 전송
                        bool SendResult = await RestService!.SendUrl(ResultDT);

                        if (SendResult)
                        {
                            // UI에 반영해야하는것들은 여기서.
                            await System.Windows.Application.Current.Dispatcher.InvokeAsync(async () =>
                            {
                                foreach (DataRow dr in ResultDT.Rows)
                                {
                                    await MDMFunctions.AddDataAsync(new SenderModel { DateTime = $"[{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}]", Query = $"\t {dr["Title"]}" });
                                }
                            });
                        }
                        else
                        {
                            await MDMFunctions.AddDataAsync(new SenderModel { DateTime = $"[{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}]", Query = $"\t 데이터 전송에 실패했습니다." });
                        }
                    }
                    */
                }
                else // 전송할 데이터가 없을때
                {
                    await System.Windows.Application.Current.Dispatcher.InvokeAsync(async () =>
                    {
                        await MDMFunctions.AddDataAsync(new SenderModel { DateTime = $"[{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}]", Query = $"\t 전송할 데이터가 없습니다." });
                    });
                }
            }
            catch(Exception ex)
            {

                await MDMFunctions.AddDataAsync(new SenderModel { DateTime = $"[{DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss")}]", Query = $"\t {ex.Message}" });
                await LogService.LogMessage(ex.ToString());
            }
        }

     
    }
}