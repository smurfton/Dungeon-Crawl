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
    public partial class Temporary_Script_Wiz : Form
    {
        public string oName = "";
        public string oDescription = "";

        public Temporary_Script_Wiz()
        {
            InitializeComponent();
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            oName = name.Text;
            oDescription = description.Text;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
