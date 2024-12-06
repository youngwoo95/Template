using MDMSender.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MDMSender.Services
{
    public class RESTService
    {
        private readonly HttpClient httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(10) // 타임아웃 설정
        };

        public RESTService()
        {
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<bool> SendUrl(DataTable SendDT)
        {
            bool allSuccessful = true; // 전체 작업 성공 여부 플래그

            foreach (DataRow dr in SendDT.Rows)
            {
                try
                {
                    // 임시 URL 생성
                    string Url = $"{Settings.Destination}/passing/sync/policy?targetId={dr["SABUN"]}&targetType=U&passingType=SG&passingStatus={dr["BUTTONRESULT"]}";

                    
                    int resultCode = await SendGetRequestAsync(Url);

                    // 여기에서 응답 내용을 검사하고 필요한 작업 수행
                    if (resultCode != 200)
                    {
                        allSuccessful = false; // 하나라도 실패하면 플래그를 false로 설정
                    }
                    else
                    {
                        await LogService.LogMessage($"전송완료 : {Url}");
                    }
                }
                catch (HttpRequestException ex)
                {
                    await LogService.LogMessage($"HTTP 오류: {ex.Message}");
                    allSuccessful = false; // 오류 발생 시 전체 성공 여부를 false로 설정
                }
                catch (Exception ex)
                {
                    await LogService.LogMessage($"일반 오류: {ex.Message}");
                    allSuccessful = false; // 오류 발생 시 전체 성공 여부를 false로 설정
                }
            }

            // 모든 행에 대한 요청이 완료된 후 성공 여부
            return allSuccessful;
        }


        private async Task<int> SendGetRequestAsync(string url)
        {
            try
            {
                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                HttpResponseMessage response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode(); // 응답 상태 코드 확인
                
                string? ContentResult = await response.Content.ReadAsStringAsync(); // 응답 본문 읽기
                return (int)response.StatusCode;
            }
            catch(Exception ex)
            {
                await LogService.LogMessage(ex.ToString());
                return 500;
            }
        }



    }
}
