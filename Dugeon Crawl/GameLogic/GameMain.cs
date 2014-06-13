using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using Dungeon_Crawl.GameLogic.GameLevel;
using Dungeon_Crawl.GameLogic.GameLevel.Placeables;

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

            // Yo simon- this is all very loose at the moment but basic idea is we're gonna make an
            // editor that does this first part so we don't have to hard-code it like this
            Map testMap = new Map(5, 5, 1);

            // K, we're gonna add a closet with these tiles, we'll make it 2x2 tiles = 10x10 feet
            List<Tile> roomTiles = new List<Tile>();
            roomTiles.Add(testMap.TileGrid[0][0][0]);
            roomTiles.Add(testMap.TileGrid[0][1][0]);
            roomTiles.Add(testMap.TileGrid[1][0][0]);
            roomTiles.Add(testMap.TileGrid[1][1][0]);

            Area TheOnlyRoomInThisDungeon = new Area("The Amazing Closet",
                "You wake up to almost complete darkness. The only source of light is a hole" +
                " in the ceiling. However, this is irrelevant because you don't need to see." +
                " You can feel using your hands with equivalent authority that you're trapped" +
                " in a closet with no escape.", roomTiles);
            testMap.Areas.Add(TheOnlyRoomInThisDungeon);
            foreach (Tile tile in roomTiles)
                tile.AssociatedAreas.Add(TheOnlyRoomInThisDungeon);

            // K, we have an area, but it is just an area, we don't have anything in it
            // Grrreeeatttt, looks like we need walls, floors, and ceilings. This is going to
            // take a while
            WallPlane nl = new WallPlane(Direction.North, testMap.TileGrid[0][1][0]);
            testMap.TileGrid[0][1][0].AssociatedPlaceables.Add(nl);
            testMap.AllPlaceables.Add(nl);

            WallPlane nr = new WallPlane(Direction.North, testMap.TileGrid[1][1][0]);
            testMap.TileGrid[1][1][0].AssociatedPlaceables.Add(nr);
            testMap.AllPlaceables.Add(nr);

            WallPlane eu = new WallPlane(Direction.East, testMap.TileGrid[1][1][0]);
            testMap.TileGrid[1][1][0].AssociatedPlaceables.Add(eu);
            testMap.AllPlaceables.Add(eu);

            WallPlane ed = new WallPlane(Direction.East, testMap.TileGrid[1][0][0]);
            testMap.TileGrid[1][0][0].AssociatedPlaceables.Add(ed);
            testMap.AllPlaceables.Add(ed);

            WallPlane sl = new WallPlane(Direction.South, testMap.TileGrid[0][0][0]);
            testMap.TileGrid[0][0][0].AssociatedPlaceables.Add(sl);
            testMap.AllPlaceables.Add(sl);

            WallPlane sr = new WallPlane(Direction.South, testMap.TileGrid[1][0][0]);
            testMap.TileGrid[1][0][0].AssociatedPlaceables.Add(sr);
            testMap.AllPlaceables.Add(sr);

            WallPlane wu = new WallPlane(Direction.West, testMap.TileGrid[0][1][0]);
            testMap.TileGrid[0][1][0].AssociatedPlaceables.Add(wu);
            testMap.AllPlaceables.Add(wu);

            WallPlane wd = new WallPlane(Direction.West, testMap.TileGrid[0][0][0]);
            testMap.TileGrid[0][0][0].AssociatedPlaceables.Add(wd);
            testMap.AllPlaceables.Add(wd);

            // Could've just gone through the room tiles list I already created, but wanted
            // to show you that you could do this
            for (int x = 0; x < 2; x++)
            {
                for (int y = 0; y < 2; y++)
                {
                    Tile tile = testMap.TileGrid[x][y][0];

                    FloorPlane floor = new FloorPlane(tile);
                    tile.AssociatedPlaceables.Add(floor);
                    testMap.AllPlaceables.Add(floor);

                    CeilingPlane ceiling = new CeilingPlane(tile);
                    tile.AssociatedPlaceables.Add(ceiling);
                    testMap.AllPlaceables.Add(ceiling);
                }
            }

            // K, finally done. So glad we won't have to write our levels like this
            testMap.SaveMap("testMap.xml");

            // But then on the game end, we'll do something like this. If you put a breakpoint on that right
            // curly bracket (after testMap2 has been generated), and then mouse over testMap2, you can see
            // the data is the same as testMap! Technology. Woah.
            Map testMap2 = Map.LoadMap("testMap.xml");
        }
    }
}
