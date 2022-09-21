using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var res = await Task.Run(() => Form1.func());
            this.BeginInvoke(new Action(() => { this.listBox1.Items.Add(res); }));
        }

        public static async Task<int> func()
        {
            Console.WriteLine("func开始执行");
            await Task.Run(() => Thread.Sleep(2000));
            Console.WriteLine("func结束执行");
            return 2;
        }
    }
}
