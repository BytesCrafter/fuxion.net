using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Fuxion.Demo
{
    public class Chat : WebSocketBehavior
    {
        private string _name = "";
        private static int _number = 0;
        private string _prefix;

        public Chat()
        {
            _prefix = "user#";
        }

        public string Prefix
        {
            get
            {
                return _prefix;
            }

            set
            {
                _prefix = !value.IsNullOrEmpty() ? value : "user#";
            }
        }

        private string getName()
        {
            var name = QueryString["name"];

            return !name.IsNullOrEmpty() ? name : _prefix + getNumber();
        }

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            if (_name == null)
                return;

            var fmt = "{0} got logged off...";
            var msg = string.Format(fmt, _name);

            Sessions.Broadcast(msg);
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            var fmt = "{0}: {1}";
            var msg = string.Format(fmt, _name, e.Data);

            Console.WriteLine($"Name: {_name}L: {msg}");
            Sessions.Broadcast(msg);
        }

        protected override void OnOpen()
        {
            _name = getName();
            Console.WriteLine($"Name: {_name}");

            var fmt = "{0} has logged in!";
            var msg = string.Format(fmt, _name);

            Sessions.Broadcast(msg);
        }
    }
}
