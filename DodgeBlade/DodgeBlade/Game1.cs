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

        Color mycolor = new Color(255, 255, 255);

        public Game1() {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferWidth = GAMEWIDTH;
            graphics.PreferredBackBufferHeight = GAMEHEIGHT;

        }

        protected override void Initialize() {

            spritePos = new Vector2(GAMEWIDTH / 2, GAMEHEIGHT / 2);
            velocity = new Vector2(200, 1);

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


            if ((spritePos.X + threerings.Width) >= GAMEWIDTH || spritePos.X <= 0) {
                velocity.X *= -1;
            }

            if ((spritePos.Y + threerings.Height) >= GAMEHEIGHT || spritePos.Y <= 0) {
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
            spriteBatch.Draw(threerings, spritePos, Color.White);

            spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
