using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Dugeon_Crawl.GameLogic.GameLevel.Loading
{
    struct XmlFloorPlane
    {
        public List<Vector3> OccupiedTiles { get; set; }
        public Tile AssociatedTile { get; set; }
    }
}
