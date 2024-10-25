using MDMSender.Services;
using System.Diagnostics;
using System.IO;

namespace MDMSender.Services
{
    public static class LogService
    {
        public static Task LogMessage(string? Message)
        {
            return Task.Run(async () =>
            {
#if DEBUG
                Debugger.Break();
#endif
                try
                {
                    DateTime ThisDay = DateTime.Now;

                    // 년도 디렉터리 생성
                    string DirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SystemLog", ThisDay.Year.ToString());


                    DirectoryInfo di = new DirectoryInfo(DirectoryPath);

                    if (!di.Exists)
                    {
                        di.Create();
                    }

                    // 월 디렉터리
                    DirectoryPath = Path.Combine(DirectoryPath, ThisDay.Month.ToString());
                    di = new DirectoryInfo(DirectoryPath);

                    if (!di.Exists)
                    {
                        di.Create();
                    }


                    string FilePath = Path.Combine(DirectoryPath, $"{ThisDay.Year}_{ThisDay.Month}_{ThisDay.Day}.txt");

                    // 없으면 생성
                    using (StreamWriter sw = new StreamWriter(FilePath, true))
                    {
                        System.Diagnostics.StackTrace objStactTrace = new System.Diagnostics.StackTrace(new System.Diagnostics.StackFrame(1));
                        var s = objStactTrace.ToString();
                        await sw.WriteLineAsync($"[{ThisDay.ToString("yyyy-MM-dd HH:mm:ss")}]\t{Message}");
                    }

                }
                catch (Exception ex)
                {
                    throw;
                }
            });
        }

    }
}