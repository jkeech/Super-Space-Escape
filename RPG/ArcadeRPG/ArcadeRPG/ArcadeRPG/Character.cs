using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    enum PlayerDir { UP, RIGHT, DOWN, LEFT };
    enum enemyType { GRUNT, BERSERKER, BEETLE, NUM_ENEM };
    enum weaponType { NONE, SWORD, LASER, GRENADE};
    enum itemType {NONE, SWORD, LASER, ATT_BOOST, DEF_BOOST, KEY, NUM_ITEMS };
    //just the basics on types
    /// <summary>
    /// Holds the data for the player
    /// </summary>
    class Player
    {
        int last_x, last_y;
        int x;
        int y;
        int width, height;
        List<Item> inventory;
        weaponType activeWeapon;
        //Weapon secondaryWeapon;
        PlayerDir dir;
        public ColToken col_tok;
        public bool moving;
        int attack;
        int speed;
        int health;
        /// <summary>
        /// Constructor for a Player object
        /// </summary>
        /// <param name="xLoc">Input the initial x coordinate of the player</param>
        /// <param name="yLoc">Input the initial y coordinate of the player</param>
        // player has an attack, speed, and health or hp stat
        // a small weapon inventory, size is to be decided
        // or a system of active/secondary weapons can be used
        public Player(int xLoc, int yLoc, int pWidth, int pHeight)
        {
            last_x = x = xLoc;
            last_y = y = yLoc;
            width = pWidth;
            height = pHeight;
            health = 100;
            speed = 3;
            attack = 0;
            activeWeapon = weaponType.NONE;
            //secondaryWeapon = null;
            dir = PlayerDir.UP;
            moving = false;
            col_tok = null;
            inventory = new List<Item>();
        }

        /// <summary>
        /// Returns the current x coordinate of the player
        /// </summary>
        public int getX()
        {
            return x;
        }

        /// <summary>
        /// Returns the current y coordinate of the player
        /// </summary>
        public int getY()
        {
            return y;
        }

        public int getWidth()
        {
            return width;
        }


        public int getHeight()
        {
            return height;
        }

        public int getSpeed()
        {
            return speed;
        }

        public weaponType getWeapon()
        {
            return activeWeapon;
        }

        /// <summary>
        /// Sets the current x coordinate of the player
        /// </summary>
        /// <param name="xLoc">Input the initial x coordinate of the player</param>
        public void setX(int xLoc)
        {
            last_x = x;
            x = xLoc;
            if (col_tok != null)
            {
                col_tok.update(x, y);
            }
        }


        /// <summary>
        /// Sets the current y coordinate of the player
        /// </summary>
        /// <param name="yLoc">Input the initial y coordinate of the player</param>
        public void setY(int yLoc)
        {
            last_y = y;
            y = yLoc;
            if (col_tok != null)
            {
                col_tok.update(x, y);
            }
        }
        //when player grabs a weapon this changes attack stats
        public void setWeapon(weaponType _weapon)
        {
            //Weapon active = this.activeWeapon;
            //attack = active.getAttackBonus(ab);

            activeWeapon = _weapon;
            //not perfectly implemented yet
            //will also change other aspects of weapon including type and animation
            
        }



        public void setDirection(PlayerDir _dir)
        {
            dir = _dir;
        }

        public PlayerDir getDirection()
        {
            return dir;
        }

        public void addItem(Item item)
        {
            inventory.Add(item);

                
        }
    }

    class Enemy
    {
        int last_x, last_y;
        int x;
        int y;
        int width, height;
        int attack;
        int speed;
        int max_health, health;

        public ColToken col_tok;
        PlayerDir dir, last_dir;
        //same attributes as player without weapons
        enemyType eType;
        Sprite sprite;

        public int next_think_time;
        Node cur_target;
        List<Node> path;

        public Enemy(int xLoc, int yLoc, int eWidth, int eHeight, enemyType t)
        {
            last_x = x = xLoc;
            last_y = y = yLoc;
            width = eWidth;
            height = eHeight;

            last_dir = dir = PlayerDir.UP;
            cur_target = null;
            col_tok = null;
            //creates the lowest level of enemy
            if (t == enemyType.GRUNT)
            {
                eType = t;
                attack = 1;
                speed = 2;
                max_health = health = 20;
            }
            //creates the second level of enemy
            else if (t == enemyType.BEETLE)
            {
                eType = t;
                attack = 2;
                speed = 2;
                max_health = health = 10;
            }
            //creates toughest level of enemy
            else if (t == enemyType.BERSERKER)
            {
                eType = t;
                attack = 3;
                speed = 3;
                max_health = health = 15;
            }

            
        }

         /// <summary>
        /// Returns the current x coordinate of the enemy
        /// </summary>
        public int getX()
        {
            return x;
        }

        /// <summary>
        /// Returns the current y coordinate of the enemy
        /// </summary>
        public int getY()
        {
            return y;
        }

        public int getWidth()
        {
            return width;
        }


        public int getHeight()
        {
            return height;
        }


        public int getSpeed()
        {
            return speed;
        }

        public void setHealth(int new_health)
        {
            health = new_health;
        }

        public int getHealth()
        {
            return health;
        }

        public int getMaxHealth()
        {
            return max_health;
        }

        public enemyType getType()
        {
            return eType; 
        }

        public Sprite getSprite() {
            return sprite;
        }

        public void setSprite(Sprite _sprite)
        {
            sprite = _sprite;
        }

        public void setDirection(PlayerDir _dir)
        {
            last_dir = dir;
            dir = _dir;
        
        }

        public void setPath(List<Node> _path)
        {
            path = _path;
            if (path.Count() == 0)
            {
                cur_target = null;
                return;
            }
            cur_target = path.First();
            path.Remove(path.First());
        }

        public Node getTarget()
        {
            return cur_target;
        }

        public void nextTarget()
        {
            if (path.Count() == 0)
            {
                cur_target = null;
                return;
            }
            cur_target = path.First();
            path.Remove(path.First());
        }

        public PlayerDir getDirection()
        {
            return dir;
        }

        public PlayerDir getLastDirection()
        {
            return last_dir;
        }


        /// <summary>
        /// Sets the current x coordinate of the enemy
        /// </summary>
        /// <param name="xLoc">Input the initial x coordinate of the enemy</param>
        public void setX(int xLoc)
        {
            last_x = x;
            //last_y = y;
            x = xLoc;
            if (col_tok != null)
            {
                col_tok.update(x, y);
            }
        }

        /// <summary>
        /// Sets the current y coordinate of the enemy
        /// </summary>
        /// <param name="yLoc">Input the initial y coordinate of the enemy</param>
        public void setY(int yLoc)
        {
            last_y = y;
            y = yLoc;
            if (col_tok != null)
            {
                col_tok.update(x, y);
            }
        }

        public void revertX()
        {

            setX(last_x);
        }

        public void revertY()
        {
            setY(last_y);
        }

    }

    class Item
    {
        int attackBonus;
        int speedBonus;
        int defenseBonus;
        itemType type;
        int time;
        //each weapon has a type and attack bonus, and an animation later on

        //constructor
        public Item(itemType _type, int _ab, int _sb, int _db, int _time)
        {
        
            attackBonus = _ab;
            type = _type;
            speedBonus = _sb;
            defenseBonus = _db;
            time = _time;
        }
        //gets the attack bonus to add on later
        public int getAttackBonus(){
            
           
           return attackBonus;
        }
        //distiguishes the weapon type

        public itemType getType()
        {
            return type;
        }

    }


}


