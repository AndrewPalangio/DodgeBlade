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

namespace SpriteClasses{
    public class Sprite{

        public Vector2 currentVelocity, originalVelocity, position, spriteOrigin;

        public Vector2 CurrentVelocity {
            get {
                return currentVelocity;
            }
            set {
                currentVelocity = value;
            }
        }

        public Vector2 OriginalVelocity {
            get {
                return originalVelocity;
            }
            set {
                originalVelocity = value;
            }
        }

        public Vector2 Position {
            get {
                return position;
            }
            set {
                position = value;
            }
        }

        public Vector2 SpriteOrigin {
            get {
                return spriteOrigin;
            }
            set {
                spriteOrigin = value;
            }
        }

        public bool Active { get; set; }
        public float Rotation { get; set; }
        public float RotationSpeed { get; set; }
        public float Scale { get; set; }
        public bool setOrigin { get; set; }
        public SpriteEffects SpriteEffect { get; set; }
        public Texture2D TextureImage { get; set; }

        public Sprite() {

        }

        public Sprite(Texture2D textureImage, Vector2 position, Vector2 velocity, bool setOrigin, float rotationSpeed, float scale, SpriteEffects spriteEffect) {
            TextureImage = textureImage;
            Position = position;
            OriginalVelocity = velocity;
            CurrentVelocity = velocity;
            RotationSpeed = rotationSpeed;
            Scale = scale;
            SpriteEffect = spriteEffect;

            if (setOrigin) {
                SpriteOrigin = new Vector2(TextureImage.Width / 2, TextureImage.Height / 2);
            }
            else {
                SpriteOrigin = new Vector2(0, 0);
            }

            Active = true;

        }

        public virtual void Update(GameTime gameTime) {
            if (Active) {
                
                if (Rotation < 360) {
                    Rotation += RotationSpeed;
                }
                else if (Rotation >= 360) {
                    Rotation = 0;
                }

                position.X += (currentVelocity.X * (float)gameTime.ElapsedGameTime.TotalSeconds);
                position.Y += (currentVelocity.Y * (float)gameTime.ElapsedGameTime.TotalSeconds);
            }
        }

        public virtual void Update(GameTime gameTime, GraphicsDevice Device) {
            if (Active) {
                if ((position.X + SpriteOrigin.X * Scale) >= Device.Viewport.Width || position.X - SpriteOrigin.X * Scale <= 0) {
                    currentVelocity.X *= -1;
                }

                if ((position.Y + SpriteOrigin.Y * Scale) >= Device.Viewport.Height || position.Y - SpriteOrigin.Y * Scale <= 0) {
                    currentVelocity.Y *= -1;
                }

                Update(gameTime);
            }
        }

        public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch) {
            if (Active) {
                spriteBatch.Draw(TextureImage, position, new Rectangle(0, 0, TextureImage.Width, TextureImage.Height), Color.White, MathHelper.ToRadians(Rotation), SpriteOrigin, Scale, SpriteEffect, 0);
            }
        }

    }
}
