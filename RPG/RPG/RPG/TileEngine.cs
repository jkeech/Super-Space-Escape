using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Xna.Framework;

namespace RPG
{
    /// <summary>
    /// Contains the map for each level, as well as loads the map data at the beginning of the game execution.
    /// The TileEngine stores a list of Map objects. The whole structure (from largest to smallest) is:
    /// TileEngine -> Map -> TileLayer -> Tile
    /// </summary>
    class TileEngine
    {
        static int TILE_SIZE; // Square tiles (size of each side in pixels)
        int currentLevel;

        List<Map> levels;

        /// <summary>
        /// Constructor for a TileEngine object
        /// </summary>
        /// <param name="tileSize">Input the size in pixels of each tile</param>
        public TileEngine(int tileSize)
        {
            TILE_SIZE = tileSize;
            levels = new List<Map>();
            currentLevel = 0;
        }

        /// <summary>
        /// Loads a level from a file and adds it to the list of levels
        /// </summary>
        /// <param name="file">Input the file name where the level is located. Only input the name, not the extension. Level files by default are stored in the Content/levels/ folder. Ex: To load "Content/levels/level1.txt", simply call loadLevel("level1");</param>
        public void loadLevel(String file)
        {
            Stream stream = TitleContainer.OpenStream("Content/levels/" + file + ".txt");
            StreamReader sr = new StreamReader(stream);

            //begin reading data

            // Title
            string title;
            title = sr.ReadLine();

            // Rest of the data
            string[] id = sr.ReadToEnd().Replace(Environment.NewLine," ").Split(' ');
            int width, height;
            int.TryParse(id[0],out width);
            int.TryParse(id[1],out height);

            // Create the map
            Map newLevel = new Map(title, width, height);

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
                    if (textureID == 1)
                        hasCollision = true;
                    else
                        hasCollision = false;

                    // Create a new tile at location (x,y) with texture id given and collision flag = 0
                    newLevel.getLayer(LayerType.BACKGROUND).getTile(x,y).setCollision(hasCollision);

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
                    newLevel.getLayer(LayerType.FOREGROUND).setTile(new Tile(textureID, false), x, y);

                    // increment the currentID
                    currentID++;
                }
            }

            levels.Add(newLevel);
        }

        /// <summary>
        /// Returns the current level
        /// </summary>
        public int getCurrentLevel()
        {
            return currentLevel;
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
        /// Returns a specific layer of the current map
        /// </summary>
        /// <param name="type">Input which layer to retrieve</param>
        public TileLayer getLayer(LayerType type)
        {
            return levels[currentLevel].getLayer(type);
        }

        /// <summary>
        /// Adds a game object to the current map. To be used by the Game Engine when adding new items to a level.
        /// </summary>
        /// <param name="o">Input the object to add</param>
        public void addObject(object o)
        {
            levels[currentLevel].addObject(o);
        }

        /// <summary>
        /// Draws all of the background tiles to the screen for the current level
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void drawBackground(int x, int y)
        {
            levels[currentLevel].drawBackground(x, y);
        }

        /// <summary>
        /// Draws all of the foreground tiles to the screen for the current level
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void drawForeground(int x, int y)
        {
            levels[currentLevel].drawForeground(x, y);
        }

        /// <summary>
        /// Draws all of the game objects to the screen for the current level
        /// </summary>
        public void drawObjects()
        {
            levels[currentLevel].drawObjects();
        }

        /// <summary>
        /// Draws the player to the screen for the current level
        /// </summary>
        public void drawPlayer()
        {
            levels[currentLevel].drawPlayer();
        }

        /// <summary>
        /// Draws everything on the screen in the correct order
        /// (background, objects, player, and foreground) for the current level
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void draw(int x, int y)
        {
            levels[currentLevel].drawBackground(x, y);
            levels[currentLevel].drawObjects();
            levels[currentLevel].drawPlayer();
            levels[currentLevel].drawForeground(x, y);
        }
    }
}
