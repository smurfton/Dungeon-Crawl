﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dungeon_Crawl.GameLogic.GameLevel
{
    /// <summary>
    /// Represents an object that has a name and description with the idea in mind
    /// that the player may be able to read about this object if they choose
    /// </summary>
    [DataContract]
    abstract class DescribableObject
    {
        /// <summary>
        /// Player-friendly name of the object to be used in Examine actions
        /// </summary>
        [DataMember]
        public string Name { get; protected set; }
        /// <summary>
        /// Player-friendly description of the object to be used in Examine actions
        /// </summary>
        [DataMember]
        public string Description { get; protected set; }
    }
}
