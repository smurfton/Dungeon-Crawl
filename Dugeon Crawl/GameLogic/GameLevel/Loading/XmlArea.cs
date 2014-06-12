using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;

namespace Dugeon_Crawl.GameLogic.GameLevel.Loading
{
    struct XmlArea
    {
        public List<Vector3> AssociatedTiles { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
