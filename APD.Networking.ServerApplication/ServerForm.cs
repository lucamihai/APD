using System;
using System.Drawing;
using System.Windows.Forms;

namespace APD.Networking.ServerApplication
{
    public partial class ServerForm : Form
    {
        private Server server;
        private const string StartServerText = "Start server";
        private const string StopServerText = "Stop server";

        private bool ServerOnline
        {
            get => buttonServerStatus.Text == StopServerText;
            set
            {
                if (value)
                {
                    // TODO: Check if port is already used
                    var selectedPort = (int)numericUpDownPort.Value;

                    server = new Server(selectedPort);
                    server.OnClientConnected += OnClientConnected;
                    server.OnClientDisconnected += OnClientDisconnected;

                    buttonServerStatus.Text = StopServerText;

                    labelServerStatus.Text = "Online";
                    labelServerStatus.ForeColor = Color.Green;
                }
                else
                {
                    server?.Stop();
                    NumberOfConnectedClients = 0;

                    buttonServerStatus.Text = StartServerText;

                    labelServerStatus.Text = "Offline";
                    labelServerStatus.ForeColor = Color.Maroon;
                }
            }
        }

        private int NumberOfConnectedClients
        {
            get => Convert.ToInt32(labelClientsConnected.Text);
            set
            {
                var methodInvoker = new MethodInvoker(() => labelClientsConnected.Text = value.ToString());
                Invoke(methodInvoker);
            }
        }

        public ServerForm()
        {
            InitializeComponent();
        }

        private void OnClientConnected(string username)
        {
            NumberOfConnectedClients++;
        }

        private void OnClientDisconnected(string username)
        {
            NumberOfConnectedClients--;
        }

        private void buttonServerStatus_Click(object sender, System.EventArgs e)
        {
            ServerOnline = !ServerOnline;
        }

        private void ServerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ServerOnline = false;
        }
    }
}
