using System;
using System.Drawing;
using System.Linq;
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

            client.OnChatReceived += OnChatReceived;
            client.OnOtherClientConnected += OnOtherClientConnected;
            client.OnOtherClientDisconnected += OnOtherClientDisconnected;
            client.OnUsernameChanged += OnUsernameChanged;
        }

        private void OnChatReceived(string chatMessage, string sourceUsername, string destinationUsername)
        {
            var methodInvoker = new MethodInvoker(() => textBoxChat.Text +=
                    $"{DateTime.Now.ToShortTimeString()} {sourceUsername} -> {destinationUsername}: {chatMessage}{Environment.NewLine}");

            Invoke(methodInvoker);
        }

        private void OnOtherClientConnected(string username)
        {
            var methodInvoker = new MethodInvoker(() =>
            {
                var radioButton = GenerateRadioButtonForClient(username);
                panelClientList.Controls.Add(radioButton);
            });

            Invoke(methodInvoker);
        }

        private void OnOtherClientDisconnected(string username)
        {
            var radioButtonForGivenUsername = panelClientList
                .Controls
                .OfType<RadioButton>()
                .FirstOrDefault(x => x.Text == username);

            if (radioButtonForGivenUsername == null)
            {
                return;
            }

            var methodInvoker = new MethodInvoker(() =>
            {
                if (radioButtonForGivenUsername.Checked)
                {
                    buttonSend.Enabled = false;
                }

                panelClientList.Controls.Remove(radioButtonForGivenUsername);

            });

            Invoke(methodInvoker);
        }

        private void OnUsernameChanged(string oldUsername, string newUsername)
        {
            var radioButtonForGivenUsername = panelClientList
                .Controls
                .OfType<RadioButton>()
                .FirstOrDefault(x => x.Text == oldUsername);

            if (radioButtonForGivenUsername == null)
            {
                return;
            }

            var methodInvoker = new MethodInvoker(() =>
            {
                radioButtonForGivenUsername.Text = newUsername;
            });

            Invoke(methodInvoker);
        }

        private RadioButton GenerateRadioButtonForClient(string clientUserName)
        {
            var checkBox = new RadioButton();
            checkBox.Text = clientUserName;
            checkBox.Location = new Point(10, panelClientList.Controls.Count * checkBox.Height + 5);
            checkBox.CheckedChanged += (sender, args) =>
            {
                if (((RadioButton) sender).Checked)
                {
                    buttonSend.Enabled = true;
                }
            };


            return checkBox;
        }

        private void buttonSend_Click(object sender, EventArgs e)
        {
            var destinationUsername = panelClientList
                .Controls.OfType<RadioButton>()
                .First(x => x.Checked)
                .Text;

            client.SendChat(textBoxMessage.Text, destinationUsername);

            textBoxChat.Text += $"{DateTime.Now.ToShortTimeString()} [{client.Username} -> {destinationUsername}]: {textBoxMessage.Text}{Environment.NewLine}";

            textBoxMessage.Text = string.Empty;
        }

        private void ClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            client?.Stop();
        }
    }
}
