using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ArcadeRPG
{

    /// <summary>
    /// Homemade sprite class to handle animation and sprite sheets.
    /// </summary>
    class Sprite
    {
        /// <summary>
        /// Path to the sprite sheet file.
        /// </summary>
        string path;
        /// <summary>
        /// Location on the sprite on the screen
        /// </summary>
        public Vector2 loc;
        /// <summary>
        /// Size of the sprite on the screen
        /// </summary>
        public Vector2 size;
        /// <summary>
        /// Sprite sheet data
        /// </summary>
        Texture2D spr_data;
        /// <summary>
        /// List of all animation frames
        /// </summary>
        List<Rectangle> frames;
        /// <summary>
        /// Current frame being drawn
        /// </summary>
        public int cur_frame;
        int start_frame;
        int end_frame;
        /// <summary>
        /// Timer that handles animation and increments the frame
        /// </summary>
        Timer anim_timer;
        int anim_time; //in ms
        bool is_animating = false;
        /// <summary>
        /// Constructor
        /// </summary>
        public Sprite()
        {
            loc = Vector2.Zero;
            size = Vector2.Zero;
            frames = new List<Rectangle>();
            spr_data = null;
            path = "";
            cur_frame = 0;
            start_frame = 0;
            end_frame = 0;
            anim_timer = null;
            anim_time = 0;
        }

        /// <summary>
        /// Loads a sprite sheet from file and breaks it into frames using width and height as guides.
        /// </summary>
        /// <param name="width">width of each sprite</param>
        /// <param name="height">height of each sprite</param>
        public void Load(ContentManager cont_man, string _path, int width, int height, int _anim_time)
        {
            path = _path;
            spr_data = cont_man.Load<Texture2D>(_path);
            size.X = width;
            size.Y = height;
            int num_rows = spr_data.Height / height;
            int num_cols = spr_data.Width / width;
            anim_time = _anim_time;
            for (int i = 0; i < num_rows; ++i)
            {
                for (int j = 0; j < num_cols; ++j)
                {
                    Rectangle new_rec = new Rectangle(j*width, i*height, width, height);
                    frames.Add(new_rec);
                }
            }

        }

        public void Load(Texture2D _spr_data, int width, int height, int _anim_time)
        {
            spr_data = _spr_data;
            size.X = width;
            size.Y = height;

            int num_rows = spr_data.Height / height;
            int num_cols = spr_data.Width / width;
            anim_time = _anim_time;

            for (int i = 0; i < num_rows; ++i)
            {
                for (int j = 0; j < num_cols; ++j)
                {
                    Rectangle new_rec = new Rectangle(j * width, i * height, width, height);
                    frames.Add(new_rec);
                }
            }
        }

        public void Draw(SpriteBatch batch)
        {
            if (frames[cur_frame] != null)
            {
                batch.Draw(spr_data, loc, frames[cur_frame], Color.White);
            }
        }

        public void Draw(SpriteBatch batch, bool fade, Color color)
        {
            int alpha;
            if(fade)
            {
                alpha = DateTime.Now.Millisecond % 255;
            }
            else
            { 
                alpha = 255;
            }
            Color transColor = new Color(color.R, color.G, color.B, alpha);
            if (frames[cur_frame] != null)
            {
                batch.Draw(spr_data, loc, frames[cur_frame], transColor);
            }
        }

        public void Draw(SpriteBatch batch, int x, int y)
        {
            if (frames[cur_frame] != null)
            {
                batch.Draw(spr_data, new Rectangle(x, y, (int)size.X, (int)size.Y), frames[cur_frame], Color.White);
            }
        }

        public void Draw(SpriteBatch batch, int frame, bool fade, Color color)
        {
            cur_frame = frame;
            Draw(batch, fade, color);
        }

        /// <summary>
        /// Sets up the frame timer and begins animating (all frames)
        /// </summary>
        public void StartAnimating()
        {
            if (anim_timer == null)
            {
                start_frame = 0;
                is_animating = true;
                end_frame = frames.Count-1;
                anim_timer = new Timer(AnimUpdate, this, anim_time, anim_time);
            }
        }

        /// <summary>
        /// Sets up the frame timer and begins animating (begin frames and end frames)
        /// </summary>
        public void StartAnimating(int begin, int end)
        {
            if (anim_timer != null)
            {
                anim_timer.Dispose();
                anim_timer = null;
            }
            is_animating = true;
                start_frame = begin;
                end_frame = end;
                cur_frame = start_frame;
                anim_timer = new Timer(AnimUpdate, this, anim_time, anim_time);

        }

        public void AnimateOnce()
        {
            if (anim_timer == null)
            {
                start_frame = 0;
                end_frame = frames.Count - 1;
                is_animating = true;
                anim_timer = new Timer(AnimOnceUpdate, this, anim_time, anim_time);
            }
        }

        public bool IsAnimating()
        {
            return is_animating;
        }

        /// <summary>
        /// Stop timer and stop animating
        /// </summary>
        public void StopAnimating()
        {
            if (anim_timer != null)
            {
                anim_timer.Dispose();
                anim_timer = null;
                cur_frame = start_frame;
                is_animating = false;
            }
        }

        /// <summary>
        /// Update function called by the 
        /// </summary>
        private static void AnimUpdate(object frame)
        {
            Sprite spr_ptr = (Sprite)frame;
            if (spr_ptr.cur_frame == spr_ptr.end_frame)
            {
                spr_ptr.cur_frame = spr_ptr.start_frame;
            }
            else
            {
                spr_ptr.cur_frame = spr_ptr.cur_frame + 1;
            }

        }

        private static void AnimOnceUpdate(object frame)
        {
            Sprite spr_ptr = (Sprite)frame;
            if (spr_ptr.cur_frame == spr_ptr.end_frame)
            {
                spr_ptr.StopAnimating();
            }
            else
            {
                spr_ptr.cur_frame = spr_ptr.cur_frame + 1;
            }
        }

    }
}
