using System.Net.Sockets;
using System.Net;
using System;

using Fuxion.Utilities;
using Fuxion.WebSocket;

class Program 
{
    static void Main(string[] args) 
    {
        if (args.Length > 0) 
        {
            Program.Process(args);
        }

        else 
        {
            Console.WriteLine("Invalid command!");
        }
    }

    protected static void Process(string[] strings) 
    {
        if( strings.Length > 0 && strings[0] == "server" ) {
            Console.WriteLine("Server is not running...");

            // Prepare the arguments for running the websocket server.
            string[] commands = StringUtil.Shift(strings, 1);
            int webport = commands.Length > 1 ? Int32.Parse(strings[0]) : 8080;
            string webip = strings.Length > 2 ? commands[1] : "127.0.0.1";

            // Instantiate WS Server and Start.
            WebSocketServer server = new WebSocketServer();
            server.Start(webip, webport);
        }
        
        Console.WriteLine("Command invalid...");
    }
}