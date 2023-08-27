using Fuxion.Client;
using Fuxion.Server;

using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace Fuxion.Desktop
{
    public partial class Form1 : Form
    {
        public WebSocket websock = null;

        public Form1()
        {
            InitializeComponent();
        }

        public void Connect(string ipadd, int port, string channel = "chat", string uname = "")
        {
            websock = new WebSocket($"ws://{ipadd}:{port}/{channel}/?name={uname}");

            websock.OnMessage += Websock_OnMessage;
            websock.OnOpen += Websock_OnOpen;
            websock.OnClose += Websock_OnClose;

            websock.Connect();
        }

        private void Websock_OnClose(object sender, CloseEventArgs e)
        {
            Console.WriteLine("Disconnected... ");
            listBox1.Items.Add("\nDisconnected... ");
        }

        private void Websock_OnOpen(object sender, EventArgs e)
        {
            Console.WriteLine("\nConnected... ");
            listBox1.Items.Add("\nConnected... ");

            panel1.Hide();
        }

        private void Websock_OnMessage(object sender, MessageEventArgs e)
        {
            this.Invoke(new MethodInvoker(delegate ()
            {
                Console.WriteLine("\nMessage: " + e.Data);
                listBox1.Items.Add("\n Msg: " + e.Data);
            }));
        }

        private void button1_Click(object sender, EventArgs e)
        {            
                string message = textBox1.Text.ToString();
                this.websock.Send(message);
                textBox1.Text = "";            
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Connect("localhost", 8000, "Chat", textBox2.Text);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            string message = textBox1.Text.ToString();
            this.websock.Send(message);
            textBox1.Text = "";
        }
    }
}
