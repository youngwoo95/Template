using MDMSender.Models;
using Oracle.ManagedDataAccess.Client;
using System;
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
                await using (OracleConnection connection = new OracleConnection(ConnStr))
                {
                    await connection.OpenAsync(); // DB 연결

                    OracleCommand comm = new OracleCommand("SELECT 1 FROM DUAL", connection);
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
                await using (OracleConnection connection = new OracleConnection(CommonModel.DBConnStr))
                {
                    DataTable DBResult = new DataTable();

                    await connection.OpenAsync(); // DB 연결

                    await using OracleCommand command = new OracleCommand(query, connection);
                    
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
                await using (OracleConnection connection = new OracleConnection(CommonModel.DBConnStr))
                {
                    await connection.OpenAsync();

                    
                    OracleTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (DataRow row in UpdateDT.Rows)
                        {
                            string? Column1 = Convert.ToString(row["humanid"]);
                            DateTime GetTime = Convert.ToDateTime(row["GETTIME"]);
                            string Column2 = GetTime.ToString("yyyyMMddHHmmss");

                            if (String.IsNullOrWhiteSpace(Column1) || String.IsNullOrWhiteSpace(Column2))
                                return false;

                            // SQL 쿼리
                            string query = @"UPDATE GUNTAE_EVENT 
                                     SET SEND_YN = 'Y' 
                                     WHERE evt_time = TO_DATE(:EvtTime, 'YYYYMMDDHH24MISS') 
                                     AND human_id = :HumanId";

                            // OracleCommand로 쿼리 실행
                            await using (OracleCommand command = new OracleCommand(query, connection))
                            {
                                // 트랜잭션 설정
                                command.Transaction = transaction;

                                // 매개변수에 값 바인딩
                                command.Parameters.Add(new OracleParameter("EvtTime", Column2));
                                command.Parameters.Add(new OracleParameter("HumanId", Column1));

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
