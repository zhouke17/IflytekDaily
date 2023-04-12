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
            Console.WriteLine(value);
        }
        private int asyncValue;
        SemaphoreSlim mutex = new SemaphoreSlim(1);
        public async Task<int> GetNextValueAsync()
        {
            await mutex.WaitAsync().ConfigureAwait(false);
            try
            {
                asyncValue = await Task.FromResult(asyncValue++);
            }
            catch (Exception ex)
            { }
            finally
            {
                mutex.Release();
            }
            return asyncValue;
        }

        private object monitorObj = new object();
        private void button6_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (Monitor.TryEnter(monitorObj, 2000))
                {
                    if (Monitor.Wait(monitorObj, 2000))
                    {
                        Console.WriteLine("2秒内成功获取到锁");
                    }
                    else
                    {
                        Console.WriteLine("2秒内未获取到锁");
                    }
                    Monitor.Exit(monitorObj);
                }
            });

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(monitorObj, 2000))
            {
                Monitor.PulseAll(monitorObj);
                Console.WriteLine("通知等待monitorObj锁的所有线程进入就绪队列，依次获取锁后执行");
                Monitor.Exit(monitorObj);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Monitor.Enter(monitorObj);
            Monitor.Wait(monitorObj);//阻塞当前线程
            Monitor.Exit(monitorObj);
        }

        private SpinLock _spinLock = new SpinLock();
        int incrValue = 0;
        private void button9_Click(object sender, EventArgs e)
        {
            spinLock();
        }

        private object lockObj = new object();
        private void spinLock()
        {
            bool locked = false;
            //_spinLock.Enter(ref locked);//获取锁
            if (Monitor.TryEnter(lockObj))
            {
                incrValue++;  //安全的逻辑计算
                Console.WriteLine(incrValue);
                //if (locked) //释放锁
                Monitor.Exit(lockObj);
                //_spinLock.Exit();
            }
        }

        private void Start()
        {
            var task = new Task(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                Console.WriteLine("task.start()");
            });
            task.Start();
            task.Wait();
        }

        private void Run()
        {
            var task = Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(3));
                Console.WriteLine("task.run()");
            });
            task.Wait();
        }

        bool lockTaken = false;
        private void button10_Click(object sender, EventArgs e)
        {
            if (Monitor.TryEnter(lockObj))
            {
                Thread.Sleep(10000);
            }
        }
    }

    /// <summary>
    /// 自己的写的AutoResetEvent
    /// </summary>
    public class AutoResetEventEx
    {
        /// <summary>
        /// 内部的设置状态 
        /// true  不等待信号
        /// false 等待信号
        /// </summary>
        private bool _initialState = false;

        /// <summary>
        /// 内部锁
        /// </summary>
        private object _objLock = new object();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="initialState">内部的设置状态</param>
        public AutoResetEventEx(bool initialState)
        {
            this._initialState = initialState;
        }

        /// <summary>
        /// 等待一个信号
        /// </summary>
        public void WaitOne()
        {
            if (!this._initialState)
            {
                lock (this._objLock)
                {
                    Monitor.Wait(this._objLock);
                }
            }
        }

        /// <summary>
        /// 发送一个信号
        /// </summary>
        public void Set()
        {
            if (!this._initialState)
            {
                this._initialState = true;

                lock (this._objLock)
                {
                    Monitor.PulseAll(this._objLock);
                }
            }
        }
    }
}
