using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    /// <summary>
    /// This class maps an ID to an actual location in the sprite sheet.
    /// For example, background tile 0 might map to location 5 in the tileset image.
    /// This class handles the IDs for background tiles, foreground tiles, and objects
    /// </summary>
    class TileLookup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TileLookup() { }

        /// <summary>
        /// Looks up the location in the texture file of an object with a corresponding ID
        /// </summary>
        /// <param name="id">ID to look up</param>
        public int getTile(int id)
        {
            switch (id)
            {
                // Background tiles
                case 0: return 0;

                // Game Objects

                // Foreground Tiles

                default: return 0;
            }
        }
    }
}
