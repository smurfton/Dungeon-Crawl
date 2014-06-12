using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dugeon_Crawl.GameLogic.GameLevel
{
    /// <summary>
    /// Represents something like a room or a certain logical division of the map.
    /// Idea is for the console version to explore using areas instead of tiles.
    /// </summary>
    class Area : DescribableObject
    {
        /// <summary>
        /// Any tile that is within this area should be added to this collection
        /// </summary>
        public List<Tile> AssociatedTiles { get; set; }

        /// <summary>
        /// Constructs a new area- a logical division in the map
        /// </summary>
        /// <param name="name">Name of the area (player friendly)</param>
        /// <param name="description">Description of the area (player friendly)</param>
        /// <param name="associatedTiles">The tiles in this area</param>
        public Area(string name, string description, List<Tile> associatedTiles)
        {
            Name = name;
            Description = description;
            AssociatedTiles = associatedTiles;
        }
    }
}
