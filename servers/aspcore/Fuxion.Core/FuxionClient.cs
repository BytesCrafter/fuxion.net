using System;
using System.Threading;
using WebSocketSharp;

namespace Fuxion.Client
{
    public class FuxionClient
    {
        public void Connect(string ipadd, int port, string channel = "chat", string uname = "", Action<WebSocket> instance = null)
        {
            using (var ws = new WebSocket($"ws://{ipadd}:{port}/{channel}/?name={uname}"))
            {
                ws.OnMessage += (sender, e) =>
                    Console.WriteLine("Client says: " + e.Data);

                instance(ws);
                ws.Connect();

                // To enable the Per-message Compression extension.
                ws.Compression = CompressionMethod.Deflate;

                // To change the wait time for the response to the WebSocket Ping or Close.
                ws.WaitTime = TimeSpan.FromSeconds (5);

                while (ws.IsAlive)
                {
                    Thread.Sleep(1000);
                    ws.Ping("");
                }
            }
        }
    }
}
