using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Xml.Serialization;

using OpenTK;

namespace Dugeon_Crawl.GameLogic.GameLevel.Loading
{
    struct XmlMap
    {
        public uint XSize { get; set; }
        public uint YSize { get; set; }
        public uint ZSize { get; set; }
        public List<XmlArea> Areas { get; set; }
        public List<XmlWallPlane> Walls { get; set; }
        public List<XmlFloorPlane> Floors { get; set; }
        public List<XmlCeilingPlane> Ceilings { get; set; }

        public static Map LoadMap(string xmlFilePath)
        {
            Stream stream = File.Open(xmlFilePath, FileMode.Open);
            XmlSerializer xFormatter = new XmlSerializer(typeof(XmlMap));
            XmlMap map = (XmlMap)xFormatter.Deserialize(stream);
            stream.Close();

            Map mapTo = new Map(map.XSize, map.YSize, map.ZSize);

            foreach (XmlArea area in map.Areas)
                mapTo.Areas.Add(new Area(area.Name, area.Description, 
                    convertTileList(area.AssociatedTiles, mapTo)));
            

            return mapTo;
        }

        private static List<Tile> convertTileList(List<Vector3> list, Map map)
        {
            List<Tile> tiles = new List<Tile>();
            foreach (Vector3 v in list)
                tiles.Add(map.TileGrid[(int)v.X][(int)v.Y][(int)v.Z]);
            return tiles;
        }
    }
}
