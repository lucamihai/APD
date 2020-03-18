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

                    buttonServerStatus.Text = StopServerText;

                    labelServerStatus.Text = "Online";
                    labelServerStatus.ForeColor = Color.Green;
                }
                else
                {
                    server.Stop();

                    buttonServerStatus.Text = StartServerText;

                    labelServerStatus.Text = "Offline";
                    labelServerStatus.ForeColor = Color.Maroon;
                }
            }
        }

        public ServerForm()
        {
            InitializeComponent();
        }

        private void buttonServerStatus_Click(object sender, System.EventArgs e)
        {
            ServerOnline = !ServerOnline;
        }
    }
}
