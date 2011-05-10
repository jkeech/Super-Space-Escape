using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace ArcadeRPG
{
    class Backpack
    {
        public enum State { SHOW, HIDE };
        private State state; // ^^ showing or not
        private Color color;
        private Texture2D backpackpic; // graphic for inventory menu (NOT BACKPACK PIC on game screen)
        private Vector2 backpackpos; // position for the backpack sprite to be displayed
        private Vector2 offset;
        public Boolean backpack_touched; // does the inventory need to be brought up?
        private const string empty = "None :(";


        public Backpack()
        {
            state = State.HIDE; // hidden until user opens menu with backpack icon on main game screen
            color = Color.White;
            offset = new Vector2(0, 0);
            backpack_touched = false; // user hasn't accessed inventory yet by default
        }


        public void Show(SpriteBatch spritebatch)
        {
            state = State.SHOW;
            spritebatch.Draw(backpackpic, backpackpos, null, Color.White, 0, offset, 1.0f, SpriteEffects.None, 0); // draw the item inventory screen
        }


        public void Hide()
        {
            if (state == State.HIDE) { return; }
            else
            {
                backpackpic.ToString().Remove(0); // "converts" the whole screen to a "string" and "removes" it. "hiding" it
                state = State.HIDE;
            }
        }


        public void loadContent(ContentManager contman)
        {
            backpackpic = contman.Load<Texture2D>("Inventory"); // load the picture
            backpackpos = new Vector2(0, 0);
        }



        public Boolean isShowing()
        {
            return (state == State.SHOW);
        }

        public string getEmptyString() // this will be implemented if the user has tapped the backpack but has no items
        {
            return empty;
        }


    }
}
