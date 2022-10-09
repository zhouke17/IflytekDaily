using System.Diagnostics;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Initialize();
            InitializeComponent();
        }

        #region 禁止方法缩小
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x112)
            {
                switch ((int)m.WParam)
                {
                    //禁止双击标题栏关闭窗体
                    case 0xF063:
                    case 0xF093:
                    case 0xA3:
                        m.WParam = IntPtr.Zero;
                        break;
                    //禁止双击标题栏
                    case 0xf122:
                    case 0xf012:
                        m.WParam = IntPtr.Zero;
                        break;

                        //default:
                        //    m.WParam = IntPtr.Zero;
                        //    break;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        #region 窗体阴影
        /// <summary>
        /// 界面加载
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            dwmInitialize();
            base.OnLoad(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _IsMouseDown = true;
            _location = this.Location;
            _startPoint = Control.MousePosition;
            base.OnMouseDown(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_IsMouseDown)
            {
                Point p = Control.MousePosition;
                this.Location = new Point(_location.X + p.X - _startPoint.X, _location.Y + p.Y - _startPoint.Y);
            }
            base.OnMouseMove(e);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            _IsMouseDown = false;
            base.OnMouseUp(e);
        }
        /// <summary>
        /// 界面绘制
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (dwmEnabled)
            {
                e.Graphics.Clear(Color.FromArgb(0, 0, 0, 0));
                DrawShadow(this.Size, dwmleft, e.Graphics);
            }
            RectangleF rect = new RectangleF(dwmleft - 0.5f, dwmtop - 0.5f, this.Width - dwmleft - dwmright + 0.5f, this.Height - dwmtop - dwmbottom + 0.5f);
            SolidBrush brush = new SolidBrush(this.BackColor);
            e.Graphics.FillRectangle(brush, rect);
            brush.Dispose(); brush = null;
        }
        /// <summary>
        /// 
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style = cp.Style | WS_MINIMIZEBOX;
                return cp;
            }
        }
        /// <summary>
        /// 绘制阴影效果
        /// </summary>
        /// <param name="size">控件尺寸</param>
        /// <param name="radius">阴影半径</param>
        /// <param name="g">绘图区</param>
        private void DrawShadow(Size size, int radius, Graphics g)
        {
            if (radius <= 0) return;
            int d = 2 * radius;
            #region[LinearGradientBrush]   
            ColorBlend blend = new ColorBlend();
            blend.Colors = new Color[] { Color.FromArgb(100, Color.Black), Color.FromArgb(40, Color.Black), Color.FromArgb(0, Color.Black) };
            blend.Positions = new float[] { 0, 0.4f, 1 };
            LinearGradientBrush brushleft = new LinearGradientBrush(new Point(radius, 0), new Point(0, 0), Color.FromArgb(60, Color.Black), Color.FromArgb(0, Color.Black));
            brushleft.InterpolationColors = blend;
            LinearGradientBrush brushright = new LinearGradientBrush(new Point(size.Width - radius - 1, 0), new Point(size.Width, 0), Color.FromArgb(80, Color.Black), Color.FromArgb(0, Color.Black));
            brushright.InterpolationColors = blend;
            LinearGradientBrush brushtop = new LinearGradientBrush(new Point(0, radius), new Point(0, 0), Color.FromArgb(60, Color.Black), Color.FromArgb(0, Color.Black));
            brushtop.InterpolationColors = blend;
            LinearGradientBrush brushbottom = new LinearGradientBrush(new Point(0, size.Height - radius - 1), new Point(0, size.Height), Color.FromArgb(80, Color.Black), Color.FromArgb(0, Color.Black));
            brushbottom.InterpolationColors = blend;
            #endregion
            #region[path]
            GraphicsPath pathlefttop = new GraphicsPath();
            pathlefttop.AddPie(new Rectangle(0, 0, d, d), 180, 90);
            GraphicsPath pathrighttop = new GraphicsPath();
            pathrighttop.AddPie(new Rectangle(this.Width - d, 0, d, d), 270, 90);
            GraphicsPath pathleftbottom = new GraphicsPath();
            pathleftbottom.AddPie(new Rectangle(0, this.Height - d, d, d), 90, 90);
            GraphicsPath pathrightbottom = new GraphicsPath();
            pathrightbottom.AddPie(new Rectangle(this.Width - d, this.Height - d, d, d), 0, 90);
            #endregion
            #region[PathGradientBrush]
            PathGradientBrush brushlefttop = new PathGradientBrush(pathlefttop);
            brushlefttop.CenterPoint = new Point(radius, radius);
            brushlefttop.CenterColor = Color.FromArgb(80, Color.Black);
            brushlefttop.SurroundColors = new Color[] { Color.FromArgb(0, Color.Black) };
            //brushlefttop.InterpolationColors = blend;
            PathGradientBrush brushrighttop = new PathGradientBrush(pathrighttop);
            brushrighttop.CenterPoint = new Point(this.Width - radius, radius);
            brushrighttop.CenterColor = Color.FromArgb(80, Color.Black);
            brushrighttop.SurroundColors = new Color[] { Color.FromArgb(0, Color.Black) };
            //brushrighttop.InterpolationColors = blend;
            PathGradientBrush brushleftbottom = new PathGradientBrush(pathleftbottom);
            brushleftbottom.CenterPoint = new Point(radius, this.Height - radius);
            brushleftbottom.CenterColor = Color.FromArgb(80, Color.Black);
            brushleftbottom.SurroundColors = new Color[] { Color.FromArgb(0, Color.Black) };
            //brushleftbottom.InterpolationColors = blend;
            PathGradientBrush brushrightbottom = new PathGradientBrush(pathrightbottom);
            brushrightbottom.CenterPoint = new Point(this.Width - radius, this.Height - radius);
            brushrightbottom.CenterColor = Color.FromArgb(80, Color.Black);
            brushrightbottom.SurroundColors = new Color[] { Color.FromArgb(0, Color.Black) };
            //brushrightbottom.InterpolationColors = blend;
            #endregion
            #region[draw]
            g.FillRectangle(brushleft, new RectangleF(1, radius - 0.5f, radius, this.Height - d + 0.5f));
            g.FillRectangle(brushright, new RectangleF(this.Width - radius - 1, radius - 0.5f, radius, this.Height - d + 0.5f));
            g.FillRectangle(brushtop, new RectangleF(radius - 0.5f, 0, this.Width - d + 0.5f, radius));
            g.FillRectangle(brushbottom, new RectangleF(radius - 0.5f, this.Height - radius - 1, this.Width - d + 0.5f, radius));
            g.FillPath(brushlefttop, pathlefttop);
            g.FillPath(brushrighttop, pathrighttop);
            g.FillPath(brushleftbottom, pathleftbottom);
            g.FillPath(brushrightbottom, pathrightbottom);
            #endregion
            #region[dispose]
            brushleft.Dispose(); brushleft = null;
            brushright.Dispose(); brushright = null;
            brushtop.Dispose(); brushtop = null;
            brushbottom.Dispose(); brushbottom = null;
            pathlefttop.Dispose(); pathlefttop = null;
            pathrighttop.Dispose(); pathrighttop = null;
            pathleftbottom.Dispose(); pathleftbottom = null;
            pathrightbottom.Dispose(); pathrightbottom = null;
            brushlefttop.Dispose(); brushlefttop = null;
            brushrighttop.Dispose(); brushrighttop = null;
            brushleftbottom.Dispose(); brushleftbottom = null;
            brushrightbottom.Dispose(); brushrightbottom = null;
            #endregion
        }
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            dwmleft = 5;
            dwmtop = 5;
            dwmright = 5;
            dwmbottom = 5;
        }
        /// <summary>
        /// dwm初始化
        /// </summary>
        private void dwmInitialize()
        {
            #region[dwmInitialize]
            this.dwmEnabled = true;
            int flag = 0;
            MARGINS mg = new MARGINS();
            mg.m_Left = dwmleft;
            mg.m_Top = dwmtop;
            mg.m_Right = dwmright;
            mg.m_Bottom = dwmbottom;
            //判断Vista系统
            if (System.Environment.OSVersion.Version.Major >= 6)
            {
                DwmIsCompositionEnabled(ref flag);    //检测Aero是否为打开
                if (flag > 0)
                {
                    DwmExtendFrameIntoClientArea(this.Handle, ref mg);
                }
                else
                {
                    dwmEnabled = false;
                    dwmleft = 0;
                    dwmtop = 0;
                    dwmright = 0;
                    dwmbottom = 0;
                    //MessageBox.Show("Desktop Composition is Disabled!");
                }
            }
            else
            {
                dwmEnabled = false;
                dwmleft = 0;
                dwmtop = 0;
                dwmright = 0;
                dwmbottom = 0;
                //MessageBox.Show("Please run this on Windows Vista.");
            }
            GC.Collect();
            #endregion
        }
        protected bool dwmEnabled;
        protected int dwmleft;
        protected int dwmtop;
        protected int dwmright;
        protected int dwmbottom;
        private bool _IsMouseDown;
        private Point _location;
        private Point _startPoint;
        private const int WS_MINIMIZEBOX = 0x00020000;

        /// <summary>
        /// 
        /// </summary>
        public struct MARGINS
        {
            public int m_Left;
            public int m_Right;
            public int m_Top;
            public int m_Bottom;
        };
        [DllImport("dwmapi.dll")]
        private static extern void DwmIsCompositionEnabled(ref int enabledptr);
        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern bool DwmIsCompositionEnabled();
        [DllImport("dwmapi.dll")]
        private static extern void DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margin);
        #endregion

        #region 打开Notepad.exe 并且输入文本
        private void button1_Click(object sender, EventArgs e)
        {
            OpenNotepad(this.textBox1.Text.Trim());
        }
        /// <summary>
        /// 打开Notepad.exe 并且输入文本
        /// </summary>
        /// <param name="message"></param>
        private void OpenNotepad(string message)
        {
            Process process = new Process();

            try
            {
                process.StartInfo = new ProcessStartInfo
                {
                    FileName = "Notepad.exe",
                    UseShellExecute = false, // 要重定向 IO流，Process对象必须将 UseShellExecute属性设置为false；
                    CreateNoWindow = true, // 是否以没有窗体的模式创建应用程序，默认为false，即有窗体，如为true，即隐藏窗体。在这里不设置该值也可以；
                    RedirectStandardInput = true, // 标准输入流的重定向，重定向至Process，我们可以通过Process.StandardInput.WriteLine将数据写入标准流；
                    RedirectStandardOutput = true // 与RedirectStandardInput相反，这是标准输出流的重定向，我们可以通过
                };
                process.Start();
            }
            catch (Exception ex)
            {
                process = null;
            }

            if (process != null)
            {
                while (process.MainWindowHandle == IntPtr.Zero)
                {
                    process.Refresh();
                }
                IntPtr vHandle = FindWindowEx(process.MainWindowHandle, IntPtr.Zero, "Edit", null);
                SendMessage(vHandle, 0x000C, 0, message);
            }
        }


        [DllImport("User32.DLL")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        public const uint WM_SETTEXT = 0x000C;

        [DllImport("User32.DLL")]
        public static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, string lParam);
        #endregion
    }
}
