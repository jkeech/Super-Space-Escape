using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Microsoft.Xna.Framework.Media;

namespace ArcadeRPG
{

    /// <summary>
    /// This is the GameEngine
    /// </summary>
    public class GameEngine
    {


        /// Sprite to display the player
        Sprite[] character_sprite;

        Texture2D[] monster_texture;
        List<Sprite> monster_sprites;
        Sprite bullet_sprite;
        Sprite sword_sprite;
        Bullet sword_bullet; //Little trick to create a "bullet" object to do damage
        bool sword_swing = false;
        int sword_delay = 0;

        ///Game State. Keeps track of TileEngine and Playerobject
        GameState game_state;

        ///List of Textures to display tiles
        List<Texture2D> tiles;

        ///HUD stuff graphics
        Texture2D uparrow;
        Texture2D downarrow;
        Texture2D leftarrow;
        Texture2D rightarrow; // graphics for "keypad"
        Texture2D fire_button;
        bool button_release = false;
       

        Color trans = new Color(255, 255, 255, 50);
        Vector2 uparrowpos = new Vector2(50, 345);
        Vector2 downarrowpos = new Vector2(50, 425);
        Vector2 leftarrowpos = new Vector2(15, 385);
        Vector2 rightarrowpos = new Vector2(85, 385); //starting positions for each arrow
        Vector2 fire_button_pos = new Vector2(700, 385);

        Vector2 imageOffset = new Vector2(0, 0); //origin

        public GameEngine()
        {

            character_sprite = new Sprite[(int) weaponType.GRENADE];
            character_sprite[(int)weaponType.NONE] = new Sprite();
            character_sprite[(int)weaponType.SWORD] = new Sprite();
            character_sprite[(int)weaponType.LASER] = new Sprite();

            monster_texture = new Texture2D[(int)enemyType.NUM_ENEM];


            bullet_sprite = new Sprite();
            sword_sprite = new Sprite();

            game_state = new GameState();
            game_state.local_player = new Player(33, 0, 32, 36);
            //game_state.local_player.setWeapon(weaponType.SWORD);
            game_state.monster_engine = new MonsterEngine(game_state);
            game_state.coll_engine = new CollisionEngine(game_state);
            game_state.fx_engine = new EffectsEngine(game_state);
            game_state.obj_mang = new ObjectManager(game_state);

            game_state.local_player.col_tok = game_state.coll_engine.register_object(game_state.local_player.getX(),
                game_state.local_player.getY(),
                game_state.local_player.getWidth(),
                game_state.local_player.getHeight(),
                ColType.PLAYER);

            
            //Content.RootDirectory = "Content";

            // Frame rate is 30 fps by default for Windows Phone.
            //TargetElapsedTime = TimeSpan.FromTicks(333333);
        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {


        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            Texture2D textureb = Content.Load<Texture2D>("background_cropped");
            Texture2D texturef = Content.Load<Texture2D>("foreground_cropped");
            Texture2D textureo = Content.Load<Texture2D>("objects1");
            tiles = new List<Texture2D>();
            tiles.Add(textureb);
            tiles.Add(texturef);
            tiles.Add(textureo);
            game_state.tile_engine = new TileEngine(32, tiles);
            game_state.tile_engine.loadLevel("Level_2");

            game_state.obj_mang.load(game_state.tile_engine.getCurrentMap().getLayer(LayerType.OBJECTS));
            //back_layer = tileEngine.getLayer(LayerType.BACKGROUND);
            //Character Sprite
            character_sprite[(int)weaponType.NONE].Load(Content, "player_no", 32, 36, 200);
            character_sprite[(int)weaponType.SWORD].Load(Content, "player_sword", 32, 36, 200);
            character_sprite[(int)weaponType.LASER].Load(Content, "player_gun", 32, 36, 200);
            //character_sprite.loc.X = 400;
            //character_sprite.loc.Y = 240;

            monster_texture[(int)enemyType.GRUNT] = Content.Load<Texture2D>("grunt");

            monster_texture[(int)enemyType.BEETLE] = Content.Load<Texture2D>("beetles");
            monster_texture[(int)enemyType.BERSERKER] = Content.Load<Texture2D>("berserker");
            //monster_sprites[(int)enemyType.GRUNT].Load(monster_texture[(int)enemyType.GRUNT], 32, 48);
            //monster_sprites[(int)enemyType.GRUNT].StartAnimating((int)PlayerDir.UP * 3, ((int)PlayerDir.UP * 3) + 2);

            bullet_sprite.Load(Content, "bullet", 9, 9, 0);
            sword_sprite.Load(Content, "player_sword_attack", 32, 36, 0);
     
            //game_state.monster_engine.AddMonster(new Enemy(500, 240, 48, 54, enemyType.GRUNT));
            //game_state.monster_engine.AddMonster(new Enemy(300, 400, 48, 54, enemyType.GRUNT));
            for (int i = 0; i < game_state.monster_engine.GetMonsters().Count(); ++i)
            {
                Enemy new_enemy = game_state.monster_engine.GetMonsters().ElementAt(i);
                Sprite enemy_sprite = new Sprite();
                int new_enemy_type = (int)new_enemy.getType();
          
                enemy_sprite.Load(monster_texture[new_enemy_type], new_enemy.getWidth(), new_enemy.getHeight(), 200);
                new_enemy.setSprite(enemy_sprite);
                //game_state.monster_engine.AddMonster(new_enemy);
            }

            game_state.bullet_engine = new BulletEngine(game_state);
            //character_sprite.StartAnimating(6, 8);
            // TODO: use this.Content to load your game content here

            uparrow = Content.Load<Texture2D>("arrowup");
            downarrow = Content.Load<Texture2D>("arrowdown");
            leftarrow = Content.Load<Texture2D>("arrowleft");
            rightarrow = Content.Load<Texture2D>("arrowright"); // components for "keypad" HUD
            fire_button = Content.Load<Texture2D>("fire");

            game_state.fx_engine.LoadSound(Content, "shoot", soundType.SHOOT);
            game_state.fx_engine.LoadSound(Content, "hurt", soundType.HURT);
            game_state.fx_engine.LoadSound(Content, "sword_swing", soundType.SWORD);
            game_state.fx_engine.LoadExplosion(Content, "expl", explosionType.SMALL);
            //SoundEffect game_music = Content.Load<SoundEffect>("01AttackPanda1");
            //game_music.Play();
            PathFind path_find = new PathFind(game_state);
            List<Node> path = path_find.FindPath(1, 1, 5, 3);


        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {


            // TODO: Add your update logic here
            PlayerDir pot_dir = game_state.local_player.getDirection();
            int pot_x = game_state.local_player.getX();
            int pot_y = game_state.local_player.getY();

            //Put a nicer function here
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            // for each place the screen has been touched at the point of "getState"
            {


                //if the screen is pressed on an arrow, move sprite accordingly
                if (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved)
                {
                    if ((tl.Position.X >= 50) && (tl.Position.X <= 100) && (tl.Position.Y >= 345) && (tl.Position.Y <= 385)) // up arrow
                    {

                        //local_player.setY(local_player.getY() - 3);
                        pot_y -= game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.UP;
                        // -1 = up direction (axis goes down incresingly)

                    }

                    if ((tl.Position.X >= 50) && (tl.Position.X <= 100) && (tl.Position.Y >= 425) && (tl.Position.Y <= 465)) // down arrow
                    {
                        pot_y += game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.DOWN;
                        // local_player.setY(local_player.getY() + 3);              
                    }

                    if ((tl.Position.X >= 15) && (tl.Position.X <= 55) && (tl.Position.Y >= 385) && (tl.Position.Y <= 425)) // left arrow
                    {
                        pot_x -= game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.LEFT;
                        //local_player.setX(local_player.getX() - 3); // -1 = left because left to right

                    }

                    if ((tl.Position.X >= 90) && (tl.Position.X <= 130) && (tl.Position.Y >= 385) && (tl.Position.Y <= 425)) // right arrow
                    {
                        pot_x += game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.RIGHT;
                        //local_player.setX(local_player.getX() + 3);

                    }


                    //Collision and updating
                    if (game_state.coll_engine.check_map_col(pot_x, pot_y, game_state.local_player.getWidth(), game_state.local_player.getHeight()) == false)
                    {
                        if (game_state.obj_mang.checkForGateAt(pot_x, pot_y, game_state.local_player.getWidth(), game_state.local_player.getHeight()) && !game_state.local_player.hasKey())
                        {// if there is a gate and the player doesn't have the key
                            game_state.local_player.col_tok.ResetCollisions();
                        }
                        else
                        {// no gate or player has the key
                            game_state.local_player.setX(pot_x);
                            game_state.local_player.setY(pot_y);
                            System.Diagnostics.Debug.WriteLine("Stuff: {0},{1}", pot_x, pot_y);

                            Item item = game_state.obj_mang.getItemAt(pot_x, pot_y, game_state.local_player.getWidth(), game_state.local_player.getHeight());
                            if (item != null)
                            {
                                game_state.local_player.addItem(item);
                                if (game_state.local_player.getWeapon() == weaponType.NONE)
                                {
                                    if (item.getType() == itemType.SWORD)
                                    {
                                        game_state.local_player.setWeapon(weaponType.SWORD);
                                    }
                                    if (item.getType() == itemType.LASER)
                                    {
                                        game_state.local_player.setWeapon(weaponType.LASER);
                                    }
                                }
                            }

                            if (!game_state.local_player.moving)
                            {
                                game_state.local_player.setDirection(pot_dir);
                                game_state.local_player.moving = true;
                                character_sprite[(int)game_state.local_player.getWeapon()].StartAnimating((int)pot_dir * 3, ((int)pot_dir * 3) + 2);
                            }
                        }

                    }
                    else
                    {
                        game_state.local_player.col_tok.ResetCollisions();
                    }
                }
                else if (tl.State == TouchLocationState.Released)
                {

                    if ((tl.Position.X >= 700) && (tl.Position.Y >= 385)) // Fire button
                    {
                        if (game_state.local_player.getWeapon() == weaponType.LASER)
                        {
                            game_state.bullet_engine.fire(game_state.local_player.getX() + (int)character_sprite[(int)game_state.local_player.getWeapon()].size.X / 2,
                                game_state.local_player.getY() + (int)character_sprite[(int)game_state.local_player.getWeapon()].size.Y / 2,
                                game_state.local_player.getDirection(),
                                bulletOwner.PLAYER,
                                bulletType.SMALL);
                            game_state.fx_engine.RequestSound(soundType.SHOOT);
                        }
                        else if (game_state.local_player.getWeapon() == weaponType.SWORD)
                        {
                            sword_swing = true;
                            game_state.fx_engine.RequestSound(soundType.SWORD);
                            int bullet_x = 0;
                            int bullet_y = 0;
                            switch (game_state.local_player.getDirection())
                            {
                                case PlayerDir.DOWN:
                                    bullet_x = game_state.local_player.getX()+game_state.local_player.getWidth()/2;
                                    bullet_y = game_state.local_player.getY()+game_state.local_player.getHeight();
                                    break;
                                case PlayerDir.UP:
                                    bullet_x = game_state.local_player.getX()+game_state.local_player.getWidth()/2;
                                    bullet_y = game_state.local_player.getY() - game_state.local_player.getHeight();
                                    break;
                                case PlayerDir.LEFT:
                                    bullet_x = game_state.local_player.getX() - game_state.local_player.getWidth();
                                    bullet_y = game_state.local_player.getY()+ game_state.local_player.getHeight()/2;
                                    break;
                                case PlayerDir.RIGHT:
                                    bullet_x = game_state.local_player.getX() + game_state.local_player.getWidth();
                                    bullet_y = game_state.local_player.getY() + game_state.local_player.getHeight()/2;
                                    break;
                            }

                            sword_bullet = game_state.bullet_engine.fire(bullet_x, bullet_y, game_state.local_player.getDirection(), bulletOwner.PLAYER, bulletType.SWORD);
                        }
                    }

                    character_sprite[(int)game_state.local_player.getWeapon()].StopAnimating();
                    game_state.local_player.moving = false;
                }

            }

            if (sword_swing)
            {
                sword_delay += gameTime.ElapsedGameTime.Milliseconds;
                if (sword_delay > 200)
                {
                    sword_delay = 0;
                    sword_swing = false;
                    game_state.bullet_engine.RemoveBullet(sword_bullet);
                }
            }

            game_state.monster_engine.Update(gameTime.ElapsedGameTime.Milliseconds);
            game_state.bullet_engine.Update();

            game_state.coll_engine.Update();

            game_state.fx_engine.Update();

        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            //spriteBatch.Begin();
            game_state.tile_engine.getCurrentMap().drawBackground(spriteBatch, game_state.local_player.getX(), game_state.local_player.getY());
            game_state.tile_engine.getCurrentMap().drawObjects(spriteBatch, game_state.local_player.getX(), game_state.local_player.getY());

            //tileEngine.draw(local_player.getX(), local_player.getY(), 0);
            if (game_state.local_player.getX() <= 400-16)
            {
                character_sprite[(int)game_state.local_player.getWeapon()].loc.X = game_state.local_player.getX();
            }
            else
            {

                if (game_state.local_player.getX() >= game_state.tile_engine.getCurrentMap().getWidth() * game_state.tile_engine.getTileSize() - 400-16)
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.X = game_state.local_player.getX() - (game_state.tile_engine.getCurrentMap().getWidth() * game_state.tile_engine.getTileSize() - 800);
                else
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.X = (800 / 2)-16;// -(32 / 2);
            }

            if (game_state.local_player.getY() <= 240-16)
            {
                character_sprite[(int)game_state.local_player.getWeapon()].loc.Y = game_state.local_player.getY();
            }
            else
            {

                if (game_state.local_player.getY() >= game_state.tile_engine.getCurrentMap().getHeight() * game_state.tile_engine.getTileSize() - 240-16)
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.Y = game_state.local_player.getY() - (game_state.tile_engine.getCurrentMap().getHeight() * game_state.tile_engine.getTileSize() - 480);
                else
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.Y = (480 / 2)-16;// - (32 / 2);

            }
            //character_sprite.cur_frame 
            if (sword_swing)
            {
             
                sword_sprite.loc = character_sprite[(int)game_state.local_player.getWeapon()].loc;
                sword_sprite.Draw(spriteBatch, (int)game_state.local_player.getDirection());
            }
            else
            {
                character_sprite[(int)game_state.local_player.getWeapon()].Draw(spriteBatch);
            }
            
            //Draw monsters
            List<Enemy> monsters = game_state.monster_engine.GetMonsters();
            int offset_x = game_state.local_player.getX() - (int)character_sprite[(int)game_state.local_player.getWeapon()].loc.X;
            int offset_y = game_state.local_player.getY() - (int)character_sprite[(int)game_state.local_player.getWeapon()].loc.Y;

            for (int i = 0; i < monsters.Count(); ++i)
            {
                Enemy monster = monsters.ElementAt(i);
                if (game_state.monster_engine.IsVisible(monster))
                {
                    /*
                    int loc = (int)monster.getType();
                    monster_sprites[(int)monster.getType()].loc.X = monster.getX() - offset_x;
                    monster_sprites[(int)monster.getType()].loc.Y = monster.getY() - offset_y;
                    monster_sprites[loc].Draw(spriteBatch);
                     * */
                    Sprite mons_sprite = monster.getSprite();
                    mons_sprite.loc.X = monster.getX() - offset_x;
                    mons_sprite.loc.Y = monster.getY() - offset_y;
                    if(mons_sprite.loc.X > 0 && mons_sprite.loc.X < 800) {
                        if(mons_sprite.loc.Y > 0 && mons_sprite.loc.Y < 400) {
                            mons_sprite.Draw(spriteBatch); //Draw if the loc is pos not neg
                        }
                        }
                }
            }

            List<Bullet> bullets = game_state.bullet_engine.GetBullets();
            for (int i = 0; i < bullets.Count(); ++i)
            {
                Bullet bullet = bullets.ElementAt(i);
                bullet_sprite.loc.X = bullet.x - offset_x;
                bullet_sprite.loc.Y = bullet.y - offset_y;
                bullet_sprite.Draw(spriteBatch);
            }
            // tileEngine.drawForeground(local_player.getX(), local_player.getY());
            game_state.fx_engine.Draw(spriteBatch,offset_x, offset_y);
            //Draw HUD
            spriteBatch.Draw(uparrow, uparrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(downarrow, downarrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(leftarrow, leftarrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(rightarrow, rightarrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(fire_button, fire_button_pos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);


            //spriteBatch.End();
            // TODO: Add your drawing code here

        }
    }
}
