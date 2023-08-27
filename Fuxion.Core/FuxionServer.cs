
using System;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

namespace Fuxion.Server
{
    public class FuxionServer
    {
        public void StartServer(string webip, int webport)
        {
            //Instantiate WebSocketServer using the arguments passed.
            var wssv = new WebSocketServer($"ws://{webip}:{webport}");

#if DEBUG   //Set debug level to trace for checking error.
            wssv.Log.Level = LogLevel.Trace;
#endif
            // To provide the HTTP Authentication (Basic/Digest).
            wssv.AuthenticationSchemes = AuthenticationSchemes.Anonymous;
            wssv.Realm = "WebSocket Test";
            wssv.UserCredentialsFinder = id => {
                var name = id.Name;

                Console.WriteLine($"Connected: {name}");
                //return new NetworkCredential("asdasd", "password", "gunfighter");
                // Return user name, password, and roles.
                return name == "nobita"
                       ? new NetworkCredential(name, "password", "gunfighter")
                       : null; // If the user credentials are not found.
            };

            // Add the WebSocket services.
            wssv.AddWebSocketService<FuxionObject>("/master");
            wssv.AddWebSocketService<FuxionObject>("/match");
            wssv.AddWebSocketService<FuxionObject>("/chat");
            wssv.AddWebSocketService<FuxionObject>("/game");

            // Start the server.
            wssv.Start();

            //Monitor the server.
            if (wssv.IsListening)
            {
                Console.WriteLine("Listening on port {0}:{1}, and providing WebSocket services:", wssv.Address, wssv.Port);

                foreach (var path in wssv.WebSocketServices.Paths)
                    Console.WriteLine("- {0}", path);
            }

            Console.ReadLine();
            wssv.Stop();

            Console.WriteLine("WebSocket server halted...");
        }
    }
}
