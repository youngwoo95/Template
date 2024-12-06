using MDMSender.Models;
using MDMSender.Services;
using Microsoft.VisualBasic.ApplicationServices;
using Microsoft.Win32;
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
        DBService DBManager;

        public SettingWindow()
        {
            InitializeComponent();
            DBManager = new DBService();
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string settingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "MDMSettingPath.txt");
            if (File.Exists(settingPath))
            {
                // JSON 파일 읽기 및 역직렬화
                string? json = File.ReadAllText(settingPath);
                JObject JsonParse = JObject.Parse(json);

                txtDBIpAddress.Text = JsonParse["DBIpAddress"]?.ToString() ?? String.Empty;
                txtDBPort.Text = JsonParse["DBPort"]?.ToString() ?? String.Empty;
                txtDBUser.Text = JsonParse["DBUser"]?.ToString() ?? String.Empty;
                txtDBPW.Password = JsonParse["DBPW"]?.ToString() ?? String.Empty;
                txtDBName.Text = JsonParse["DBName"]?.ToString() ?? String.Empty;
                txtDestination.Text = JsonParse["Destination"]?.ToString() ?? String.Empty;
            }
            else
            {
                Console.WriteLine("JSON 파일이 존재하지 않습니다.");
            }
        }

        /// <summary>
        /// SETTING 파일 저장
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
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

                if (!di.Exists)
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
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
            }
        }

        private async void btnDBCheck_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
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

                //string DBConnCheck = $"Server={txtDBIpAddress.Text};Port={txtDBPort.Text};Database={txtDBName.Text};User Id={txtDBUser.Text};Password={txtDBPW.Password};Connect Timeout=30;SslMode=None;Pooling=true;Min Pool Size=2;Max Pool Size=30;";
                string DBConnCheck = $"User Id={txtDBUser.Text};Password={txtDBPW.Password};Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={txtDBIpAddress.Text})(PORT={txtDBPort.Text}))(CONNECT_DATA=(SERVICE_NAME={txtDBName.Text})));Pooling=true;Min Pool Size=2;Max Pool Size=30;";

                bool DBConnectionCheck = await DBManager.DBConnectionCheck(DBConnCheck);

                if (DBConnectionCheck)
                    MessageBox.Show("데이터베이스 연결성공.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("데이터베이스 연결실패.", "알림", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
            }
        }

      
    }
}
