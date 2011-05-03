using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Game
{
    class Item
    {
        //item properties:
        public enum itemType { ATTACK, DEFENSE, SPEED, KEY, WEAPON }; //type of item can be picked up.
        private itemType type; // to be implemented by specific item classes derived from Item later

        private string name; // name of item
        public Boolean collected;
        private int boost; // what the item can do for the user (i.e., boost attack by 5, then boost will = 5). to be implemented in derived classes
        private Vector2 offset; // default offset for drawing a sprite (keep it zero for consistency)
        private Color color; // for drawing function, keep it white for draw to draw no layer in front of item to be drawn

        //no time limit, items will last duration of level. upon level "advance", items that are ATTACK/DEFENSE/SPEED will be cleared out of item vector in main class
        private Texture2D itempic;
        private Vector2 itempos;

        public Item()
        {
            color = Color.White;
            offset = new Vector2(0, 0); // don't offset image once drawn on screen (for simplicity)
            name = "dummyitem"; // placeholder
            boost = 0; // placeholder
            collected = false; // user by default hasnt collected the item yet
        }

        public void loadContent(ContentManager contman)
        {
            itempic = contman.Load<Texture2D>("Pictures\\dummyitem");
        }

        public void setPos(Vector2 v2)
        {
            itempos.X = v2.X;
            itempos.Y = v2.Y;
        }

        public Vector2 getPos()
        {
            return itempos;
        }

        public Texture2D get2D()
        {
            return itempic;
        }

        public int getBoost()
        {
            return boost;
        }

        public itemType getType()
        {
            return type;
        }

        public string getName()
        {
            return name;
        }


    }
}
