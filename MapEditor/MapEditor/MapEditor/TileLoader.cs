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
using System.Drawing;

namespace MapEditor
{
    /***********************************************************************
          TileSet class - stores the individual tile bitmap images
    ***********************************************************************/

    class TileSet
    {
        public int numTiles;
        public Bitmap allTiles;
        public Bitmap[] tiles;

        public TileSet()
        {
            numTiles = 0;
        }
    }

    /***********************************************************************
          TileLoader class - Loads tile bitmaps from disk, and handles
                             the manipulation of the level bitmaps
    ***********************************************************************/

    class TileLoader
    {
        int tileSize;
        Bitmap collision;
        Bitmap eraser;
        Bitmap[] mapLayers;
        Bitmap[] tileLayers;
        TileSet[] tileSet;

        /*  These methods are for loading the individual tile images from the file into the arrays  */
        public TileLoader(int size)
        {
            tileSize = size;

            collision = new Bitmap(MapEditor.Properties.Resources.collision);
            eraser = new Bitmap(MapEditor.Properties.Resources.eraser_tile);

            mapLayers = new Bitmap[4];
            tileLayers = new Bitmap[4];

            // Load tilesets for each layer type
            tileSet = new TileSet[4];
            for(int i = 0; i < 4; i++)
                tileSet[i] = new TileSet();
            tileSet[0].allTiles = new Bitmap(MapEditor.Properties.Resources.background_cropped);
            tileSet[1].allTiles = new Bitmap(MapEditor.Properties.Resources.foreground_cropped);
            tileSet[2].allTiles = new Bitmap(MapEditor.Properties.Resources.objects_cropped);
            tileSet[3].allTiles = new Bitmap(MapEditor.Properties.Resources.collision);
            for (int i = 0; i < 4; i++)
            {
                tileSet[i].numTiles = (tileSet[i].allTiles.Width / tileSize) * (tileSet[i].allTiles.Height / tileSize);
                tileSet[i].tiles = new Bitmap[tileSet[i].numTiles];
            }

            for(int i = 0; i < 4; i++)
                for (int j = 0; j < (tileSet[i].allTiles.Height / tileSize); j++)
                    for (int k = 0; k < (tileSet[i].allTiles.Width / tileSize); k++)
                        tileSet[i].tiles[j * (tileSet[i].allTiles.Width / tileSize) + k] = tileSet[i].allTiles.Clone(
                                                                            new Rectangle(k * tileSize, j * tileSize, tileSize, tileSize),
                                                                            tileSet[i].allTiles.PixelFormat);

            // Convert tileset Bitmaps into images for use in the map editor
            convertTileSetsToBitmap();
        }

        // Creates the tile palette that goes on the left side of
        // the editor
        public void convertTileSetsToBitmap()
        {
            for (int k = 0; k < 4; k++)
            {
                int newHeight = (int)Math.Ceiling(tileSet[k].numTiles / 5.0);
                Bitmap displayTiles = new Bitmap(5 * tileSize, newHeight * tileSize);
                Graphics g = Graphics.FromImage(displayTiles);
                bool finished = false;
                for (int i = 0; i < newHeight; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (i * 5 + j >= tileSet[k].numTiles)
                        {
                            finished = true;
                            break;
                        }
                        g.DrawImage(getTile((LayerType)k, i * 5 + j), j * tileSize, i * tileSize, new Rectangle(0, 0, tileSize, tileSize), GraphicsUnit.Pixel);
                    }
                    if (finished)
                        break;
                }
                g.Dispose();
                tileLayers[k] = displayTiles;
            }
        }

        // returns the number of tiles in a TileSet
        public int getNumTiles(LayerType layer)
        {
            return tileSet[(int)layer].numTiles;
        }

        // returns the bitmap for a specific tile in a TileSet
        public Bitmap getTile(LayerType layer, int number)
        {
            if (number >= 0 && number < tileSet[(int)layer].numTiles)
                return tileSet[(int)layer].tiles[number];
            else
                return eraser;
        }

        /***********************************************************************
                    Manipulate the bitmap layers of the existing map
        ***********************************************************************/

        // Resets the layers to a certain size
        public void resetMapLayers(int w, int h)
        {
            for (int i = 0; i < 4; i++)
                mapLayers[i] = new Bitmap(w * tileSize, h * tileSize);
        }

        // Adds a tile to a specific layer's bitmap. Uses The Layer enum
        // instead of an int for clarity.
        public void addToMapLayer(LayerType layer, int x, int y, int id)
        {
            Graphics g = Graphics.FromImage(mapLayers[(int)layer]);
            g.Clip = new Region(new Rectangle(x * tileSize, y * tileSize, tileSize, tileSize));
            g.Clear(Color.Empty);
            if (id >= 0)
            {
                if(layer == LayerType.COLLISION) {
                    if(id == 0)
                        g.DrawImage(collision, x * tileSize, y * tileSize, new Rectangle(0, 0, tileSize, tileSize), GraphicsUnit.Pixel);
                } else
                    g.DrawImage(getTile(layer, id), x * tileSize, y * tileSize, new Rectangle(0, 0, tileSize, tileSize), GraphicsUnit.Pixel);
            }
            g.Dispose();
        }

        public Bitmap getMapLayer(LayerType layer)
        {
            return mapLayers[(int)layer];
        }

        public Bitmap getTileLayer(LayerType layer)
        {
            return tileLayers[(int)layer];
        }
    }

}