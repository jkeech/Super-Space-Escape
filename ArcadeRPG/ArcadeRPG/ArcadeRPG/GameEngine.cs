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
        Sprite bullet_sprite;
        Sprite sword_sprite;
        Bullet sword_bullet; //Little trick to create a "bullet" object to do damage
        bool sword_swing = false;
        int sword_delay = 0;
        int att_boost_delay = 0;
        int def_boost_delay = 0;
        int exit = 13;
        //*******************************************************//



        ///Game State. Keeps track of TileEngine and Playerobject
        GameState game_state;

        ///List of Textures to display tiles
        List<Texture2D> tiles;


        //************************MENU INSTANCE*****************//
        Backpack backpackmenu;
        TimeExpired timex; // for "timeout" instance
        GameOver gameOver;
        //*****************************************************//

        //******************GRAPHIC SPRITES*****************//
        Texture2D backpack; // backpack graphic user can tap on for item inventory
        Texture2D healthbar_empty; // displays user character's health
        Texture2D healthbar_full;
        Texture2D uparrow;
        Texture2D downarrow;
        Texture2D leftarrow;
        Texture2D rightarrow; // graphics for "keypad"
        Texture2D fire_button;
        Texture2D item_background;
        //*************************************************//


        //*********************POSITIONS***********************//
        Vector2 backpackpos = new Vector2(700, 330); // position for backpack sprite
        Rectangle backpackExit = new Rectangle(529, 334, 84, 44);
        Vector2 healthpos = new Vector2(280, 445); // position for health bar

        Vector2 scoreStringPos = new Vector2(0, 20); // position of score string
        Vector2 currScorePos = new Vector2(75, 20); // position of actual score
        Vector2 timeLeftPos = new Vector2(0, 0); // "Time Remaining" position
        Vector2 timePos = new Vector2(75, 0); // displaying actual time left

        Vector2 livesStringPos = new Vector2(0, 40);
        Vector2 levelstringpos = new Vector2(700, 0); // "Level: " position
        Vector2 levelnumpos = new Vector2(755, 0); // level number position
       

        Color trans = new Color(255, 255, 255, 50);
        Color lucen = new Color(255, 255, 255, 120);
        Vector2 uparrowpos = new Vector2(60, 330);
        Vector2 downarrowpos = new Vector2(60, 420);
        Vector2 leftarrowpos = new Vector2(15, 375);
        Vector2 rightarrowpos = new Vector2(100, 375); //positions for each arrow
        Vector2 fire_button_pos = new Vector2(700, 385);

        Rectangle health_bar_rec, health_bar_empty_rec;
        static int health_bar_width = 200;
        //****************************************************//


        //**************DISPLAY STRINGS******************//
        public const string timeLeft = "Time: ";
        public const string levelstring = "Level: ";
        public const string livesString = "Lives Remaining: ";

        public int levelnum = 0; // can be modified for when a user advances through levels, not a "const"
        //************************************************//


        //*************FONTS**************************//
        SpriteFont displayFont; // in separate file (fonts subfolder). arbitrary
        SpriteFont itemfont; // ^^
        SpriteFont expiredfont; // ^^
        //*********************************************//


        //***************************MISC*********************//
        public const int LEVEL_TIME = 120;
        public TimeSpan currTime = TimeSpan.FromSeconds(LEVEL_TIME); // grant the player a certain time per round
        // currTime will be 90 and count down each second, checking against 0 each second,  for each level
        public int hurt_time = 3000;
        public int livesRemaining = 5;
        bool button_release = false;
        public bool gameEnded = false;
        Vector2 imageOffset = new Vector2(0, 0); //origin
        //*****************************************************//


        public GameEngine(TimeExpired t, GameOver g)
        {

            backpackmenu = new Backpack();
            timex = t;
            gameOver = g;

            character_sprite = new Sprite[(int)weaponType.GRENADE];
            character_sprite[(int)weaponType.NONE] = new Sprite();
            character_sprite[(int)weaponType.SWORD] = new Sprite();
            character_sprite[(int)weaponType.LASER] = new Sprite();

            monster_texture = new Texture2D[(int)enemyType.NUM_ENEM];


            bullet_sprite = new Sprite();
            sword_sprite = new Sprite();

            game_state = new GameState();
            game_state.local_player = new Player(33, 0, 32, 36);
            game_state.monster_engine = new MonsterEngine(game_state);
            game_state.coll_engine = new CollisionEngine(game_state);
            game_state.fx_engine = new EffectsEngine(game_state);
            game_state.obj_mang = new ObjectManager(game_state);
            //instantiate each type of object, respectively

            game_state.local_player.col_tok = game_state.coll_engine.register_object(game_state.local_player, ColType.PLAYER);

        }



        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        public void Initialize()
        {
            //nothing
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        public void LoadContent(ContentManager Content)
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            Texture2D textureb = Content.Load<Texture2D>("background_cropped"); // background
            Texture2D texturef = Content.Load<Texture2D>("foreground_cropped"); // foreground
            Texture2D textureo = Content.Load<Texture2D>("objects1"); // objects
            tiles = new List<Texture2D>(); // tiles
            tiles.Add(textureb);
            tiles.Add(texturef);
            tiles.Add(textureo); // add bg, fg, obj's to draw
            game_state.tile_engine = new TileEngine(32, tiles);
            
            //Character Sprite
            character_sprite[(int)weaponType.NONE].Load(Content, "player_no", 32, 36, 200);
            character_sprite[(int)weaponType.SWORD].Load(Content, "player_sword", 32, 36, 200);
            character_sprite[(int)weaponType.LASER].Load(Content, "player_gun", 32, 36, 200);

            monster_texture[(int)enemyType.GRUNT] = Content.Load<Texture2D>("grunt");

            monster_texture[(int)enemyType.BEETLE] = Content.Load<Texture2D>("beetles");
            monster_texture[(int)enemyType.BERSERKER] = Content.Load<Texture2D>("berserker");
            monster_texture[(int)enemyType.TROOPER] = Content.Load<Texture2D>("trooper");

            LoadLevel(0);
            bullet_sprite.Load(Content, "bullet", 9, 9, 0);
            sword_sprite.Load(Content, "player_sword_attack", 32, 36, 0);


            for (int i = 0; i < game_state.monster_engine.GetMonsters().Count(); ++i) // draw new monsters randomly
            {
                Enemy new_enemy = game_state.monster_engine.GetMonsters().ElementAt(i);
                Sprite enemy_sprite = new Sprite();
                int new_enemy_type = (int)new_enemy.getType();

                enemy_sprite.Load(monster_texture[new_enemy_type], new_enemy.getWidth(), new_enemy.getHeight(), 200);
                new_enemy.setSprite(enemy_sprite);
            }

            game_state.bullet_engine = new BulletEngine(game_state);


            uparrow = Content.Load<Texture2D>("arrowup");
            downarrow = Content.Load<Texture2D>("arrowdown");
            leftarrow = Content.Load<Texture2D>("arrowleft");
            rightarrow = Content.Load<Texture2D>("arrowright"); // components for "keypad" HUD
            fire_button = Content.Load<Texture2D>("fire"); // "fire" button (attacks enemies when pushed)
            item_background = Content.Load<Texture2D>("item_background"); // user taps this to bring up inventory

            game_state.fx_engine.LoadSound(Content, "shoot", soundType.SHOOT);
            game_state.fx_engine.LoadSound(Content, "hurt", soundType.HURT); // sounds
            
            game_state.fx_engine.LoadSound(Content, "sword_swing", soundType.SWORD);
            game_state.fx_engine.LoadExplosion(Content, "expl", explosionType.SMALL); // explosion sprites
            SoundEffect game_music = Content.Load<SoundEffect>("01AttackPanda1"); // song
            //Song game_music = Content.Load<Song>("01AttackPanda1");
            //MediaPlayer.Play(game_music);
            //game_music.Play();

            //********************************LOADING GRAPHIC SPRITES********************************//
            backpack = Content.Load<Texture2D>("backpack"); // loading backpack
            healthbar_empty = Content.Load<Texture2D>("healthbar"); // load health bar
            healthbar_full = Content.Load<Texture2D>("healthbar_full"); // load health bar
            health_bar_rec = new Rectangle((int)healthpos.X, (int)healthpos.Y, health_bar_width, 30);
            health_bar_empty_rec = new Rectangle((int)healthpos.X, (int)healthpos.Y, health_bar_width, 30);
            //**************************************************************************************//


            //********************MISCELLANEOUS*****************************************************//
            displayFont = Content.Load<SpriteFont>("StatsFont"); //load a font from a formatted file
            //displayFont = Content.Load<SpriteFont>("Courier New");
            itemfont = Content.Load<SpriteFont>("ItemFont"); // ^^
            expiredfont = Content.Load<SpriteFont>("TimeExpired"); // ^^
            //***************************************************************************************//

            backpackmenu.loadContent(Content);
            gameOver.loadContent(Content);
            timex.loadContent(Content);
            //load content for each respective menu
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        public void UnloadContent()
        {
        }

        public void LoadLevel(int level_num) // which level should we load?
        {
            game_state.obj_mang.Clear();
            resetTimer(); // restart the level timer for each new level
            game_state.tile_engine.loadLevel(level_num);
            game_state.obj_mang.load(game_state.tile_engine.getCurrentMap().getLayer(LayerType.OBJECTS));

            for (int i = 0; i < game_state.monster_engine.GetMonsters().Count(); ++i) // put the enemies on the map
            {
                Enemy new_enemy = game_state.monster_engine.GetMonsters().ElementAt(i);
                Sprite enemy_sprite = new Sprite();
                int new_enemy_type = (int)new_enemy.getType();
          
                enemy_sprite.Load(monster_texture[new_enemy_type], new_enemy.getWidth(), new_enemy.getHeight(), 200);
                new_enemy.setSprite(enemy_sprite);
            }

        }

        public void resetTimer()
        {
            currTime = TimeSpan.FromSeconds(LEVEL_TIME);
        }

        public bool hasTimeLeft() // has the user run out of time?
        {
            return currTime.TotalSeconds > 0;
        }

        public bool testAtExit(int x, int y)
        {
            int tileT=game_state.tile_engine.getMap(game_state.tile_engine.getCurrentLevel()).getLayer(0).getTile(x/32, y/32).getTexture();
            if (tileT == exit)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Update(GameTime gameTime) // is the user dead? if so handle it
        {
            if (game_state.local_player.getHealth() <= 0)
            {
                die();
                return;
            }

            PlayerDir pot_dir = game_state.local_player.getDirection();
            int pot_x = game_state.local_player.getX();
            int pot_y = game_state.local_player.getY();

            double new_width = ((double)health_bar_width) * (double)((double)game_state.local_player.getHealth() / (double)game_state.local_player.getMaxHealth());
            health_bar_rec.Width = (int)new_width;

            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            // for each place the screen has been touched at the point of "getState"
            {

                //if the screen is pressed on an arrow, move sprite accordingly
                if (tl.State == TouchLocationState.Pressed || tl.State == TouchLocationState.Moved)
                {
                    if ((tl.Position.X >= 60) && (tl.Position.X <= 110) && (tl.Position.Y >= 330) && (tl.Position.Y <= 380)) // up arrow
                    {
                        pot_y -= game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.UP;
                        // -1 = up direction (axis goes down incresingly)

                    }

                    if ((tl.Position.X >= 60) && (tl.Position.X <= 110) && (tl.Position.Y >= 420) && (tl.Position.Y <= 470)) // down arrow
                    {
                        pot_y += game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.DOWN;             
                    }

                    if ((tl.Position.X >= 15) && (tl.Position.X <= 65) && (tl.Position.Y >= 375) && (tl.Position.Y <= 425)) // left arrow
                    {
                        pot_x -= game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.LEFT;
                    }

                    if ((tl.Position.X >= 100) && (tl.Position.X <= 150) && (tl.Position.Y >= 375) && (tl.Position.Y <= 425)) // right arrow
                    {
                        pot_x += game_state.local_player.getSpeed();
                        pot_dir = PlayerDir.RIGHT;
                    }


                    //Collision and updating
                    if (game_state.coll_engine.check_map_col(pot_x, pot_y, game_state.local_player.getWidth(), game_state.local_player.getHeight()) == false)
                    {
                        if (game_state.obj_mang.checkForGateAt(pot_x, pot_y, game_state.local_player.getWidth(), game_state.local_player.getHeight()) && !game_state.local_player.hasKey())
                        {
                            //there is a gate and the player does not have a key
                        }
                        else //no gate or the player has the key
                        {
                            game_state.local_player.setX(pot_x);
                            game_state.local_player.setY(pot_y);


                            Item item = game_state.obj_mang.getItemAt(pot_x, pot_y, game_state.local_player.getWidth(), game_state.local_player.getHeight());
                            if (item != null)
                            {
                                game_state.local_player.addItem(item); // item collected, set weapon type to respective type of item collected
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

                            if (!game_state.local_player.moving) // player is still
                            {
                                game_state.local_player.setDirection(pot_dir);
                                game_state.local_player.moving = true;
                                character_sprite[(int)game_state.local_player.getWeapon()].StartAnimating((int)pot_dir * 3, ((int)pot_dir * 3) + 2);
                            }
                        }

                    }

                }
                else if (tl.State == TouchLocationState.Released)
                {
                    if (tl.Position.X >= backpackpos.X && tl.Position.X <= backpackpos.X + backpack.Width && tl.Position.Y >= backpackpos.Y && tl.Position.Y <= backpackpos.Y + backpack.Height)
                    {
                        // has user elected to bring up inventory?
                        backpackmenu.backpack_touched = true;
                        //^^ yes
                    }
                    if (backpackmenu.backpack_touched == false) // if they havent
                    {
                        if ((tl.Position.X >= 700) && (tl.Position.Y >= 385)) // and the Fire button is tapped
                        {
                            if (game_state.local_player.getWeapon() == weaponType.LASER) //if the proper weapon is selected and able to shoot bullets while the fire button is tapped
                            {
                                game_state.bullet_engine.fire(game_state.local_player.getX() + (int)character_sprite[(int)game_state.local_player.getWeapon()].size.X / 2,
                                    game_state.local_player.getY() + (int)character_sprite[(int)game_state.local_player.getWeapon()].size.Y / 2,
                                    game_state.local_player.getDirection(),
                                    bulletOwner.PLAYER,
                                    bulletType.SMALL);
                                game_state.fx_engine.RequestSound(soundType.SHOOT); // shoot a bullet
                            }
                            else if (game_state.local_player.getWeapon() == weaponType.SWORD)
                                // else swing the sword, dont shoot bullets
                            {
                                sword_swing = true;
                                game_state.fx_engine.RequestSound(soundType.SWORD);
                                int bullet_x = 0;
                                int bullet_y = 0;
                                switch (game_state.local_player.getDirection())
                                {
                                    case PlayerDir.DOWN:
                                        bullet_x = game_state.local_player.getX() - 2;
                                        bullet_y = game_state.local_player.getY() + game_state.local_player.getHeight();
                                        break;
                                    case PlayerDir.UP:
                                        bullet_x = game_state.local_player.getX() - 2;
                                        bullet_y = game_state.local_player.getY() - game_state.local_player.getHeight();
                                        break;
                                    case PlayerDir.LEFT:
                                        bullet_x = game_state.local_player.getX() - (game_state.local_player.getWidth());
                                        bullet_y = game_state.local_player.getY();
                                        break;
                                    case PlayerDir.RIGHT:
                                        bullet_x = game_state.local_player.getX() + game_state.local_player.getWidth();
                                        bullet_y = game_state.local_player.getY();
                                        break;
                                }

                                sword_bullet = game_state.bullet_engine.fire(bullet_x, bullet_y, game_state.local_player.getDirection(), bulletOwner.PLAYER, bulletType.SWORD);
                            }
                        }
                    }
                    else // bring up the inventory and display the items properly and in an orderly fashion
                    {
                        Vector2 formatpos = new Vector2(315, 170);
                        int tileSize = game_state.tile_engine.getTileSize();

                        Item toRemove = null;
                        foreach (Item i in game_state.local_player.getInventory())
                        {
                            Rectangle dest = new Rectangle((int)formatpos.X, (int)formatpos.Y, 300, 40);
                            if(dest.Contains((int)tl.Position.X,(int)tl.Position.Y))
                            {
                                //menu item Clicked and ready to be used in game
                                switch (i.getType())
                                {
                                    case itemType.LASER: game_state.local_player.setWeapon(weaponType.LASER); break;
                                    case itemType.SWORD: game_state.local_player.setWeapon(weaponType.SWORD); break;
                                    case itemType.ATT_BOOST: toRemove = i; att_boost_delay = 10000; game_state.local_player.setAttackBonus(5); break;
                                    case itemType.DEF_BOOST: toRemove = i; def_boost_delay = 10000; game_state.local_player.setDefenseBonus(5); break;
                                    case itemType.KEY:
                                    default: break;
                                }
                                backpackmenu.backpack_touched = false;  // exit from inventory now
                            }
                            formatpos.Y += 50;
                        }
                        if (toRemove != null)
                            game_state.local_player.removeItem(toRemove);

                        if (backpackExit.Contains((int)tl.Position.X, (int)tl.Position.Y)) // coordinates of the "exit" button in inventory
                        {
                            backpackmenu.backpack_touched = false; //user has exited, return to main game screen
                        }
                    }

                    character_sprite[(int)game_state.local_player.getWeapon()].StopAnimating();
                    game_state.local_player.moving = false;
                }

            }

            if (sword_swing) // swing sword
            {
                sword_delay += gameTime.ElapsedGameTime.Milliseconds;
                if (sword_delay > 200)
                {
                    sword_delay = 0;
                    sword_swing = false;
                    game_state.bullet_engine.RemoveBullet(sword_bullet);
                }
            }

            if (game_state.local_player.getAttackBonus() > 0) //add attack bonuses
            {
                att_boost_delay -= gameTime.ElapsedGameTime.Milliseconds;
                if (att_boost_delay <= 0)
                {
                    game_state.local_player.setAttackBonus(0);
                }
            }

            if (game_state.local_player.getDefenseBonus() > 0) // add defensive bonuses
            {
                def_boost_delay -= gameTime.ElapsedGameTime.Milliseconds;
                if (def_boost_delay <= 0)
                {
                    game_state.local_player.setDefenseBonus(0);
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
                        int damage = 0;
                        if (coll.GetLocalType() == ColType.BULLET)
                        {

                            Bullet bul = (Bullet)coll.GetParent();
                            if (bul.owner == bulletOwner.PLAYER)
                            {
                                continue;
                            }

          
                        }
                        else if (coll.GetLocalType() == ColType.MONSTER)
                        {
                            Enemy enem = (Enemy)coll.GetParent();
                            damage = enem.getAttack();
                        }
                        //Player gets hurt!

                        game_state.local_player.setHealth(game_state.local_player.getHealth() + game_state.local_player.getDefenseBonus() - damage);
                        game_state.local_player.hurt = true;
                   
                        //Get hurt by a little
                        hurt_time = 500;
                    }
                }
            }
            game_state.local_player.col_tok.ResetCollisions();
            game_state.local_player.col_tok.update(game_state.local_player.getX(), game_state.local_player.getY());

            if(game_state.local_player.hurt) {
                hurt_time -= gameTime.ElapsedGameTime.Milliseconds; // dont repeatedly get hurt, only get hurt once per attack by an enemy
                if (hurt_time <= 0)
                {
                    game_state.local_player.hurt = false; // time to get hurt has run out
                }
            }
            currTime -= gameTime.ElapsedGameTime; // start timer on actual game


            if (currTime.TotalSeconds <= 0) // time per level has run out
            {
                return;
            }

            game_state.monster_engine.Update(gameTime.ElapsedGameTime.Milliseconds);
            game_state.bullet_engine.Update();

            game_state.coll_engine.Update();

            game_state.fx_engine.Update();
            if (testAtExit( game_state.local_player.getX(),(game_state.local_player.getY()+game_state.local_player.getHeight()-1))||testAtExit( game_state.local_player.getX()+game_state.local_player.getWidth(),(game_state.local_player.getY()+game_state.local_player.getHeight())))
            {
                int tempn = game_state.tile_engine.getCurrentLevel() + 1;
                LoadLevel(tempn);
            }

        }

        public void die() // user has died in a level. level will start over and a life will be decucted
        {
            livesRemaining--;
            if (!hasMoreLives()) // if the user has 0 more lives, game over
            {
                if (backpackmenu.isShowing())
                    backpackmenu.Hide();
                if (timex.isShowing())
                    timex.Hide();
                gameEnded = true;
                return;
            }

            LoadLevel(game_state.tile_engine.getCurrentLevel()); // restart if the user has more lives
        }

        public Boolean hasMoreLives()
        {
            return livesRemaining > 0;
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public void Draw(GameTime gameTime, SpriteBatch spriteBatch) // draw everything on the screen
        {
            game_state.tile_engine.getCurrentMap().drawBackground(spriteBatch, game_state.local_player.getX(), game_state.local_player.getY());
            game_state.tile_engine.getCurrentMap().drawObjects(spriteBatch, game_state.local_player.getX(), game_state.local_player.getY());

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

            // Fade a color over the player if they are hurt or have a boost active
            bool shouldFadeHurt = game_state.local_player.hurt;
            bool shouldFadeAttack = game_state.local_player.getAttackBonus() > 0;
            bool shouldFadeDefense = game_state.local_player.getDefenseBonus() > 0;
            Color toFade;
            //choose arbitrary colors for boosts
            if (shouldFadeHurt)
            {
                toFade = Color.Red;
            }
            else if (shouldFadeAttack && shouldFadeDefense)
            {
                toFade = Color.LightGreen;
            }
            else if (shouldFadeAttack)
            {
                toFade = Color.LightYellow;
            }
            else if (shouldFadeDefense)
            {
                toFade = Color.LightBlue;
            }
            else
            {
                toFade = Color.White;
            }

            if (sword_swing) // if the sword has been swung
            {
                sword_sprite.loc = character_sprite[(int)game_state.local_player.getWeapon()].loc;
                sword_sprite.Draw(spriteBatch, (int)game_state.local_player.getDirection(), (shouldFadeHurt || shouldFadeAttack || shouldFadeDefense), toFade);
            }
            else
            {
                character_sprite[(int)game_state.local_player.getWeapon()].Draw(spriteBatch, (shouldFadeHurt || shouldFadeAttack || shouldFadeDefense), toFade);
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

                    Sprite mons_sprite = monster.getSprite();
                    mons_sprite.loc.X = monster.getX() - offset_x;
                    mons_sprite.loc.Y = monster.getY() - offset_y;
                    if (mons_sprite.loc.X > 0 && mons_sprite.loc.X < 800)
                    {
                        if (mons_sprite.loc.Y > 0 && mons_sprite.loc.Y < 400)
                        {
                            mons_sprite.Draw(spriteBatch); //Draw if the location is not negative
                        }
                    }
                }
            }

            List<Bullet> bullets = game_state.bullet_engine.GetBullets();
            //draw the bullets appropriately
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

            //******************DRAWING ARROWS**************************//
            spriteBatch.Draw(uparrow, uparrowpos, null, trans, 0, imageOffset, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(downarrow, downarrowpos, null, trans, 0, imageOffset, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(leftarrow, leftarrowpos, null, trans, 0, imageOffset, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(rightarrow, rightarrowpos, null, trans, 0, imageOffset, 4.0f, SpriteEffects.None, 0);
            spriteBatch.Draw(fire_button, fire_button_pos, null, trans, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
            spriteBatch.DrawString(displayFont, "FIRE!", new Vector2(fire_button_pos.X + 38, fire_button_pos.Y + 45), Color.Black);
            //draw the arrow graphics to the screen with given position, 3x as big as original size, no effects
            //************************************************************//


            //***********************DRAWING GRAPHIC SPRITES***********************//
            spriteBatch.Draw(backpack, backpackpos, null, trans, 0, imageOffset, new Vector2(0.4f,1.0f), SpriteEffects.None, 0); //draw the backpack "button"    
            spriteBatch.DrawString(displayFont, "Inventory", new Vector2(backpackpos.X + 20, backpackpos.Y + 12), Color.Black);
            spriteBatch.Draw(healthbar_empty, health_bar_empty_rec, null, trans);  //draw health bar
            spriteBatch.Draw(healthbar_full, health_bar_rec, null, trans);  //draw health bar
            string curHealth = "" + game_state.local_player.getHealth();
            string maxHealth = "" + game_state.local_player.getMaxHealth();
            spriteBatch.DrawString(displayFont, "Health: " + curHealth + "/" + maxHealth, new Vector2(health_bar_rec.X + 42, health_bar_rec.Y + 5), Color.Black);
            //********************************************************************//


            //***************************DRAWING STRINGS***********************************//
            spriteBatch.DrawString(displayFont, levelstring, scoreStringPos, Color.Cyan); // draw "level: "
            spriteBatch.DrawString(displayFont, game_state.tile_engine.getCurrentLevelName(), currScorePos, Color.Cyan); // draw level number
            spriteBatch.DrawString(displayFont, livesString+livesRemaining.ToString(), livesStringPos, Color.Cyan); // draw lives remaining
            //****************************************************************************//


            //check to see if the score needs to continue to be drawn (if time hasn't run out)
            //if it has, undraw it and display time expired string and menu
            if (!hasTimeLeft())
            {
                die();
                if(hasMoreLives())
                    timex.Show(spriteBatch); // brings up time expired screen
            }
            else
            {
                spriteBatch.DrawString(displayFont, timeLeft, timeLeftPos, Color.Cyan);
                spriteBatch.DrawString(displayFont, ((int)(currTime.TotalSeconds)).ToString(), timePos, Color.Cyan);

            }
            if(backpackmenu.backpack_touched) // backpack button has been pressed
            {
                if (hasTimeLeft()) // if time hasnt run out during the period where the inventory is brought up by the user, show the inventory
                {
                    backpackmenu.Show(spriteBatch); // draw backpack menu
                    drawItems(spriteBatch, itemfont); // draw items according to layout in itemfont
                }
                else // time has run out while the inventory is up, hide inventory and bring up time expired menu
                {
                    backpackmenu.Hide();
                    timex.Show(spriteBatch);
                }
            }


        } // end draw function


        public void drawItems(SpriteBatch sb, SpriteFont sf)
        {
            //check to see if user hit exit button


            //format display on screen for string to be drawn
            Vector2 formatpos = new Vector2(315, 170);
            float origx = formatpos.X;

            if (game_state.local_player.getInventory().Count == 0) // no items collected, display a message
            {
                sb.DrawString(sf, backpackmenu.getEmptyString(), new Vector2(formatpos.X + 45,formatpos.Y), Color.Black);
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
                    sb.DrawString(sf, i.getName(), i.getPos(), Color.Black, 0, imageOffset, 1.0f, SpriteEffects.None, 0);

                    // Reset position for next item
                    formatpos.Y += 50;
                    formatpos.X = origx; // re-allign for next item to be displayed
                }
            } // end else

        }//end drawitems function

    }
}
