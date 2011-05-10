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
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        private const bool DEV_MODE = false; // DEV_MODE indicates whether or not to skip the menus (testing purposes)

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Vector2 imageOffset = new Vector2(0, 0); //don't offset anything
        Color backColor = Color.White; // arbitrary color for background

        //**********************MENU INSTANCES**************************//
        Introduction intro;
        TimeExpired timex;
        GameOver gameover;
        Instructions2 instruct2;
        Instructions instruct; // instantiate objects
        //*************************************************************//

        GameEngine game_engine; // isntance of a class to handle most of the gameplay

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            TargetElapsedTime = TimeSpan.FromTicks(333333);

            intro = new Introduction();
            instruct = new Instructions();
            instruct2 = new Instructions2();
            gameover = new GameOver();
            timex = new TimeExpired(); // instantiate menus


            // use the whole screen
            graphics.IsFullScreen = true;

            game_engine = new GameEngine(timex, gameover);

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

            //***********************************LOADING MENUS***************************************//
            intro.loadContent(Content);
            timex.loadContent(Content);
            instruct.loadContent(Content);
            instruct2.loadContent(Content);
            gameover.loadContent(Content);
            //***************************************************************************************//

            game_engine.LoadContent(Content); // load all game content separately in the game engine

        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            //nothing to unload
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (DEV_MODE) // if want to skip the menus (for testing purposes)
            {
                if (intro.isShowing()) intro.Hide();
                if (instruct.isShowing()) instruct.Hide();
                if (instruct2.isShowing()) instruct2.Hide();
            }

            if (intro.isShowing()) // if the introduction screen is showing, continue showing until introTime runs out (5 seconds)
            {
                intro.update(gameTime);
            }
            else if (instruct.isShowing()) // if instructions are showing, continue showing until user taps it to advance
            {
                instruct.update(gameTime);
            }
            else if (instruct2.isShowing()) // if second instructions are showing, continue showing until user taps it to advance
            {
                instruct2.update(gameTime);
            }
            else if (timex.isShowing()) // user ran out of time, time expired screen is up. will wait here for user to choose to play again or not
            {
                timex.update(gameTime);
                if (timex.play_again) // user wants to play again
                {
                    timex.Hide(); // so hide game screen and
                    timex.reset(); // reset the timer and
                    game_engine.Update(gameTime); // go to game environment
                }
                else if (!timex.isRunning()) // user did not select play again before time ran out
                {
                    timex.Hide();
                    spriteBatch.Begin();
                    gameover.Show(spriteBatch); // exit game
                    spriteBatch.End();
                }
            }
            else if (gameover.isShowing()) // game over screen is showing
            {
                gameover.update(gameTime);
                if (gameover.exit)
                {
                    this.Exit(); // end game
                }
            }
            else // game is playing
            {
                game_engine.Update(gameTime); //update the game based on UI, AI, time
                if (game_engine.gameEnded)
                {
                    spriteBatch.Begin();
                    gameover.Show(spriteBatch); // if all 5 lives run out, show the game over screen
                    spriteBatch.End();
                }
            }


            base.Update(gameTime);
        }

       

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            if (intro.isShowing())
            {
                spriteBatch.Begin();
                intro.Show(spriteBatch); // show intro screen until timer runs out
                spriteBatch.End();
            }
            else if (instruct.isShowing())
            {
                spriteBatch.Begin();
                instruct.Show(spriteBatch); // show instructions screen until user taps
                spriteBatch.End();
            }
            else if (instruct2.isShowing())
            {
                spriteBatch.Begin();
                instruct2.Show(spriteBatch); // show instructions screen until user taps
                spriteBatch.End();
            }
            else if (timex.isShowing())
            {
                spriteBatch.Begin();
                timex.Show(spriteBatch); // show time expired screen until user makes a selection / time runs out
                spriteBatch.End();
            }
            else if (gameover.isShowing()) // show game over screen for 5 seconds. game will exit back to the windows phone platform after this
            {
                spriteBatch.Begin();
                gameover.Show(spriteBatch);
                spriteBatch.End();
            }
            else // no menus displayed
            {
                spriteBatch.Begin();
                game_engine.Draw(gameTime, spriteBatch); // draw game contents according to UI, AI, time
                spriteBatch.End();
            }
            base.Draw(gameTime);
        } // end draw function

    } // end class
} // end program
