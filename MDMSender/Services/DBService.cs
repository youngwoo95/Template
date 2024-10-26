using MDMSender.Models;
using MySqlConnector;
using System.Data;

namespace MDMSender.Services
{
    public class DBService
    {
        public DBService()
        {
            // DB 연결문자열
            //CommonModel.DBConnStr = $"Server={Settings.DBIpAddress};Port={Settings.DBPort};Database={Settings.DBName};User Id={Settings.DBUser};Password={Settings.DBPW};Connect Timeout=30;SslMode=None;";
        }

        /// <summary>
        /// 데이터베이스 연결검사
        /// </summary>
        /// <param name="ConnStr"></param>
        /// <returns></returns>
        public async Task<bool> DBConnectionCheck(string ConnStr)
        {
            try
            {
                await using (MySqlConnection connection = new MySqlConnection(ConnStr))
                {
                    await connection.OpenAsync(); // DB 연결

                    MySqlCommand comm = new MySqlCommand("SELECT 1", connection);
                    var result = await comm.ExecuteScalarAsync();

                    if (result != null && result.ToString() == "1")
                        return true;
                    else
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
        /// 데이터베이스 연결
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable?> DBSelectQuery(string query)
        {
            try
            {
                await using (MySqlConnection connection = new MySqlConnection(CommonModel.DBConnStr))
                {
                    DataTable DBResult = new DataTable();

                    await connection.OpenAsync(); // DB 연결

                    await using MySqlCommand command = new MySqlCommand(query, connection);
                    
                    // 비동기 Reader
                    await using var reader = await command.ExecuteReaderAsync();

                    DBResult.Load(reader);

                    return DBResult;
                }
            }
            catch (Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// 데이터베이스 업데이트
        /// </summary>
        /// <param name="UpdateDT"></param>
        /// <returns></returns>
        public async Task<bool> DBUpdateQuery(DataTable UpdateDT)
        {
            try
            {
                await using (MySqlConnection connection = new MySqlConnection(CommonModel.DBConnStr))
                {
                    await connection.OpenAsync();

                    using var transaction = await connection.BeginTransactionAsync();
                    try
                    {
                        foreach (DataRow row in UpdateDT.Rows)
                        {
                            string? Column1 = Convert.ToString(row["Title"]);
                            string? Column2 = Convert.ToString(row["CurrentDT"]);

                            if (String.IsNullOrWhiteSpace(Column1) || String.IsNullOrWhiteSpace(Column2))
                                return false;

                            // 한건식 적용하기 위함.
                            string query = "UPDATE temptable SET Title = @Title, CurrentDT = @CurrentDT WHERE Title = @ID";

                            await using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                            {
                                // 매개변수에 값 할당
                                command.Parameters.AddWithValue("@Title", Column1);
                                command.Parameters.AddWithValue("@CurrentDT", DateTime.Now);
                                command.Parameters.AddWithValue("@ID", Column1); // 조건 컬럼

                                // 쿼리 실행
                                await command.ExecuteNonQueryAsync();
                            }
                        }

                        // 모든 업데이트가 성공적으로 완료되면 커밋
                        await transaction.CommitAsync();
                        return true;
                    }
                    catch
                    {
                        // 예외 발생 시 롤백
                        await transaction.RollbackAsync();
                        return false;
                    }
                }
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
                return false;
            }
        }



    }
}
