using System.Windows.Forms;

namespace APD.Networking.ClientApplication
{
    public partial class ConnectionOptionsForm : Form
    {
        public string HostName { get; set; }
        public int Port { get; set; }

        public ConnectionOptionsForm()
        {
            InitializeComponent();
        }

        private void buttonSelect_Click(object sender, System.EventArgs e)
        {
            // TODO: Check if a connection can be made here
            HostName = textBoxHostName.Text;
            Port = (int)numericUpDownPort.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
