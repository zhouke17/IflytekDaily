using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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

        #region 异步同步执行
        private async void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 5; i++)
            {
                var res = await Task.Run(() => Form1.func());
                this.BeginInvoke(new Action(() => { this.listBox1.Items.Add(res); }));
            }
        }

        public static Task<int> func()
        {
            Console.WriteLine("func开始执行");
            Task.Run(() => Thread.Sleep(2000));
            Console.WriteLine("func结束执行");
            return Task.FromResult(2);
        }
        #endregion



        #region TaskCompletionSource 实现异步同步执行
        private async void button2_Click(object sender, EventArgs e)
        {
            var result = await AwaitByTaskCompleteSource(TestWithResultAsync);
            Debug.WriteLine($"TaskCompleteSource end:{result}");
        }
        private static async Task<string> TestWithResultAsync()
        {
            Debug.WriteLine("1. 异步任务start……");
            await Task.Delay(2000);
            Debug.WriteLine("2. 异步任务end……");
            return "执行了2秒时间";
        }
        private async Task<string> AwaitByTaskCompleteSource(Func<Task<string>> func)
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            Task.Run(async () =>
            {
                var result = await func.Invoke();
                taskCompletionSource.SetResult(result);//一般设置在单独的线程进行结果回调，类似于EventWaitHandle
            }
            );
            return await taskCompletionSource.Task;
        }
        #endregion



        #region AutoResetEvent 异步转同步 防止死锁
        private void button3_Click(object sender, EventArgs e)
        {
            AwaitUsingAutoResetEvent(TestAsync());
        }

        public void AwaitUsingAutoResetEvent(Task task)
        {
            AutoResetEvent autoResetEvent = new AutoResetEvent(false);

            task.ContinueWith(t =>
            {
                autoResetEvent.Set();
            });
            Task.Run(() =>
            {
                autoResetEvent.WaitOne();
                Debug.WriteLine("AwaitAutoResetEvent end");
            });
        }
        private static async Task TestAsync()
        {
            Debug.WriteLine("异步任务start……");
            await Task.Delay(2000);
            Debug.WriteLine("异步任务end……");
        }

        #endregion


        #region 线程安全的字典
        private static readonly object lockobj = new object();
        private void button4_Click(object sender, EventArgs e)
        {
            var list1 = new List<int>();
            ConcurrentDictionary<int, string> keyValuePairs = new ConcurrentDictionary<int, string>();
            for (int i = 0; i < 10; i++)
            {
                Task.Run(() =>
                {
                    //lock (lockobj)
                    {
                        keyValuePairs.TryAdd(i, i.ToString());
                    }
                });
            }
            Thread.Sleep(3000);
            foreach (var item in keyValuePairs)
            {
                Console.WriteLine($"输出：键-{item.Key}，值-{item.Value}");
            }
        }
        #endregion

        private async void button5_Click(object sender, EventArgs e)
        {
            int value;
            value = await GetNextValueAsync();

        }
        public async Task<int> GetNextValueAsync()
        {
            return await Task.FromResult(new Random().Next(1, 100));
        }
    }
}
