using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    enum bulletType {SMALL, SWORD};
    enum bulletOwner {ENEMY, PLAYER};

    class Bullet
    {
        public int x, y; // position of the bullet
        public int width, height; // dimensions of bullet sprite
        public int vel_x, vel_y; // speed of traveling bullet
        public bulletOwner owner; // our main character or AI shooting bullets
        public bulletType type;
        public ColToken col_tok = null;
    }

    class BulletEngine
    {
        GameState game_state;
        List<Bullet> bullets;

        public BulletEngine(GameState _game_state)
        {
            game_state = _game_state;
            bullets = new List<Bullet>();
        }
        public Bullet fire(int x, int y, PlayerDir dir, bulletOwner owner, bulletType type) 
        {
            Bullet bullet = new Bullet();
            bullet.x = x;
            bullet.y = y;

            int vel = 0;
            int width = 0;
            int height = 0;
            switch (type)
            {
                case bulletType.SMALL: //adjust properties here
                    vel = 8;
                    width = 9;
                    height = 9;
                    break;
                case bulletType.SWORD: // no bullets if a sword is the weapon
                    vel = 0;
                    width = 36;
                    height = 36;
                    break;
            }

            switch (dir) // determine direction the character is looking, so we know what trail to fire the bullets in
            {
                case PlayerDir.UP:
                    bullet.vel_x = 0;
                    bullet.vel_y = -vel;
                    break;
                case PlayerDir.DOWN:
                    bullet.vel_y = vel;
                    bullet.vel_x = 0;
                    break;
                case PlayerDir.LEFT:
                    bullet.vel_x = -vel;
                    bullet.vel_y = 0;
                    break;
                case PlayerDir.RIGHT:
                    bullet.vel_x = vel;
                    bullet.vel_y = 0;
                    break;
            }

            bullet.owner = owner;
            bullet.type = type;
            bullet.width = width;
            bullet.height = height;
            bullet.col_tok = game_state.coll_engine.register_object(bullet, ColType.BULLET);
            //set some final bullet properties

            bullets.Add(bullet); // add the bullet to the list to be displayed later

            return bullet;
        }

        public void Update()
        {
          
            for (int i = 0; i < bullets.Count(); ++i)
            {
                Bullet bullet = bullets.ElementAt(i);
                bool throw_out = false;
                if (bullet.col_tok.HasCollisions()) // if a bullet runs into a tree, for instance- stop displaying it
                {
                    List<ColToken> cols = bullet.col_tok.GetCollisions();
                    for (int j = 0; j < cols.Count(); ++j)
                    {
                        if ((bullet.owner == bulletOwner.PLAYER && cols.ElementAt(j).GetLocalType() == ColType.PLAYER))
                        {
                            continue;
                        } else {
                            ColToken hit = cols.ElementAt(j);
                            if (hit.GetLocalType() == ColType.MONSTER) // if bullet runs into a monster
                            {
                                Enemy monster = (Enemy)hit.GetParent();
                                int damage = 0;
                                switch (bullet.type) // a bullet can technically be a sword in our design, so determine what kind of weapon is being used
                                {
                                    case bulletType.SMALL: // bullet does damage to the enemy
                                        damage = 5;
                                        break;
                                    case bulletType.SWORD: //sword does more damage then a bullet
                                        damage = 10;
                                        break;

                                }
                                monster.setHealth(monster.getHealth() - (game_state.local_player.getAttackBonus() + damage)); // substract damage from monster's health
                                game_state.fx_engine.RequestSound(soundType.ENEMY_HURT); // play a sound when hitting enemies

                            }
                            else if (hit.GetLocalType() == ColType.PLAYER) // if enemy bullet hits our character
                            {
                                int damage = 0;
                                switch (bullet.type)
                                {
                                    case bulletType.SMALL: // take damage
                                        damage = 5;
                                        break;
                                    case bulletType.SWORD: //take more damage then a bullet
                                        damage = 10;
                                        break;

                                }
                                game_state.local_player.setHealth(game_state.local_player.getHealth() + game_state.local_player.getDefenseBonus() - damage); // reset health for our character
                            }
                            game_state.coll_engine.remove_object(bullet.col_tok); // remove bullet from screen once it hits enemy/character
                            bullet.col_tok.ResetCollisions();
                            bullets.RemoveAt(i);
                            throw_out = true; // no need for this bullet object anymore
                        }
                    }
                    bullet.col_tok.ResetCollisions();
                }
                if (throw_out == false) // if the bullet hasnt hit anything yet, continue drawing it
                {
                    bullet.x += bullet.vel_x;
                    bullet.y += bullet.vel_y;
                    bullet.col_tok.update(bullet.x, bullet.y); // use the bullets velocity and update its position

                    //We need to dispose of bullets if they leave the area
                    if (bullet.x > game_state.tile_engine.getCurrentMap().getWidth() * game_state.tile_engine.getTileSize() || bullet.x < 0)
                    {
                        bullets.RemoveAt(i);
                        game_state.coll_engine.remove_object(bullet.col_tok);
                    }

                    if (bullet.y > game_state.tile_engine.getCurrentMap().getHeight() * game_state.tile_engine.getTileSize() || bullet.y < 0)
                    {
                        bullets.RemoveAt(i);
                        game_state.coll_engine.remove_object(bullet.col_tok);
                    }
                }
            }
        }


        public void RemoveBullet(Bullet bullet)
        {
            game_state.coll_engine.remove_object(bullet.col_tok);
            bullet.col_tok.ResetCollisions();
            bullets.Remove(bullet);
        }

        public List<Bullet> GetBullets() // get all the bullets the user/AI monsters have created
        {
            return bullets;
        }
    }
}
