﻿using Fuxion.Utilities;
using Fuxion.WebSocket;

class Program
{
    static void Main(string[] args)
    {
        // Prepare the arguments for running the websocket server.
        string[] commands = StringUtil.Shift(args, 1);
        string webip = args.Length > 2 ? commands[0] : "127.0.0.1";
        int webport = commands.Length > 1 ? Int32.Parse(commands[1]) : 8000;

        // Instantiate WS Server and Start.
        WebSocketServer server = new WebSocketServer();
        server.StartOption1(webip, webport);
    }
    
    protected static void Process(string[] strings)
    {
        if (strings.Length > 0 && strings[0] == "server")
        {
            Console.WriteLine("Server is running...");

            // Prepare the arguments for running the websocket server.
            string[] commands = StringUtil.Shift(strings, 1);
            string webip = strings.Length > 2 ? commands[0] : "127.0.0.1";
            int webport = commands.Length > 1 ? Int32.Parse(commands[1]) : 8080;

            // Instantiate WS Server and Start.
            WebSocketServer server = new WebSocketServer();
            server.StartOption1(webip, webport);
        }


        if (strings.Length > 0 && strings[0] == "client")
        {
            Console.WriteLine("Client is connecting...");

            // Prepare the arguments for running the websocket client.
            string[] commands = StringUtil.Shift(strings, 1);
            string webip = strings.Length > 2 ? commands[0] : "127.0.0.1";
            int webport = commands.Length > 1 ? Int32.Parse(commands[1]) : 8080;

            // Instantiate WS Client and Connect.
            WebSocketClient client = new WebSocketClient();
            client.Connect(webip, webport);
        }

        Console.WriteLine("Command invalid...");
    }
}