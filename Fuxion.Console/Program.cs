
using Fuxion;
using Fuxion.Utilities;

using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using WebSocketSharp;
using WebSocketSharp.Net;
using WebSocketSharp.Server;
using Fuxion.Server;
using Fuxion.Client;

class Program 
{
    static void Main(string[] args) 
    {
        if (args.Length >= 1 && args[0]=="client") 
        {
            int clients = 100;
            for (int i = 0; i < clients; i++)
            {
                FuxionClient client = new FuxionClient();
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
        FuxionServer server = new FuxionServer();
        server.StartServer(webip, webport);
    }
}