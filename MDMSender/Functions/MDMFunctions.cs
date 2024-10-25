using MDMSender.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MDMSender.Functions
{
    public class MDMFunctions
    {
        public static async Task AddDataAsync(SenderModel newItem)
        {
            // UI 스레드에서 안전하게 데이터 추가
            await System.Windows.Application.Current.Dispatcher.InvokeAsync(() =>
            {
                if (CommonModel.lstModel.Count >= 20)
                {
                    CommonModel.lstModel.Clear(); // 20건 초과 시 Clear
                }

                CommonModel.lstModel.Add(newItem); // 새로운 데이터 추가
            });
        }
    }
}
