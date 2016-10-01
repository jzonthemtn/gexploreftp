using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace gExploreFTP
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {

            int port;

            try
            {
                port = Convert.ToInt32(textBoxPort.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The port must be a number.", "gExploreFTP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBoxPort.Focus();
                return;
            }

            gExploreFTP.Common.Registry.setPort(port);
            Common.Registry.setStart(checkBoxStartServer.Checked);

            this.DialogResult = DialogResult.OK;
            this.Close();

        }

        private void Settings_Load(object sender, EventArgs e)
        {

            textBoxPort.Text = gExploreFTP.Common.Registry.getPort().ToString();
            checkBoxStartServer.Checked = gExploreFTP.Common.Registry.getStart();

        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

            this.DialogResult = DialogResult.Cancel;
            this.Close();

        }

    }
}
