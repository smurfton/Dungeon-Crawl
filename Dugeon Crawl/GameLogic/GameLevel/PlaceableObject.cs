using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Runtime.Serialization;

namespace Dungeon_Crawl.GameLogic.GameLevel
{
    /// <summary>
    /// Represents a simple 3D object that is placed in a map
    /// </summary>
    [DataContract]
    abstract class PlaceableObject
    {
        /// <summary>
        /// Typically a placeable will just occupy a single tile. However, if you have a
        /// huge-ass placeable, I could see it taking up more than one
        /// </summary>
        [DataMember]
        protected List<Tile> occupiedTiles = new List<Tile>();

        /// <summary>
        /// Position in 3D space. Doesn't have to be uints like tiles! Could be anywhere!
        /// </summary>
        [DataMember]
        public Vector3 Position { get; protected set; }

        /// <summary>
        /// An Euler rotation (xyz) in radians. Appropriate for a placeable object
        /// </summary>
        [DataMember]
        public Vector3 EulerRotation { get; protected set; }

        /// <summary>
        /// Get the world transform based on Position and Rotation
        /// </summary>
        /// <returns></returns>
        protected Matrix4 GetTransform()
        {
            return Matrix4.CreateRotationX(EulerRotation.X) *
                Matrix4.CreateRotationY(EulerRotation.Y) *
                Matrix4.CreateRotationZ(EulerRotation.Z) *
                Matrix4.CreateTranslation(Position);
        }
    }
}
