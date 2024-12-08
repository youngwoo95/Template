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
                // DB 연결
                await using (OracleConnection connection = new OracleConnection(CommonModel.DBConnStr))
                {
                    await connection.OpenAsync();

                    // 트랜잭션 시작
                    await using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            foreach (DataRow row in UpdateDT.Rows)
                            {
                                // DataTable에서 값 추출
                                string? Column1 = row["HUMAN_ID"] as string;


                                if (!DateTime.TryParse(row["GETTIME"]?.ToString(), out DateTime GetTime))
                                {
                                    Console.WriteLine("GETTIME이 유효하지 않습니다.");
                                    return false; // 유효하지 않은 GETTIME
                                }

                                string Column2 = GetTime.ToString("yyyyMMddHHmmss");

                                if (string.IsNullOrWhiteSpace(Column1) || string.IsNullOrWhiteSpace(Column2))
                                {
                                    Console.WriteLine("매개변수 Column1과 Column2가 유효하지 않습니다.");
                                    return false; // 유효하지 않은 데이터
                                }

                                // SQL 쿼리
                                string query = @"UPDATE GUNTAE_EVENT 
                                         SET SEND_YN = 'Y' 
                                         WHERE evt_time = TO_DATE(:EvtTime, 'YYYYMMDDHH24MISS') 
                                         AND human_id = :HumanId";

                                // OracleCommand로 쿼리 실행
                                await using (OracleCommand command = new OracleCommand(query, connection))
                                {
                                    command.Transaction = transaction;

                                    // 매개변수 바인딩
                                    command.Parameters.Add(new OracleParameter("EvtTime", Column2));
                                    command.Parameters.Add(new OracleParameter("HumanId", Column1));

                                    // 쿼리 실행
                                    int affectedRows = await command.ExecuteNonQueryAsync();
                                    if (affectedRows == 0)
                                    {
                                        Console.WriteLine($"No rows affected for humanid={Column1} and evt_time={Column2}");
                                    }
                                }
                            }

                            // 모든 업데이트가 성공적으로 완료되면 커밋
                            await transaction.CommitAsync();
                            return true;
                        }
                        catch (Exception ex)
                        {
                            // 예외 발생 시 트랜잭션 롤백
                            await transaction.RollbackAsync();
                            Console.WriteLine($"Transaction rolled back due to error: {ex.Message}");
                            await LogService.LogMessage(ex.ToString());
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 최상위 예외 처리
                Console.WriteLine($"Unhandled exception: {ex.Message}");
                await LogService.LogMessage(ex.ToString());
                return false;
            }
        }


    }
}
