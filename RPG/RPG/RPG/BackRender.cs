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

//using System;
//using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace RPG
{

    class BackRender
    {
        Texture2D tile;
        int tile_size = 32;//possibly needs to be set, should be make the option for HD maps, bad idea though for collision purposes
        int tile_row_num;
        int locx =30;
        int locy = 133;
        int big_small = 1; //small or large res. small =0, large=1. win phones have 2 resolutions, need to plan for both eventually 
        private List<Texture2D> tiles;
        List<List<int>> tile_map;
        
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

        public BackRender(List<List<int>> tile_mapt, Texture2D tiles, int tile_dim, int tile_num)//tile dim is the width of the tiles, tile_num is the number of tiles per row of a tilesheet. Keep to square sheets for ease of use
        {
            // TODO: Complete member initialization
            tile_row_num = tile_num;
            tile_map = tile_mapt;
            tile_size = tile_dim;
            tile = tiles;
        }
        internal void draw_back(SpriteBatch tile_batch, int xpos, int ypos)
        {
            locx = xpos;
            locy = ypos;

            List<int> tile_pos = get_cur_tile_pos();//(tile x,pos x,tile y,pos y)
            List<int> tile_bound = new System.Collections.Generic.List<int>();//(xmin,xmax,ymin,ymax)
            
            tile_bound.Add(tile_pos[0] - 13);
            tile_bound.Add(tile_pos[0] + 13);
            tile_bound.Add(tile_pos[3] - 8);
            tile_bound.Add(tile_pos[3] + 8);
            List<Rectangle> tile_spot = new List<Rectangle>();
            List<Rectangle> source = new List<Rectangle>();
            int xdisp=(tile_size/2)-tile_pos[1];
            if (xdisp < 0)
                xdisp += tile_size;
            int cur_row = 0;
            int cur_col = 0;
            for (int x = 0; x < tile_map.Count;x++ )
                for (int y = 0; y < tile_map[x].Count; y++)
                {
                    cur_row = tile_map[x][y] / tile_row_num;
                    cur_col = tile_map[x][y] % tile_row_num;
                    source.Add(new Rectangle(cur_row * tile_size, cur_col * tile_size,tile_size,tile_size));
                }
            if (big_small == 1)//set displacement of tiles
            {
                for (int x = -1; x < 26; x++)
                    for (int y = 0; y < 16; y++)
                    {
                        tile_spot.Add(new Rectangle((x * tile_size) - xdisp, y * tile_size, tile_size, tile_size));
                    }
            }
            
            for (int x=0;x<tile_spot.Count;x++) 
            {    
                tile_batch.Draw(tile, tile_spot[x], source[x], Color.White);//white necessary for no tinting      
            }
            
            
        }
    }
}








/* add back in if needed for speed, prolly not
            int quadrant;
            if (tile_pos[1] <= tile_size/2)//quadrants based on cartesian coordinate plane
            {
                if(tile_pos[3]<= tile_size/2)//is in quad2
                {
                    quadrant = 2;
                    if(big_small==1)//if full res
                    {
                        tile_bound.Add(tile_pos[0]-13);
                        tile_bound.Add(tile_pos[0]+12);
                        tile_bound.Add(tile_pos[0]-8);
                        tile_bound.Add(tile_pos[0]+7);
                    }
                    //else will be programmed later, requires more math
                }
                if(tile_pos[3] > tile_size/2)//is in quad3
                {
                    quadrant = 3;
                    if(big_small==1)//if full res
                    {
                        tile_bound.Add(tile_pos[0]-13);
                        tile_bound.Add(tile_pos[0]+12);
                        tile_bound.Add(tile_pos[0]-7);
                        tile_bound.Add(tile_pos[0]+8);
                    }
                    //else will be programmed later, requires more math
                }
            }
            if (tile_pos[1] > tile_size/2)//quadrants based on cartesian coordinate plane
            {
                if(tile_pos[3]<= tile_size/2)//is in quad1
                {
                    quadrant = 1;
                    if(big_small==1)//if full res
                    {
                        tile_bound.Add(tile_pos[0]-12);
                        tile_bound.Add(tile_pos[0]+13);
                        tile_bound.Add(tile_pos[0]-8);
                        tile_bound.Add(tile_pos[0]+7);
                    }
                    //else will be programmed later, requires more math
                }
                if(tile_pos[3] > tile_size/2)//is in quad4
                {
                    quadrant = 4;
                    if(big_small==1)//if full res
                    {
                        tile_bound.Add(tile_pos[0]-12);
                        tile_bound.Add(tile_pos[0]+13);
                        tile_bound.Add(tile_pos[0]-7);
                        tile_bound.Add(tile_pos[0]+8);

                    }
                    //else will be programmed later, requires more math
                }
            }*/