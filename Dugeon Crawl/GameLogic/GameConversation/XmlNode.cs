using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dungeon_Crawl.GameLogic.GameConversation
{
    public class XmlNode
    {
        public bool isLinkedNode { get; set; }
        public string nodeLinkedTo { get; set; }
        public string speakerType { get; set; }
        public string dialog { get; set; }
        public string tag { get; set; }
        public List<string> executeScripts { get; set; }
        public List<string> executeScriptDescriptions { get; set; }
        public string checkScript { get; set; }
        public string checkScriptDescription { get; set; }
        public List<XmlNode> nodes { get; set; }
        public string parentNode { get; set; }
        public bool checkScriptInverted { get; set; }
    }
}
