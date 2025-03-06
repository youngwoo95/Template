using System.Configuration;
using System.Data;
using System.Windows;
using MessageBox = System.Windows.MessageBox;

namespace MDMSender
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        // 프로그램 이름으로 Mutex 검사
        private static Mutex _mutex = new Mutex(true, "MDMSender");

        protected override void OnStartup(StartupEventArgs e)
        {
            if(!_mutex.WaitOne(TimeSpan.Zero, true))
            {
                System.Windows.MessageBox.Show("프로그램이 이미 실행 중입니다.", "알림", MessageBoxButton.OK, MessageBoxImage.Information);
                
                // 프로그램 종료
                System.Windows.Application.Current.Shutdown();
                return;
            }

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            // 종료 시 mutex 해제
            _mutex.ReleaseMutex();
            base.OnExit(e);
        }


    }

}
