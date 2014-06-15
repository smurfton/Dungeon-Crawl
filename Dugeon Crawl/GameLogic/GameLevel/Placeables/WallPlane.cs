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
    /// Represents- you got it- a wall. Think of it as a 1x1 plane sitting in 3d space.
    /// Although the actual geometry is not defined here (nor should it be)
    /// </summary>
    [DataContract]
    class WallPlane : PlaceableObject
    {
        /// <summary>
        /// Constructs a default wall placeable
        /// </summary>
        /// <param name="direction">If you say north, the wall will be the northern wall of the tile,
        /// but it will face south</param>
        /// <param name="associatedTile">The tile to add the wall to</param>
        public WallPlane(Direction direction, Tile associatedTile)
        {
            Vector2 xyPos = new Vector2(associatedTile.PositionX, associatedTile.PositionY) +
                Map.GetCardinalDirectionUnitVector(direction) * 0.5f;
            Position = new Vector3(xyPos.X, xyPos.Y, associatedTile.PositionZ);
            // A wall in the north would face south
            EulerRotation = new Vector3(MathHelper.PiOver2, 0,
                Map.GetCardinalDirectionYaw(Map.GetOppositeDirection(direction)));
            occupiedTiles.Add(associatedTile);
        }
    }
}
