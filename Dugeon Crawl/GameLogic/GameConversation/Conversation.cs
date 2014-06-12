using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.IO;

namespace Dungeon_Crawl.GameLogic.GameConversation
{
    class Conversation
    {
        ConversationNode currentNode;
        List<ConversationNode> fileRoot;

        /// <summary>
        /// Loads a new conversation
        /// </summary>
        /// <param name="filePath">The file name + path of the xml conversation file</param>
        public Conversation(string filePath)
        {
            // Read from file
            Stream stream = File.Open(filePath, FileMode.Open);
            XmlSerializer xFormatter = new XmlSerializer(typeof(List<XmlNode>));
            List<XmlNode> xmlNodes = xFormatter.Deserialize(stream) as List<XmlNode>;
            stream.Close();

            // Convert to convenient game-time format
            fileRoot = new List<ConversationNode>();
            ConversationNode.TreeFromXML(fileRoot, xmlNodes);
        }

        /// <summary>
        /// Initiates the conversation
        /// </summary>
        /// <returns>The opening dialog of the NPC</returns>
        public string StartConversation()
        {
            currentNode = fileRoot[0]; //@tmp: Assume we'll always use the first branch for now
            return currentNode.dialog;
        }

        /// <summary>
        /// Gets a list of choices available to the player
        /// </summary>
        /// <returns>The list of choices in string form</returns>
        public List<string> GetChoices()
        {
            List<string> strings = new List<string>();
            foreach (ConversationNode node in currentNode.nodes)
                strings.Add(node.dialog);
            return strings;
        }

        /// <summary>
        /// Use this when player has decided what choice they want
        /// </summary>
        /// <param name="indexOfChoice">Index should correspond to the index of the choice
        ///  obtained from GetChoices</param>
        /// <returns>The NPC's response</returns>
        public string Choose(int indexOfChoice)
        {
            try
            {
                currentNode = currentNode.nodes[indexOfChoice].nodes[0]; //@tmp: Assume we'll always use the first branch for now
                if (currentNode.isLinkedNode)
                    currentNode = currentNode.nodeLinkedTo;
                return currentNode.dialog;
            }
            catch
            {
                throw new InvalidOperationException("That choice does not exist.");
            }
        }
    }
}
