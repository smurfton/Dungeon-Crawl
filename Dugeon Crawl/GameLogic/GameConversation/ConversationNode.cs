using System;
using System.Collections.Generic;

namespace Dungeon_Crawl.GameLogic.GameConversation
{
    enum ConversationNodeSpeakerType
    {
        PC = 0,
        NPC = 1,
    }
    /// <summary>
    /// This holds all information associated with somebody saying something
    /// </summary>
    class ConversationNode
    {
        public bool isLinkedNode = false;
        public ConversationNode nodeLinkedTo = null;
        public ConversationNodeSpeakerType speakerType = ConversationNodeSpeakerType.PC;
        public string dialog = "";
        public string tag = "";
        public List<string> executeScripts = new List<string>();
        public List<string> executeScriptDescriptions = new List<string>();
        public string checkScript = "";
        public string checkScriptDescription = "";
        public List<ConversationNode> nodes = new List<ConversationNode>();
        public ConversationNode parentNode = null;
        public bool checkScriptInverted = false;

        public void Remove()
        {
            if (parentNode != null)
                parentNode.nodes.Remove(this);
        }

        /// <summary>
        /// Please input your information for storage :)
        /// </summary>
        /// <param name="speakerType"></param>
        /// <param name="soundEffect"></param>
        /// <param name="dialogue"></param>
        public ConversationNode(ConversationNodeSpeakerType speakerType,
            string dialog, string tag, ConversationNode parentNode)
        {
            this.speakerType = speakerType;
            this.dialog = dialog;
            this.tag = tag;
            this.parentNode = parentNode;

            if (parentNode != null)
                parentNode.nodes.Add(this);
        }

        public ConversationNode() { }

        static ConversationNode FromXMLNodeShallow(XmlNode node)
        {
            ConversationNode n = new ConversationNode();
            n.isLinkedNode = node.isLinkedNode;
            n.nodeLinkedTo = null;
            n.speakerType = node.speakerType == "PC" ? ConversationNodeSpeakerType.PC :
                ConversationNodeSpeakerType.NPC;
            n.dialog = node.dialog;
            n.tag = node.tag;
            n.executeScripts = node.executeScripts;
            n.executeScriptDescriptions = node.executeScriptDescriptions;
            n.checkScript = node.checkScript;
            n.checkScriptDescription = node.checkScriptDescription;
            n.parentNode = null;
            n.checkScriptInverted = node.checkScriptInverted;
            foreach (XmlNode nd in node.nodes)
            {
                n.nodes.Add(FromXMLNodeShallow(nd));
            }
            return n;
        }

        public static void TreeFromXML(List<ConversationNode> conversationNodes,
            List<XmlNode> xmlNodes)
        {
            foreach (XmlNode n in xmlNodes)
                conversationNodes.Add(FromXMLNodeShallow(n));

            for (int c = 0; c < conversationNodes.Count; c++)
                fillInNode(conversationNodes[c], xmlNodes[c], conversationNodes);
        }

        static void fillInNode(ConversationNode cNode, XmlNode xmlNode,
            List<ConversationNode> rootNodes)
        {
            if (xmlNode.nodeLinkedTo != "")
                cNode.nodeLinkedTo = getFromTag(xmlNode.nodeLinkedTo, rootNodes);
            if (xmlNode.parentNode != "")
                cNode.parentNode = getFromTag(xmlNode.parentNode, rootNodes);

            for (int n = 0; n < cNode.nodes.Count; n++)
                fillInNode(cNode.nodes[n], xmlNode.nodes[n], rootNodes);
        }

        static ConversationNode getFromTag(string tag, List<ConversationNode> nodes)
        {
            foreach (ConversationNode n in nodes)
            {
                if (n.tag == tag)
                    return n;
                else
                {
                    ConversationNode n2 = getFromTag(tag, n.nodes);
                    if (n2 != null)
                        return n2;
                }
            }

            return null;
        }
    }
}
