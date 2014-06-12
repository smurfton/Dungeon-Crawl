using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Dugeon_Crawl.GameLogic.GameLevel.Placeables
{
    /// <summary>
    /// Represents a ceiling- a 1x1 plane that is hovering
    /// </summary>
    class CeilingPlane : PlaceableObject
    {
        /// <summary>
        /// Constructs a default ceiling plane for the specified tile
        /// </summary>
        /// <param name="associatedTile">The tile to add a ceiling to</param>
        public CeilingPlane(Tile associatedTile)
        {
            Position = new Vector3(associatedTile.PositionX, associatedTile.PositionY,
                associatedTile.PositionZ + 0.5f);
            EulerRotation = new Vector3(MathHelper.Pi, 0, 0);
            occupiedTiles.Add(associatedTile);
        }
    }
}
