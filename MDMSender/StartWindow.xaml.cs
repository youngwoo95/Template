using MDMSender.Models;
using MDMSender.Services;
using MySqlConnector;
using Newtonsoft.Json.Linq;
using System.Data;
using System.IO;

using UserControl = System.Windows.Controls.UserControl;

namespace MDMSender
{
    /// <summary>
    /// StartWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StartWindow : UserControl
    {
        DBService DBManager;

        public StartWindow()
        {
            InitializeComponent();

            MyListView.ItemsSource = CommonModel.lstModel;
            DBManager = new DBService();
        }

        /// <summary>
        /// 프로그램 세팅파일 읽기
        /// </summary>
        /// <returns></returns>
        public async Task<bool> LoadSettingFiles()
        {
            try
            {
                // 설정파일 읽기
                string settingPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Settings", "MDMSettingPath.txt");
                if (File.Exists(settingPath))
                {
                    // JSON 파일 읽기 및 역직렬화
                    string? json = File.ReadAllText(settingPath);
                    JObject JsonParse = JObject.Parse(json);

                    Settings.DBIpAddress = JsonParse["DBIpAddress"]?.ToString() ?? String.Empty;
                    Settings.DBPort = JsonParse["DBPort"]?.ToString() ?? String.Empty;
                    Settings.DBUser = JsonParse["DBUser"]?.ToString() ?? String.Empty;
                    Settings.DBPW = JsonParse["DBPW"]?.ToString() ?? String.Empty;
                    Settings.DBName = JsonParse["DBName"]?.ToString() ?? String.Empty;
                    Settings.Destination = JsonParse["Destination"]?.ToString() ?? String.Empty;


                    if (String.IsNullOrWhiteSpace(Settings.DBIpAddress) ||
                        String.IsNullOrWhiteSpace(Settings.DBPort) ||
                        String.IsNullOrWhiteSpace(Settings.DBUser) ||
                        String.IsNullOrWhiteSpace(Settings.DBPW) ||
                        String.IsNullOrWhiteSpace(Settings.DBName) ||
                        String.IsNullOrWhiteSpace(Settings.Destination))
                    {
                        Console.WriteLine("연결문자열 읽기 실패");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine(CommonModel.DBConnStr);
                        CommonModel.DBConnStr = $"Server={Settings.DBIpAddress};Port={Settings.DBPort};Database={Settings.DBName};User Id={Settings.DBUser};Password={Settings.DBPW};Connect Timeout=30;SslMode=None;Pooling=true;Min Pool Size=2;Max Pool Size=30;";
                        return true;
                    }
                }
                else
                {
                    Console.WriteLine("JSON 파일이 존재하지 않습니다.");
                    return false;
                }
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// DB 연결 검사
        /// </summary>
        /// <returns></returns>
        public async Task<bool> DBConnection()
        {
            try
            {
                if (String.IsNullOrWhiteSpace(CommonModel.DBConnStr))
                    return false;

                bool DBConnectionCheck = await DBManager.DBConnectionCheck(CommonModel.DBConnStr);
                if (DBConnectionCheck)
                    return true;
                else
                    return false;
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
                return false;
            }
        }

        /// <summary>
        /// 조회결과 SELECT
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable?> SelectTable()
        {
            try
            {
                string query = $"SELECT * FROM temptable";
                
                DataTable? ResultDT = await DBManager.DBSelectQuery(query);

                if (ResultDT != null)
                    return ResultDT;
                else
                    return null;
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
                return null;
            }
        }
        
        public async Task<bool> UpdateTable(DataTable UpdateDT)
        {
            try
            {
                if (UpdateDT.Rows.Count > 0)
                {
                    bool UpdateResult = await DBManager.DBUpdateQuery(UpdateDT);
                    return UpdateResult;
                }
                else
                    return false;
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
                return false;
            }
        }


    }
}
