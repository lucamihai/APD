using System;
using System.Windows.Forms;

namespace APD.Networking.ClientApplication
{
    public partial class ClientForm : Form
    {
        private Client client;

        public ClientForm()
        {
            InitializeComponent();
        }

        private void connectToServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var connectionOptionsForm = new ConnectionOptionsForm();
            var result = connectionOptionsForm.ShowDialog();

            if (result != DialogResult.OK)
            {
                return;
            }

            client = new Client(connectionOptionsForm.HostName, connectionOptionsForm.Port);
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            client.SendString(textBoxMessage.Text);
        }
    }
}
