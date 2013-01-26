using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using SpriteClasses;

namespace DodgeBlade {
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D spike, wario, bg;                                         // textures
        const byte NUM_WARIOS = 10, NUM_SPIKES = 5;                         // number of spikes and warios

        Sprite[] sprites;
        Random random = new Random();
        const String SPIKE = "Spike", WARIO = "Wario";                      // names for the textureimages

        // Keyboard and mouse state
        KeyboardState kbState;
        KeyboardState oldKbState;
        MouseState mouseState;

        // "My" variables (Things not specified in assignment)
        const byte IDLE = 0, LEFT = 1, UP = 2, RIGHT = 3, DOWN = 4;         // Used for input handling - see Sprite.Controls
        bool isGamePaused = false;                                          // controlling if the game is paused or not
        byte wariosKilled = 0;                                              // Number of warios killed - used this because other method of checking win was not working

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = 469;
            graphics.PreferredBackBufferHeight = 349;
            this.IsMouseVisible = true;

        }

        protected override void Initialize() {
            base.Initialize();
        }

        // (Re)Initialize important game settings
        private void InitGame() {
            sprites = new Sprite[NUM_SPIKES + NUM_WARIOS];

            for (int i = 0; i < sprites.Length; i++) {
                if (i < NUM_SPIKES) {       // from 0 to 4, in this case
                    sprites[i] = new Sprite(spike, new Vector2(random.Next((spike.Width / 2) + 1, (GraphicsDevice.Viewport.Width - spike.Width / 2 - 1)), random.Next((spike.Height / 2 + 1), (GraphicsDevice.Viewport.Height - spike.Height / 2 - 1))), new Vector2(random.Next(-200, 201), random.Next(-200, 201)), true, 3, 1, SpriteEffects.None);
                    sprites[i].TextureImage.Name = SPIKE;
                } else if (i < (NUM_WARIOS + NUM_SPIKES)) {     // from 4 to 14, in this case
                    sprites[i] = new Sprite(wario, new Vector2(random.Next((wario.Width / 2) + 1, (GraphicsDevice.Viewport.Width - wario.Width / 2 - 1)), random.Next((wario.Height / 2 + 1), (GraphicsDevice.Viewport.Height - wario.Height / 2 - 1))), new Vector2(random.Next(-200, 201), random.Next(-200, 201)), true, 0, 1, SpriteEffects.None);
                    sprites[i].TextureImage.Name = WARIO;
                }
            }
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = Content.Load<Texture2D>("images/bg01");
            spike = Content.Load<Texture2D>("images/spike00");
            wario = Content.Load<Texture2D>("images/wario00");

            InitGame();
        }

        protected override void UnloadContent() {

        }

        public void Win() {
            System.Windows.Forms.MessageBox.Show("You Win!");
            Exit();
        }

        public void Lose() {
            System.Windows.Forms.MessageBox.Show("You Lose!");
            Exit();
        }


        protected override void Update(GameTime gameTime) {
            kbState = Keyboard.GetState(PlayerIndex.One);

            // Compare previous keyboard state to current, preventing the user from holding a button down indefinitely
            if (kbState.IsKeyDown(Keys.F2) && oldKbState.IsKeyUp(Keys.F2)){
                InitGame();
            } else if (kbState.IsKeyDown(Keys.Escape) && oldKbState.IsKeyUp(Keys.Escape)){
                // toggle game pause state
                if (isGamePaused) {
                    isGamePaused = false;
                } else {
                    isGamePaused = true;
                }
            }

            // Grab the 'old' keyboard state to compare to the new one
            oldKbState = kbState;

            // Handle Mouse
            // -- Didn't feel an oldMouseState checker was necessary in this project
            mouseState = Mouse.GetState();


            // make sure game isn't paused, then loop through all sprites and handle things accordingly
            if (!isGamePaused) {
                foreach (Sprite s in sprites) {
                   if (mouseState.LeftButton == ButtonState.Pressed) {
                       if (s.CollisionMouse(mouseState.X, mouseState.Y)) {
                           if (s.TextureImage.Name == SPIKE) {
                               Lose();

                           } else if (s.TextureImage.Name == WARIO) {
                               if (++wariosKilled >= 10) {
                                   Win();
                               }

                               /*
                               // This doesn't work, and I can't quite see why, so I switched to the 'wariosKilled' counter way of doing things.
                               for (int i = 5; i < sprites.Length; i++) {
                                   if (sprites[i].TextureImage.Name == WARIO && sprites[i].Alive == true) {
                                       break;
                                   } else {
                                       Console.WriteLine("{0}, name: {1}, alive: {2}", i, sprites[i].TextureImage.Name, sprites[i].Alive);
                                       Win();
                                   }
                               }
                               */
                           }
                       }
                   }

                   s.Update(gameTime, GraphicsDevice);

                }

            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            // Draw background - Background is not a sprite object because it seems very unecessary at this point - will change when background is no longer static
            spriteBatch.Draw(bg, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);

            // Draw all sprites
            foreach (Sprite s in sprites) {
                s.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
