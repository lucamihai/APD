using System.Windows.Forms;

namespace APD.Networking.ClientApplication
{
    public partial class NewUsernameForm : Form
    {
        public string NewUsername { get; set; }

        public NewUsernameForm()
        {
            InitializeComponent();
        }

        private void buttonSelect_Click(object sender, System.EventArgs e)
        {
            NewUsername = textBoxUsername.Text;

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
