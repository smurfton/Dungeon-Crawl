using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Conversation_Editor
{
    public partial class StringDialog : Form
    {
        public string output = "";

        public StringDialog(string header)
        {
            InitializeComponent();
            this.Text = header;
        }

        private void cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void ok_Click(object sender, EventArgs e)
        {
            output = text.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
