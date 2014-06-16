using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Dungeon_Crawl.GameLogic.GameLevel
{
    /// <summary>
    /// You can think of a tile as a cube that sits in 3D space approximately 5x5x5 feet, or
    /// 1x1x1 units in space. Not only is it the basic building block for all movement, but it also
    /// serves as a convenient way to divide the scene so relevant objects can be looked up
    /// faster.
    /// </summary>
    [DataContract]
    class Tile
    {
        /// <summary>
        /// Constructs a new tile
        /// </summary>
        /// <param name="x">x position of the tile in the map</param>
        /// <param name="y">y position of the tile in the map</param>
        /// <param name="z">z position of the tile in the map</param>
        public Tile(uint x, uint y, uint z)
        {//This stuff is performed on creation, right?
            AssociatedAreas = new List<Area>();
            AssociatedPlaceables = new List<PlaceableObject>();

            PositionX = x;
            PositionY = y;
            PositionZ = z;

            HasCeiling = false;
            HasFloor = false;
        }
        /// <summary>
        /// X position in space. Should correspond to an index stored in a map
        /// </summary>
        [DataMember]
        public uint PositionX { get; private set; }
        /// <summary>
        /// Y position in space. Should correspond to an index stored in a map
        /// </summary>
        [DataMember]
        public uint PositionY { get; private set; }
        /// <summary>
        /// Z position in space. Should correspond to an index stored in a map
        /// </summary>
        [DataMember]
        public uint PositionZ { get; private set; }
        /// <summary>
        /// Stores the placeables associated with this tile
        /// </summary>
        [DataMember]
        public List<PlaceableObject> AssociatedPlaceables { get; set; }
        /// <summary>
        /// Typically a tile will only have one area, but it is possible for a tile to be
        /// shared between more areas (or have none at all)
        /// </summary>
        [DataMember]
        public List<Area> AssociatedAreas { get; set; }

        [DataMember]
        public Boolean HasFloor { get; set; }

        [DataMember]
        public Boolean HasCeiling { get; set; }

    }
}
