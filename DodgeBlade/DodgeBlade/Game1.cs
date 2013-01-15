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

namespace DodgeBlade {
    public class Game1 : Microsoft.Xna.Framework.Game {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public const short GAMEWIDTH = 1280;
        public const short GAMEHEIGHT = 720;

        Texture2D background;
        Texture2D threerings;

        Vector2 spritePos;
        Vector2 velocity;

        Random random = new Random();

        Color mycolor = new Color(255, 255, 255);

        float rotation;
        float scale = 50;
        int scaleOperation = 1;

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GAMEWIDTH;
            graphics.PreferredBackBufferHeight = GAMEHEIGHT;

        }

        protected override void Initialize() {

            spritePos = new Vector2(GAMEWIDTH / 2, GAMEHEIGHT / 2);
            velocity = new Vector2(random.Next(200, 301), random.Next(200, 301));

            base.Initialize();
        }

        protected override void LoadContent() {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            background = Content.Load<Texture2D>("images/background");
            threerings = Content.Load<Texture2D>("images/threeringsSingle");

        }

        protected override void UnloadContent() {
        }

        protected override void Update(GameTime gameTime) {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            if (GamePad.GetState(PlayerIndex.One).Buttons.X == ButtonState.Pressed) {
                mycolor = new Color(0, 0, 255);
            }
            else if (GamePad.GetState(PlayerIndex.One).Buttons.Y == ButtonState.Pressed) {
                mycolor = new Color(255, 255, 0);
            }
            else if (GamePad.GetState(PlayerIndex.One).Buttons.B == ButtonState.Pressed) {
                mycolor = new Color(255, 0, 50);
            }
            else if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed) {
                mycolor = new Color(0, 255, 0);
            }
            else {
                mycolor = new Color(255, 255, 255);
            }


            if (rotation < 360) {
                rotation++;
            }
            else if (rotation >= 360) {
                rotation = 0;
            }

            if (scale == 100 || scale == 0) {
                scaleOperation *= -1;
            }

            scale += scaleOperation;

            if ((spritePos.X + threerings.Width/2) >= GAMEWIDTH || spritePos.X - threerings.Width/2 <= 0) {
                velocity.X *= -1;
            }

            if ((spritePos.Y + threerings.Height/2) >= GAMEHEIGHT || spritePos.Y - threerings.Height/2 <= 0) {
                velocity.Y *= -1;
            }

            spritePos.X += (velocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds);
            spritePos.Y += (velocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime) {
            GraphicsDevice.Clear(Color.Lime);

            spriteBatch.Begin();

            spriteBatch.Draw(background, new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), mycolor);
            // spriteBatch.Draw(threerings, spritePos, Color.White);
            // spriteBatch.Draw(threerings, new Rectangle((int)spritePos.X, (int)spritePos.Y, threerings.Width, threerings.Height), Color.White);
            spriteBatch.Draw(threerings, new Rectangle((int)spritePos.X, (int)spritePos.Y, (int)(threerings.Width * (scale * 0.01)), (int)(threerings.Height * (scale * 0.01))), new Rectangle(0, 0, threerings.Width, threerings.Height), Color.White, MathHelper.ToRadians(rotation), new Vector2(threerings.Width / 2, threerings.Height / 2), SpriteEffects.FlipHorizontally, 1f);


            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
