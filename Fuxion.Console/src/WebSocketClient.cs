
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;

namespace Fuxion.WebSocket;

public class WebSocketClient
{
    public string Connect(string webip, int webport)
    {
        var connection = this.TryConnect(webip, webport);
        return connection.Result; 
    }

    public async Task<string> TryConnect(string webip, int webport) 
    {
        CancellationTokenSource source = new CancellationTokenSource();
        using (var ws = new ClientWebSocket())
        {
            await ws.ConnectAsync(new Uri($"wss://{webip}:{webport}"), CancellationToken.None);
            byte[] buffer = new byte[256];
            while (ws.State == WebSocketState.Open)
            {
                var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, CancellationToken.None);
                    return "closed";
                }
                
                else
                {
                    HandleMessage(buffer, result.Count);
                }
            }
            return "connected";
        }
    }

    private static void HandleMessage(byte[] buffer, int count)
    {
        Console.WriteLine($"Received {BitConverter.ToString(buffer, 0, count)}");
    }
}