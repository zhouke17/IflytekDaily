using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace SoundCard
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var url = $"ws://172.29.100.138:9696/ts-service/ast/kezhou3-1-01?bizId=2564&devId=kezhou3-1-01&lastFrame=1416&caseNo=2564&courtId=kezhou3&meeting=&sr=16000&bps=16&fs=3200&actionName=rest-ast-chin-v20@tuling-ast&scene=online_ast&micId=101";
            //var url = $"ws://172.29.100.138:9696/ts-service/ast/kezhou3-1-01?bizId=2564&devId=kezhou3-1-01&lastFrame=1416&caseNo=2564&courtId=kezhou3&meeting=&sr=16000&bps=16&fs=3200&actionName=local.iat.actionName&scene=online_ast&micId=101";
            //var url = $"ws://172.29.100.138:9696/ts-service/ast/193-1-02?bizId=2563&devId=193-1-02&lastFrame=20296&caseNo=2563&courtId=193&meeting=&sr=16000&bps=16&fs=2560&actionName=rest-ast-cant-v20@tuling-ast&micId=102";
            //var args = $"userId=34291470&userName=主审1法官&conferenceId=12345{j}&caseNo=123.139.184.72-f922921bc7c349ecb3b3a154d3d05a4b";
            //var args = $"/ts-service/ast/{soundCardMac}-1-{micId}?bizId=11441&devId={soundCardMac}-1-{micId}&lastFrame=0&caseNo=11441&courtId={soundCardMac}&meeting=&sr=16000&bps=16&fs=2560&actionName=rest-ast-chin-v20@tuling-ast&micId={micId}&groupId=dad0b15f-69aa-45ca-a593-12103a795236";
            using (var ws = new WebSocketSharp.WebSocket(url))
            //using (var ws = new WebSocketSharp.WebSocket("ws://127.0.0.1:8087/ts-service/v1/ast/?"+args))
            //using (var ws = new WebSocketSharp.WebSocket("ws://10.103.61.61:8086/ts-service/v1/ast/?"+args))
            //using (var ws = new WebSocketSharp.WebSocket("ws://172.31.234.123:9401/ts-service/ast/?"+args))
            //using (var ws = new WebSocketSharp.WebSocket("ws://58.242.248.66/ts-service/ast/?"+args))
            //using (var ws = new WebSocketSharp.WebSocket("ws://172.31.97.233:8087/ts-service/ast/?" + args))
            {
                ws.OnMessage += (sender, e) =>
                {
                    //Console.Write(e.Data);
                    var root = JsonConvert.DeserializeObject<RootObject>(e.Data);
                    //Console.WriteLine(e.Data);
                    var a = root.ws.Select(w => w.cw[0].w).Aggregate((x, y) => x + y);
                    //Console.WriteLine($"{DateTime.Now}: {a}");
                    if (root.msgtype == "sentence")
                    {
                        Console.Write(a);
                    }
                };

                ws.OnClose += (sender,closeEventArgs) =>
                {
                    Console.WriteLine($"断开连接 {DateTime.Now},原因：{closeEventArgs.Reason}");
                };

                ws.Connect();


                //while (!ws.IsAlive)
                //{
                //	Console.WriteLine(ws.ReadyState);
                //	Thread.Sleep(1000);
                //	//ws.Connect();
                //}


                // 创建一个内存流对象ms，用于存储录制的音频数据
                MemoryStream ms = new MemoryStream();

                // 创建一个WaveInEvent对象waveInEvent，用于从麦克风捕获音频
                // 设置它的采样率为22050Hz，声道数为1，设备编号为0（默认设备）
                var waveInEvent = new NAudio.Wave.WaveInEvent();
                waveInEvent.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
                waveInEvent.DeviceNumber = 0;

                // 创建一个WaveFileWriter对象waveFileWriter，用于将捕获的音频数据写入内存流ms
                // 设置它的波形格式与waveInEvent相同
                // 注意:在这里需要用NAudio.Utils.IgnoreDisposeStream来包装MemoryStream。因为避免在释放waveFileWriter时，同时释放内存流ms，从而导致无法播放录制的音频,如果这里用的不是MemoryStream,是普通的文件流(FileStream),千万不要用"NAudio.Utils.IgnoreDisposeStream"来包装，这样的话会导致无法保存录音文件到磁盘上。
                //var waveFileWriter = new NAudio.Wave.WaveFileWriter(@"d:\1.wav", waveInEvent.WaveFormat);
                var waveFileWriter = new NAudio.Wave.WaveFileWriter(new NAudio.Utils.IgnoreDisposeStream(ms), waveInEvent.WaveFormat);
                // 为waveInEvent添加DataAvailable事件处理程序
                // 当有音频数据可用时，调用waveFileWriter的Write方法，将数据写入内存流ms
                waveInEvent.DataAvailable += (_s, _e) =>
                {
                    ws.Send(Convert.ToBase64String(_e.Buffer));
                    Console.WriteLine(_e.Buffer.Length);
                    waveFileWriter.Write(_e.Buffer, 0, _e.BytesRecorded);
                };

                // 为waveInEvent添加RecordingStopped事件处理程序
                // 当录制停止时，释放waveFileWriter和waveInEvent对象，并将内存流ms的位置重置为0
                waveInEvent.RecordingStopped += (_s, _e) =>
                {
                    waveFileWriter.Dispose();
                    waveInEvent.Dispose();
                    ms.Position = 0;

                    // 创建一个WaveOutEvent对象waveOutEvent，用于播放内存流ms中的音频数据
                    var waveOutEvent = new NAudio.Wave.WaveOutEvent();

                    // 创建一个WaveFileReader对象waveReader，用于从内存流ms中读取音频数据
                    var waveReader = new NAudio.Wave.WaveFileReader(ms);

                    // 调用waveOutEvent的Init方法，将waveReader作为输入源
                    waveOutEvent.Init(waveReader);

                    // 调用waveOutEvent的Play方法，开始播放音频
                    waveOutEvent.Play();
                };

                // 调用waveInEvent的StartRecording方法，开始录制音频
                waveInEvent.StartRecording();

                // 在控制台输出提示信息，并等待用户输入
                Console.WriteLine("正在录音，按回车键停止。");
                Console.ReadLine();

                // 调用waveInEvent的StopRecording方法，停止录制并触发RecordingStopped事件
                waveInEvent.StopRecording();

                // 在控制台输出提示信息，并等待用户输入
                Console.WriteLine("按回车键退出");
                Console.ReadLine();
            }
        }


        public class Cw
        {
            public string sc { get; set; }
            public string w { get; set; }
            public string wb { get; set; }
            public string we { get; set; }
            public string wc { get; set; }
            public string wp { get; set; }
            public string sf { get; set; }
        }

        public class Ws
        {
            public string bg { get; set; }
            public List<Cw> cw { get; set; }
        }

        public class RootObject
        {
            public string segId { get; set; }
            public string bg { get; set; }
            public string ed { get; set; }
            public string rl { get; set; }
            public string ei { get; set; }
            public string ls { get; set; }
            public string metadata { get; set; }
            public string msgtype { get; set; }
            public string sn { get; set; }
            public string pa { get; set; }
            //	public Vad vad { get; set; }
            public List<Ws> ws { get; set; }
            public string speakerItem { get; set; }
        }
        // You can define other methods, fields, classes and namespaces here
    }
}
