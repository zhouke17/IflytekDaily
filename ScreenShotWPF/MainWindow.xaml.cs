using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace ScreenShotWPF
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        int picIndex = 0;
        public MainWindow()
        {
            InitializeComponent();
            GetScreenShot();
        }


        private void pic_Click(object sender, EventArgs e)
        {
            ++picIndex;
            switch (picIndex)
            {
                case 1:
                    pic.Image = Properties.Resources._1;
                    break;
                case 2:
                    pic.Image = Properties.Resources._2;
                    break;
                case 3:
                    pic.Image = Properties.Resources._3;
                    break;
                case 4:
                    pic.Image = Properties.Resources._4;
                    break;
                case 5:
                    pic.Image = Properties.Resources._5;
                    break;
                case 6:
                    pic.Image = Properties.Resources._6;
                    break;
                case 7:
                    pic.Image = Properties.Resources._7;
                    picIndex = 0;
                    break;
                default:
                    break;
            }
        }

        private void GetScreenShot()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(20);
            timer.Tick += (s, e) =>
            {
                var windowIntPtr = new IntPtr(winForm.Handle.ToInt32());
                //1
                //var bitmapImage = GetShotCutFromWinApi(windowIntPtr);
                //ScreenShot.Source = ConvertToBitmapImage(bitmapImage);
                //2
                ScreenShot.Source = GetShotCutFromGraphic();
            };
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rectangle rect);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleDC(IntPtr hdc);
        [DllImport("gdi32.dll")]
        private static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);
        [DllImport("gdi32.dll")]
        private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll")]
        private static extern int DeleteDC(IntPtr hdc);
        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, int nFlags);
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowDC(IntPtr hwnd);

        /// <summary>
        /// 根据句柄通过windowsApi获取截图
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static Bitmap GetShotCutFromWinApi(IntPtr hWnd)
        {
            var hscrdc = GetWindowDC(hWnd);
            var windowRect = new Rectangle();
            GetWindowRect(hWnd, ref windowRect);
            int width = Math.Abs(windowRect.Width - windowRect.X);
            int height = Math.Abs(windowRect.Height - windowRect.Y);
            var hbitmap = CreateCompatibleBitmap(hscrdc, width, height);
            var hmemdc = CreateCompatibleDC(hscrdc);
            SelectObject(hmemdc, hbitmap);
            PrintWindow(hWnd, hmemdc, 0);
            var bmp = Image.FromHbitmap(hbitmap);
            DeleteDC(hscrdc);
            DeleteDC(hmemdc);
            return bmp;
        }


        public static BitmapImage GetShotCutFromGraphic()
        {
            ImageCodecInfo[] encoders = ImageCodecInfo.GetImageEncoders();
            ImageCodecInfo ParseEncoder = (from item in encoders where item.MimeType == "image/jpeg" select item).FirstOrDefault();
            EncoderParameters EncoderParams = new EncoderParameters(1);
            EncoderParams.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 50);//截图压缩质量[0-100]，值越小压缩后图片越小
            // 获取整个屏幕的大小
            Rectangle screenBounds = Screen.PrimaryScreen.Bounds;
            // 创建一个和屏幕大小相同的bitmap对象
            using (Bitmap bmp = new Bitmap(screenBounds.Width, screenBounds.Height))
            {
                // 使用GDI+的Graphics对象拷贝整个屏幕图像到bitmap对象中
                using (Graphics gfx = Graphics.FromImage(bmp))
                {
                    gfx.CopyFromScreen(screenBounds.X, screenBounds.Y, 0, 0, bmp.Size, CopyPixelOperation.SourceCopy);
                    using (MemoryStream sm = new MemoryStream())
                    {
                        bmp.Save(sm, ImageFormat.Bmp);
                        sm.Position = 0;
                        BitmapImage result = new BitmapImage();
                        result.BeginInit();
                        result.CacheOption = BitmapCacheOption.OnLoad;
                        result.StreamSource = sm;
                        result.EndInit();
                        result.Freeze();
                        return result;
                    }
                }
            }
        }

        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
        public static BitmapSource ConvertToBitMapSource(Bitmap bitmap)
        {
            IntPtr intPtrl = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(intPtrl,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(intPtrl);
            return bitmapSource;
        }
        public static BitmapImage ConvertToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Bmp);
                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
    }
}
