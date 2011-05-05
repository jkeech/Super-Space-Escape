using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;


namespace Game
{
    class TimeExpired
    {
        public enum State { SHOW, HIDE };
        private State state; // ^^ being displayed or not
        private Color color;
        private Texture2D timeoutpic; // graphic for menu
        private Vector2 timeoutpos;
        private Vector2 offset;
        public Boolean play_again;
        private double g_o_time=0;
        private Vector2 d_time_pos;
        private double MAX_TIME = 10;

        public TimeExpired()
        {
            state = State.HIDE; // hidden by default until shown explicitly
            color = Color.Crimson; // the color of defeat....
            offset = new Vector2(0, 0);
            d_time_pos = new Vector2(325, 250); // formatting
            play_again = false;
        }

        public void Show(SpriteBatch spritebatch, SpriteFont sf)
        {
            state = State.SHOW;
            spritebatch.Draw(timeoutpic, timeoutpos, null, Color.White, 0 , offset, 1.0f, SpriteEffects.None, 0); // draw the time expired screen
        }

        public void Hide()
        {
            if (state == State.HIDE) { return; } // if already hiding, no need to hide again, just return
            else
            {
                timeoutpic.ToString().Remove(0); // "undraws" screen
                state = State.HIDE;
            }
        }

        public void reset()
        {
            play_again = false;
            state = State.HIDE;
            g_o_time = 0;
        }

        public void loadContent(ContentManager contman) // load placeholder menu for time expired
        {
            timeoutpic = contman.Load<Texture2D>("Menus\\OutOfTime");
            timeoutpos = new Vector2(0, 0);
        }


        public Boolean isShowing()
        {
            return (state == State.SHOW); // is the time expired screen showing?
        }


        public void update(SpriteBatch sb, SpriteFont sf, GameTime gt)
        {
            // have a "timer" count down from arbitrary time (10 seconds for this menu)
            //if user doesnt click to play again in that amount of time, auto exit

            g_o_time += gt.ElapsedGameTime.TotalSeconds;
            if (g_o_time >= MAX_TIME)
            {
                shutDown();
                return;
            }
            else
            {
                TouchCollection tc = TouchPanel.GetState();
                if(tc.Count > 0) 
                {
                    play_again = true;
                    return; // return before timer runs out
                }
                sb.DrawString(sf, (10-Convert.ToInt32(g_o_time)).ToString(), d_time_pos, Color.White);
            }


        } // end update


        public int get_remaining_time()
        {
            return (int)g_o_time;
        }

        public void shutDown() // send here when timer runs out, game over screen and shut down
        {
            play_again = false;
        }

    }
}
