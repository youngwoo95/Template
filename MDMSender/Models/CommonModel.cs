using MDMSender.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDMSender
{
    public class CommonModel
    {
        /// <summary>
        /// 결과 바인딩 모델클래스
        /// </summary>
        public static ObservableCollection<SenderModel> lstModel = new ObservableCollection<SenderModel>();
    }
}
