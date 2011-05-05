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

//help from MSDN examples online
//game dimensions: 800x480 (x,y)

//TO-DO:
//transition from timex to gameover screen, make gameover class
//scale health bar
//get game name --> do welcome screen
//get sensor on timex update to work
//change instruct screens --> add "tap screen to advance" on photoshop file


namespace Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Matts : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 imageOffset = new Vector2(0, 0); //don't offset anything
        Color backColor = Color.White; // arbitrary color for background

        //**********************MENU INSTANCES**************************//
        Introduction intro;
        TimeExpired timex;
        Backpack backpackmenu;
        Instructions2 instruct2;
        Instructions instruct; // instantiate objects
        //*************************************************************//

        //***********************SOUNDS***********************//
        SoundEffectInstance soundEffect;
        SoundEffectInstance soundEffect2;
        SoundEffectInstance soundEffect3;
        SoundEffectInstance soundEffect4; // to experiment with sound simultaneously with input
        //****************************************************//


        //******************GRAPHIC SPRITES*****************//
        Texture2D uparrow;
        Texture2D downarrow;
        Texture2D leftarrow;
        Texture2D rightarrow; // graphics for "keypad"

        Texture2D testchar; //arbitrary picture chosen for spirite for now
        Texture2D backpack; // backpack graphic user can tap on for item inventory
        Texture2D gunbutton; // gun button user taps to fire weapons at enemies
        Texture2D healthbar; // displays user character's health
        //*************************************************//


        //*********************POSITIONS***********************//
        Vector2 uparrowpos = new Vector2(50, 345);
        Vector2 downarrowpos = new Vector2(50, 425);
        Vector2 leftarrowpos = new Vector2(15, 385);
        Vector2 rightarrowpos = new Vector2(85, 385); //positions for each arrow

        Vector2 charpos = new Vector2(275, 200); // starting position for the sprite test character
        Vector2 backpackpos = new Vector2(725, 425); // position for backpack sprite
        Vector2 gunbuttonpos = new Vector2(630, 425); // position for attack button
        Vector2 healthpos = new Vector2(280, 445); // position for health bar

        Vector2 scoreStringPos = new Vector2(0, 20); // position of score string
        Vector2 currScorePos = new Vector2(75, 20); // position of actual score
        Vector2 timeLeftPos = new Vector2(0, 0); // "Time Remaining" position
        Vector2 timePos = new Vector2(75, 0); // displaying actual time left

        Vector2 levelstringpos = new Vector2(700, 0); // "Level: " position
        Vector2 levelnumpos = new Vector2(755, 0); // level number position
        //****************************************************//


        //**************DISPLAY STRINGS******************//
        public const string scoreString = "SCORE: "; // display string
        public int currScore = 0; // actual score, again can be modified like the levelnum var. not "const"
        public const string timeLeft = "TIME: ";
        public const string levelstring = "LEVEL: ";
        public int levelnum = 0; // can be modified for when a user advances through levels, not a "const"
        //************************************************//


        //***************GAMEPLAY ELEMENTS*********************//
        //List<Student> list = new List<Student>();
        //list.Add(new Student("bob"));
        //list.Add(new Student("joe"));
        //Student joe = list[1];
        //^^ example of using dynamic list in c#
        List<Item> inventory = new List<Item>(); // inventory of all items user has collected
        List<Item> all_items = new List<Item>();
        Item dummy1 = new Item();
        Item dummy2 = new Item();
        Item dummy3 = new Item(); // dummy items for trial
        //*****************************************************//

        public TimeSpan currTime = TimeSpan.FromSeconds(5.0); // grant the player a certain time per round
        // currTime will be 90 and count down each second, checking against 0 each second,  for each level

        SpriteFont displayFont; // in separate file (fonts subfolder). arbitrary
        SpriteFont itemfont; // ^^
        SpriteFont expiredfont; // ^^

        Boolean timeOut; // alerts program when timer for roundtime has expired

        public Matts()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromTicks(333333);

            intro = new Introduction();
            instruct = new Instructions();
            instruct2 = new Instructions2();
            backpackmenu = new Backpack(); 
            timex = new TimeExpired(); // instantiate menus

            Vector2 dummy1pos = new Vector2(600, 15);
            Vector2 dummy2pos = new Vector2(125, 300);
            Vector2 dummy3pos = new Vector2(300, 75); // create initial positions to set each item's position as
            dummy1.setPos(dummy1pos);
            dummy2.setPos(dummy2pos);
            dummy3.setPos(dummy3pos); // ^^


            all_items.Add(dummy1);
            all_items.Add(dummy2);
            all_items.Add(dummy3); // add items to possible items to collect in game

            // use the whole screen
            graphics.IsFullScreen = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //***************************LOADING ARROWS*******************************//        
            uparrow = this.Content.Load<Texture2D>("Pictures\\arrowup");
            downarrow = this.Content.Load<Texture2D>("Pictures\\arrowdown");
            leftarrow = this.Content.Load<Texture2D>("Pictures\\arrowleft");
            rightarrow = this.Content.Load<Texture2D>("Pictures\\arrowright"); // components for "keypad" HUD
            //***********************************************************************//

            //********************************LOADING GRAPHIC SPRITES********************************//
            testchar = this.Content.Load<Texture2D>("Pictures\\pic"); // loading test sprite
            backpack = this.Content.Load<Texture2D>("Pictures\\backpack"); // loading backpack
            gunbutton = this.Content.Load<Texture2D>("Pictures\\gun"); // load gun
            healthbar = this.Content.Load<Texture2D>("Pictures\\healthbar"); // load health bar

            dummy1.loadContent(this.Content);
            dummy2.loadContent(this.Content);
            dummy3.loadContent(this.Content); // trial content
            //**************************************************************************************//

            //**********************************LOADING SOUNDS*****************************************//
            soundEffect = this.Content.Load<SoundEffect>("Audio\\Waves\\yoshi_spitting").CreateInstance();
            soundEffect2 = this.Content.Load<SoundEffect>("Audio\\Waves\\mario_power_up").CreateInstance();
            soundEffect3 = this.Content.Load<SoundEffect>("Audio\\Waves\\power_star").CreateInstance();
            soundEffect4 = this.Content.Load<SoundEffect>("Audio\\Waves\\boo_laugh").CreateInstance();
            //****************************************************************************************//

            //***********************************LOADING MENUS***************************************//
            intro.loadContent(this.Content);
            timex.loadContent(this.Content);
            instruct.loadContent(this.Content);
            instruct2.loadContent(this.Content);
            backpackmenu.loadContent(this.Content);
            //***************************************************************************************//

            displayFont = this.Content.Load<SpriteFont>("Fonts\\StatsFont"); //load a font from a formatted file
            itemfont = this.Content.Load<SpriteFont>("Fonts\\ItemFont"); // ^^
            expiredfont = this.Content.Load<SpriteFont>("Fonts\\TimeExpired"); // ^^
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //nothing unloaded
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit when the "back" button is pressed
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (intro.isShowing()) // if the introduction screen is showing, continue showing until introTime runs out (5 seconds)
            {
                intro.update(gameTime);
            }
            else if (instruct.isShowing()) // if instructions are showing, continue showing for 5 seconds and let it advance
            {
                instruct.update(gameTime);
            }
            else if (instruct2.isShowing())
            {
                instruct2.update(gameTime);
            }
            //else if (timex.isShowing()) // user ran out of time, time expired screen is up. will wait here for user to choose to play again or not
            //{

            //    if (timex.play_again) // user wants to play again
            //    {
            //        timex.Hide(); // so hide game screen and
            //        playGame(gameTime); // go to game environment
            //    }
            //    else if (!timex.play_again) // user wants to quit or time ran out so
            //    {
            //        this.Exit(); // exit game
            //    }

            //}

            base.Update(gameTime);
        } // end update function


        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {

            GraphicsDevice.Clear(backColor);
        
            if( intro.isShowing() )
            {
                spriteBatch.Begin();
                intro.Show(spriteBatch); // show intro screen until timer runs out
                spriteBatch.End();
            }
            else if (instruct.isShowing())
            {
                spriteBatch.Begin();
                instruct.Show(spriteBatch); // show instructions screen until timer runs out
                spriteBatch.End();
            }
            else if (instruct2.isShowing())
            {
                spriteBatch.Begin();
                instruct2.Show(spriteBatch); // show instructions screen until timer runs out
                spriteBatch.End();
            }
            else if (timex.isShowing())
            {

                spriteBatch.Begin();
                timex.Show(spriteBatch, displayFont); // show time expired screen until user makes a selection
                timex.update(spriteBatch, expiredfont, gameTime);
                if (timex.play_again)
                {
                    spriteBatch.End();
                    //hide
                    timex.Hide();

                    // reset game here before playing again
                    this.currTime = TimeSpan.FromSeconds(5.0);
                    this.timeOut = false;
                    playGame(gameTime);
                    timex.reset();
                }
                else if (!timex.play_again && timex.get_remaining_time() >= 10)
                    //timex.shutDown();
                    this.Exit();

                else
                    spriteBatch.End();
               
            }
            else // no menus displayed
            {
                playGame(gameTime); // go to game environment
            }

            base.Draw(gameTime);
        }


        public void playGame(GameTime gameTime) // main function
        {
            // setup/logic for game begins

            currTime -= gameTime.ElapsedGameTime; // start timer on actual game

            if ((currTime.Seconds <= 0) && (currTime.Milliseconds <= 0))
            {
                timeOut = true; // if round time has run out, display the time expired screen (setting this bool to true will flag the menu later)
            }

            // Process touch events (mouse click in emulator)
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            // for each place the screen has been touched at the point of "getState"
            {
                //if the screen is pressed on an arrow, move sprite accordingly
                if ((tl.Position.X >= 50) && (tl.Position.X <= 100) && (tl.Position.Y >= 345) && (tl.Position.Y <= 385)) // up arrow
                {
                    if (charpos.Y == 0) { charpos.Y = 0; }
                    else
                    {
                        charpos.Y -= 1; // -1 = up direction (axis goes down incresingly)
                        //soundEffect.Play(); // play a sound to experiment with sounds being played too
                    }
                }

                if ((tl.Position.X >= 50) && (tl.Position.X <= 100) && (tl.Position.Y >= 425) && (tl.Position.Y <= 465)) // down arrow
                {
                    if (charpos.Y == 450) { charpos.Y = 450; }
                    else
                    {
                        charpos.Y += 1;
                        //soundEffect2.Play(); // ^^ ditto
                    }
                }

                if ((tl.Position.X >= 15) && (tl.Position.X <= 55) && (tl.Position.Y >= 385) && (tl.Position.Y <= 425)) // left arrow
                {
                    if (charpos.X == 0) { charpos.X = 0; }
                    else
                    {
                        charpos.X -= 1; // -1 = left because left to right
                        //soundEffect3.Play(); // ^^ ditto
                    }
                }

                if ((tl.Position.X >= 90) && (tl.Position.X <= 130) && (tl.Position.Y >= 385) && (tl.Position.Y <= 425)) // right arrow
                {
                    if (charpos.X == 768) { charpos.X = 768; }
                    else
                    {
                        charpos.X += 1;
                        //soundEffect4.Play(); // ^^ ditto
                    }
                }

                if ((tl.Position.X >= 725) && (tl.Position.X <= 745) && (tl.Position.Y >= 425) && (tl.Position.Y <= 450)) // backpack icon is touched
                {
                    backpackmenu.backpack_touched = true;
                }

            } // end "for each" tl loop

            foreach (Item i in all_items) // search and see if any items have been collected
            {
                if ((charpos.X >= i.getPos().X) && (charpos.X <= i.getPos().X + 2) && (charpos.Y <= i.getPos().Y) && (charpos.Y <= i.getPos().Y + 2)) // can change "range" of allowed item collection later
                {
                    i.collected = true;
                    soundEffect2.Play(); // play a sound when pick up item
                }

            }// end for each item loop


            //logic / setup part of game ends here
            //drawing / display portion of game begins here


            spriteBatch.Begin();
            //begin operations on display textures
            //gets spritebatch in a state to be 'ready' to draw
            if (!backpackmenu.backpack_touched)
            {

                //******************DRAWING ARROWS**************************//
                spriteBatch.Draw(uparrow, uparrowpos, null, Color.White, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(downarrow, downarrowpos, null, Color.White, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(leftarrow, leftarrowpos, null, Color.White, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                spriteBatch.Draw(rightarrow, rightarrowpos, null, Color.White, 0, imageOffset, 3.0f, SpriteEffects.None, 0);
                //draw the arrow graphics to the screen with given position, 3x as big as original size, no effects
                //************************************************************//


                //***********************DRAWING GRAPHIC SPRITES***********************//
                spriteBatch.Draw(testchar, charpos, null, Color.White, 0, imageOffset, .6f, SpriteEffects.None, 0); //draw the character
                spriteBatch.Draw(backpack, backpackpos, null, Color.White, 0, imageOffset, 0.4f, SpriteEffects.None, 0); //draw the backpack "button"
                spriteBatch.Draw(gunbutton, gunbuttonpos, null, Color.White, 0, imageOffset, 0.5f, SpriteEffects.None, 0); //draw gun "button"
                spriteBatch.Draw(healthbar, healthpos, null, Color.White, 0, imageOffset, 0.25f, SpriteEffects.None, 0);  //draw health bar

                foreach (Item i in all_items)
                {
                    if (!i.collected) // if an item hasnt been collected, continue drawing it to the screen
                    {
                        spriteBatch.Draw(i.get2D(), i.getPos(), null, Color.White, 0, imageOffset, 0.05f, SpriteEffects.None, 0);
                    }
                    else // item has been collected
                    {
                        if (!inventory.Contains(i))
                        {
                            inventory.Add(i); // if user "collides" or runs over an item on the level, add that item to the collected inventory
                            i.ToString().Remove(0); // remove it from display
                        }
                    }
                }
                //*********************************************************************//


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
                    spriteBatch.DrawString(displayFont, currTime.Seconds.ToString(), timePos, Color.Black);
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
            
            spriteBatch.End(); //finished operations on display textures, must end before begin can be called again
            //end of display / graphics part of game

        } // end playGame function



        public void drawItems(SpriteBatch sb, SpriteFont sf)
        {
            //check to see if user hit exit button
            TouchCollection touchCollection = TouchPanel.GetState();
            foreach (TouchLocation tl in touchCollection)
            {
                if ((tl.Position.X >= 675) && (tl.Position.X <= 715) && (tl.Position.Y >= 400) && (tl.Position.Y <= 425)) // coordinates of the "exit" button in inventory
                {
                    backpackmenu.backpack_touched = false; //user has exited, return to main game screen
                    return;
                }
            }

            //format display on screen for string to be drawn
            Vector2 formatpos = new Vector2(275, 125);
            float origx = formatpos.X;

            if (inventory.Count == 0) // no items collected, display a message
            {
                sb.DrawString(sf, backpackmenu.getEmptyString(), formatpos, Color.White);
            }
            else // inventory has a capacity
            {
                //draws (each) item on inventory screen
                foreach (Item i in inventory)
                {
                    //display format: name, type, boost
                    i.setPos(formatpos);
                    sb.DrawString(sf, i.getName(), i.getPos(), Color.White, 0, imageOffset, 1.0f, SpriteEffects.None, 0);

                    formatpos.X += 100;
                    i.setPos(formatpos); // need to move starting display position to the right to not run into the name already printed
                    sb.DrawString(sf, i.getType().ToString(), i.getPos(), Color.White, 0, imageOffset, 1.0f, SpriteEffects.None, 0);

                    formatpos.X += 100;
                    i.setPos(formatpos); // need to move starting display position to the right to not run into the name already printed
                    sb.DrawString(sf, i.getBoost().ToString(), i.getPos(), Color.White, 0, imageOffset, 1.0f, SpriteEffects.None, 0);

                    formatpos.Y += 40;
                    formatpos.X = origx; // re-allign for next item to be displayed
                }
            } // end else

        }//end drawitems function



    } // end class
} // end program
