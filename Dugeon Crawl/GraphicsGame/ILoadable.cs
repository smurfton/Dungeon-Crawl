using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeon_Crawl.GraphicsGame
{
    /// <summary>
    /// If included, the object should be loadable AT THE START OF THE PROGRAM
    /// </summary>
    interface ILoadable
    {
        void Load();
    }
}
