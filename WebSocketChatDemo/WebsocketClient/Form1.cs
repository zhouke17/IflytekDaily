using IFLYRemoteCall;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocket4Net;

namespace WebsocketClient
{
    public partial class Form1: Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);
        private static WebSocket4Net.WebSocket webSocket4Net = null;
        private IntPtr intPtr;
        ControlService controlService = null;
        public Form1()
        {
            InitializeComponent();
            string exe = @"D:\Court\icourt5.2\code\bin\iflytek.Court.Client.Host.exe";
            Process process = new Process();
            process.StartInfo.FileName = exe;
            process.Start();
            intPtr = process.Handle;
        }

        private void WebSocket4Net_MessageReceived(object sender, MessageReceivedEventArgs e)
        {
            var result = JsonConvert.DeserializeObject<responseData>(e.Message);
            //if (result.type == "viewLoadStatus" && result.code == "success")
            {
                SetParent(intPtr,this.panel_main.Handle);
            }
        }

        private void websocket_Closed(object sender, EventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void WebSocket4Net_Error(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void WebSocket4Net_Opened(object sender, EventArgs e)
        {
            MessageBox.Show("已连接服务器");
        }

        private void btn_start_Click(object sender, EventArgs e)
        {
            webSocket4Net = new WebSocket4Net.WebSocket("ws://127.0.0.1:8899");
            webSocket4Net.Opened += WebSocket4Net_Opened;
            webSocket4Net.Error += WebSocket4Net_Error;
            webSocket4Net.Closed += new EventHandler(websocket_Closed);
            webSocket4Net.MessageReceived += WebSocket4Net_MessageReceived;
            webSocket4Net.Open();
            controlService = new ControlService(webSocket4Net);
        }

        private void raiseClient_Click(object sender, EventArgs e)
        {
            Process[] processes = Process.GetProcessesByName("iflytek.Court.Client.Host");
            intPtr = processes[0].Handle;
            SetParent(intPtr, this.panel_main.Handle);
            controlService.StartClient("测试123号");
        }

        private void close_Click(object sender, EventArgs e)
        {
            controlService.CloseCourt();
        }

        private void restart_Click(object sender, EventArgs e)
        {
            controlService.RestartClient();
        }

        private void openCourt_Click(object sender, EventArgs e)
        {
            controlService.OpenCourt();
        }

        private void pauseCourt_Click(object sender, EventArgs e)
        {
            controlService.PauseCourt();
        }

        private void recoveryCourt_Click(object sender, EventArgs e)
        {
            controlService.RecoveryCourt();
        }

        private void closeCourt_Click(object sender, EventArgs e)
        {
            controlService.CloseCourt() ;
        }

        private void realtiemTransparent_Click(object sender, EventArgs e)
        {
            
        }

        private void courtState_Click(object sender, EventArgs e)
        {

        }

        private void summaryNote_Click(object sender, EventArgs e)
        {

        }

        private void fullNote_Click(object sender, EventArgs e)
        {

        }
    }
}
