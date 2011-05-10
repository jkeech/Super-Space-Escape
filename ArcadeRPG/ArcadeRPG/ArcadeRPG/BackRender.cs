using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using System.Text;

namespace ArcadeRPG
{

    class BackRender
    {
        Texture2D tile; // rerpesents each tile of the map
        int tile_size = 32;//possibly needs to be set, should be make the option for HD maps, bad idea though for collision purposes
        int tile_row_num;
        int locx =30; // location in the x direction
        int locy = 133; // location in the y direction
        int big_small = 1; //small or large res. small =0, large=1. win phones have 2 resolutions, need to plan for both eventually 
        private List<Texture2D> tiles; // all the tiles of a map in a collective structure
        List<List<int>> tile_map;
        int map_size_x; // map size along the x axis
        int map_size_y; // map size along the y axis
        
        public List<int> get_cur_tile_pos() //returns the current position, according to the tiles (tile, pos on tile)
        {
            int xtile= locx / tile_size;
            int xpos = locx % tile_size;
            int ytile= locy / tile_size;
            int ypos = locy % tile_size;
            
            List<int> ret= new System.Collections.Generic.List<int>();
            ret.Add(xtile);
            ret.Add(xpos);
            ret.Add(ytile);
            ret.Add(ypos);

            return ret;
        }

        public BackRender(List<List<int>> tile_mapt, Texture2D tiles, int tile_dim, int tile_num) //tile dim is the width of the tiles, tile_num is the number of tiles per row of a tilesheet. Keep to square sheets for ease of use
        {
            tile_row_num = tile_num;
            tile_map = tile_mapt;
            tile_size = tile_dim;
            tile = tiles;
            map_size_x = tile_map.Count;
            map_size_y = tile_map[0].Count;
        }
        internal void draw_back(SpriteBatch tile_batch, int xpos, int ypos)
        {
            locx = xpos;
            locy = ypos;
            bool at_edge = false; // is the tile located at the border of the screen? no by default

            List<int> tile_pos = get_cur_tile_pos(); //(tile x,pos x,tile y,pos y)
            List<int> tile_bound = new System.Collections.Generic.List<int>(); //(xmin,xmax,ymin,ymax)

            //from this point until the draw function call, determine where to place the tiles

            if ( (locx < 400) || (locx > (map_size_x - 400)) ) // is the tile located on the border of the screen?
            {
                at_edge = true;
                if ((locx < 400))
                {
                    tile_bound.Add(0);
                    tile_bound.Add(25);
                }
                if ((locx > (map_size_x - 400)))
                {
                    tile_bound.Add(tile_map.Count-26);
                    tile_bound.Add(tile_map.Count-1);
                }
            }
            else
            {
                tile_bound.Add(tile_pos[0] - 13);
                tile_bound.Add(tile_pos[0] + 13);
            }
            if ((locy < 240) || (locy > (map_size_y - 240)) )
            {
                at_edge = true;
                if ((locy < 240))
                {
                    tile_bound.Add(0);
                    tile_bound.Add(15);
                }
                if ((locy > (map_size_y - 240)))
                {
                    tile_bound.Add(tile_map.Count-16);
                    tile_bound.Add(tile_map.Count-1);
                }
            }
            else
            {
                tile_bound.Add(tile_pos[0] - 8);
                tile_bound.Add(tile_pos[0] + 8);
            }
             
            
            List<Rectangle> tile_spot = new List<Rectangle>();
            List<Rectangle> source = new List<Rectangle>();
            int xdisp=0;
            int ydisp=0;
            if (!at_edge)
            {
                xdisp = (tile_size / 2) - tile_pos[1];
                if (xdisp < 0)
                    xdisp += tile_size;
                ydisp = (tile_size / 2) - tile_pos[3];
                if (ydisp < 0)
                    ydisp += tile_size;
            }
            int cur_row = 0;
            int cur_col = 0;
            for (int x = tile_bound[0]; x < tile_bound[1]; x++)
                for (int y = tile_bound[2]; y < tile_bound[3]; y++)
                {
                    cur_row = tile_map[x][y] / tile_row_num;
                    cur_col = tile_map[x][y] % tile_row_num;
                    source.Add(new Rectangle(cur_row * tile_size, cur_col * tile_size, tile_size, tile_size));
                }

            if (big_small == 1) //set displacement of tiles
            {
                for (int x = -1; x < 27; x++)
                    for (int y = -1; y < 17; y++)
                    {
                        tile_spot.Add(new Rectangle((x * tile_size) - xdisp, y * tile_size, tile_size, tile_size));
                    }
            }
            
            for (int x=0;x<tile_spot.Count;x++)  // draw all of the tiles
            {    
                tile_batch.Draw(tile, tile_spot[x], source[x], Color.White);      
            }
            
            
        }
    }
}
