using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DumpWinForm
{
    public partial class Form1 : Form
    {
        static List<List<string>> memoryLeakList = new List<List<string>>();
        public Form1()
        {
            InitializeComponent();
            Task.Run(() => { Init(); });
        }

        private void Init()
        {
            //编写一段死循环代码用于演示内存泄漏的情况
            //每次迭代都会创建一个新的List<string>对象并将其添加到一个静态的List<List<string>>集合中，但却没有释放这些对象，从而导致内存泄漏
            //while (true)
            //{
            //    var newList = new List<string>();
            //    {
            //        var currentValue = Guid.NewGuid().ToString();
            //        richTextBox1.Text += currentValue;
            //        newList.Add(currentValue);
            //    }
            //    memoryLeakList.Add(newList);
            //}


            HogeHoge("hoge-");
        }

        static void HogeHoge(string s)
        {
            HogeHoge(s);
        }
    }
}
