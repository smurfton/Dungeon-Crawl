using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace Dungeon_Crawl.GameLogic
{
    /// <summary>
    /// The write-once logic for the entire game - for both console and graphics
    /// </summary>
    static class GameMain
    {
        /// <summary>
        /// This is the entry point for the logic of the game
        /// </summary>
        /// <param name="gameControl">Pass in either a GameConsoleVersion or a GameGraphicsVersion</param>
        public static void StartGame(IGame gameControl)
        {
            gameControl.ChangeTriangleColor(Color.Red);
            gameControl.TalkToBob(new GameConversation.Conversation("Content\\Conversations\\Bob\\Bob.xml"));
        }
    }
}
