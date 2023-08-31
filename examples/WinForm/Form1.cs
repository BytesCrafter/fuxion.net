
using Fuxion.Desktop.Models;
using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Fuxion.Desktop
{
    public partial class Form1 : Form
    {
        SocketIO client = null;
        public List<Messages> messages = new List<Messages>();

        public Form1()
        {
            InitializeComponent();
        }

        private int counter = 0;

        public async void Connect(string ipadd, int port, string channel = "/chat", string uname = "")
        {
            client = new SocketIO("http://localhost:3000/");

            client.On("chat message", async response =>
            {
                // You can print the returned data first to decide what to do next.
                // output: ["ok",{"id":1,"name":"tom"}]
                Console.WriteLine(response);

                // Get the first data in the response
                string text = response.GetValue<string>();

                // Get the second data in the response
                //var dto = response.GetValue<TestDTO>(1);

                this.onMessageReceive(text);
            });

            client.OnConnected += async (sender, e) =>
            {
                this.onMessageReceive("Connected...\n");

                Invoke(new Action(() => panel1.Hide()));

                // Emit a string and an object
                //var dto = new TestDTO { Id = 123, Name = "bob" };
                await client.EmitAsync("chat message", "Hello! New Client");
            };

            client.OnDisconnected += (sender, e) =>
            {
                this.onMessageReceive("Disconnected...\n");
            };

            await client.ConnectAsync();
        }

        protected void onMessageReceive(string message)
        {
            var t = Task.Run(() =>
            {
                Console.WriteLine("\nMessage: " + message);
                Invoke(new Action(() => listBox1.Items.Add("\n Msg: " + message) ));
            });
            t.Wait();
        }

        protected void SendMessage()
        {
            string message = textBox1.Text.ToString();
            this.client.EmitAsync(message);
            textBox1.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.SendMessage();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Connect("localhost", 3000, "", textBox2.Text);
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            this.SendMessage();
        }
    }
}
