using Microsoft.Office.Interop.Word;
using System;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Threading;
using System.Windows.Xps.Packaging;
using WpfApp.WordContorl;

namespace WPFApp
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        private IWord word;
        private WordMainView WordMain;
        private string fileName;
        public MainWindow()
        {
            InitializeComponent();
            WordMain = new WordMainView();
            WordHost.Child = WordMain;
            //ConvertWordToXPS(@"D:\wordTest.doc");//利用DocumentViewer加载word：无法编辑
        }

        private XpsDocument ConvertWordToXps1(string wordDocName, string xpsFilename)
        {
            XpsDocument xpsDocument = null;

            // Create a WordApplication and host word document
            ApplicationClass wordApp = new ApplicationClass();
            try
            {
                wordApp.Documents.Add(wordDocName);

                // To Invisible the word document
                wordApp.Application.Visible = false;

                // Minimize the opened word document
                wordApp.WindowState = WdWindowState.wdWindowStateMinimize;

                Microsoft.Office.Interop.Word.Document doc = wordApp.ActiveDocument;

                doc.SaveAs(xpsFilename, WdSaveFormat.wdFormatXPS);

                xpsDocument = new XpsDocument(xpsFilename, FileAccess.ReadWrite);

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("发生错误，该错误消息  " + ex.ToString());
            }
            finally
            {
                wordApp.Documents.Close();
                ((_Application)wordApp).Quit(WdSaveOptions.wdDoNotSaveChanges);
            }
            return xpsDocument;
        }

        private XpsDocument ConvertWordToXps2(string wordDocName, string xpsFilename)
        {
            Spire.Doc.Document document = new Spire.Doc.Document(wordDocName);
            document.SaveToFile(xpsFilename, Spire.Doc.FileFormat.XPS);
            document.Close();

            return new XpsDocument(xpsFilename, FileAccess.Read);
        }
        /// <summary>
        /// 将word转换为XPS文件
        /// </summary>
        /// <param name="wordDocName"></param>
        public void ConvertWordToXPS(string wordDocName)
        {
            string convertedXpsDoc = string.Concat(AppDomain.CurrentDomain.BaseDirectory, Guid.NewGuid().ToString(), ".xps");
            XpsDocument xpsDocument = ConvertWordToXps2(wordDocName, convertedXpsDoc);
            if (xpsDocument == null)
            {
                return;
            }

            //documentviewWord.Document = xpsDocument.GetFixedDocumentSequence();

        }
        /// <summary>
        /// 打开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}初始化");
            word = new Word();
            word.Init();
            Console.WriteLine($"{DateTime.Now:HH:mm:ss.ffff}初始化End");

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Word文档(*.doc;*.docx)|*.doc;*.docx";
            ofd.Title = "导入文档";
            ofd.ValidateNames = true; // 验证用户输入是否是一个有效的Windows文件名
            ofd.CheckFileExists = true; //验证路径的有效性
            ofd.CheckPathExists = true;//验证路径的有效性
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) //用户点击确认按钮，发送确认消息
            {
                fileName = ofd.FileName;
                WordMain.OpenWord(word, ofd.FileName);
            }
        }
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            word?.CloseFile(fileName);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("定时器");
        }

        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.MessageBox.Show("Window_PreviewKeyDown");
        }

        private void StackPanel_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.MessageBox.Show("StackPanel_PreviewKeyDown");
        }
        private void Grid_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.MessageBox.Show("Grid_PreviewKeyDown");
            //e.Handled = true;//阻止了文本框接受隧道事件，相应的冒泡事件将不会产生。
        }

        private void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.MessageBox.Show("Grid_KeyDown");
            e.Handled = true;//阻止冒泡事件的产生
        }
        private void StackPanel_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.MessageBox.Show("StackPanel_KeyDown");
        }
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.MessageBox.Show("Window_KeyDown");
        }
    }
}
