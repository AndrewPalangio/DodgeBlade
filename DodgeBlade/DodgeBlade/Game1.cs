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

        Sprite[] sprites = new Sprite[15];

        Random random = new Random();

        Color mycolor = new Color(255, 255, 255);

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

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            bg = Content.Load<Texture2D>("images/bg01");
            spike = Content.Load<Texture2D>("images/spike00");
            wario = Content.Load<Texture2D>("images/wario00");

            for (int i = 0; i < sprites.Length; i++) {
                if (i <= 5) {
                    sprites[i] = new Sprite(spike, new Vector2(random.Next((spike.Width / 2) + 1, (GraphicsDevice.Viewport.Width - spike.Width / 2 - 1)), random.Next((spike.Height / 2 + 1), (GraphicsDevice.Viewport.Height - spike.Height / 2 - 1))), new Vector2(random.Next(100, 201), random.Next(100, 201)), true, 0, 1, SpriteEffects.None);
                }
                else {
                    sprites[i] = new Sprite(wario, new Vector2(random.Next((wario.Width / 2) + 1, (GraphicsDevice.Viewport.Width - wario.Width/2 -1)), random.Next((wario.Height / 2 + 1), (GraphicsDevice.Viewport.Height - wario.Height / 2 - 1))), new Vector2(random.Next(100, 201), random.Next(100, 201)), true, 0, 1, SpriteEffects.None);
                }
            } 
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {

            foreach (Sprite s in sprites) {
                s.Update(gameTime, GraphicsDevice);
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
