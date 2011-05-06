using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeRPG
{
    /// <summary>
    /// Contains the map for each level, as well as loads the map data at the beginning of the game execution.
    /// The TileEngine stores a list of Map objects. The whole structure (from largest to smallest) is:
    /// TileEngine -> Map -> TileLayer -> Tile
    /// </summary>
    class TileEngine
    {
        static public int TILE_SIZE; // Square tiles (size of each side in pixels)
        int currentLevel;
       // TileLoader tileLoader;
        List<Texture2D> tiles;

        List<Map> levels;

        /// <summary>
        /// Constructor for a TileEngine object
        /// </summary>
        /// <param name="tileSize">Input the size in pixels of each tile</param>
        public TileEngine(int tileSize, List<Texture2D> tilest)
        {
            TILE_SIZE = tileSize;
            levels = new List<Map>();
            tiles = tilest;
            currentLevel = -1;
        }

        /// <summary>
        /// Loads a level from a file and adds it to the list of levels
        /// </summary>
        /// <param name="file">Input the file name where the level is located. Only input the name, not the extension. Level files by default are stored in the Content/levels/ folder. Ex: To load "Content/levels/level1.txt", simply call loadLevel("level1");</param>
        public void loadLevel(String file)
        {
            Stream stream = TitleContainer.OpenStream("Content/levels/" + file + ".lvl");
            StreamReader sr = new StreamReader(stream);

            //begin reading data

            // Title
            string title;
            title = sr.ReadLine();

            // Rest of the data
            //XXX - Problem here?
            string[] id = sr.ReadToEnd().Replace(Environment.NewLine," ").Split(' ');

            int width, height;
            int.TryParse(id[0],out width);
            int.TryParse(id[1],out height);

            // Create the map
            Map newLevel = new Map(title, width, height, TILE_SIZE, tiles);
           // tileLoader.resetLayers(width, height);

            //Populate the map data
            int currentID = 3;
            int textureID = 0;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Convert the texture ID to an int
                    int.TryParse(id[currentID], out textureID);

                    // Create a new tile at location (x,y) with texture id given and collision flag = 0
                    newLevel.getLayer(LayerType.BACKGROUND).setTile(new Tile(textureID, false), x, y);
                   // tileLoader.addToLayer(LayerType.BACKGROUND, x, y, textureID);

                    // increment the currentID
                    currentID++;
                }
            }

            currentID++; // For line break after each array
            bool hasCollision = false;
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Convert the texture ID to an int
                    int.TryParse(id[currentID], out textureID);
                    if (textureID == 0)
                        hasCollision = true;
                    else
                        hasCollision = false;

                    // Create a new tile at location (x,y) with texture id given and collision flag = 0
                    newLevel.getLayer(LayerType.BACKGROUND).getTile(x,y).setCollision(hasCollision);
                    //tileLoader.addToLayer(LayerType.COLLISION, x, y, textureID);

                    // increment the currentID
                    currentID++;
                }
            }

            currentID++; // For line break after each array
            for (int y = 0; y < height*2; y++)
            {
                for (int x = 0; x < width*2; x++)
                {

                    // Convert the texture ID to an int
                    int.TryParse(id[currentID], out textureID);

                    if (x == 59 && y == 44)
                    {
                        Console.WriteLine(textureID);
                    }

                    // Create a new tile at location (x,y) with texture id given and collision flag = 0
                    newLevel.getLayer(LayerType.FOREGROUND).setTile(new Tile(textureID, false), x, y);
                    //tileLoader.addToLayer(LayerType.FOREGROUND, x, y, textureID);

                    // increment the currentID
                    currentID++;
                }
            }

            currentID++; // For line break after each array
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // Convert the texture ID to an int
                    int.TryParse(id[currentID], out textureID);

                    // Create a new tile at location (x,y) with texture id given and collision flag = 0
                    newLevel.getLayer(LayerType.OBJECTS).setTile(new Tile(textureID, false), x, y);
                    //tileLoader.addToLayer(LayerType.OBJECTS, x, y, textureID);

                    // increment the currentID
                    currentID++;
                }
            }

            levels.Add(newLevel);
            currentLevel++;
        }

        /// <summary>
        /// Returns the current level
        /// </summary>
        public int getCurrentLevel()
        {
            return currentLevel;
        }

        public int getTileSize()
        {
            return TILE_SIZE;
        }

        /// <summary>
        /// Advances the game to the next level if possible. If successful, it returns true.
        /// If there are no levels to advance to, it returns false.
        /// </summary>
        public bool advanceLevel()
        {
            if (currentLevel < (levels.Count - 1))
            {
                currentLevel++;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Jumps to a specific level if possible. If successful, it returns true.
        /// If there are no levels to advance to, it returns false.
        /// </summary>
        /// <param name="levelNumber">Input which level number to jump to</param>
        public bool advanceToLevel(int levelNumber)
        {
            if (levelNumber < (levels.Count - 1))
            {
                currentLevel = levelNumber;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Resets the game to level 0 if possible. It successful, it returns true.
        /// If there are no levels, it returns false. Theoretically, this should never
        /// happen, but it is a good failsafe to have.
        /// </summary>
        public bool resetProgress()
        {
            if (levels.Count > 0)
            {
                currentLevel = 0;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the current map
        /// </summary>
        public Map getCurrentMap()
        {
            return levels[currentLevel];
        }

        /// <summary>
        /// Returns the map of a certain level number
        /// </summary>
        /// <param name="type">Input which map to retrieve</param>
        public Map getMap(int level)
        {
            if (level < (levels.Count - 1) && level >= 0)
            {
                return levels[level];
            }
            return levels[currentLevel];
        }

    }
}
