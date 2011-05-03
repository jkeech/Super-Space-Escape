/*******************************************************************************
  MapEditor v1.00
  Developed by: Zach Pollock, Michael Perkins, and John Keech
  Copyright 2011 - All rights reserved
 
  Disclaimer: The following software is provided "as-is" for educational
               purposes and is the intellectual property of Texas A&M University
               created by the forementioned developers for the Computer Science
               and Engineering Department Programming Studio course.
*******************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MapEditor
{
    public enum LayerType { BACKGROUND, FOREGROUND, OBJECTS, COLLISION };
    public enum ForeType { NONE,SOLID,RANDOM,DIAGL,DIAGR,BARH,BARV};

    /***********************************************************************
      Contains the entire map data as well as all of the objects in the
      game world. Each map is made of multiple TileLayer objects, which in
      turn have multiple tiles per layer. Map -> TileLayer -> Tile
    ***********************************************************************/
    class Map
    {
        // Foreground and background layers
        TileLayer[] map;
        int width, height;
        string title;
        ForeType fore=ForeType.SOLID;
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
            
            map = new TileLayer[3]; // Background layer then foreground layer then objects
            map[(int)LayerType.BACKGROUND] = new TileLayer(w, h);
            map[(int)LayerType.FOREGROUND] = new TileLayer(w*2, h*2);
            map[(int)LayerType.OBJECTS] = new TileLayer(w, h);
        }

        /// <summary>
        /// Returns a specific layer of the map
        /// </summary>
        /// <param name="type">Input which layer to retrieve</param>
        public TileLayer getLayer(LayerType type)
        {
            if (type == LayerType.BACKGROUND)
                return map[0];
            else if (type == LayerType.FOREGROUND)
                return map[1];
            else
                return map[2];
        }

        public string getName()
        {
            return title;
        }

        public int getHeight()
        {
            return height;
        }

        public int getWidth()
        {
            return width;
        }
    }
}
