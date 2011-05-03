using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG
{
    /// <summary>
    /// Holds the data for one tile on either the foreground or background map.
    /// </summary>
    class Tile
    {
        int texture;
        bool collision;

        /// <summary>
        /// Constructor for a Tile object
        /// </summary>
        /// <param name="tex">Input the texture ID of the tile</param>
        /// <param name="col">Boolean to determine whether the tile has collision</param>
        public Tile(int tex, bool col)
        {
            texture = tex;
            collision = col;
        }

        /// <summary>
        /// Sets the texture for a tile
        /// </summary>
        /// <param name="tex">Input the texture ID to set the tile to</param>
        public void setTexture(int tex)
        {
            texture = tex;
        }

        /// <summary>
        /// Sets the collision flag for a tile
        /// </summary>
        /// <param name="col">Input the boolean value that collision should be set to</param>
        public void setCollision(bool col)
        {
            collision = col;
        }

        /// <summary>
        /// Returns the current texture ID for the tile
        /// </summary>
        public int getTexture()
        {
            return texture;
        }

        /// <summary>
        /// Returns the current collision setting for the tile
        /// </summary>
        public bool hasCollision()
        {
            return collision;
        }
    }
}
