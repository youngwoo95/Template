using UserControl = System.Windows.Controls.UserControl;

namespace MDMSender
{
    /// <summary>
    /// StartWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StartWindow : UserControl
    {
        public StartWindow()
        {
            InitializeComponent();

            MyListView.ItemsSource = CommonModel.lstModel;
            
        }


    }
}
