using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.Serialization;

using OpenTK;

namespace Dungeon_Crawl.GameLogic.GameLevel.Placeables
{
    /// <summary>
    /// Represents a floor- a 1x1 plane sitting on the ground
    /// </summary>
    [DataContract]
    class FloorPlane : PlaceableObject
    {
        /// <summary>
        /// Constructs a default floor plane for the specified tile
        /// </summary>
        /// <param name="associatedTile">The tile to add a floor to</param>
        public FloorPlane(Tile associatedTile)
        {
            Position = new Vector3(associatedTile.PositionX, associatedTile.PositionY,
                associatedTile.PositionZ - 0.5f);
            occupiedTiles.Add(associatedTile);
        }
    }
}
