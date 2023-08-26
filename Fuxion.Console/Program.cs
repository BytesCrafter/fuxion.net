
using Fuxion;
using Fuxion.Utilities;
using Fuxion.Demo;

using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;

class Program 
{
    static void Main(string[] args) 
    {
        if (args.Length >= 1 && args[0]=="client") 
        {
            int clients = 100;
            for (int i = 0; i < clients; i++)
            {
                Client client = new Client();
                client.Connect();
            }
        } 
        
        else
        {
           Program.Process(args);
        }
    }

    protected static void Process(string[] strings) 
    {
        // Prepare the arguments for running the websocket server.
        string[] commands = strings; // StringUtil.Shift(strings, 1);
        string webip = commands.Length >= 1 ? commands[0] : System.Net.IPAddress.Loopback.ToString();
        int webport = commands.Length >= 2 ? Int32.Parse(commands[1]) : 8000;

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
        wssv.AddWebSocketService<Echo>("/Echo");
        wssv.AddWebSocketService<Chat>("/Chat");

        // Start the server.
        wssv.Start();

        //Monitor the server.
        if (wssv.IsListening)
        {
            Console.WriteLine("Listening on port {0}, and providing WebSocket services:", wssv.Port);

            foreach (var path in wssv.WebSocketServices.Paths)
                Console.WriteLine("- {0}", path);
        }

        Console.ReadLine();
        //wssv.Stop();

        Console.WriteLine("WebSocket server halted...");
    }
}