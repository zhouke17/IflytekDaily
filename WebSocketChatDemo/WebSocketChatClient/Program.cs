using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketChatClient
{
    class Program
    {
        private static WebSocket4Net.WebSocket webSocket4Net = null;
        static void Main(string[] args)
        {
            webSocket4Net = new WebSocket4Net.WebSocket("ws://127.0.0.1:1234");
            webSocket4Net.Opened += WebSocket4Net_Opened;
            webSocket4Net.Error += WebSocket4Net_Error;
            webSocket4Net.Closed += new EventHandler(websocket_Closed);
            webSocket4Net.MessageReceived += WebSocket4Net_MessageReceived;
            webSocket4Net.Open();
        }

        private static void WebSocket4Net_MessageReceived(object sender, WebSocket4Net.MessageReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void websocket_Closed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void WebSocket4Net_Error(object sender, SuperSocket.ClientEngine.ErrorEventArgs e)
        {
            throw new NotImplementedException();
        }

        private static void WebSocket4Net_Opened(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
