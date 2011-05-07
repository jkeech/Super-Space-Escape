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
        public int x, y;
        public int width, height;
        public int vel_x, vel_y;
        public bulletOwner owner;
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
                case bulletType.SMALL:
                    vel = 5;
                    width = 9;
                    height = 9;
                    break;
                case bulletType.SWORD:
                    vel = 0;
                    width = 20;
                    height = 20;
                    break;
            }

            switch (dir)
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

            bullets.Add(bullet);

            return bullet;
        }

        public void Update()
        {
          
            for (int i = 0; i < bullets.Count(); ++i)
            {
                Bullet bullet = bullets.ElementAt(i);
                bool throw_out = false;
                if (bullet.col_tok.HasCollisions())
                {
                    List<ColToken> cols = bullet.col_tok.GetCollisions();
                    for (int j = 0; j < cols.Count(); ++j)
                    {
                        if ((bullet.owner == bulletOwner.PLAYER && cols.ElementAt(j).GetLocalType() == ColType.PLAYER) || (bullet.owner == bulletOwner.ENEMY && cols.ElementAt(j).GetLocalType() == ColType.MONSTER))
                        {
                            continue;
                        } else {
                            game_state.coll_engine.remove_object(bullet.col_tok);
                            bullet.col_tok.ResetCollisions();
                            bullets.RemoveAt(i);
                            throw_out = true;
                        }
                    }
                    bullet.col_tok.ResetCollisions();
                }
                if (throw_out == false)
                {
                    bullet.x += bullet.vel_x;
                    bullet.y += bullet.vel_y;
                    bullet.col_tok.update(bullet.x, bullet.y);

                    //We need to dispose of bullets if they leave the area.
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

        public List<Bullet> GetBullets()
        {
            return bullets;
        }
    }
}
