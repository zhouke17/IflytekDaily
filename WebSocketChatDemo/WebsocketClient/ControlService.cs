using IFLYRemoteCall;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WebsocketClient
{
    public class ControlService
    {
        WebSocket4Net.WebSocket webSocket4Net = null;
        public ControlService(WebSocket4Net.WebSocket webSocket4Net) 
        {
            this.webSocket4Net = webSocket4Net;
        }

        /// <summary>
        /// 消息发送
        /// </summary>
        /// <param name="msg">消息</param>
        private void Send(string msg)
        {
            webSocket4Net?.Send(msg);
        }

        /// <summary>
        /// 拉起客户端
        /// </summary>
        /// <param name="caseName">案件号</param>
        /// <param name="timesSpeak">是否实时推送</param>
        public void StartClient(string caseName, bool timesSpeak = false, string caseData = null)
        {
            //MessageBox.Show("拉起方法执行!");
            try
            {
                var msg = MessageHelper.ProcessControl(1, caseName, timesSpeak, caseData);
                Send(msg);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            //MessageBox.Show("拉起方法结束!");
        }

        /// <summary>
        /// 关闭客户端
        /// </summary>
        public void CloseClient()
        {
            //MessageBox.Show("关闭方法执行!");
            var msg = MessageHelper.ProcessControl(2);
            Send(msg);
            //MessageBox.Show("关闭方法结束!");
        }

        /// <summary>
        /// 重启客户端
        /// </summary>
        public void RestartClient()
        {
            //MessageBox.Show("重启方法执行!");
            var msg = MessageHelper.ProcessControl(3);
            Send(msg);
            //MessageBox.Show("重启方法结束!");
        }

        /// <summary>
        /// 开庭
        /// </summary>
        public void OpenCourt()
        {
            //MessageBox.Show("开始发送开庭消息！");
            var msg = MessageHelper.CourtControl(1);
            Send(msg);
            //MessageBox.Show("发送开庭消息结束！");
        }

        /// <summary>
        /// 休庭
        /// </summary>
        public void PauseCourt()
        {
            //MessageBox.Show("开始发送休庭消息！");
            var msg = MessageHelper.CourtControl(2);
            Send(msg);
            //MessageBox.Show("发送休庭消息结束！");
        }

        /// <summary>
        /// 再次开庭
        /// </summary>
        public void RecoveryCourt()
        {
            //MessageBox.Show("开始发送复庭消息！");
            var msg = MessageHelper.CourtControl(3);
            Send(msg);
            //MessageBox.Show("发送复庭消息结束！");
        }

        /// <summary>
        /// 闭庭
        /// </summary>
        public void CloseCourt()
        {
            //MessageBox.Show("开始发送闭庭消息！");
            var msg = MessageHelper.CourtControl(4);
            Send(msg);
            //MessageBox.Show("发送闭庭消息结束！");
        }
    }
    public class MessageHelper
    {
        public static string ProcessControl(int type, string caseName = null, bool isTimeSpeak = false, string caseData = null)
        {
            return JsonConvert.SerializeObject(new
            {
                operation = "processControl",
                controlType = type,
                caseName = caseName,
                timesSpeak = isTimeSpeak,
                caseData = caseData
            });
        }

        public static string CourtControl(int type)
        {
            return JsonConvert.SerializeObject(new
            {
                operation = "courtControl",
                controlType = type
            });
        }
    }
}
