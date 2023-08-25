
using System.Net;
using System.Net.Sockets;

namespace Fuxion.WebSocket;

public class WebSocketServer 
{
    public void Start(string webip, int webport) 
    {
        // Intialized the TcpListener with the arguments provided.
        TcpListener server = new TcpListener(IPAddress.Parse(webip), webport);

        // Start the websocket server and log the ip address and port used.
        server.Start();
        Console.WriteLine($"Server has started on {webip}:{webport}. {Environment.NewLine} Waiting for a connection…");

        // Listen and wait for clients to connect.
        TcpClient client = server.AcceptTcpClient();

        // A Client connected to the websocket server.
        Console.WriteLine("A client connected.");
    }
}