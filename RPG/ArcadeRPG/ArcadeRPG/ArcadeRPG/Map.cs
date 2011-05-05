using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ArcadeRPG
{
    public enum LayerType { BACKGROUND, FOREGROUND, OBJECTS, COLLISION };
    public enum ForeType { SOLID,RANDOM,DIAGL,DIAGR,BARH,BARV};
    /// <summary>
    /// Contains the entire map data as well as all of the objects in the game world.
    /// Each map is made of multiple TileLayer objects, which in turn have multiple
    /// tiles per layer. Map -> TileLayer -> Tile
    /// </summary>
    class Map
    {
        // Foreground and background layers

        TileLayer[] map;
        int sprite_x;
        int sprite_y;
        int tile_size;
        int width, height;
        string title;
        List<Texture2D> texture_layers;
        ForeType fore=ForeType.SOLID;
        int[] sprite_position = new int[4];
        /// <summary>
        /// Constructor for a Map object
        /// </summary>
        /// <param name="w">Input the width of the map in tiles</param>
        /// <param name="h">Input the height of the map in tiles</param>
        public Map(string name, int w, int h, int tl_size, List<Texture2D> tiles)
        {
            title = name;
            width = w;
            height = h;
            tile_size = tl_size;
            texture_layers = tiles;
            sprite_x = 0;//initially set here, changed when drawing
            sprite_y = 240;

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

        public int[] get_cur_tile_pos()
        {
            int xtile = sprite_x / tile_size;
            int xpos = sprite_x % tile_size;
            int ytile = sprite_y / tile_size;
            int ypos = sprite_y % tile_size;

            int[] ret = new int[4];
            ret[0] = xtile;
            ret[1] = xpos;
            ret[2] = ytile;
            ret[3] = ypos;

            return ret;
        }
        /// <summary>
        /// Draws all of the background tiles to the screen
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void drawBackground(SpriteBatch tile_batch, int x, int y)
        {
            sprite_x = x;
            sprite_y = y;
            sprite_position = get_cur_tile_pos();
            int[] bounds = new int[4];
            bool at_edgex = false;
            bool at_edgey = false;
            int tlcornerx;//top left corner, ending off screen
            int tlcornery;
            int brcornerx;
            int brcornery;//bottom right corner, ending off screen
            double swidth = 800 / tile_size;//screen width, based on tiles
            double sheight = 480 / tile_size;//screen width, based on tiles
            if (sprite_x < 400-(tile_size/2))
            {
                tlcornerx = 0;
                brcornerx = (int)swidth + 1;
                at_edgex = true;
            }
            else
            {
                tlcornerx = sprite_position[0] - (int)Math.Floor(swidth / 2) - 1;

                if (sprite_x > (width * tile_size) - 400 - (tile_size / 2))
                {
                    tlcornerx = width - (int)swidth;
                    brcornerx = width;
                    at_edgex = true;
                }
                else
                    brcornerx = sprite_position[0] + (int)Math.Floor(swidth / 2) + 1;
            }

            if (sprite_y < 240 - (tile_size / 2))
            {
                tlcornery = 0;
                brcornery = (int)sheight + 1;
                at_edgey = true;
            }
            else
            {
                tlcornery = sprite_position[2] - (int)Math.Floor(sheight / 2) - 1;

                if (sprite_y > (height * tile_size) - 240 - (tile_size / 2))
                {
                    tlcornery = height - (int)sheight;
                    brcornery = height;
                    at_edgey = true;
                }
                else
                    brcornery = sprite_position[2] + (int)Math.Floor(sheight / 2) + 1;
            }

            int temp_texture;
            Rectangle drawR;
            Rectangle sourceR;
            int countx = 0;
            int county = 0;
            for (int a = tlcornerx; a <= brcornerx; a++)
            {
                county = 0;
                for (int b = tlcornery; b <= brcornery; b++)
                {

                    int xdisp = 0;
                    int ydisp = 0;
                    if (!at_edgex)
                    {
                        xdisp = sprite_position[1] + tile_size;
                    }
                    if (!at_edgey)
                    {
                        ydisp = sprite_position[3] + tile_size;
                    }
                    if (!(a * tile_size >= width * tile_size || a * tile_size < 0 || b * tile_size >= height * tile_size || b * tile_size < 0))
                    {
                        temp_texture = map[0].getTile(a, b).getTexture();//get texture of this tile

                        sourceR = new Rectangle((temp_texture % (texture_layers[0].Width / 32)) * tile_size, (temp_texture / (texture_layers[0].Width / 32)) * tile_size, tile_size, tile_size);
                        drawR = new Rectangle((countx * tile_size) - xdisp, (county * tile_size) - ydisp, tile_size, tile_size);

                        tile_batch.Draw(texture_layers[0], drawR, sourceR, Color.White);
                    }
                    county++;
                }
                countx++;
            }

        }


        /// <summary>
        /// Draws all of the objects tiles to the screen
        /// </summary>
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void drawObjects(SpriteBatch tile_batch, int x, int y)
        {
            sprite_x = x;
            sprite_y = y;
            sprite_position = get_cur_tile_pos();
            int[] bounds = new int[4];
            bool at_edgex = false;
            bool at_edgey = false;
            int tlcornerx;//top left corner, ending off screen
            int tlcornery;
            int brcornerx;
            int brcornery;//bottom right corner, ending off screen
            double swidth = 800 / tile_size;//screen width, based on tiles
            double sheight = 480 / tile_size;//screen width, based on tiles
            if (sprite_x < 400 - (tile_size / 2))
            {
                tlcornerx = 0;
                brcornerx = (int)swidth + 1;
                at_edgex = true;
            }
            else
            {
                tlcornerx = sprite_position[0] - (int)Math.Floor(swidth / 2) - 1;

                if (sprite_x > (width * tile_size) - 400 - (tile_size / 2))
                {
                    tlcornerx = width - (int)swidth;
                    brcornerx = width;
                    at_edgex = true;
                }
                else
                    brcornerx = sprite_position[0] + (int)Math.Floor(swidth / 2) + 1;
            }

            if (sprite_y < 240 - (tile_size / 2))
            {
                tlcornery = 0;
                brcornery = (int)sheight + 1;
                at_edgey = true;
            }
            else
            {
                tlcornery = sprite_position[2] - (int)Math.Floor(sheight / 2) - 1;

                if (sprite_y > (height * tile_size) - 240 - (tile_size / 2))
                {
                    tlcornery = height - (int)sheight;
                    brcornery = height;
                    at_edgey = true;
                }
                else
                    brcornery = sprite_position[2] + (int)Math.Floor(sheight / 2) + 1;
            }

            int temp_texture;
            Rectangle drawR;
            Rectangle sourceR;
            int countx = 0;
            int county = 0;
            for (int a = tlcornerx; a <= brcornerx; a++)
            {
                county = 0;
                for (int b = tlcornery; b <= brcornery; b++)
                {

                    int xdisp = 0;
                    int ydisp = 0;
                    if (!at_edgex)
                    {
                        xdisp = sprite_position[1] + tile_size;
                    }
                    if (!at_edgey)
                    {
                        ydisp = sprite_position[3] + tile_size;
                    }
                    if (!(a * tile_size >= width * tile_size || a * tile_size < 0 || b * tile_size >= height * tile_size || b * tile_size < 0))
                    {
                        temp_texture = map[2].getTile(a, b).getTexture();//get texture of this tile
                        if (temp_texture != -1)
                        {
                            sourceR = new Rectangle((temp_texture % (texture_layers[2].Width / 32)) * tile_size, (temp_texture / (texture_layers[2].Width / 32)) * tile_size, tile_size, tile_size);
                            drawR = new Rectangle((countx * tile_size) - xdisp, (county * tile_size) - ydisp, tile_size, tile_size);

                            tile_batch.Draw(texture_layers[2], drawR, sourceR, Color.White);
                        }
                    }
                    county++;
                }
                countx++;
            }

        }
        /// <summary>
        /// Draws all of the foreground tiles to the screen
        /// </summary>
        /// objects in the foreground are expected to be twice the size and therefore resolution of those in the background.
        /// <param name="x">Input the x coordinate (in pixels?) of the center tile to draw</param>
        /// <param name="y">Input the y coordinate (in pixels?) of the center tile to draw</param>
        public void drawForeground(SpriteBatch tile_batch, int x, int y)
        {
            sprite_x = x;
            sprite_y = y;
            sprite_position = get_cur_tile_pos();
            int[] bounds = new int[4];
            bool at_edge = false;
            int tlcornerx;//top left corner, ending off screen
            int tlcornery;
            int brcornerx;
            int brcornery;//bottom right corner, ending off screen
            double swidth = 800 / tile_size;//screen width, based on tiles
            double sheight = 480 / tile_size;//screen width, based on tiles
            if (sprite_x < 400 - (tile_size / 2)-16)
            {
                tlcornerx = 0;
                brcornerx = (int)swidth + 1;
                at_edge = true;
            }
            else
            {
                tlcornerx = sprite_position[0] - (int)Math.Floor(swidth / 2) - 1;

                if (sprite_x > (width * tile_size) - 400 - (tile_size / 2) - 16)
                {
                    tlcornerx = width - (int)swidth;
                    brcornerx = width;
                    at_edge = true;
                }
                else
                    brcornerx = sprite_position[0] + (int)Math.Floor(swidth / 2) + 1;
            }

            if (sprite_y < 240 - (tile_size / 2) - 16)
            {
                tlcornery = 0;
                brcornery = (int)sheight + 1;
                at_edge = true;
            }
            else
            {
                tlcornery = sprite_position[2] - (int)Math.Floor(sheight / 2) - 1;

                if (sprite_y > (height * tile_size) - 240 - (tile_size / 2) - 16)
                {
                    tlcornery = height - (int)sheight;
                    brcornery = height;
                    at_edge = true;
                }
                else
                    brcornery = sprite_position[2] + (int)Math.Floor(sheight / 2) + 1;
            }

            int temp_texture;
            Rectangle drawR;
            Rectangle sourceR;
            int countx = 0;
            int county = 0;
            for (int a = tlcornerx; a <= brcornerx; a++)
            {
                county = 0;
                for (int b = tlcornery; b <= brcornery; b++)
                {

                    int xdisp = 0;
                    int ydisp = 0;
                    if (!at_edge)
                    {
                        xdisp = sprite_position[1];
                        ydisp = sprite_position[3];
                    }
                    if (!(a * tile_size >= width * tile_size || a * tile_size < 0 || b * tile_size >= height * tile_size || b * tile_size < 0))
                    {
                        temp_texture = map[1].getTile(a, b).getTexture();//get texture of this tile
                        if (temp_texture != -1)
                        {
                            sourceR = new Rectangle((temp_texture % (texture_layers[1].Width / 32)) * tile_size * 2, 0, tile_size * 2, tile_size * 2);
                            drawR = new Rectangle((countx * tile_size * 2) - (xdisp * 2), (county * tile_size * 2) - (ydisp * 2), (tile_size * 2), (tile_size * 2));
                            if (drawR.Left < 801 && drawR.Right > -1)
                                tile_batch.Draw(texture_layers[1], drawR, sourceR, new Color(200, 200, 200, 60));
                        }
                    }
                    county++;
                }
                countx++;
            }



        }
    }

}
