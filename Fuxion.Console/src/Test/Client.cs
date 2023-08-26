using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Fuxion
{
    public class Client
    {
        public void Connect()
        {
            using (var ws = new WebSocket("ws://10.12.0.2:8000/Chat/?name=nobita"))
            {
                /*
                ws.OnMessage += (sender, e) =>
                Console.WriteLine("Laputa says: " + e.Data);
                */

                ws.Connect();


                while (ws.IsAlive)
                {
                    Thread.Sleep(100);
                    ws.Send("Keep in mind that WebSocket performance and limits can vary between different WebSocket libraries, browsers, and server configurations. If you're dealing with specific requirements or scenarios, it's recommended to perform testing to determine the optimal approach for your use case.");
                }

                Console.ReadKey(true);
            }
        }
    }
}
