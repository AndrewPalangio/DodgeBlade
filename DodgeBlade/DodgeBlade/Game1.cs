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

        Texture2D spike, wario, bg;
        const byte NUM_WARIOS = 10, NUM_SPIKES = 5;

        Sprite[] sprites;
        Random random = new Random();
        const String SPIKE = "Spike", WARIO = "Wario";

        KeyboardState kbState;
        MouseState mouseState;

        // my vars
        const byte IDLE = 0, LEFT = 1, UP = 2, RIGHT = 3, DOWN = 4;
        bool isGamePaused = false;
        byte wariosKilled = 0;

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

        private void InitGame() {
            sprites = new Sprite[NUM_SPIKES + NUM_WARIOS];

            for (int i = 0; i < sprites.Length; i++) {
                if (i < NUM_SPIKES) {
                    sprites[i] = new Sprite(spike, new Vector2(random.Next((spike.Width / 2) + 1, (GraphicsDevice.Viewport.Width - spike.Width / 2 - 1)), random.Next((spike.Height / 2 + 1), (GraphicsDevice.Viewport.Height - spike.Height / 2 - 1))), new Vector2(random.Next(-200, 201), random.Next(-200, 201)), true, 3, 1, SpriteEffects.None);
                    sprites[i].TextureImage.Name = SPIKE;
                } else if (i < (NUM_WARIOS + NUM_SPIKES)) {
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

            if (kbState.IsKeyDown(Keys.F2)){
                InitGame();
            } else if (kbState.IsKeyDown(Keys.Escape)){
                if (isGamePaused) {
                    isGamePaused = false;
                } else {
                    isGamePaused = true;
                }
            }

            // Handle Mouse
            mouseState = Mouse.GetState();

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

                               //for (int i = 4; i < sprites.Length; i++) {
                               //    if (sprites[i].TextureImage.Name == WARIO && sprites[i].Alive == true) {
                               //        break;
                               //    } else {
                               //        Console.WriteLine("{0}, name: {1}, alive: {2}", i, sprites[i].TextureImage.Name, sprites[i].Alive);
                               //        Win();
                               //    }
                               //}
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
            spriteBatch.Draw(bg, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), Color.White);
            foreach (Sprite s in sprites) {
                s.Draw(gameTime, spriteBatch);
            }
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
