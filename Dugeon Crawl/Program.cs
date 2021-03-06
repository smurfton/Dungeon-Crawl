﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System.Drawing;

namespace Dungeon_Crawl
{
    class Program
    {
        /// <summary>
        /// Our main entry point for the program
        /// </summary>
        /// <param name="args">Command line arguments</param>
        [STAThread]
        static void Main(string[] args)
        {
            if (ChooseGameType() == 1) // Start console game
            {
                ConsoleGame.GameConsoleVersion game = new ConsoleGame.GameConsoleVersion();
                GameLogic.GameMain.StartGame(game);
            }
            else // Start graphics game
            {
                using (GraphicsGame.GameGraphicsVersion game = new GraphicsGame.GameGraphicsVersion())
                {
                    game.Load += (object o, EventArgs e) =>
                        {
                            GameLogic.GameMain.StartGame(game);
                        };
                    game.Run(60.0);
                }
            }
        }

        /// <summary>
        /// Finds what game type the user wants to play with validation check
        /// </summary>
        /// <returns>1 for console, 2 for graphics</returns>
        static int ChooseGameType()
        {
            Console.Write("Which version would you like to play? \n1.) Console\n2.) Graphics\n");
            char input = Console.ReadKey().KeyChar;

            switch (input)
            {
                case '1':
                    return 1;
                case '2':
                    return 2;
                default:
                    Console.WriteLine("That game version does not exist. Please try again.");
                    return ChooseGameType();
            }
        }
    }
}
