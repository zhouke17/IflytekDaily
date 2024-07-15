using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace AsyncForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private static void TaskInfos(string name = "")
        {
            //获取正在运行的线程
            Thread thread = Thread.CurrentThread;
            //设置线程的名字
            //thread.Name = name;//只能设置一次
            Console.WriteLine(thread.Name);
            Console.WriteLine(name);
            //获取当前线程的唯一标识符
            int id = thread.ManagedThreadId;
            //获取当前线程的状态
            System.Threading.ThreadState state = thread.ThreadState;
            //获取当前线程的优先级
            ThreadPriority priority = thread.Priority;
            string strMsg = string.Format("Thread ID:{0}\n" + "Thread Name:{1}\n" +
                "Thread State:{2}\n" + "Thread Priority:{3}\n", id, thread.Name,
                state, priority);
            Console.WriteLine(strMsg);
            //执行耗时间耗资源的任务
            Console.WriteLine(DateTime.Now);
        }

        #region 异步同步执行(async,await 推荐安全的调用方式)

        private async void button1_Click(object sender, EventArgs e)
        {
            var res1 = await Task.Run(() => func());//防止func内部线程阻塞导致调用线程被阻塞，即安全的调用方式
            this.BeginInvoke(new Action(() =>
            {
                this.listBox1.Items.Add(res1);
            }));

            //var res2 = await func();//默认切换调用线程的上下文(func内部有可能被阻塞，从而导致主线程阻塞)
            //this.listBox1.Items.Add(res2);

            //var res3 = await func().ConfigureAwait(false);//不切换到调用线程的上下文(是否尝试将延续任务封送回原始上下文)
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            //this.BeginInvoke(new Action(() => { this.listBox1.Items.Add(res3); }));//可能不是主线程id,故使用BeginInvoke
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            //var res4 = func();
            //this.listBox1.Items.Add(res4.Result);//阻塞式调用获取异步结果，导致死锁
            //Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());

            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
        }

        public async Task<int> func()
        {
            Console.WriteLine("func开始执行");
            Thread.Sleep(TimeSpan.FromSeconds(3)); //func内部线程阻塞导致调用线程被阻塞
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            await Task.Delay(TimeSpan.FromSeconds(3));
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId.ToString());
            Console.WriteLine("func结束执行");
            return DateTime.Now.Day;
        }

        #endregion 异步同步执行(async,await 推荐安全的调用方式)

        #region TaskCompletionSource 实现异步同步执行

        private async void button2_Click(object sender, EventArgs e)
        {
            Debug.WriteLine($"TaskCompleteSource start!");
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

        #endregion TaskCompletionSource 实现异步同步执行

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

        #endregion AutoResetEvent 异步转同步 防止死锁

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

        #endregion 线程安全的字典

        #region 信号量异步

        private async void button5_Click(object sender, EventArgs e)
        {
            int value;
            value = await GetNextValueAsync();
            Console.WriteLine(value);
        }

        private int asyncValue;
        private SemaphoreSlim mutex = new SemaphoreSlim(1);

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

        #endregion 信号量异步

        #region Monitor用户态锁实现超时检测

        private object monitorObj = new object();

        private void Monitor_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                if (System.Threading.Monitor.TryEnter(monitorObj, 5000))
                {
                    if (System.Threading.Monitor.Wait(monitorObj, 5000))
                    {
                        Console.WriteLine("5秒内成功获取到锁");
                    }
                    else
                    {
                        Console.WriteLine("5秒内未获取到锁");
                    }
                    Thread.Sleep(5000);
                    System.Threading.Monitor.Exit(monitorObj);
                }
            });
        }

        private void MonitorPulseAll_Click(object sender, EventArgs e)
        {
            if (System.Threading.Monitor.TryEnter(monitorObj, 2000))
            {
                System.Threading.Monitor.PulseAll(monitorObj);
                Console.WriteLine("通知等待monitorObj锁的所有线程进入就绪队列，依次获取锁后执行");
                System.Threading.Monitor.Exit(monitorObj);
            }
        }

        private void MonitorWait_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                System.Threading.Monitor.Enter(monitorObj);
                System.Threading.Monitor.Wait(monitorObj);
                Console.WriteLine("获取到monitorObj锁");
                System.Threading.Monitor.Exit(monitorObj);
            });
        }

        #endregion Monitor用户态锁实现超时检测

        #region AutoResetEvent 例子

        private void button9_Click(object sender, EventArgs e)
        {
            // 创建AutoResetEvent对象
            AutoResetEvent resetEvent = new AutoResetEvent(false);
            // 创建计时器Thread3
            Thread thread3 = new Thread(() =>
            {
                // 等待5秒钟，然后释放AutoResetEvent
                Thread.Sleep(5000);
                resetEvent.Set();
            });

            // 创建生产者Thread1,用于生产产品
            Thread producerThread = new Thread(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    resetEvent.WaitOne();
                    Console.WriteLine("Producer produced product #" + i);
                    resetEvent.Set();
                    /*  resetEvent.WaitOne();*/ // 等待消费者Thread2完成操作后才能继续生产产品
                }
            });

            // 创建消费者Thread2,用于消费产品
            Thread consumerThread = new Thread(() =>
            {
                // 在消费者线程开始时，等待生产者线程生产完所有产品后释放AutoResetEvent
                //resetEvent.WaitOne();

                for (int i = 0; i < 10; i++)
                {
                    resetEvent.WaitOne();
                    Console.WriteLine("Consumer consumed product #" + i);
                    resetEvent.Set();
                }

                // 当AutoResetEvent被释放时，通知生产者线程可以继续生产产品了
                //resetEvent.Set();
            });

            // 启动生产者Thread1和消费者Thread2,以及计时器Thread3
            producerThread.Start();
            consumerThread.Start();
            thread3.Start();
        }

        #endregion AutoResetEvent 例子

        #region Parallel并行

        private void button10_Click(object sender, EventArgs e)
        {
            //fromInclusive：起始索引，包括在内。
            //toExclusive：结束索引，不包括在内。
            //body：每个迭代的操作，使用当前迭代的索引作为参数。
            Parallel.For(0, 5, i => { Console.WriteLine("i=" + i); TaskInfos(); });
            //source：要迭代的集合。
            //body：每个元素的操作，使用当前元素作为参数。
            Parallel.ForEach(new string[] { "0", "1", "2", "3", "4" }, j => { Console.WriteLine("j=" + j); TaskInfos(); });

            // 将json转化为 JArray
            var jArray = JArray.Parse("[1, 2, 3]");
            Console.WriteLine(jArray[0]);// 输出：1
            var list = jArray.ToObject<List<int>>();//父类JToken的方法
            Console.WriteLine(list[0]); // 输出：1

            // 将json转化为JObject、JToken 
            var jObject = JObject.Parse(@"{""name"":""John Smith"",""age"":30}");
            var age = jObject.Value<int>("age");
            Console.WriteLine(age); // 输出：30
            var jToken = JToken.Parse(@"{""name"":""John Smith"",""age"":30}");
            var name = jToken["name"].Value<string>();
            Console.WriteLine(name);

            string res = "{\"success\":\"0\",\"msg\":\"\",\"data\":[{\"id\":null,\"ah\":null,\"ahdm\":null,\"litigantId\":null,\"litigantXh\":null,\"name\":\"李四\",\"ssdw\":\"被告\",\"telephone\":\"\",\"licenceId\":\"\",\"address\":null,\"litigantType\":\"法人\",\"litigantTypeCode\":null},{\"id\":null,\"ah\":null,\"ahdm\":null,\"litigantId\":null,\"litigantXh\":null,\"name\":\"张三\",\"ssdw\":\"原告\",\"telephone\":\"13111111111\",\"licenceId\":\"1234\",\"address\":null,\"litigantType\":\"自然人\",\"litigantTypeCode\":null}]}";
            var obj = (dynamic)JObject.Parse(res);
            //var obj = JObject.Parse(res).ToObject<dynamic>();//与上同理

            var obj2 = JObject.Parse(res);

            List<string> list2 = new List<string>();
            foreach (var item in obj.data)
            {
                string name2 = item.name;
                list2.Add(name2);
            }
            list2.Clear();
            foreach (var item in JArray.Parse(obj.data.ToString()))
            {
                string name2 = item.name;
                list2.Add(name2);
            }

            list2.Clear();
            foreach (var item in obj2["data"].Value<JArray>())
            {
                list2.Add(item.Value<string>("name"));
            }
            list2.Clear();
            foreach (var item in (JArray)obj2["data"])
            {
                list2.Add(item.Value<string>("name"));
            }
        }

        #endregion Parallel并行

        private void button11_Click(object sender, EventArgs e)
        {
            AutoResetEvent resetEvent = new AutoResetEvent(true);
            Mutex mutex = new Mutex();
            for (int i = 0; i < 3; i++)
            {
                resetEvent.WaitOne();
                //mutex.WaitOne();
                Thread newThread = new Thread(() =>
                {
                    TaskInfos($"Thread{i + 1}");
                });
                newThread.Start();
                resetEvent.Set();
                //mutex.ReleaseMutex();
            }
        }

        #region 系统崩溃

        private void 内存溢出_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();

            for (int i = 0; i < int.MaxValue; i++)
            {
                list.Add(string.Join(",", Enumerable.Range(0, 10000)));
            }
        }

        private void cpu爆炸_Click(object sender, EventArgs e)
        {
            Parallel.For(0, int.MaxValue, (i) =>
             {
                 while (true)
                 {
                 }
             });
        }

        #endregion 系统崩溃

        private async void button6_Click(object sender, EventArgs e)
        {
            var cancelAbleleTask = new CancelableThreadTask(LongRunningWork);
            using (CancellationTokenSource cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromMilliseconds(3000)))
            {
                try
                {
                    await cancelAbleleTask.RunAsync(cancellationTokenSource.Token);
                }
                catch (ThreadInterruptedException ex)
                {
                    Console.WriteLine("长时间运行任务已被取消");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"发生异常：{ex}");
                }
            };
        }

        private void LongRunningWork()
        {
            Console.WriteLine("开始运行长时间任务");
            Thread.Sleep(10000);
            Console.WriteLine("结束运行长时间任务");
        }
    }

    public class CancelableThreadTask
    {
        private Thread _thread;
        private TaskCompletionSource<bool> taskCompletionSource;
        private readonly Action action;
        private int isRunnig = 0;

        public CancelableThreadTask(Action action)
        {
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            if (Interlocked.CompareExchange(ref isRunnig, 1, 0) == 1)
            {
                throw new InvalidOperationException("Task is already running!");
            }

            taskCompletionSource = new TaskCompletionSource<bool>();
            _thread = new Thread(() =>
            {
                try
                {
                    action?.Invoke();
                    taskCompletionSource.SetResult(true);
                }
                catch (Exception ex)
                {
                    if (ex is ThreadInterruptedException)
                    {
                        taskCompletionSource.TrySetCanceled();
                    }
                    else
                    {
                        taskCompletionSource.TrySetException(ex);
                    }
                }
                finally
                {
                    Interlocked.Exchange(ref isRunnig, 0);
                }
            });

            cancellationToken.Register(() =>
            {
                if (Interlocked.CompareExchange(ref isRunnig, 0, 1) == 1)
                {
                    _thread.Interrupt();
                    _thread.Join();
                    taskCompletionSource.TrySetCanceled(cancellationToken);
                }
            });

            _thread.Start();
            return taskCompletionSource.Task;
        }
    }

    /// <summary>
    /// AutoResetEvent
    /// </summary>
    public class AutoResetEventEx
    {
        /// <summary>
        /// 内部的设置状态 true 不等待信号 false 等待信号
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