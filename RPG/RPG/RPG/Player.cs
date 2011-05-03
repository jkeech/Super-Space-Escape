using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPG
{
    /// <summary>
    /// Holds the data for the player
    /// </summary>
    class Player
    {
        int x;
        int y;

        /// <summary>
        /// Constructor for a Player object
        /// </summary>
        /// <param name="xLoc">Input the initial x coordinate of the player</param>
        /// <param name="yLoc">Input the initial y coordinate of the player</param>
        public Player(int xLoc, int yLoc)
        {
            x = xLoc;
            y = yLoc;
        }

        /// <summary>
        /// Returns the current x coordinate of the player
        /// </summary>
        public int getX()
        {
            return x;
        }

        /// <summary>
        /// Returns the current y coordinate of the player
        /// </summary>
        public int getY()
        {
            return y;
        }

        /// <summary>
        /// Sets the current x coordinate of the player
        /// </summary>
        /// <param name="xLoc">Input the initial x coordinate of the player</param>
        public void setX(int xLoc)
        {
            x = xLoc;
        }

        /// <summary>
        /// Sets the current y coordinate of the player
        /// </summary>
        /// <param name="yLoc">Input the initial y coordinate of the player</param>
        public void setY(int yLoc)
        {
            y = yLoc;
        }
    }
}
