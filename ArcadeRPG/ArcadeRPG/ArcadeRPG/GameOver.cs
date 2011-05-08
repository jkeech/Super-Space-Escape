using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace ArcadeRPG
{
    class GameOver
    {
        public enum State { SHOW, HIDE };
        private State state; // ^^ showing or not
        private Color color;
        private Texture2D gameoverpic; // graphic for menu
        private Vector2 gameoverpos;
        private Vector2 offset;

        private TimeSpan gameovertime = TimeSpan.FromSeconds(5.0);
        public Boolean exit;

        public GameOver()
        {
            state = State.HIDE;
            color = Color.Black;
            offset = new Vector2(0, 0);
            exit = false;
        }

        public void Show(SpriteBatch spritebatch)
        {
            state = State.SHOW;
            spritebatch.Draw(gameoverpic, gameoverpos, null, Color.White, 0, offset, 1.0f, SpriteEffects.None, 0); // draw the intro screen
        }

        public void loadContent(ContentManager contman)
        {
            gameoverpic = contman.Load<Texture2D>("GameOver");
            gameoverpos = new Vector2(0, 0);
        }

        public void update(GameTime gameTime)
        {
            gameovertime -= gameTime.ElapsedGameTime; // start timer on actual game

            if ((gameovertime.Seconds <= 0) && (gameovertime.Milliseconds <= 0))
            {
                exit = true;
            }

        }

        public Boolean isShowing()
        {
            return (state == State.SHOW);
        }

    }
}
