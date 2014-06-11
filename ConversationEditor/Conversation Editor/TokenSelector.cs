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
    public partial class TokenSelector : Form
    {
        public string Token = "";
        public TokenSelector()
        {
            InitializeComponent();
        }

        private void add_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Token = (string)possibleTokens.SelectedItem;
            this.Close();
        }
    }
}
