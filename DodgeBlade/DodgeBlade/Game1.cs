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

        public const short GAMEWIDTH = 1280;
        public const short GAMEHEIGHT = 720;

        Texture2D backgroundTexture;
        Texture2D threeringsTexture;

        Sprite background;
        Sprite threerings;

        Random random = new Random();

        Color mycolor = new Color(255, 255, 255);

        //float rotation;
        //float scale = 50;
        //int scaleOperation = 1;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GAMEWIDTH;
            graphics.PreferredBackBufferHeight = GAMEHEIGHT;

        }

        protected override void Initialize() {

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            backgroundTexture = Content.Load<Texture2D>("images/background");
            threeringsTexture = Content.Load<Texture2D>("images/threeringsSingle");

            background = new Sprite(backgroundTexture, new Vector2(0, 0), new Vector2(0, 0), false, 0, 1, SpriteEffects.None);
            threerings = new Sprite(threeringsTexture, new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2), new Vector2(random.Next(200, 301), random.Next(200, 301)), true, 10, 1, SpriteEffects.None);
        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {

            threerings.Update(gameTime, GraphicsDevice);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            background.Draw(gameTime, spriteBatch);
            threerings.Draw(gameTime, spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
