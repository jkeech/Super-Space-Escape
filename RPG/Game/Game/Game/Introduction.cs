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
    class Introduction
    {
        public enum State { SHOW, HIDE };
        private State state; // ^^ showing or not
        private Color color;
        private Texture2D intropic; // graphic for menu
        private Vector2 intropos;
        private Vector2 offset;

        private static TimeSpan menuTime = TimeSpan.FromSeconds(5.0);
        TimeSpan introTime = menuTime;
        // introtime will give the original introduction screen 5 seconds to display then transition to the game

        public Introduction()
        {
            state = State.SHOW;
            color = Color.Black;
            offset = new Vector2(0, 0);
        }

        public void Show(SpriteBatch spritebatch)
        {
            state = State.SHOW;
            spritebatch.Draw(intropic, intropos, null, Color.White, 0 , offset, 1.0f, SpriteEffects.None, 0); // draw the intro screen
        }

        public void Hide()
        {
            if (state == State.HIDE) { return; }
            else
            {
                intropic.ToString().Remove(0); // "undraws" screen
                state = State.HIDE;
            }
        }

        public void loadContent(ContentManager contman)
        {
            intropic = contman.Load<Texture2D>("Menus\\Welcome");
            intropos = new Vector2(0, 0);
        }

        public void update(GameTime gameTime)
        {
            introTime -= gameTime.ElapsedGameTime;
            if ((introTime.Seconds <= 0) && (introTime.Milliseconds <= 0))
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
