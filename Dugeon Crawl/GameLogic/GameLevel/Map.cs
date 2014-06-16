using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using System.Runtime.Serialization;
using System.IO;

using TileGrid = System.Collections.Generic.List<
    System.Collections.Generic.List<
    System.Collections.Generic.List<
    Dungeon_Crawl.GameLogic.GameLevel.Tile>>>;

namespace Dungeon_Crawl.GameLogic.GameLevel
{
    /// <summary>
    /// Defines the cardinal directions!
    /// </summary>
    public enum Direction
    {
        /// <summary>
        /// Defined as (0, 1)
        /// </summary>
        North,
        /// <summary>
        /// Defined as (1, 0)
        /// </summary>
        East,
        /// <summary>
        /// Defined as (0, -1)
        /// </summary>
        South,
        /// <summary>
        /// Defined as (-1, 0)
        /// </summary>
        West
    }

    /// <summary>
    /// Represents a whole map which is a chunk of the world that the player will explore
    /// </summary>
    [DataContract]
    class Map
    {
        [DataMember]
        public List<Area> Areas { get; set; }
        [DataMember]
        public TileGrid TileGrid { get; set; }
        [DataMember]
        public List<PlaceableObject> AllPlaceables { get; set; }

        /// <summary>
        /// Creates a new map from an xml document
        /// </summary>
        /// <param name="xmlFilePath">The path to the .xml</param>
        /// <returns>The deserialized map</returns>
        public static Map LoadMap(string xmlFilePath)
        {
            Stream stream = File.Open(xmlFilePath, FileMode.Open);
            DataContractSerializer serializer = GetSerializer();
            object map = serializer.ReadObject(stream);
            stream.Close();
            return map as Map;
        }

        /// <summary>
        /// Saves this map to a xml file
        /// </summary>
        /// <param name="xmlFilePath">The path to the .xml</param>
        public void SaveMap(string xmlFilePath)
        {
            Stream stream = File.Create(xmlFilePath);
            DataContractSerializer serializer = GetSerializer();
            serializer.WriteObject(stream, this);
            stream.Close();
        }

        private static DataContractSerializer GetSerializer()
        {
            DataContractSerializerSettings settings = new DataContractSerializerSettings();
            Type[] types = { typeof(Area), typeof(Tile), typeof(PlaceableObject),
                           typeof(DescribableObject), typeof(Placeables.WallPlane),
                           typeof(Placeables.FloorPlane), typeof(Placeables.CeilingPlane)};
            settings.KnownTypes = types;
            settings.PreserveObjectReferences = true;
            return new DataContractSerializer(typeof(Map), settings);
        }

        /// <summary>
        /// Constructs a new map
        /// </summary>
        /// <param name="xSize">X size, in tile units, of the map</param>
        /// <param name="ySize">Y size, in tile units, of the map</param>
        /// <param name="zSize">Z size, in tile units, of the map</param>
        public Map(uint xSize, uint ySize, uint zSize)
        {
            Areas = new List<Area>();
            AllPlaceables = new List<PlaceableObject>();

            // Populate grid! This part is verbose, but luckily it's only done once
            TileGrid = new TileGrid();
            for (uint x = 0; x < xSize; x++)
            {
                List<List<Tile>> yzGrid = new List<List<Tile>>();
                TileGrid.Add(yzGrid);
                for (uint y = 0; y < ySize; y++)
                {
                    List<Tile> zGrid = new List<Tile>();
                    yzGrid.Add(zGrid);
                    for (uint z = 0; z < zSize; z++)
                    {
                        zGrid.Add(new Tile(x, y, z));
                    }
                }
            }
        }

        /// <summary>
        /// Converts a Direction enum into unit vector format
        /// </summary>
        /// <param name="direction">The direction to convert</param>
        /// <returns>A unit vector that points in the direction given</returns>
        public static Vector2 GetCardinalDirectionUnitVector(Direction direction)
        {
            switch(direction)
            {
                case Direction.North:
                    return new Vector2(0, 1);
                case Direction.East:
                    return new Vector2(1, 0);
                case Direction.South:
                    return new Vector2(0, -1);
                default:
                    return new Vector2(-1, 0);
            }
        }

        /// <summary>
        /// Converts a Direction enum into an angle format
        /// </summary>
        /// <param name="direction">The direction to convert</param>
        /// <returns>An angle that points in the direction given</returns>
        public static float GetCardinalDirectionYaw(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return 0.0f;
                case Direction.East:
                    return MathHelper.PiOver2;
                case Direction.South:
                    return MathHelper.Pi;
                default:
                    return MathHelper.PiOver2 * 3;
            }
        }

        /// <summary>
        /// Gets the opposite direction of the direction passed in
        /// </summary>
        /// <param name="direction">The initial direction</param>
        /// <returns>The opposite direction of the initial direction</returns>
        public static Direction GetOppositeDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    return Direction.South;
                case Direction.East:
                    return Direction.West;
                case Direction.South:
                    return Direction.North;
                default:
                    return Direction.East;
            }
        }
    }
}
