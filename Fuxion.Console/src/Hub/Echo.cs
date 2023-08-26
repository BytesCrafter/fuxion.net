using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Fuxion.Demo
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Send(e.Data);
        }
    }
}
