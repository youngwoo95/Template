using LiveCharts;
using LiveCharts.Wpf;
using MDMSender.Models;
using Microsoft.VisualBasic.ApplicationServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

using UserControl = System.Windows.Controls.UserControl;

namespace MDMSender
{
    /// <summary>
    /// Setting.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class SettingWindow : UserControl
    {
        public SettingWindow()
        {
            InitializeComponent();

        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            txtDBIpAddress.Text = Settings.DBIpAddress ?? String.Empty;
            txtDBPort.Text = Settings.DBPort ?? String.Empty;
            txtDBUser.Text = Settings.DBUser ?? String.Empty;
            txtDBPW.Password = Settings.DBPW ?? String.Empty;
            txtDBName.Text = Settings.DBName ?? String.Empty;
            txtDestination.Text = Settings.Destination ?? String.Empty;

            // 파이 차트에 데이터 추가
            pieChart.Series = new SeriesCollection
            {
                new PieSeries
                {
                    Title = "SUCCESS",
                    Values = new ChartValues<double> { 55 },
                    DataLabels = false,
                },
                new PieSeries
                {
                    Title = "FAIL",
                    Values = new ChartValues<double> { 15 },
                    DataLabels = false,
                },
            };
        }

        /// <summary>
        /// SETTING 파일 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtDBIpAddress.Text) ||
                String.IsNullOrWhiteSpace(txtDBPort.Text) ||
                String.IsNullOrWhiteSpace(txtDBUser.Text) ||
                String.IsNullOrWhiteSpace(txtDBPW.Password) ||
                String.IsNullOrWhiteSpace(txtDBName.Text) ||
                String.IsNullOrWhiteSpace(txtDestination.Text))
            {
                MessageBox.Show("입력형식이 잘못되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Settings.DBIpAddress = txtDBIpAddress.Text;
            Settings.DBPort = txtDBPort.Text;
            Settings.DBUser = txtDBUser.Text;
            Settings.DBPW = txtDBPW.Password;
            Settings.DBName = txtDBName.Text;
            Settings.Destination = txtDestination.Text;

            JObject SettingObj = new JObject();
            SettingObj.Add("DBIpAddress", Settings.DBIpAddress); // IP주소
            SettingObj.Add("DBPort", Settings.DBPort); // PORT
            SettingObj.Add("DBUser", Settings.DBUser); // USER
            SettingObj.Add("DBPW", Settings.DBPW); // PW
            SettingObj.Add("DBName", Settings.DBName); // NAME
            SettingObj.Add("Destination", Settings.Destination);


            string SettingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings");
            
            DirectoryInfo di = new DirectoryInfo(SettingPath);

            if(!di.Exists)
            {
                di.Create();
            }

            string FilePath = Path.Combine(SettingPath, "MDMSettingPath.txt");

            // 없으면 생성
            using (StreamWriter sw = new StreamWriter(FilePath, false))
            {
                System.Diagnostics.StackTrace objStackTrace = new System.Diagnostics.StackTrace(new System.Diagnostics.StackFrame(1));
                var s = objStackTrace.ToString();
                await sw.WriteLineAsync(SettingObj.ToString());

                MessageBox.Show("저장이 완료되었습니다.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

    }
}
