using SuperSocket.SocketBase;
using System.Windows;

namespace SuperSocketWpf
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public MainWindow()
        {
            InitializeComponent();
            InitSuperSocket();
        }

        private void InitSuperSocket()
        {
            var appServer = new AppServer();
            if (!appServer.Setup(2023))
            {
                Log.Error("服务监听的端口：【2023】被占用！");
                textBlock.Text = "服务监听的端口：【2023】被占用！";
                return;
            }

            if (!appServer.Start())
            {
                Log.Error("服务开启失败！");
                textBlock.Text = "服务开启失败!";
            }

            appServer.NewRequestReceived += AppServer_NewRequestReceived;
            appServer.NewSessionConnected += AppServer_NewSessionConnected;
            appServer.SessionClosed += AppServer_SessionClosed;
        }

        private void AppServer_SessionClosed(AppSession session, CloseReason value)
        {
            App.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                textBlock.Text = "已关闭连接!";
            }));
        }

        private void AppServer_NewSessionConnected(AppSession session)
        {
            App.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                textBlock.Text = "开启连接!";
            }));
        }

        private void AppServer_NewRequestReceived(AppSession session, SuperSocket.SocketBase.Protocol.StringRequestInfo requestInfo)
        {
            App.Current.Dispatcher.Invoke(new System.Action(() =>
            {
                var key = requestInfo.Key;
                var body = requestInfo.Body;

                textBlock.Text += key;

                session.Send(key);
            }));
        }
    }
}
