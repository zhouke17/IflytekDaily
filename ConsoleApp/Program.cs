using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp1
{
    class Program
    {
        #region 变量
        private static DateTime audioTime;
        private static DateTime inOrOutTime;
        private static System.Threading.Timer timer2;
        #endregion

        static void Main(string[] args)
        {
            #region JObject遍历
            string res = "{  \"error_code\": 0,  \"error_msg\": \"SUCCESS\",  \"log_id\": 3014595066,  \"timestamp\": 1527234617,  \"cached\": 0,  \"result\": {    \"face_token\": \"49d58eacd7811e463429a1ae10b42173\",    \"user_list\": [      {        \"group_id\": \"NR\",        \"user_id\": \"1028867728\",        \"user_info\": \"cactus0117\",        \"score\": 97.499450683594      },      {        \"group_id\": \"BR\",        \"user_id\": \"3456378901\",        \"user_info\": \"cactus0117\",        \"score\":89.499450683594      }    ]  }}";

            JObject obj = JObject.Parse(res);

            var error_code = obj.Value<string>("error_code");
            var error_code2 = obj["error_code"].Value<string>();
            var error_code3 = obj["error_code"].ToString();
            Console.WriteLine($"三种JObject单层对象解析方式：{Environment.NewLine} error_code:{error_code}{Environment.NewLine}error_code2:{error_code2}{Environment.NewLine}error_code3:{error_code3}");


            var face_token = obj["result"]["face_token"].Value<string>();
            var face_token2 = obj["result"].Value<string>("face_token");
            var face_token3 = ((dynamic)obj).result.face_token;
            Console.WriteLine($"三种JObject双层对象解析方式：{Environment.NewLine} face_token:{face_token}{Environment.NewLine}face_token2:{face_token2}{Environment.NewLine}face_token3:{face_token3}");


            Console.WriteLine("转化成JArray后遍历");
            foreach (var item in obj["result"]["user_list"].Value<JArray>())
            {
                Console.WriteLine($"{item["group_id"].Value<string>()}");
            }
            Console.WriteLine("直接遍历");
            foreach (var item in obj["result"]["user_list"])
            {
                Console.WriteLine($"{item["group_id"].Value<string>()}");
            }
            #endregion



            #region 字典遍历异常情况
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("key1", "value1");
            try
            {
                if (dic.ContainsKey(null)) //判断指定参数(可能为null)是否包含该键值时会报异常
                {
                    Console.WriteLine("");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.WriteLine(dic["key1"].ToString());
            #endregion



            #region 定时器
            System.Timers.Timer timer = new System.Timers.Timer(1000); //设置时间间隔为1秒
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = false; //每到指定时间Elapsed事件是触发一次（false），还是一直触发（true）
            timer.Enabled = true; //是否触发Elapsed事件
            timer.Start();
            timer2 = new System.Threading.Timer(new TimerCallback(Callback), new { arg = 1 }, 1 * 1000, 2 * 1000);
            #endregion



            #region 循环遍历删除
            List<int> ints = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0 };
            for (int i = ints.Count - 1; i >= 0; --i)
            {
                Console.WriteLine($"删除的元素为：{ints[i]}");
                ints.Remove(ints[i]);
            }
            #endregion



            #region 迭代器构建
            string[] strList = { "Hello", "From", "Tutorials", "Point" };
            Collection collection = new Collection(strList);
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
            #endregion


            #region AutoResetEvent
            AutoResetEvent autoReset = new AutoResetEvent(true);

            object monitorObj = new object();
            Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    autoReset.WaitOne(2000);//等待指定的时间，超时直接进入。
                    RegularExe(() =>
                    {
                        Console.WriteLine(i);
                    },
                    ref audioTime);//委托中传值易忽略变量值不变的问题
                    Console.WriteLine(audioTime.ToString());
                    autoReset.Set();
                }
                for (int i = 0; i < 3; i++)
                {
                    if (Monitor.TryEnter(monitorObj, 1000))//尝试在指定的时间内获取锁，超时后不再等待直接离开。
                    {
                        Console.WriteLine("1获得对象的排它锁");
                        Monitor.Exit(monitorObj);
                        Console.WriteLine("1释放对象的排它锁");
                    }
                }
            });
            Task.Run(() =>
            {
                for (int i = 0; i < 10; i++)
                {
                    autoReset.WaitOne();//等待信号时长，约等于初始化时设置为True非终止状态；
                    RegularExe(() =>
                    {
                        Console.WriteLine(i);
                    }, ref inOrOutTime);//委托中传值易忽略变量值不变的问题
                    Console.WriteLine(audioTime.ToString());
                    autoReset.Set();
                }
                for (int i = 0; i < 3; i++)
                {
                    if (Monitor.TryEnter(monitorObj))
                    {
                        Console.WriteLine("2获得对象的排它锁");
                        Thread.Sleep(3000);
                        Monitor.Exit(monitorObj);
                        Console.WriteLine("2释放对象的排它锁");
                    }
                }
            });

            #endregion


            #region CancellationTokenSource
            //CancellationTokenSource cancellationToken1 = new CancellationTokenSource();
            //CancellationTokenSource cancellationToken2 = new CancellationTokenSource();

            //Task.Run(() =>
            //{
            //    while (!cancellationToken1.IsCancellationRequested)
            //    {
            //    }
            //}, cancellationToken1.Token);
            //Console.WriteLine("超时停止方式?");
            //Console.ReadLine();
            //cancellationToken1.CancelAfter(TimeSpan.FromSeconds(3));
            //Console.WriteLine("Task已停止");


            //Task.Run(() =>
            //{
            //    while (!cancellationToken2.IsCancellationRequested)
            //    {
            //    }
            //}, cancellationToken2.Token);
            //Console.WriteLine("Cancel停止方式?");
            //Console.ReadLine();
            //cancellationToken2.Cancel();
            //Console.WriteLine("Task已停止");

            #endregion


            #region task异常捕获
            Task.Run(() =>
            {
                try
                {
                    Console.WriteLine("异常Task开始执行");
                    throw new Exception("task有异常");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("捕获到Task异常");
                }

            });

            //throw new Exception();
            #endregion



            #region 匹配时间戳字符串
            string HEX_REG = @"[0-9a-fA-F]";
            string GUID_REG = "^(" + HEX_REG + "{8}(-" + HEX_REG + "{4}){3}-" + HEX_REG + "{12})?$";
            string timeReg = DateTime.Now.ToString("yyyyMMdd");
            Regex regex = new Regex(GUID_REG);
            var testStr = "nihao8214D33C-6CFE-4043-83E2-615A1A2B99C8";
            var timeStr = "nihao20220830165131983";
            //var res1 = regex.IsMatch(testStr);

            if (timeStr.Contains(timeReg))
            {
                var index = timeStr.IndexOf(timeReg);
            }

            var str1 = "本地20220830183500848.png";
            var int1 = str1.IndexOf(DateTime.Now.ToString("yyyyMMdd"));
            //var str2 = str1.Substring(0, str1.IndexOf(DateTime.Now.ToString("yyyyMMdd")));

            var str3 = "{\"success\":null,\"msg\":null,\"data\":null}";
            var res1 = JObject.Parse(str3);
            var res2 = res1["success"].Value<string>();
            Console.WriteLine(res2 == "null");
            Console.WriteLine(str1.Substring(0, str1.IndexOf(".")));

            //DeleteIsRunning(@"D:/绩效自评.png");
            File.Delete(@"D:/1.png");
            #endregion


            #region params
            ParamsMethod(1, 2, 3);
            #endregion


            #region Reflect
            Type type = Type.GetType("ConsoleApp1.CustomClass");
            dynamic dynamic = Activator.CreateInstance(type);
            dynamic.Method();
            #endregion


            #region UTC
            Console.WriteLine($"localTime:{DateTime.Now},ToUniversalTime:{DateTime.Now.ToUniversalTime()},ToLocalTime:{DateTime.Now.ToUniversalTime().ToLocalTime()}");
            #endregion


            #region ContinueWith问题
            Task.Factory.StartNew(async () =>
            {
                await Task.Run(async () =>
                {
                    await Task.Delay(3000);
                });
                Console.WriteLine("Task任务——后执行");
            }).ContinueWith((t) =>
            {
                Console.WriteLine("Task延续任务——先执行");
            });
            #endregion 总结：StartNew类型的延续任务在遇到await关键字时直接执行。


            #region TaskCompletionSource实现IO操作
            Task.Run(async () =>
            {
                var taskCompleteRresult = await DownloadStringAsync(new Uri("https://www.baidu.com"));
                Console.WriteLine(taskCompleteRresult);
            });
            #endregion

            Console.Read();
        }

        public static Task<string> DownloadStringAsync(Uri url)
        {
            var tcs = new TaskCompletionSource<string>();
            var webClient = new WebClient();
            webClient.DownloadStringCompleted += (s, e) =>
            {
                if (e.Error != null)
                {
                    tcs.TrySetException(e.Error);
                }
                else if (e.Cancelled)
                {
                    tcs.TrySetCanceled();
                }
                else
                {
                    tcs.TrySetResult(e.Result);
                }
            };
            webClient.DownloadStringAsync(url);
            return tcs.Task;
        }

        private static void Callback(object state)
        {
            Console.WriteLine("Time定时器");
            dynamic dynamic = state;
            Console.WriteLine($"Time传入值：{dynamic.arg}");
            new Thread(() => { timer2.Dispose(); }).Start();
        }

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer定时器");
        }

        private static void RegularExe(Action action, ref DateTime Time)
        {
            action?.Invoke();
            Time = DateTime.Now;
        }

        static void ParamsMethod(params object[] args)
        {
            Console.WriteLine(args[0]);
            foreach (var item in args)
            {
                Console.WriteLine(item);
            }
        }

    }
}

public class Collection
{
    private readonly string[] str;

    public Collection(string[] str)
    {
        this.str = str;
    }

    public IEnumerator GetEnumerator()
    {
        for (int i = 0; i < str.Length; i++)
        {
            yield return str[i];
        }
    }
}
