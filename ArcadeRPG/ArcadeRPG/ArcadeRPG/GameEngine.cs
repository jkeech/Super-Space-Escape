using System;
using System.Collections.Generic;
using System.Threading;
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

        //*******************SPRITE PROPERTIES******************//
        /// Sprite to display the player
        Sprite[] character_sprite;

        Texture2D[] monster_texture;
        //List<Sprite> monster_sprites;
        Sprite bullet_sprite;
        Sprite sword_sprite;
        Bullet sword_bullet; //Little trick to create a "bullet" object to do damage
        bool sword_swing = false;
        int sword_delay = 0;
        //*******************************************************//



        ///Game State. Keeps track of TileEngine and Playerobject
        GameState game_state;

        ///List of Textures to display tiles
        List<Texture2D> tiles;


        //************************MENU INSTANCE*****************//
        Backpack backpackmenu;
        TimeExpired timex; // for "timeout" instance
        //*****************************************************//

        //******************GRAPHIC SPRITES*****************//
        Texture2D backpack; // backpack graphic user can tap on for item inventory
        Texture2D healthbar; // displays user character's health
        Texture2D uparrow;
        Texture2D downarrow;
        Texture2D leftarrow;
        Texture2D rightarrow; // graphics for "keypad"
        Texture2D fire_button;
        Texture2D item_background;
        //*************************************************//


        //*********************POSITIONS***********************//
        Vector2 backpackpos = new Vector2(700, 335); // position for backpack sprite
        //Vector2 gunbuttonpos = new Vector2(630, 425); // position for attack button
        Vector2 healthpos = new Vector2(280, 445); // position for health bar

        Vector2 scoreStringPos = new Vector2(0, 20); // position of score string
        Vector2 currScorePos = new Vector2(75, 20); // position of actual score
        Vector2 timeLeftPos = new Vector2(0, 0); // "Time Remaining" position
        Vector2 timePos = new Vector2(75, 0); // displaying actual time left

        Vector2 levelstringpos = new Vector2(700, 0); // "Level: " position
        Vector2 levelnumpos = new Vector2(755, 0); // level number position

        Color trans = new Color(255, 255, 255, 50);
        Vector2 uparrowpos = new Vector2(50, 345);
        Vector2 downarrowpos = new Vector2(50, 425);
        Vector2 leftarrowpos = new Vector2(15, 385);
        Vector2 rightarrowpos = new Vector2(85, 385); //positions for each arrow
        Vector2 fire_button_pos = new Vector2(700, 385);

        Rectangle health_bar_rec;
        static int health_bar_width = 200;
        //****************************************************//

        //**************DISPLAY STRINGS******************//
        public const string scoreString = "SCORE: "; // display string
        public int currScore = 0; // actual score, again can be modified like the levelnum var. not "const"
        public const string timeLeft = "TIME: ";
        public const string levelstring = "LEVEL: ";
        public int levelnum = 0; // can be modified for when a user advances through levels, not a "const"
        //************************************************//


        //*************FONTS**************************//
        SpriteFont displayFont; // in separate file (fonts subfolder). arbitrary
        SpriteFont itemfont; // ^^
        SpriteFont expiredfont; // ^^
        //*********************************************//


        //***************************MISC*********************//
        public TimeSpan currTime = TimeSpan.FromSeconds(600.0); // grant the player a certain time per round
        // currTime will be 90 and count down each second, checking against 0 each second,  for each level
        public int hurt_time = 3000;

        Boolean timeOut; // alerts program when timer for roundtime has expired
        bool button_release = false;
        Vector2 imageOffset = new Vector2(0, 0); //origin
        //*****************************************************//


        public GameEngine()
        {

            backpackmenu = new Backpack();

            character_sprite = new Sprite[(int)weaponType.GRENADE];
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

            game_state.local_player.col_tok = game_state.coll_engine.register_object(game_state.local_player, ColType.PLAYER);



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
            item_background = Content.Load<Texture2D>("item_background");

            game_state.fx_engine.LoadSound(Content, "shoot", soundType.SHOOT);
            game_state.fx_engine.LoadSound(Content, "hurt", soundType.HURT);
            game_state.fx_engine.LoadSound(Content, "sword_swing", soundType.SWORD);
            game_state.fx_engine.LoadExplosion(Content, "expl", explosionType.SMALL);
            //SoundEffect game_music = Content.Load<SoundEffect>("01AttackPanda1");
            Song game_music = Content.Load<Song>("01AttackPanda1");
            MediaPlayer.Play(game_music);
            //game_music.Play();

            //********************************LOADING GRAPHIC SPRITES********************************//
            backpack = Content.Load<Texture2D>("backpack"); // loading backpack
            healthbar = Content.Load<Texture2D>("healthbar"); // load health bar
            health_bar_rec = new Rectangle((int)healthpos.X, (int)healthpos.Y, health_bar_width, 30);
            //**************************************************************************************//


            //********************MISCELLANEOUS*****************************************************//
            displayFont = Content.Load<SpriteFont>("StatsFont"); //load a font from a formatted file
            itemfont = Content.Load<SpriteFont>("ItemFont"); // ^^
            expiredfont = Content.Load<SpriteFont>("TimeExpired"); // ^^
            //***************************************************************************************//

            backpackmenu.loadContent(Content);

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime)
        {

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
                        game_state.local_player.setX(pot_x);
                        game_state.local_player.setY(pot_y);


                        //System.Diagnostics.Debug.WriteLine("Stuff: {0},{1}", pot_x, pot_y);

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
                else if (tl.State == TouchLocationState.Released)
                {
                    if (tl.Position.X >= backpackpos.X && tl.Position.X <= backpackpos.X + backpack.Width && tl.Position.Y >= backpackpos.Y && tl.Position.Y <= backpackpos.Y + backpack.Height)
                    {
                        backpackmenu.backpack_touched = true;
                    }
                    if (backpackmenu.backpack_touched == false)
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
                                        bullet_x = game_state.local_player.getX() + game_state.local_player.getWidth() / 2;
                                        bullet_y = game_state.local_player.getY() + game_state.local_player.getHeight();
                                        break;
                                    case PlayerDir.UP:
                                        bullet_x = game_state.local_player.getX() + game_state.local_player.getWidth() / 2;
                                        bullet_y = game_state.local_player.getY() - game_state.local_player.getHeight();
                                        break;
                                    case PlayerDir.LEFT:
                                        bullet_x = game_state.local_player.getX() - game_state.local_player.getWidth();
                                        bullet_y = game_state.local_player.getY() + game_state.local_player.getHeight() / 2;
                                        break;
                                    case PlayerDir.RIGHT:
                                        bullet_x = game_state.local_player.getX() + game_state.local_player.getWidth();
                                        bullet_y = game_state.local_player.getY() + game_state.local_player.getHeight() / 2;
                                        break;
                                }

                                sword_bullet = game_state.bullet_engine.fire(bullet_x, bullet_y, game_state.local_player.getDirection(), bulletOwner.PLAYER, bulletType.SWORD);
                            }
                        }
                    }
                    else
                    {
                        Vector2 formatpos = new Vector2(275, 125);
                        int tileSize = game_state.tile_engine.getTileSize();

                        Item toRemove = null;
                        foreach (Item i in game_state.local_player.getInventory())
                        {
                            Rectangle dest = new Rectangle((int)formatpos.X, (int)formatpos.Y, 300, 40);
                            if(dest.Contains((int)tl.Position.X,(int)tl.Position.Y))
                            {
                                //menu item Clicked
                                switch (i.getType())
                                {
                                    case itemType.LASER: game_state.local_player.setWeapon(weaponType.LASER); break;
                                    case itemType.SWORD: game_state.local_player.setWeapon(weaponType.SWORD); break;
                                    case itemType.ATT_BOOST: toRemove = i; game_state.local_player.setAttack(5); break;
                                    case itemType.DEF_BOOST: toRemove = i; break;
                                    case itemType.KEY:
                                    default: break;
                                }
                                backpackmenu.backpack_touched = false;  // exit from inventory now
                            }
                            formatpos.Y += 50;
                        }
                        if (toRemove != null)
                            game_state.local_player.removeItem(toRemove);

                        if ((tl.Position.X >= 642) && (tl.Position.X <= 758) && (tl.Position.Y >= 360) && (tl.Position.Y <= 435)) // coordinates of the "exit" button in inventory
                        {
                            backpackmenu.backpack_touched = false; //user has exited, return to main game screen
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
            List<ColToken> cols = game_state.local_player.col_tok.GetCollisions();
            for (int j = 0; j < cols.Count(); ++j)
            {
                ColToken coll = cols.ElementAt(j);
                if (coll.GetLocalType() != ColType.MAP)
                {
                    if (!game_state.local_player.hurt)
                    {
                        //Player gets hurt!
                        game_state.local_player.setHealth(game_state.local_player.getHealth() - 3);
                        game_state.local_player.hurt = true;
                        double new_width = ((double)health_bar_width) * (double)((double)game_state.local_player.getHealth() / (double)game_state.local_player.getMaxHealth());
                        health_bar_rec.Width = (int)new_width;
                        //Get hurt by a little
                        hurt_time = 500;
                    }
                }
            }
            game_state.local_player.col_tok.ResetCollisions();
            game_state.local_player.col_tok.update(game_state.local_player.getX(), game_state.local_player.getY());

            if(game_state.local_player.hurt) {
                hurt_time -= gameTime.ElapsedGameTime.Milliseconds;
                if (hurt_time <= 0)
                {
                    game_state.local_player.hurt = false;
                }
            }
            currTime -= gameTime.ElapsedGameTime; // start timer on actual game


            if ((currTime.Minutes <= 0) && (currTime.Seconds <= 0) && (currTime.Milliseconds <= 0))
            {
                timeOut = true; // if round time has run out, display the time expired screen (setting this bool to true will flag the menu later)
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
            if (game_state.local_player.getX() <= 400 - 16)
            {
                character_sprite[(int)game_state.local_player.getWeapon()].loc.X = game_state.local_player.getX();
            }
            else
            {

                if (game_state.local_player.getX() >= game_state.tile_engine.getCurrentMap().getWidth() * game_state.tile_engine.getTileSize() - 400 - 16)
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.X = game_state.local_player.getX() - (game_state.tile_engine.getCurrentMap().getWidth() * game_state.tile_engine.getTileSize() - 800);
                else
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.X = (800 / 2) - 16;// -(32 / 2);
            }

            if (game_state.local_player.getY() <= 240 - 16)
            {
                character_sprite[(int)game_state.local_player.getWeapon()].loc.Y = game_state.local_player.getY();
            }
            else
            {

                if (game_state.local_player.getY() >= game_state.tile_engine.getCurrentMap().getHeight() * game_state.tile_engine.getTileSize() - 240 - 16)
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.Y = game_state.local_player.getY() - (game_state.tile_engine.getCurrentMap().getHeight() * game_state.tile_engine.getTileSize() - 480);
                else
                    character_sprite[(int)game_state.local_player.getWeapon()].loc.Y = (480 / 2) - 16;// - (32 / 2);

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
                    if (mons_sprite.loc.X > 0 && mons_sprite.loc.X < 800)
                    {
                        if (mons_sprite.loc.Y > 0 && mons_sprite.loc.Y < 400)
                        {
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

            game_state.tile_engine.getCurrentMap().drawForeground(spriteBatch,game_state.local_player.getX(), game_state.local_player.getY());
            game_state.fx_engine.Draw(spriteBatch,offset_x, offset_y);

            //Draw HUD

            //begin operations on display textures
            //gets spritebatch in a state to be 'ready' to draw
            if (!backpackmenu.backpack_touched)
            {

                //******************DRAWING ARROWS**************************//
                spriteBatch.Draw(uparrow, uparrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(downarrow, downarrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(leftarrow, leftarrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(rightarrow, rightarrowpos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(fire_button, fire_button_pos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                //draw the arrow graphics to the screen with given position, 3x as big as original size, no effects
                //************************************************************//


                //***********************DRAWING GRAPHIC SPRITES***********************//
                spriteBatch.Draw(backpack, backpackpos, null, trans, 0, imageOffset, 0.4f, SpriteEffects.None, 0); //draw the backpack "button"
                
                spriteBatch.Draw(healthbar, health_bar_rec, null, trans);  //draw health bar
                //********************************************************************//


                //***************************DRAWING STRINGS***********************************//
                spriteBatch.DrawString(displayFont, scoreString, scoreStringPos, Color.Black); // draw "score: "
                spriteBatch.DrawString(displayFont, currScore.ToString(), currScorePos, Color.Black); // draw actual score

                spriteBatch.DrawString(displayFont, levelstring, levelstringpos, Color.Black); // draw "level: "
                spriteBatch.DrawString(displayFont, levelnum.ToString(), levelnumpos, Color.Black); // draw level number
                //****************************************************************************//


                //check to see if the score needs to continue to be drawn (if time hasn't run out)
                //if it has, undraw it and display time expired string and menu
                if (timeOut)
                {
                    timex.Show(spriteBatch, expiredfont); // brings up time expired screen
                }
                else
                {
                    spriteBatch.DrawString(displayFont, timeLeft, timeLeftPos, Color.Black);
                    spriteBatch.DrawString(displayFont, (currTime.Minutes*60+currTime.Seconds).ToString(), timePos, Color.Black);
                }

            } // end backpack button NOT pressed if
            else // backpack button has been pressed
            {
                if (!timeOut) // if time hasnt run out during the period where the inventory is brought up by the user, show the inventory
                {
                    backpackmenu.Show(spriteBatch); // draw backpack menu
                    drawItems(spriteBatch, itemfont); // draw items according to layout in itemfont
                }
                else // time has run out while the inventory is up, hide inventory and bring up time expired menu
                {
                    backpackmenu.Hide();
                    timex.Show(spriteBatch, expiredfont);
                }
            }


        } // end draw function


        public void drawItems(SpriteBatch sb, SpriteFont sf)
        {
            //check to see if user hit exit button


            //format display on screen for string to be drawn
            Vector2 formatpos = new Vector2(275, 125);
            float origx = formatpos.X;

            if (game_state.local_player.getInventory().Count == 0) // no items collected, display a message
            {
                sb.DrawString(sf, backpackmenu.getEmptyString(), formatpos, Color.White);
            }
            else // inventory has a capacity
            {
                //draws (each) item on inventory screen
                foreach (Item i in game_state.local_player.getInventory())
                {
                    //display format: Item image, image name

                    // Draw item image to screen
                    i.setPos(formatpos);
                    int tileSize = game_state.tile_engine.getTileSize();
                    Rectangle dest = new Rectangle((int)formatpos.X,(int)formatpos.Y,tileSize,tileSize);
                    Rectangle imageSource = new Rectangle((i.getTexture() % (tiles[2].Width / tileSize)) * tileSize,(i.getTexture() / (tiles[2].Width / tileSize)) * tileSize,tileSize,tileSize);
                    sb.Draw(item_background, new Vector2(formatpos.X - 4, formatpos.Y - 4), Color.White);
                    sb.Draw(tiles[2], dest, imageSource, Color.White);

                    // Draw item name to screen
                    formatpos.X += 50;
                    i.setPos(formatpos);
                    sb.DrawString(sf, i.getName(), i.getPos(), Color.White, 0, imageOffset, 1.0f, SpriteEffects.None, 0);

                    // Reset position for next item
                    formatpos.Y += 50;
                    formatpos.X = origx; // re-allign for next item to be displayed
                }
            } // end else

        }//end drawitems function



    }
}
