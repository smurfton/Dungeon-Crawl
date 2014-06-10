using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Dungeon_Crawl
{
    /// <summary>
    /// An interface used to describe functions common to both Console and Graphics game types
    /// </summary>
    interface IGame
    {
        /// <summary>
        /// A temporary function to demonstrate the design of the game. Changes the triangle's color.
        /// </summary>
        /// <param name="color">Color to change to.</param>
        void ChangeTriangleColor(Color color);
    }
}
