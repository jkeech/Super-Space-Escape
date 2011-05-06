using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    
    /// <summary>
    /// Holds the data for one tile layer (either the foreground or background layer).
    /// </summary>
    class TileLayer
    {
        Tile[,] layer;
        int width;
        int height;

        /// <summary>
        /// Constructor for a TileLayer object
        /// </summary>
        /// <param name="w">Input the width of the layer (number of tiles wide)</param>
        /// <param name="h">Input the height of the layer (number of tiles tall)</param>
        public TileLayer(int w, int h)
        {
            width = w;
            height = h;
            layer = new Tile[h, w];
        }

        /// <summary>
        /// Sets a tile on the layer at a specific location (x,y)
        /// </summary>
        /// <param name="tile">Input the Tile object to store at that location</param>
        /// <param name="x">Input the x location of the tile</param>
        /// <param name="y">Input the y location of the tile</param>
        public void setTile(Tile tile, int x, int y)
        {
            if(x >= 0 && x < width && y >= 0 && y < height) // Bounds checking
                layer[y, x] = tile;
        }

        /// <summary>
        /// Returns a tile at a specific location (x,y)
        /// </summary>
        /// <param name="x">Input the x location of the tile</param>
        /// <param name="y">Input the y location of the tile</param>
        public Tile getTile(int x, int y)
        {
            if (x >= 0 && x < width && y >= 0 && y < height) // Bounds checking
                return layer[y, x];
            else
                return null;
        }

        public int getWidth()
        {
            return width;
        }

        public int getHeight()
        {
            return height;
        }
    }   
}
