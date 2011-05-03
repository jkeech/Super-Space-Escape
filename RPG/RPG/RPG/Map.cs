using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG
{
    public enum LayerType { BACKGROUND, FOREGROUND };

    /// <summary>
    /// Contains the entire map data as well as all of the objects in the game world.
    /// Each map is made of multiple TileLayer objects, which in turn have multiple
    /// tiles per layer. Map -> TileLayer -> Tile
    /// </summary>
    class Map
    {
        // Foreground and background layers
        TileLayer[] map;
        int width, height;
        string title;

        // List of all game objects
        List<object> gameObjects;


        /// <summary>
        /// Constructor for a Map object
        /// </summary>
        /// <param name="w">Input the width of the map in tiles</param>
        /// <param name="h">Input the height of the map in tiles</param>
        public Map(string name, int w, int h)
        {
            title = name;
            width = w;
            height = h;
            
            map = new TileLayer[2]; // Background layer then foreground layer
            map[0] = new TileLayer(w, h);
            map[1] = new TileLayer(w, h);

            gameObjects = new List<object>();
        }

        /// <summary>
        /// Returns a specific layer of the map
        /// </summary>
        /// <param name="type">Input which layer to retrieve</param>
        public TileLayer getLayer(LayerType type)
        {
            if (type == LayerType.BACKGROUND)
                return map[0];
            else
                return map[1];
        }

        /// <summary>
        /// Adds a game object to the map. To be used by the Game Engine when adding new items to a level.
        /// </summary>
        /// <param name="o">Input the object to add</param>
        public void addObject(object o)
        {
            gameObjects.Add(o);
        }

        /// <summary>
        /// Draws all of the background tiles to the screen
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void drawBackground(int x, int y)
        {

        }

        /// <summary>
        /// Draws all of the foreground tiles to the screen
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void drawForeground(int x, int y)
        {

        }

        /// <summary>
        /// Draws all of the game objects to the screen
        /// </summary>
        public void drawObjects()
        {

        }

        /// <summary>
        /// Draws the player to the screen
        /// </summary>
        public void drawPlayer()
        {

        }

        /// <summary>
        /// Draws everything on the screen in the correct order (background, objects, player, and foreground)
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void draw(int x, int y)
        {
            drawBackground(x, y);
            drawObjects();
            drawPlayer();
            drawForeground(x, y);
        }
    }
}
