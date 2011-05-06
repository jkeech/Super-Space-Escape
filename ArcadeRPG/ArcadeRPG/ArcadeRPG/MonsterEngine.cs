using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArcadeRPG
{
    enum actionDecision { FLEE, ALIGN, ADVANCE, EVADE, FIRE, IDLE, NUM_ACTIONS };
    enum actionFactor {DP, DB, AL, HL, NUM_FACTORS};
    class MonsterEngine
    {
        //Game state object
        GameState game_state;
        
        //All monsters to handle
        List<Enemy> monsters;




        float[,,] decision_matrix = new float[(int)enemyType.NUM_ENEM,(int)actionDecision.NUM_ACTIONS,(int)actionFactor.NUM_FACTORS];

        public MonsterEngine(GameState _game_state)
        {
            game_state = _game_state;
            monsters = new List<Enemy>();

            LoadDecisionMatrix();
        }

        //Hard code stuff for enemies
        void LoadDecisionMatrix()
        {
            //Load grunt!
            //decision_matrix[(int)enemyType.GRUNT, (int)actionDecision.ADVANCE, (int)actionFactor.DP] = .3f; //Wants to advance towards player
            //decision_matrix[(int)enemyType.GRUNT, (int)actionDecision.FIRE, (int)actionFactor.AL] = .4f; //Wants to shoot player
            //decision_matrix[(int)enemyType.GRUNT, (int)actionDecision.ALIGN, (int)actionFactor.DP] = .4f; //Wants to align with player
            decision_matrix[(int)enemyType.GRUNT, (int)actionDecision.FLEE, (int)actionFactor.HL] = .7f;//wants to flee from player

            //Load Berserker
            //decision_matrix[(int)enemyType.BERSERKER, (int)actionDecision.ADVANCE, (int)actionFactor.DP] = .7f; //Wants to advance towards player
            decision_matrix[(int)enemyType.BERSERKER, (int)actionDecision.FLEE, (int)actionFactor.HL] = .7f; //Wants to advance towards player

            //Load Beetle
            //decision_matrix[(int)enemyType.BEETLE, (int)actionDecision.ADVANCE, (int)actionFactor.DP] = .4f; //Wants to advance towards player
            decision_matrix[(int)enemyType.BEETLE, (int)actionDecision.FLEE, (int)actionFactor.HL] = .7f; //Wants to run towards player
        }


        //Two basic functions for the Monster engine
        actionDecision think(Enemy monster)
        {
            float[] ratings = new float[(int)actionDecision.NUM_ACTIONS];
            int dist_x = game_state.local_player.getX() - monster.getX(); 
            int dist_y = game_state.local_player.getY() - monster.getY();

            int Dp = GetRating(Math.Sqrt(Math.Pow(dist_x, 2) + Math.Pow(dist_y, 2)), 800.0f);
            int Db = 0; //Bullet system not in yet
            int Alg = 0;
            if (Math.Abs(dist_x) < Math.Abs(dist_y))
            {
                Alg = GetRating(Math.Abs(dist_x), 400);
            }
            else
            {
                Alg = GetRating(Math.Abs(dist_y), 240);
            }

            int Hlt = GetRating(monster.getHealth(), monster.getMaxHealth());

            actionDecision retval = actionDecision.FLEE;
            float max_value = 0;

            for (int i = 0; i < (int)actionDecision.NUM_ACTIONS; ++i)
            {
                ratings[i] = 
                    decision_matrix[(int)monster.getType(), i, (int)actionFactor.DP]*Dp + 
                    decision_matrix[(int)monster.getType(), i, (int)actionFactor.DB]*Db +
                    decision_matrix[(int)monster.getType(), i, (int)actionFactor.AL]*Alg +
                    decision_matrix[(int)monster.getType(), i, (int)actionFactor.HL]*Hlt;

                if (ratings[i] > max_value)
                {
                    retval = (actionDecision)i;
                    max_value = ratings[i];
                }
            }

            if (max_value < 5)
            {
                return actionDecision.IDLE;
            }
            return retval;
        }

        void act(Enemy monster, actionDecision action)
        {
            switch (action)
            {
                case actionDecision.FLEE:
                    flee(monster);
                    break;
                case actionDecision.ALIGN:
                    align(monster);
                    break;
                case actionDecision.ADVANCE:
                    advance(monster);
                    break;
                case actionDecision.EVADE:
                    evade(monster);
                    break;
                case actionDecision.FIRE:
                    fire(monster);
                    break;
                case actionDecision.IDLE:
                    idle(monster);
                    break;

            }
        }
        
        //Functions to calculate actions
        void flee(Enemy monster)
        {

            PathFind pf = new PathFind(game_state);
            int dist_x = game_state.local_player.getX() - monster.getX();
            int dist_y = game_state.local_player.getY() - monster.getY();
            int mons_tile_x = (monster.getX() + (monster.getWidth() / 2)) / game_state.tile_engine.getTileSize();
            int mons_tile_y = (monster.getY() + (monster.getHeight() / 2)) / game_state.tile_engine.getTileSize();
            int pl_tile_x = game_state.local_player.getX() / game_state.tile_engine.getTileSize();
            int pl_tile_y = game_state.local_player.getY() / game_state.tile_engine.getTileSize();
            Random r = new Random();
            int mons_tile_xr = mons_tile_x;
            int mons_tile_yr = mons_tile_y;
            //int pl_tile_yr = pl_tile_y + r.Next(-5, 5);
            //int pl_tile_xr = pl_tile_x + r.Next(-5, 5);
            if (Math.Abs(dist_x) < Math.Abs(dist_y))
            {
                //Flee in the Y direction from the player
                if (dist_y <= 0)
                {
                    mons_tile_yr += r.Next(-5, -10);
                    mons_tile_xr += r.Next(-10, 10);
                    monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_xr, mons_tile_yr));
                }
                else
                {
                    mons_tile_yr += r.Next(5, 10);
                    mons_tile_xr += r.Next(-10, 10);
                    monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_xr, mons_tile_yr));
                }
            }
            else
            {
                //Flee in the x direction from the player
                if (dist_x <= 0)
                {
                    mons_tile_yr += r.Next(-10, 10);
                    mons_tile_xr += r.Next(5, 10);
                    monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_xr, mons_tile_yr));
                }
                else
                {
                    mons_tile_yr += r.Next(-10, 10);
                    mons_tile_xr += r.Next(5, 10);
                    monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_xr, mons_tile_yr));
                }
                monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_xr, mons_tile_yr));
            }

            //monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_xr, mons_tile_yr));
            
        }

        void align(Enemy monster)
        {

            PathFind pf = new PathFind(game_state);
            int dist_x = game_state.local_player.getX() - monster.getX();
            int dist_y = game_state.local_player.getY() - monster.getY();
            int mons_tile_x = (monster.getX() + (monster.getWidth() / 2)) / game_state.tile_engine.getTileSize();
            int mons_tile_y = (monster.getY() + (monster.getHeight() / 2)) / game_state.tile_engine.getTileSize();
            int pl_tile_x = game_state.local_player.getX() / game_state.tile_engine.getTileSize();
            int pl_tile_y = game_state.local_player.getY() / game_state.tile_engine.getTileSize();

            
            if (Math.Abs(dist_x) < Math.Abs(dist_y))
            {
                //Advance in the X direction toward the player

                monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, pl_tile_x, mons_tile_y));
            }
            else
            {
                //Advance in the Y direction toward the player

                monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_x, pl_tile_y-1));
            }
            
        }
        void advance(Enemy monster)
        {
            PathFind pf = new PathFind(game_state);
            int mons_tile_x = (monster.getX() + (monster.getWidth() / 2)) / game_state.tile_engine.getTileSize();
            int mons_tile_y = (monster.getY() + (monster.getHeight() / 2)) / game_state.tile_engine.getTileSize();
            int pl_tile_x = game_state.local_player.getX()/game_state.tile_engine.getTileSize();
            int pl_tile_y = game_state.local_player.getY()/game_state.tile_engine.getTileSize();
            pl_tile_y += 1;//get the enemies to head toward the bottom of the player so that they follow you on the bridge
            monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, pl_tile_x, pl_tile_y));
        }

        void idle(Enemy monster)
        {
            //THIS IS FOR DEBUG PURPOSES
            //PathFind pf = new PathFind(game_state);
            //int mons_tile_x = (monster.getX() + (monster.getWidth() / 2)) / game_state.tile_engine.getTileSize();
            //int mons_tiel_y = (monster.getY() + (monster.getHeight())) / game_state.tile_engine.getTileSize();
            //monster.setPath(pf.FindPath(mons_tile_x, mons_tiel_y, 1, 1));

            PathFind pf = new PathFind(game_state);
            int mons_tile_x = (monster.getX() + (monster.getWidth() / 2)) / game_state.tile_engine.getTileSize();
            int mons_tile_y = (monster.getY() + (monster.getHeight() / 2)) / game_state.tile_engine.getTileSize();
            Random r = new Random();
            int mons_tile_xr = mons_tile_x + r.Next(-10, 10);
            int mons_tile_yr = mons_tile_y + r.Next(-10, 10);
            monster.setPath(pf.FindPath(mons_tile_x, mons_tile_y, mons_tile_xr, mons_tile_yr));

            /*
            //Random number
            System.Random generator = new System.Random();
            int dir = generator.Next(4);
            switch ((PlayerDir)dir)
            {
                case PlayerDir.DOWN:
                    monster.setY(monster.getY() + monster.getSpeed());
                    monster.setDirection(PlayerDir.DOWN);
                    break;
                case PlayerDir.LEFT:
                    monster.setX(monster.getX() - monster.getSpeed());
                    monster.setDirection(PlayerDir.LEFT);
                    break;
                case PlayerDir.RIGHT:
                    monster.setX(monster.getX() + monster.getSpeed());
                    monster.setDirection(PlayerDir.RIGHT);
                    break;
                case PlayerDir.UP:
                    monster.setY(monster.getY() - monster.getSpeed());
                    monster.setDirection(PlayerDir.UP);
                    break;
            }
             * */
        }

        void evade(Enemy monster)
        {
            //Place holder till bullet system works
        }

        void fire(Enemy monster)
        {
            int player_x = game_state.local_player.getX();
            int player_y = game_state.local_player.getY();
            int monster_x = monster.getX();
            int monster_y = monster.getY();
            int fire_x = 0;
            int fire_y = 0;
            int min = 0;
            int max = 0;
            PlayerDir dir = PlayerDir.UP;
            Random random = new Random(); 
            bool fire = false;
            if (player_x < monster_x + monster.getWidth() && player_x > monster_x - monster.getWidth())
            {
                fire = true;
                min = monster.getX();
                max = monster.getX()+monster.getWidth();
                fire_x = random.Next(min,max);
                if (monster_y - player_y >= 0)
                {
                    dir = PlayerDir.UP;
                    fire_y = monster.getY() + 5;
                }
                else
                {
                    dir = PlayerDir.DOWN;
                    fire_y = monster.getY() + monster.getHeight();
                }
            }
            else if (player_y < monster_y + monster.getHeight() && player_y > monster_y - monster.getHeight())
            {
                fire = true;
                min = monster.getY();
                max = monster.getY() + monster.getHeight();
                fire_y = random.Next(min, max);
                if (monster_x - player_x >= 0)
                {
                    dir = PlayerDir.LEFT;
                    fire_x = monster.getX() - 5;
                }
                else
                {
                    dir = PlayerDir.RIGHT;
                    fire_x = monster.getX() + monster.getWidth();
                }
            }
            if (fire)
            {
                game_state.bullet_engine.fire(fire_x, fire_y, dir, bulletOwner.ENEMY, bulletType.SMALL);
                fire = false;
            }
            else
            {
                this.advance(monster);
            }
             //Place holder till bullet system works
        }

        void move_towards_target(Enemy monster)
        {
            if (monster.getTarget() == null)
            {
                return;
            }
            int dist_x = monster.getTarget().loc_x - (monster.getX()+(monster.getWidth()/2))/game_state.tile_engine.getTileSize();
            int dist_y = monster.getTarget().loc_y - (monster.getY()+(monster.getHeight()/2)) / game_state.tile_engine.getTileSize();
            if (dist_x == 0 && dist_y == 0)
            {
                monster.nextTarget();
                return;
            }

            if (Math.Abs(dist_x) > Math.Abs(dist_y))
            {
                //Advance in the X direction
                if (dist_x > 0)
                {
                    monster.setX(monster.getX() + monster.getSpeed());
                    monster.setDirection(PlayerDir.RIGHT);
                }
                else
                {
                    monster.setX(monster.getX() - monster.getSpeed());
                    monster.setDirection(PlayerDir.LEFT);
                }
            }
            else
            {
                //Advance in the Y direction
                if (dist_y > 0)
                {
                    monster.setY(monster.getY() + monster.getSpeed());
                    monster.setDirection(PlayerDir.DOWN);
                }
                else
                {
                    monster.setY(monster.getY() - monster.getSpeed());
                    monster.setDirection(PlayerDir.UP);
                }
            }
        }

        public bool IsVisible(Enemy monster) 
        {
            //MAGIC NUMBERS. Screen is 800 long by 480
            //We can determine if the monster is visible based on the players position
            int screen_x = Math.Abs(game_state.local_player.getX() - monster.getX());
            int screen_y = Math.Abs(game_state.local_player.getY() - monster.getY());
            
            if (screen_x > 800 || screen_y > 400)
            {
                return false;
            }

            return true;
        }

        int GetRating(double x, double max)
        {
            double rating = (-100 / max) * x + 100;
            return (int)rating;
        }

        public void AddMonster(Enemy monster)
        {
            //Request collision token and push monster to monster list
            monster.col_tok = game_state.coll_engine.register_object(monster.getX(), monster.getY(), monster.getWidth(), monster.getHeight(), ColType.MONSTER);
            monsters.Add(monster);
        }

        public void Update(int elapsed_time)
        {
            for (int i = 0; i < monsters.Count(); ++i)
            {
                Enemy monster = monsters.ElementAt(i);
                bool mons_removed = false;
                if (monster.col_tok.HasCollisions())//&& monster.col_tok.GetHitType() == ColType.BULLET)
                {

                    List<Collision> cols = monster.col_tok.GetCollisions();
                    for (int j = 0; j < cols.Count(); ++j)
                    {
                        if (cols.ElementAt(j).type == ColType.BULLET )
                        {
                            //MAGIC NUMBER
                            monster.setHealth(monster.getHealth() - 5);
                            //dmg_sound.Play();
                            if (monster.getHealth() <= 0)
                            {
                                game_state.monster_engine.Remove(monster);
                                mons_removed = true;
                                game_state.fx_engine.RequestExplosion(explosionType.SMALL, monster.getX()+(monster.getWidth()/2), monster.getY()+(monster.getHeight()/2));
                            }
                            game_state.fx_engine.RequestSound(soundType.HURT);
                        }
                        else if (cols.ElementAt(j).type == ColType.MAP)
                        {
                            //monster.revertX();
                            //monster.revertY();
                        }
                    }

                    monster.col_tok.ResetCollisions();
                }
                if (mons_removed == false)
                {
                    if (IsVisible(monster))
                    {
                        move_towards_target(monster);

                        if (monster.next_think_time >= 2000) //TIME DELAY
                        {
                            actionDecision action = think(monster);
                            act(monster, action);
                            monster.next_think_time = 0;
                        }
                        else
                        {
                            monster.next_think_time += elapsed_time;
                        }
                    }
                }
            }
        }

        public void Remove(Enemy monster)
        {
            game_state.coll_engine.remove_object(monster.col_tok);
            monsters.Remove(monster);
        }

        public List<Enemy> GetMonsters()
        {
            return monsters;
        }
    }
}
