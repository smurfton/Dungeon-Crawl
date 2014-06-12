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
    public partial class ScriptSelector : Form
    {
        public string scriptName = "";
        public string scriptDescription = "";

        private List<string> descriptions = new List<string>();
        private bool isCheckScript = false;

        private TreeNode rootNode;

        public ScriptSelector(TreeNode rootNode, bool isCheckScript)
        {
            this.isCheckScript = isCheckScript;
            this.rootNode = rootNode;
            InitializeComponent();

            SearchForScripts(rootNode.Nodes);
        }

        private void SearchForScripts(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                ConversationNode cnode = node.Tag as ConversationNode;
                if (isCheckScript)
                {
                    if (cnode.checkScript != null)
                    {
                        if (cnode.checkScript != "" && !ScriptAlreadyThere(cnode.checkScript))
                        {
                            scripts.Items.Add(cnode.checkScript);
                            descriptions.Add(cnode.checkScriptDescription);
                        }
                    }
                }
                else
                {
                    int n = 0;
                    foreach (string str in cnode.executeScripts)
                    {
                        if (str != null)
                        {
                            if (str != "" && !ScriptAlreadyThere(str))
                            {
                                scripts.Items.Add(str);
                                descriptions.Add(cnode.executeScriptDescriptions[n]);
                            }
                        }
                        n++;
                    }
                }
                SearchForScripts(node.Nodes);
            }
        }

        private bool ScriptAlreadyThere(string script)
        {
            return scripts.Items.Contains(script);
        }

        private void addScript_Click(object sender, EventArgs e)
        {
            Temporary_Script_Wiz wiz = new Temporary_Script_Wiz();

            if (wiz.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                scriptName = wiz.oName;
                scriptDescription = wiz.oDescription;
                DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void Ok_Click(object sender, EventArgs e)
        {
            if (scripts.SelectedItem == null)
            {
                MessageBox.Show("You must select a script before clicking 'Ok'", "Sorry, Bro.");
                return;
            }
            DialogResult = System.Windows.Forms.DialogResult.OK;
            scriptName = (string)scripts.SelectedItem;
            scriptDescription = descriptions[scripts.SelectedIndex];
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void deleteScript_Click(object sender, EventArgs e)
        {
            string item = (string)scripts.SelectedItem;
            int itemIndex = scripts.SelectedIndex;
            DeleteScripts(item, descriptions[itemIndex], rootNode.Nodes);
            scripts.Items.Remove(scripts.SelectedItem);
            descriptions.RemoveAt(itemIndex);
        }

        private void DeleteScripts(string name, string description, TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                ConversationNode cnode = node.Tag as ConversationNode;
                if (isCheckScript)
                {
                    if (cnode.checkScript == name)
                    {
                        cnode.checkScript = "";
                        cnode.checkScriptDescription = "";
                    }
                }
                else
                {
                    if (cnode.executeScripts.Contains(name))
                    {
                        int index = cnode.executeScripts.IndexOf(name);
                        cnode.executeScripts.Remove(name);
                        cnode.executeScriptDescriptions.RemoveAt(index);
                    }
                }

                DeleteScripts(name, description, node.Nodes);
            }
        }

        private void scripts_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (scripts.SelectedItem != null)
            {
                description.Text = descriptions[scripts.SelectedIndex];
            }
            else
                description.Text = "";
        }
    }
}
