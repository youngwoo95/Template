using MDMSender.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDMSender
{
    public class CommonModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 결과 바인딩 모델클래스
        /// </summary>
        public static ObservableCollection<SenderModel> lstModel = new ObservableCollection<SenderModel>();


        /// <summary>
        /// 데이터베이스 연결문자열
        /// </summary>
        public static string? DBConnStr { get; set; }


        /// <summary>
        /// 프로그램 실행시간
        /// </summary>
        private static string? _runTime;

        public static string? RunTime
        {
            get => _runTime;
            set
            {
                if(_runTime != value)
                {
                    _runTime = value;
                    OnStaticPropertyChanged(nameof(RunTime));
                }
            }
        }

        public static event PropertyChangedEventHandler? StaticPropertyChanged;

        private static void OnStaticPropertyChanged(string propertyName)
        {
            StaticPropertyChanged?.Invoke(null, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler? PropertyChanged;

    }
}
