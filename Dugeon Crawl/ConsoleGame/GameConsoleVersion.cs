﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using Dungeon_Crawl.GameLogic.GameConversation;

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
        }


        public void TalkToBob(Conversation whatConversation)
        {
            Console.WriteLine();
            Console.WriteLine("Bob suddenly pops out of a bush and approaches you.");
            Console.WriteLine("Bob: " + whatConversation.StartConversation());
            bool validConversation = true;
            while (validConversation)
            {
                try
                {
                    Console.WriteLine("Bob: " + 
                        whatConversation.Choose(playerConversationChoices(whatConversation)));
                }
                catch
                {
                    validConversation = false;
                }
            }
        }

        /// <summary>
        /// Displays conversation choices, allows user to choose, returns index of choice
        /// </summary>
        /// <param name="conv">Conversation to choose from</param>
        /// <returns>The player's choice</returns>
        int playerConversationChoices(Conversation conv)
        {
            List<string> choices = conv.GetChoices();
            for (int index = 0; index < choices.Count; index++)
                Console.WriteLine((index + 1).ToString() + ".) " + choices[index]);
            char c = Console.ReadKey(true).KeyChar;
            Console.WriteLine();
            return Convert.ToInt32(c.ToString()) - 1;
        }
    }
}
