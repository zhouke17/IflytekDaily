using CustomControls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WpfTestDemo.MyRouteEvent;
using Size = System.Windows.Size;

namespace WpfTestDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        #region 属性
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private string passWord2;

        public string PassWord2
        {
            get { return passWord2; }
            set
            {
                passWord2 = value;
                OnPropertyChanged();
            }
        }

        public string MyPassWord
        {
            get { return (string)GetValue(MyPassWordProperty); }
            set { SetValue(MyPassWordProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PassWord.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyPassWordProperty =
            DependencyProperty.Register("MyPassWord", typeof(string), typeof(MainWindow), new PropertyMetadata(""));



        public CornerRadius MyCorner
        {
            get { return (CornerRadius)GetValue(MyCornerProperty); }
            set { SetValue(MyCornerProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCorner.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCornerProperty =
            DependencyProperty.Register("MyCorner", typeof(CornerRadius), typeof(MainWindow), new PropertyMetadata(new CornerRadius(1, 1, 1, 1)));



        private ObservableCollection<ImageSource> imageList;

        public ObservableCollection<ImageSource> ImageList
        {
            get { return imageList; }
            set { imageList = value; OnPropertyChanged("ImageList"); }
        }


        public ObservableCollection<string> ImagePathList
        {
            get { return (ObservableCollection<string>)GetValue(ImagePathListProperty); }
            set { SetValue(ImagePathListProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImagePathList.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImagePathListProperty =
            DependencyProperty.Register("ImagePathList", typeof(ObservableCollection<string>), typeof(MainWindow), new PropertyMetadata(default(string)));

        #endregion

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            this.RootGrid.AddHandler(CornerButton.ClickEvent, new RoutedEventHandler(CornerButton_Click));//添加内置路由事件的路由处理程序

            this.RootGrid.AddHandler(TimeButton.ReportTimeEvent, new RoutedEventHandler(RootEvent_Listener));//添加自定义路由事件的路由处理程序

            //ImageList = new ObservableCollection<ImageSource>();
            //var selectedPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ImageList");
            //var files = Directory.GetFiles(selectedPath);
            //foreach (var item in files)
            //{
            //    var bitmap = new BitmapImage();
            //    using (var fileStream = new FileStream(item, FileMode.Open, FileAccess.Read))
            //    {
            //        bitmap.BeginInit();
            //        bitmap.CacheOption = BitmapCacheOption.OnLoad;
            //        //缩放，以减少内存占用并提高性能
            //        //bitmap.DecodePixelWidth = 100;
            //        //bitmap.DecodePixelHeight = 100;
            //        bitmap.StreamSource = fileStream;
            //        bitmap.EndInit();
            //        bitmap.Freeze();
            //    }
            //    ImageList.Add(bitmap);
            //}

            ImagePathList = new ObservableCollection<string>();
            var paths = "ImageList";
            var files = Directory.GetFiles(paths);
            foreach (var item in files)
            {
                ImagePathList.Add(item);
            }

        }
        private void Window_unLoaded(object sender, RoutedEventArgs e)
        {
            //browser?.Dispose();
            //engine?.Dispose();
        }
        private void RootEvent_Listener(object sender, RoutedEventArgs e)
        {
            var args = e as ReportTimeEventArgs;
            var originalSource = $"VisualTree start point：{(args.OriginalSource as FrameworkElement).Name}， Type is ：{args.OriginalSource.GetType().Name}";

            var source = $"LogicalTree start point：{(args.Source as FrameworkElement).Name}， Type is ：{args.Source.GetType().Name}";


            MessageBox.Show($"{args.ClickTime.ToLocalTime()}\r\n{originalSource}\r\n{source}");

        }

        private void CornerButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((e.OriginalSource as FrameworkElement).Name);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInfo(new TipsMassageBody { IsAutoClose = true, Message = "窗体加载完成", MessageType = TipsMassageType.Info }, this.TopTips);
        }

        private void ShowInfo(TipsMassageBody msg, Panel panel)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var tip = new TipBar
                {
                    Text = msg.Message,
                    IsAutoClose = msg.IsAutoClose
                };

                switch (msg.MessageType)
                {
                    case TipsMassageType.Info:
                        tip.BorderBrush = panel?.TryFindResource("Common.TipBar.Info_Border") as SolidColorBrush;
                        tip.Foreground = panel?.TryFindResource("Common.TipBar.Info_Foreground") as SolidColorBrush;
                        tip.Background = panel?.TryFindResource("Common.TipBar.Info_Background") as SolidColorBrush;
                        break;
                    case TipsMassageType.Success:
                        tip.BorderBrush = panel?.TryFindResource("Common.TipBar.Success_Border") as SolidColorBrush;
                        tip.Foreground = panel?.TryFindResource("Common.TipBar.Success_Foreground") as SolidColorBrush;
                        tip.Background = panel?.TryFindResource("Common.TipBar.Success_Background") as SolidColorBrush;
                        break;
                    case TipsMassageType.Error:
                        tip.BorderBrush = panel?.TryFindResource("Common.TipBar.Error_Border") as SolidColorBrush;
                        tip.Foreground = panel?.TryFindResource("Common.TipBar.Error_Foreground") as SolidColorBrush;
                        tip.Background = panel?.TryFindResource("Common.TipBar.Error_Background") as SolidColorBrush;
                        break;
                }

                TipBar.Show(tip, panel);
            }));
        }

        private void pwdBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            //PassWord = pwdBox.Password;
        }

        private void stackPanel_ReportTime(object sender, ReportTimeEventArgs e)
        {
            var originalSource = $"VisualTree start point：{(e.OriginalSource as FrameworkElement).Name}， Type is ：{e.OriginalSource.GetType().Name}";

            var source = $"LogicalTree start point：{(e.Source as FrameworkElement).Name}， Type is ：{e.Source.GetType().Name}";


            MessageBox.Show($"{e.ClickTime.ToLocalTime()}\r\n{originalSource}\r\n{source}");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ShowPwd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"PassWord绑定源值：{MyPassWord}");
        }

        #region 视觉同步
        private bool _isDown;
        private System.Windows.Point _relativeToBlockPosition;
        private void TestTextBlock_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            _isDown = true;
            _relativeToBlockPosition = e.MouseDevice.GetPosition(TestTextBlock);
            TestTextBlock.CaptureMouse();
        }

        private void TestTextBlock_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (_isDown)
            {
                var position = e.MouseDevice.GetPosition(Grid1);
                Canvas.SetTop(TestTextBlock, position.Y - _relativeToBlockPosition.Y);
                Canvas.SetLeft(TestTextBlock, position.X - _relativeToBlockPosition.X);
            }
        }

        private void TestTextBlock_OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            TestTextBlock.ReleaseMouseCapture();
            _isDown = false;
        }
        #endregion

        #region 控件截图 https://mp.weixin.qq.com/s/K0Hav3xRnqapNE7gfBKbgA
        private void screenShot_Click(object sender, RoutedEventArgs e)
        {
            using (var dpi = System.Drawing.Graphics.FromHwnd(IntPtr.Zero))
            {
                var bitmapSource = ToImageSource(screenShot);
                CaptureImage.Source = bitmapSource;
            }
        }
        /// <summary>
        /// Visual转图片
        /// </summary>
        public static BitmapSource ToImageSource(Visual visual, Size size, double dpiX, double dpiY)
        {
            var validSize = size.Width > 0 && size.Height > 0;
            if (!validSize) throw new ArgumentException($"{nameof(size)}值无效:${size.Width},${size.Height}");
            if (Math.Abs(size.Width) > 0.0001 && Math.Abs(size.Height) > 0.0001)
            {
                RenderTargetBitmap bitmap = new RenderTargetBitmap((int)(size.Width * dpiX), (int)(size.Height * dpiY), dpiX * 96, dpiY * 96, PixelFormats.Pbgra32);
                bitmap.Render(visual);
                return bitmap;
            }
            return new BitmapImage();
        }
        /// <summary>
        /// 兼容控件未加载情况
        /// </summary>
        /// <param name="visual"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public BitmapSource ToImageSource(Visual visual, Size size = default)
        {
            if (!(visual is FrameworkElement element))
            {
                return null;
            }
            if (!element.IsLoaded)
            {
                if (size == default)
                {
                    //计算元素的渲染尺寸
                    element.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                    element.Arrange(new Rect(new System.Windows.Point(), element.DesiredSize));
                    size = element.DesiredSize;
                }
                else
                {
                    //未加载到视觉树的，按指定大小布局
                    //按size显示，如果设计宽高大于size则按sie裁剪，如果设计宽度小于size则按size放大显示。
                    element.Measure(size);
                    element.Arrange(new Rect(size));
                }
            }
            else if (size == default)
            {
                Rect rect = VisualTreeHelper.GetDescendantBounds(visual);
                if (rect.Equals(Rect.Empty))
                {
                    return null;
                }
                size = rect.Size;
            }

            using (var dpi = Graphics.FromHwnd(IntPtr.Zero))
                return ToImageSource(visual, size, dpi.DpiX, dpi.DpiY);
        }
        /// <summary>
        /// WPF位图转换
        /// </summary>
        private static BitmapImage ToBitmapImage(BitmapSource bitmap, Size size, double dpiX, double dpiY)
        {
            MemoryStream memoryStream = new MemoryStream();
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(memoryStream);
            //可以保存至本地文件
            //using Stream stream = File.Create(imagePath);
            //encoder.Save(stream);
            memoryStream.Seek(0L, SeekOrigin.Begin);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.DecodePixelWidth = (int)(size.Width * dpiX);
            bitmapImage.DecodePixelHeight = (int)(size.Height * dpiY);
            bitmapImage.StreamSource = memoryStream;
            bitmapImage.EndInit();
            bitmapImage.Freeze();
            return bitmapImage;
        }
        #endregion
    }

    public class TipsMassageBody
    {
        public TipsMassageType MessageType { get; set; }
        public string Message { get; set; }
        public bool IsAutoClose { get; set; }

        public bool VisibleOnIndexScreen { get; set; } = true;
    }

    public enum TipsMassageType
    {
        Info,
        Success,
        Error
    }
}
