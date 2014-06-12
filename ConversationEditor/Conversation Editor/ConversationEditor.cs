using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Media;

namespace Conversation_Editor
{
    public partial class ConversationEditor : Form
    {
        private string saveLocation = "";
        private string fileName = "";

        public ConversationEditor()
        {
            InitializeComponent();
        }

        private void AddNode_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tree.SelectedNode;

            if (selectedNode.Nodes.Count == 1)
            {
                if ((selectedNode.Nodes[0].Tag as ConversationNode).isLinkedNode)
                {
                    MessageBox.Show("You cannot add a node here because this node is already attatched to a linked node." +
                        " Delete the linked node to proceed.", "Sorry, Bro.");
                    return;
                }
            }

            int imageIndex = 0;

            if (selectedNode == tree.Nodes["root"])
            {
                imageIndex = 2;
            }
            else if (selectedNode.ImageIndex == 1)
            {
                imageIndex = 2;
            }
            else if (selectedNode.ImageIndex == 2)
            {
                imageIndex = 1;
            }

            TreeNode node = new TreeNode("", imageIndex, imageIndex);
            selectedNode.Nodes.Add(node);
            int maxNumber = SearchTreeForMax(tree.Nodes, imageIndex);

            ConversationNodeSpeakerType type = ConversationNodeSpeakerType.PC;

            if (imageIndex == 2)
            {
                node.Name = "n" + (maxNumber + 1).ToString();
                type = ConversationNodeSpeakerType.NPC;
            }
            else if (imageIndex == 1)
                node.Name = "p" + (maxNumber + 1).ToString();



            ConversationNode convNode = new ConversationNode(type, "", node.Name,
                selectedNode.Tag as ConversationNode);
            node.Tag = convNode;

            tree.SelectedNode = node;

            text.Focus();
        }

        private void DeleteNode_Click(object sender, EventArgs e)
        {
            TreeNode selectedNode = tree.SelectedNode;

            if (selectedNode != tree.Nodes["root"])
            {
                DeleteChildTreeNodeLinks(selectedNode.Nodes);
                SearchTreeToDeleteLinkedNodes(selectedNode.Tag as ConversationNode, tree.Nodes["root"].Nodes);
                (selectedNode.Tag as ConversationNode).Remove();
                selectedNode.Remove();
            }

            tree_AfterSelect(null, new TreeViewEventArgs(tree.SelectedNode));
        }

        private void DeleteChildTreeNodeLinks(TreeNodeCollection nodes)
        {
            foreach (TreeNode node in nodes)
            {
                SearchTreeToDeleteLinkedNodes(node.Tag as ConversationNode, tree.Nodes["root"].Nodes);
            }
        }

        private void SearchTreeToDeleteLinkedNodes(ConversationNode nodeToDeleteLink, TreeNodeCollection treeNodes)
        {
            foreach (TreeNode node in treeNodes)
            {
                SearchTreeToDeleteLinkedNodes(nodeToDeleteLink, node.Nodes);
                ConversationNode tag = node.Tag as ConversationNode;
                if (tag.isLinkedNode && tag.nodeLinkedTo == nodeToDeleteLink)
                {
                    tag.Remove();
                    node.Remove();
                }
            }
        }

        private int SearchTreeForMax(TreeNodeCollection nodes, int imageIndex)
        {
            int highestInt = 0;
            foreach (TreeNode node in nodes)
            {
                bool startsCorrectly = false;
                if (imageIndex == 2 && node.Name.StartsWith("n"))
                {
                    startsCorrectly = true;
                }
                else if (imageIndex == 1 && node.Name.StartsWith("p"))
                {
                    startsCorrectly = true;
                }

                if (startsCorrectly)
                {
                    try
                    {
                        int number = Convert.ToInt32(node.Name.Substring(1));
                        if (number > highestInt)
                            highestInt = number;
                    }
                    catch
                    { }
                }

                int n = SearchTreeForMax(node.Nodes, imageIndex);
                if (n > highestInt)
                    highestInt = n;
            }
            return highestInt;
        }

        /// <summary>
        /// Update the text box and tabs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node == tree.Nodes["root"])
            {
                splitContainer1.Enabled = false;
                AddNode.Enabled = true;
                LinkNode.Enabled = true;
                text.Text = "";
                tag.Text = "";
                SoundName.Text = "";
                checkScript.Text = "";
                executeScripts.Items.Clear();
            }
            else if ((e.Node.Tag as ConversationNode).isLinkedNode)
            {
                splitContainer1.Enabled = false;
                AddNode.Enabled = false;
                LinkNode.Enabled = false;
                text.Text = e.Node.Text;
                tag.Text = e.Node.Name;

                ConversationNode cnode = e.Node.Tag as ConversationNode;
                if (cnode.checkScript != null)
                    checkScript.Text = cnode.checkScript;
                else
                    checkScript.Text = "";

                invertCheckScriptOutput.Checked = cnode.checkScriptInverted;

                executeScripts.Items.Clear();

                foreach (string str in cnode.executeScripts)
                {
                    if (str != null)
                    {
                        executeScripts.Items.Add(str);
                    }
                }

                if (File.Exists(saveLocation + "\\" + e.Node.Name + ".wav"))
                    SoundName.Text = e.Node.Name + ".wav";
            }
            else
            {
                AddNode.Enabled = true;
                if (LinkNode.Checked && (e.Node.Tag as ConversationNode).speakerType == ConversationNodeSpeakerType.NPC)
                {
                    LinkNode.Enabled = false;
                }
                else if (LinkNode.Checked && (e.Node.Tag as ConversationNode).speakerType == ConversationNodeSpeakerType.PC)
                {
                    LinkNode.Enabled = true;
                }
                else if (!LinkNode.Checked && (e.Node.Tag as ConversationNode).speakerType == ConversationNodeSpeakerType.PC)
                {
                    LinkNode.Enabled = false;
                }
                else if (!LinkNode.Checked && (e.Node.Tag as ConversationNode).speakerType == ConversationNodeSpeakerType.NPC)
                {
                    LinkNode.Enabled = true;
                }
                splitContainer1.Enabled = true;
                text.Text = e.Node.Text;
                tag.Text = e.Node.Name;

                ConversationNode cnode = e.Node.Tag as ConversationNode;
                if (cnode.checkScript != null)
                    checkScript.Text = cnode.checkScript;
                else
                    checkScript.Text = "";

                invertCheckScriptOutput.Checked = cnode.checkScriptInverted;

                executeScripts.Items.Clear();

                foreach (string str in cnode.executeScripts)
                {
                    if (str != null)
                    {
                        executeScripts.Items.Add(str);
                    }
                }

                if (File.Exists(saveLocation + "\\" + e.Node.Name + ".wav"))
                    SoundName.Text = e.Node.Name + ".wav";
            }
        }

        /// <summary>
        /// Keep current node updated
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void text_TextChanged(object sender, EventArgs e)
        {
            if (!splitContainer1.Enabled)
                return;
            tree.SelectedNode.Text = text.Text;
            (tree.SelectedNode.Tag as ConversationNode).dialog = text.Text;
            UpdateLinkedNodes(tree.SelectedNode, tree.Nodes["root"].Nodes);
        }

        private void tag_TextChanged(object sender, EventArgs e)
        {
            if (!splitContainer1.Enabled)
                return;
            tree.SelectedNode.Name = tag.Text;
            (tree.SelectedNode.Tag as ConversationNode).tag = tag.Text;
            UpdateLinkedNodes(tree.SelectedNode, tree.Nodes["root"].Nodes);
        }

        private void UpdateLinkedNodes(TreeNode node, TreeNodeCollection nodes)
        {
            foreach (TreeNode nd in nodes)
            {
                ConversationNode n1 = nd.Tag as ConversationNode;
                ConversationNode n2 = node.Tag as ConversationNode;
                if (n1.isLinkedNode && n1.nodeLinkedTo == n2)
                {
                    nd.Text = n2.dialog;
                    nd.Name = n2.tag;
                    n1.dialog = n2.dialog;
                    n1.tag = n2.tag;
                }

                UpdateLinkedNodes(node, nd.Nodes);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveLocation = Path.GetDirectoryName(openFileDialog1.FileName);
                fileName = openFileDialog1.FileName;
                tree.Nodes["root"].Nodes.Clear();
                List<ConversationNode> nodes =
                    (List<ConversationNode>)
                    Serializer.DeSerializeObject(openFileDialog1.FileName);
                foreach (ConversationNode node in nodes)
                {
                    AddTreeNode(node, tree.Nodes["root"]);
                }
            }
        }

        private void AddTreeNode(ConversationNode node, TreeNode nodeToAddTo)
        {
            int imageIndex = 0;
            if (node.speakerType == ConversationNodeSpeakerType.PC)
            {
                imageIndex = 1;
            }
            else
            {
                imageIndex = 2;
            }

            if (node.isLinkedNode)
                imageIndex = 3;

            TreeNode nodeToAdd = new TreeNode(node.dialog, imageIndex, imageIndex);
            nodeToAdd.Name = node.tag;
            nodeToAdd.Tag = node;

            if (imageIndex == 3)
                nodeToAdd.ForeColor = Color.LightGray;

            nodeToAddTo.Nodes.Add(nodeToAdd);

            foreach (ConversationNode nd in node.nodes)
            {
                AddTreeNode(nd, nodeToAdd);
            }
        }

        /// <summary>
        /// Save the document by serializing a list of conversation nodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode badNode = SearchTreeForBadNPCNodes(tree.Nodes["root"].Nodes);
            if (badNode != null)
            {
                MessageBox.Show("This document is invalid because you have 2 or more consecutive NPC nodes, and one" +
                    " or more of those nodes do not have a check script. This is a requirement.", "Sorry, Bro.");
                tree.SelectedNode = badNode;
                return;
            }
            if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                saveLocation = Path.GetDirectoryName(saveFileDialog1.FileName);

                if (Path.GetExtension(fileName) != ".fgconv")
                    saveFileDialog1.FileName += ".fgconv";

                fileName = saveFileDialog1.FileName;
                List<ConversationNode> nodes = new List<ConversationNode>();
                foreach (TreeNode node in tree.Nodes["root"].Nodes)
                {
                    nodes.Add(node.Tag as ConversationNode);
                }

                Serializer.SerializeObject(saveFileDialog1.FileName, nodes);
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TreeNode badNode = SearchTreeForBadNPCNodes(tree.Nodes["root"].Nodes);
            if (badNode != null)
            {
                MessageBox.Show("This document is invalid because you have 2 or more consecutive NPC nodes, and one" +
                    " or more of those nodes do not have a check script. This is a requirement.", "Sorry, Bro.");
                tree.SelectedNode = badNode;
                return;
            }


            if (fileName == "")
            {
                if (saveFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (Path.GetExtension(fileName) != ".fgconv")
                        saveFileDialog1.FileName += ".fgconv";
                    saveLocation = Path.GetDirectoryName(saveFileDialog1.FileName);
                    fileName = saveFileDialog1.FileName;
                }
                else
                {
                    return;
                }
            }

            List<ConversationNode> nodes = new List<ConversationNode>();
            foreach (TreeNode node in tree.Nodes["root"].Nodes)
            {
                nodes.Add(node.Tag as ConversationNode);
            }

            

            Serializer.SerializeObject(fileName, nodes);
        }

        private TreeNode SearchTreeForBadNPCNodes(TreeNodeCollection nodes)
        {
            if (nodes.Count > 0)
            {
                if ((nodes[0].Tag as ConversationNode).speakerType == ConversationNodeSpeakerType.NPC &&
                    nodes.Count > 1)
                {
                    foreach (TreeNode node in nodes)
                    {
                        string checkScript = (node.Tag as ConversationNode).checkScript;
                        if (checkScript == null || checkScript == "")
                            return nodes[0];
                    }
                }
            }
            foreach (TreeNode node in nodes)
            {
                TreeNode nd = SearchTreeForBadNPCNodes(node.Nodes);
                if (nd != null)
                    return nd;
            }
            return null;
        }

        private List<ConversationNode> GetContainedNodes(ConversationNode node)
        {
            List<ConversationNode> returnVal = new List<ConversationNode>();

            returnVal.Add(node);

            foreach (ConversationNode nd in node.nodes)
            {
                List<ConversationNode> nds = GetContainedNodes(nd);
                foreach (ConversationNode n in nds)
                {
                    returnVal.Add(n);
                }
            }
            return returnVal;
        }

        private void ExpandAll_Click(object sender, EventArgs e)
        {
            tree.ExpandAll();
        }

        private void ContractAll_Click(object sender, EventArgs e)
        {
            tree.CollapseAll();
        }

        ConversationNode destinationLinkNode = null;

        /// <summary>
        /// Link nodes to other nodes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LinkNode_Click(object sender, EventArgs e)
        {
            if (LinkNode.Checked)
            {
                destinationLinkNode = tree.SelectedNode.Tag as ConversationNode;
            }
            else
            {
                if (destinationLinkNode != null)
                {
                    if (tree.SelectedNode.Nodes.Count > 0)
                    {
                        MessageBox.Show("The target link node must not have children.", "Sorry, Bro.");
                        return;
                    }
                    if (!((tree.SelectedNode.Tag as ConversationNode).speakerType == ConversationNodeSpeakerType.PC &&
                        destinationLinkNode.speakerType == ConversationNodeSpeakerType.NPC))
                    {
                        MessageBox.Show("The destination link node must be NPC, and the target link node must be PC.",
                            "Sorry, Bro.");
                        return;
                    }


                    TreeNode treeNodeToAdd = new TreeNode(destinationLinkNode.dialog, 3, 3);
                    treeNodeToAdd.Name = destinationLinkNode.tag;
                    treeNodeToAdd.ForeColor = Color.LightGray;

                    ConversationNode linkedNode = new ConversationNode(destinationLinkNode.speakerType,
                        destinationLinkNode.dialog, destinationLinkNode.tag,
                        tree.SelectedNode.Tag as ConversationNode);
                    linkedNode.isLinkedNode = true;
                    linkedNode.nodeLinkedTo = destinationLinkNode;

                    treeNodeToAdd.Tag = linkedNode;
                    tree.SelectedNode.Nodes.Add(treeNodeToAdd);
                    tree.SelectedNode = treeNodeToAdd;
                }
            }
        }

        private void invertCheckScriptOutput_CheckedChanged(object sender, EventArgs e)
        {
            (tree.SelectedNode.Tag as ConversationNode).checkScriptInverted = invertCheckScriptOutput.Checked;
        }

        private void soundBrowse_Click(object sender, EventArgs e)
        {
            if (fileName == "")
            {
                MessageBox.Show("You need to save your document at least once because the sounds will be copied to "
                    + "the same directory with the correct tag name. This is how it is loaded into the game.",
                    "Sorry, Bro.");
                return;
            }
            openFileDialog2.Multiselect = false;
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string copyLocation = "";
                if (!soundChangeToFilename.Checked)
                {
                    copyLocation = saveLocation + "\\" + tree.SelectedNode.Name + ".wav";
                }
                else
                {
                    copyLocation = saveLocation + "\\" + Path.GetFileName(openFileDialog2.FileName);
                    string newTag = Path.GetFileNameWithoutExtension(openFileDialog2.FileName);
                    tree.SelectedNode.Name = newTag;
                    tag.Text = newTag;
                    (tree.SelectedNode.Tag as ConversationNode).tag = newTag;
                }

                if (!File.Exists(copyLocation))
                {
                    File.Copy(openFileDialog2.FileName, copyLocation);
                }

                SoundName.Text = Path.GetFileName(copyLocation);
            }
        }

        SoundPlayer player = new SoundPlayer();

        private void playButton_Click(object sender, EventArgs e)
        {
            player.SoundLocation = saveLocation + "\\" + tree.SelectedNode.Name + ".wav";
            player.Play();
        }

        private void stopButton_Click(object sender, EventArgs e)
        {
            player.Stop();
        }

        private void syncSounds_Click(object sender, EventArgs e)
        {
            if (fileName == null)
            {
                MessageBox.Show("You need to save your document at least once because the sounds will be copied to "
                    + "the same directory with the correct tag name. This is how it is loaded into the game.",
                    "Sorry, Bro.");
                return;
            }
            openFileDialog2.Multiselect = true;
            if (openFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (string filename in openFileDialog2.FileNames)
                {
                    string tag = Path.GetFileNameWithoutExtension(filename);
                    if (SearchMatchingTag(tree.Nodes["root"].Nodes, tag))
                    {
                        string newFile = saveLocation + "\\" + tag + ".wav";
                        if (!File.Exists(newFile))
                        {
                            File.Copy(filename, newFile);
                        }

                        if (tag == tree.SelectedNode.Name)
                        {
                            SoundName.Text = tag + ".wav";
                        }
                    }
                }
            }
        }

        private bool SearchMatchingTag(TreeNodeCollection nodes, string tag)
        {
            foreach (TreeNode node in nodes)
            {
                if (node.Name == tag)
                    return true;

                if (SearchMatchingTag(node.Nodes, tag))
                    return true;
            }
            return false;
        }

        private void token_Click(object sender, EventArgs e)
        {
            TokenSelector selector = new TokenSelector();
            if (selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                text.Paste("<<" + selector.Token + ">>");
            }
        }

        private void deleteExecuteScript_Click(object sender, EventArgs e)
        {
            string item = (string)executeScripts.SelectedItem;
            int index = executeScripts.SelectedIndex;
            executeScripts.Items.Remove(executeScripts.SelectedItem);
            (tree.SelectedNode.Tag as ConversationNode).executeScripts.Remove(item);
            (tree.SelectedNode.Tag as ConversationNode).executeScriptDescriptions.RemoveAt(index);
        }

        private void deleteCheckScript_Click(object sender, EventArgs e)
        {
            checkScript.Text = "";
            (tree.SelectedNode.Tag as ConversationNode).checkScript = "";
            (tree.SelectedNode.Tag as ConversationNode).checkScriptDescription = "";
        }

        private void addExecuteScript_Click(object sender, EventArgs e)
        {
            ScriptSelector selector = new ScriptSelector(tree.Nodes["root"], false);
            if (selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConversationNode node = tree.SelectedNode.Tag as ConversationNode;
                node.executeScripts.Add(selector.scriptName);
                node.executeScriptDescriptions.Add(selector.scriptDescription);
                executeScripts.Items.Add(selector.scriptName);
            }
            else
            {
                executeScripts.Items.Clear();
                foreach (string str in (tree.SelectedNode.Tag as ConversationNode).executeScripts)
                    executeScripts.Items.Add(str);
            }
        }

        private void addCheckScript_Click(object sender, EventArgs e)
        {
            ScriptSelector selector = new ScriptSelector(tree.Nodes["root"], true);
            if (selector.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ConversationNode node = tree.SelectedNode.Tag as ConversationNode;
                node.checkScript = selector.scriptName;
                node.checkScriptDescription = selector.scriptDescription;

                checkScript.Text = selector.scriptName;
            }
            else
            {
                checkScript.Text = (tree.SelectedNode.Tag as ConversationNode).checkScript;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveLocation = "";
            fileName = "";
            tree.Nodes["root"].Nodes.Clear();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        string stringToPrint = "";
        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StringDialog strDialogN = new StringDialog("Please enter a page header for your NPC character.");
            StringDialog strDialogP = new StringDialog("Thank-you. Now please enter a page header for your PC character.");
            if (strDialogN.ShowDialog() != DialogResult.OK)
                return;
            if (strDialogP.ShowDialog() != DialogResult.OK)
                return;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                string timeStamp = getTimeStamp();
                PrintDocument doc = new PrintDocument();
                doc.PrinterSettings = printDialog.PrinterSettings;
                doc.PrintPage += printDocument_PrintPage;
                stringToPrint = strDialogN.output + " - " + Path.GetFileName(fileName) + " - NPC Script - " + timeStamp + "\n\n";
                foreach (TreeNode nd in tree.Nodes[0].Nodes)
                    printTree(nd, ConversationNodeSpeakerType.NPC);
                doc.Print();
                stringToPrint = strDialogP.output + " - " + Path.GetFileName(fileName) + " - PC Script - " + timeStamp + "\n\n";
                foreach (TreeNode nd in tree.Nodes[0].Nodes)
                    printTree(nd, ConversationNodeSpeakerType.PC);
                doc.Print();
            }
        }

        private string getTimeStamp()
        {
            return DateTime.Now.ToShortDateString() + " - " + DateTime.Now.ToShortTimeString();
        }

        private void printTree(TreeNode search, ConversationNodeSpeakerType type)
        {
            ConversationNode cn = search.Tag as ConversationNode;
            if (cn.speakerType == type && !cn.isLinkedNode)
                stringToPrint += cn.tag + " - " + cn.dialog + "\n";
            foreach (TreeNode node in search.Nodes)
                printTree(node, type);
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            int charactersOnPage = 0;
            int linesPerPage = 0;

            // Sets the value of charactersOnPage to the number of characters 
            // of stringToPrint that will fit within the bounds of the page.
            e.Graphics.MeasureString(stringToPrint, this.Font,
                e.MarginBounds.Size, StringFormat.GenericTypographic,
                out charactersOnPage, out linesPerPage);

            // Draws the string within the bounds of the page
            e.Graphics.DrawString(stringToPrint, this.Font, Brushes.Black,
                e.MarginBounds, StringFormat.GenericTypographic);

            // Remove the portion of the string that has been printed.
            stringToPrint = stringToPrint.Substring(charactersOnPage);

            // Check to see if more pages are to be printed.
            e.HasMorePages = (stringToPrint.Length > 0);
        }

        private void exportAsXMLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog2.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<XmlNode> nds = new List<XmlNode>();
                foreach (TreeNode node in tree.Nodes["root"].Nodes)
                {
                    nds.Add((node.Tag as ConversationNode).toXMLNode());
                }

                if (Path.GetExtension(saveFileDialog2.FileName) != ".xml")
                    saveFileDialog2.FileName += ".xml";

                Serializer.SerializeObjectAsXML(saveFileDialog2.FileName,
                    typeof(List<XmlNode>), nds);
            }
        }

    }
}
