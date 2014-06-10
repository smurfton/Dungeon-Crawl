﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Dungeon_Crawl.ConsoleGame
{
    /// <summary>
    /// The main implementation for the console version of the game
    /// </summary>
    class GameConsoleVersion : IGame
    {
        public void ChangeTriangleColor(Color color)
        {
            Console.WriteLine();
            Console.WriteLine("The triangle in front of you changes to a lovely " + color.Name + ".");
            Console.ReadKey();
        }
    }
}
