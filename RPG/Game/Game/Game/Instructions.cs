using System;
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
    class Instructions
    {
        public enum State { SHOW, HIDE };
        private State state; // ^^ showing or not
        private Color color;
        private Texture2D instructpic; // graphic for menu
        private Vector2 instructpos;
        private Vector2 offset;

        public Instructions()
        {
            state = State.SHOW;
            color = Color.Black;
            offset = new Vector2(0, 0);
        }

        public void Show(SpriteBatch spritebatch)
        {
            state = State.SHOW;
            spritebatch.Draw(instructpic, instructpos, null, Color.White, 0, offset, 1.0f, SpriteEffects.None, 0); // draw the intro screen
        }

        public void Hide()
        {
            if (state == State.HIDE) { return; }
            else
            {
                instructpic.ToString().Remove(0); // "undraws" screen
                state = State.HIDE;
            }
        }

        public void loadContent(ContentManager contman)
        {
            instructpic = contman.Load<Texture2D>("Menus\\Instructions");
            instructpos = new Vector2(0, 0);
        }

        public void update(GameTime gameTime)
        {
            TouchCollection tc = TouchPanel.GetState(); // when user taps screen, advances to next screen
            if (tc.Count > 0)
            {
                Hide();
            }
            
        }

        public Boolean isShowing()
        {
            return (state == State.SHOW);
        }

    }
}
