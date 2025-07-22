using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsocketClient
{
    public class responseData
    {
        public string type { get; set; }
        public string  message { get; set; }
        public string code { get; set; }
    }
    public static class OperationHelper
    {

        public enum OperationType
        {
            processControl,
            courtControl
        }
        public enum ProcessControlType
        {
            [Description("准备")]
            Ready,//准备
            [Description("拉起")]
            Start,//拉起
            [Description("关闭")]
            Closed,//关闭
            [Description("重启")]
            Restart//重启
        }

        public enum ControlCourtType
        {
            [Description("准备")]
            Ready,//准备
            [Description("开庭")]
            Begin,//开庭
            [Description("休庭")]
            Suspend,//休庭
            [Description("再次开庭")]
            Recovery,//再次开庭
            [Description("闭庭")]
            End,//闭庭
            [Description("插入转写")]
            Transform,//插入转写
            [Description("停止转写")]
            StopTransform,//停止转写
            [Description("快速定位")]
            Follow,//快速定位
            [Description("转撤诉")]
            CheSu = 11,
            [Description("转调解")]
            TiaoJie
        }
        public class StartMessage
        {
            /// <summary>
            /// 案件号
            /// </summary>
            [JsonProperty("caseName")]
            public string CaseName { get; set; }
            /// <summary>
            /// 实时转写笔录（pgs=1）
            /// </summary>
            [JsonProperty("timesSpeak")]
            public bool IsTimesSpeak { get; set; }
            /// <summary>
            /// 庭审数据
            /// </summary>
            [JsonProperty("caseData")]
            public string CaseData { get; set; }
            /// <summary>
            /// 本地模板url
            /// </summary>
            [JsonProperty("templateUrl")]
            public string TemplateUrl { get; set; }

        }
    }
}
