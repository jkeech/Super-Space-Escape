using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace ArcadeRPG
{
    class Instructions2
    {
        public enum State { SHOW, HIDE };
        private State state; // ^^ showing or not
        private Color color;
        private Texture2D instruct2pic; // graphic for menu
        private Vector2 instruct2pos;
        private Vector2 offset;

        public Instructions2()
        {
            state = State.SHOW;
            color = Color.Black;
            offset = new Vector2(0, 0);
        }

        public void Show(SpriteBatch spritebatch)
        {
            state = State.SHOW;
            spritebatch.Draw(instruct2pic, instruct2pos, null, Color.White, 0, offset, 1.0f, SpriteEffects.None, 0); // draw the intro screen
        }

        public void Hide()
        {
            if (state == State.HIDE) { return; }
            else
            {
                instruct2pic.ToString().Remove(0); // "undraws" screen
                state = State.HIDE;
            }
        }

        public void loadContent(ContentManager contman)
        {
            instruct2pic = contman.Load<Texture2D>("Instructions2");
            instruct2pos = new Vector2(0, 0);
        }

        public void update(GameTime gameTime, TouchCollection tc2)
        {
            tc2 = TouchPanel.GetState();
            foreach (TouchLocation tl2 in tc2)
            {
                if (tl2.State == TouchLocationState.Pressed)
                {
                    Hide();
                }
            }

        }

        public Boolean isShowing()
        {
            return (state == State.SHOW);
        }

    }
}
