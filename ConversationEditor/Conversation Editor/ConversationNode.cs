using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Conversation_Editor
{
    enum ConversationNodeSpeakerType
    {
        PC = 0,
        NPC = 1,
    }
    /// <summary>
    /// This holds all information associated with somebody saying something
    /// </summary>
    [Serializable()]
    class ConversationNode : ISerializable
    {
        public bool isLinkedNode = false;
        public ConversationNode nodeLinkedTo;
        public ConversationNodeSpeakerType speakerType;
        public string dialog;
        public string tag;
        public List<string> executeScripts = new List<string>();
        public List<string> executeScriptDescriptions = new List<string>();
        public string checkScript;
        public string checkScriptDescription;
        public List<ConversationNode> nodes = new List<ConversationNode>();
        public ConversationNode parentNode;
        public bool checkScriptInverted = false;

        public void Remove()
        {
            if (parentNode != null)
                parentNode.nodes.Remove(this);
        }

        public XmlNode toXMLNode()
        {
            XmlNode nd = new XmlNode();
            nd.isLinkedNode = isLinkedNode;
            nd.nodeLinkedTo = nodeLinkedTo != null ? nodeLinkedTo.tag : "";
            if (speakerType == ConversationNodeSpeakerType.PC)
                nd.speakerType = "PC";
            else
                nd.speakerType = "NPC";
            nd.dialog = dialog;
            nd.tag = tag;
            nd.executeScripts = executeScripts;
            nd.executeScriptDescriptions = executeScriptDescriptions;
            nd.checkScript = checkScript;
            nd.checkScriptDescription = checkScriptDescription;
            nd.nodes = new List<XmlNode>();
            foreach (ConversationNode node in nodes)
            {
                nd.nodes.Add(node.toXMLNode());
            }
            nd.parentNode = parentNode != null ? parentNode.tag : "";
            nd.checkScriptInverted = checkScriptInverted;

            return nd;
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

        #region serialization
        public ConversationNode(SerializationInfo info, StreamingContext context)
        {
            speakerType = (ConversationNodeSpeakerType)info.GetInt32("speakerType");
            dialog = info.GetString("dialog");
            tag = info.GetString("tag");
            executeScripts = (List<string>)info.GetValue("executeScripts", typeof(List<string>));
            checkScript = info.GetString("checkScript");
            nodes = (List<ConversationNode>)info.GetValue("nodes", typeof(List<ConversationNode>));
            parentNode = (ConversationNode)info.GetValue("parentNode", typeof(ConversationNode));
            isLinkedNode = info.GetBoolean("isLinkedNode");
            nodeLinkedTo = (ConversationNode)info.GetValue("nodeLinkedTo", typeof(ConversationNode));
            checkScriptInverted = info.GetBoolean("checkScriptInverted");
            checkScriptDescription = info.GetString("checkScriptDescription");
            executeScriptDescriptions = (List<string>)info.GetValue("executeScriptDescriptions", typeof(List<string>));
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("speakerType", (int)speakerType);
            info.AddValue("dialog", dialog);
            info.AddValue("tag", tag);
            info.AddValue("executeScripts", executeScripts);
            info.AddValue("checkScript", checkScript);
            info.AddValue("nodes", nodes);
            info.AddValue("parentNode", parentNode);
            info.AddValue("isLinkedNode", isLinkedNode);
            info.AddValue("nodeLinkedTo", nodeLinkedTo);
            info.AddValue("checkScriptInverted", checkScriptInverted);
            info.AddValue("checkScriptDescription", checkScriptDescription);
            info.AddValue("executeScriptDescriptions", executeScriptDescriptions);
        }
        #endregion
    }
}
