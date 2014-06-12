using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dugeon_Crawl.GameLogic.GameLevel
{
    /// <summary>
    /// Represents an object that has a name and description with the idea in mind
    /// that the player may be able to read about this object if they choose
    /// </summary>
    abstract class DescribableObject
    {
        /// <summary>
        /// Player-friendly name of the object to be used in Examine actions
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Player-friendly description of the object to be used in Examine actions
        /// </summary>
        public string Description { get; protected set; }
    }
}
